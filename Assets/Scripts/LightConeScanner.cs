using UnityEngine.Rendering.Universal;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/// <summary>
/// 在 2D Point / Spot Light (URP) 的光锥内扫描带有 <c>"Monster"</c> 标签的目标：
/// <list type="bullet">
/// <item>进入光锥 → 暂停寻路</item>
/// <item>离开光锥 → 恢复寻路</item>
/// <item>连续被照射 <c>killAfter</c> 秒 → <b>消灭</b>（仅对含 <see cref="LightSensitive"/> 组件的怪物生效）</item>
/// </list>
/// 「连续」= 只要离开光锥即重置计时。
/// 请把 Light2D 挂在会旋转的 <b>HeadLampPivot</b> 上，PlayerCharacter 本体可保持冻结 Z 旋转。
/// </summary>
public class LightConeScanner : MonoBehaviour
{
    //产生药物的音效事件
    public ObjectEventSO GenerateDrugEvent;


    // ───────────────────── Inspector ─────────────────────
    [Header("References")]
    [Tooltip("2D Point/Spot Light2D 组件，应挂在 HeadLampPivot 上")]
    [SerializeField] private Light2D spotLight;

    [Tooltip("Monster 所在 Physics2D Layer")]
    [SerializeField] private LayerMask monsterLayer;

    [Header("Gameplay")]
    [Tooltip("怪物连续被照射多少秒后销毁")]
    [SerializeField] private float killAfter = 2.5f;

    [Tooltip("怪物被消灭时在原地生成的 Prefab，可为空")]
    [SerializeField] private GameObject deathSpawnPrefab;

    // ───────────────────── Runtime ─────────────────────
    public readonly List<Transform> visibleMonsters = new();   // 光锥内的所有怪物（含不被光杀的）

    private readonly List<Collider2D> _hits = new();          // 物理检测缓存
    private readonly HashSet<Transform> _frozen = new();          // 已冻结的怪物
    private readonly List<Transform> _tmp = new();          // 临时差集
    private readonly Dictionary<Transform, float> _exposureTime = new(); // 连续曝光计时（仅 LightSensitive）

    private ContactFilter2D _filter;
    private Transform _lampTf;

    // ───────────────────── Unity Events ─────────────────────
    private void Awake()
    {
        if (spotLight == null)
        {
            Debug.LogError("[LightConeScanner] spotLight 未赋值！");
            enabled = false;
            return;
        }

        _lampTf = spotLight.transform;

        _filter = new ContactFilter2D
        {
            layerMask = monsterLayer,
            useLayerMask = true,
            useTriggers = true
        };
    }

    private void LateUpdate()
    {
        if (spotLight.lightType != Light2D.LightType.Point) return;

        CollectVisibleMonsters();          // 1) 扇形检测
        HandleExposureAndElimination(Time.deltaTime); // 2) 连续照射 & 消灭
        ProcessFreezeLogic();              // 3) 冻结 / 解冻
    }

    // ───────────────────── Detection ─────────────────────
    private void CollectVisibleMonsters()
    {
        _hits.Clear();
        visibleMonsters.Clear();

        Vector2 origin = _lampTf.position;
        float radius = spotLight.pointLightOuterRadius;
        float radiusSq = radius * radius;
        float halfAng = spotLight.pointLightOuterAngle * 0.5f;
        Vector2 forward2D = _lampTf.up;

        Physics2D.OverlapCircle(origin, radius, _filter, _hits);

        foreach (var col in _hits)
        {
            if (col == null || !col.CompareTag("Monster")) continue;

            Vector2 to = (Vector2)col.transform.position - origin;
            if (to.sqrMagnitude > radiusSq) continue;
            if (Vector2.Angle(forward2D, to) > halfAng) continue;

            visibleMonsters.Add(col.transform);
        }
    }

