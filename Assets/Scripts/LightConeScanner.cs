#if UNITY_2021_2_OR_NEWER
using UnityEngine.Rendering.Universal;
#else
using UnityEngine.Experimental.Rendering.Universal;
#endif
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 在 2D Spot/Point Light (URP) 的光锥内实时搜寻带 "Monster" Tag 的目标。<br/>
/// 使用 Physics2D.OverlapCircle / ContactFilter2D 把结果填进可扩展的 List，避免容量限制。
/// </summary>
public class LightConeScanner : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Light2D spotLight;        // 2D Point/Spot Light
    [SerializeField] private LayerMask monsterLayer;    // 只检测怪物层

    [Header("Runtime")]
    public readonly List<Transform> visibleMonsters = new(); // 外部读取

    // 内部复用的碰撞结果列表（容量自动扩展）
    private readonly List<Collider2D> _hits = new();

    // 提前构建 ContactFilter，避免每帧重新分配
    private ContactFilter2D _filter;

    private void Awake()
    {
        _filter = new ContactFilter2D
        {
            layerMask = monsterLayer,
            useLayerMask = true,
            useTriggers = true  // 如果怪物碰撞体是 Trigger；否则设 false
        };
    }

    private void LateUpdate()
    {
        if (spotLight == null || spotLight.lightType != Light2D.LightType.Point)
            return; // 仅在 Point/Spot Light 下工作

        /* ---------- 1) 收集候选 ---------- */
        _hits.Clear();
        Vector2 origin = spotLight.transform.position;
        float radius = spotLight.pointLightOuterRadius;
        Physics2D.OverlapCircle(origin, radius, _filter, _hits);  // 结果填进 List

        /* ---------- 2) 角度 / 距离筛选 ---------- */
        visibleMonsters.Clear();

        float halfAngle = spotLight.pointLightOuterAngle * 0.5f;
        Vector2 forward = spotLight.transform.up;                // +Y 为光束正前
        float maxDistSq = radius * radius;
        foreach (var col in _hits)
        {
            if (col == null || !col.CompareTag("Monster")) continue;

            Vector2 toTarget = (Vector2)col.transform.position - origin;
            if (toTarget.sqrMagnitude > maxDistSq) continue;      // 圆形外

            if (Vector2.Angle(forward, toTarget) <= halfAngle)
                visibleMonsters.Add(col.transform);
        }
    }

    /* ---------- 调试用扇形 Gizmos ---------- */
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (spotLight == null || spotLight.lightType != Light2D.LightType.Point) return;

        Gizmos.color = Color.yellow;

        Vector3 pos = spotLight.transform.position;
        float radius = spotLight.pointLightOuterRadius;
        float half = spotLight.pointLightOuterAngle * 0.5f;

        Vector3 forward3D = spotLight.transform.up;

        Vector3 dirCCW = Quaternion.Euler(0, 0, -half) * forward3D;
        Vector3 dirCW = Quaternion.Euler(0, 0, half) * forward3D;

        // 两条边
        Gizmos.DrawLine(pos, pos + dirCCW * radius);
        Gizmos.DrawLine(pos, pos + dirCW * radius);

        // 圆弧
        const int steps = 24;
        float step = (half * 2f) / steps;
        Vector3 prev = pos + dirCCW * radius;
        for (int i = 1; i <= steps; ++i)
        {
            Vector3 dir = Quaternion.Euler(0, 0, -half + step * i) * forward3D;
            Vector3 next = pos + dir * radius;
            Gizmos.DrawLine(prev, next);
            prev = next;
        }
    }
#endif
}