    // ───────────────────── Exposure & Kill ─────────────────────
    private void HandleExposureAndElimination(float dt)
    {
        // 1) 对光锥内且可被光杀的怪物累计时间
        for (int i = 0; i < visibleMonsters.Count; i++)
        {
            var tr = visibleMonsters[i];
            if (!IsKillable(tr))
                continue;   // 该怪物对光线免疫

            if (!_exposureTime.ContainsKey(tr))
                _exposureTime[tr] = 0f;

            _exposureTime[tr] += dt;

            if (_exposureTime[tr] >= killAfter)
            {
                EliminateMonster(tr);
            }
        }

        // 2) 离开光锥或免疫 → 重置计时
        _tmp.Clear();
        foreach (var kvp in _exposureTime)
        {
            if (!visibleMonsters.Contains(kvp.Key))
                _tmp.Add(kvp.Key);
        }
        foreach (var tr in _tmp)
            _exposureTime[tr] = 0f;
    }

    private bool IsKillable(Transform tr) => tr.GetComponent<LightSensitive>() != null;

    private void EliminateMonster(Transform tr)
    {
        // 从所有数据结构里移除
        _exposureTime.Remove(tr);
        _frozen.Remove(tr);
        visibleMonsters.Remove(tr);

        // 可在这里播放特效 / 得分等
        Destroy(tr.gameObject);

        var spawned = Instantiate(deathSpawnPrefab, tr.position, Quaternion.identity);
        GenerateDrugEvent.RaiseEvent(null, this);
        spawned.SetActive(true); 
    }

    // ───────────────────── Freeze / Unfreeze ─────────────────────
    private void ProcessFreezeLogic()
    {
        // ① 冻结新进入
        foreach (var tr in visibleMonsters)
            if (_frozen.Add(tr))
                SetFrozen(tr, true);

        // ② 解冻离开
        _tmp.Clear();
        foreach (var tr in _frozen)
            if (!visibleMonsters.Contains(tr))
                _tmp.Add(tr);

        foreach (var tr in _tmp)
        {
            SetFrozen(tr, false);
            _frozen.Remove(tr);
        }
    }

    /// <summary>
    /// 根据怪物所挂组件统一暂停 / 恢复移动。
    /// </summary>
    private static void SetFrozen(Transform tr, bool frozen)
    {
        // — AIDestinationSetter (更新目标点)
        if (tr.TryGetComponent<AIDestinationSetter>(out var setter))
            setter.enabled = !frozen;

        // — IAstarAI (AIPath, RichAI, etc.)
        if (tr.TryGetComponent<IAstarAI>(out var ai))
        {
            ai.canMove = !frozen;
            ai.canSearch = !frozen;
            ai.isStopped = frozen;
        }

        // — Rigidbody2D 兜底
        if (frozen && tr.TryGetComponent<Rigidbody2D>(out var rb))
            rb.velocity = Vector2.zero;
    }

#if UNITY_EDITOR
    // ───────────────────── Gizmos ─────────────────────
    private void OnDrawGizmosSelected()
    {
        if (spotLight == null || spotLight.lightType != Light2D.LightType.Point) return;

        Gizmos.color = Color.yellow;

        Vector3 pos = spotLight.transform.position;
        float radius = spotLight.pointLightOuterRadius;
        float half = spotLight.pointLightOuterAngle * 0.5f;
        Vector3 fwd3D = spotLight.transform.up;

        Vector3 dirCCW = Quaternion.Euler(0, 0, -half) * fwd3D;
        Vector3 dirCW = Quaternion.Euler(0, 0, half) * fwd3D;

        Gizmos.DrawLine(pos, pos + dirCCW * radius);
        Gizmos.DrawLine(pos, pos + dirCW * radius);

        const int steps = 24;
        float step = half * 2f / steps;
        Vector3 prev = pos + dirCCW * radius;
        for (int i = 1; i <= steps; ++i)
        {
            Vector3 dir = Quaternion.Euler(0, 0, -half + step * i) * fwd3D;
            Vector3 next = pos + dir * radius;
            Gizmos.DrawLine(prev, next);
            prev = next;
        }
    }
#endif
}
