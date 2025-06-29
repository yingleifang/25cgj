using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering.Universal;

public class SimpleCutsceneTrigger : MonoBehaviour
{
    /* ---------- Inspector 参数 ---------- */

    [Header("虚拟相机")]
    public CinemachineVirtualCamera gameplayCam;   // 正常游戏机位
    public CinemachineVirtualCamera cutsceneCam;   // 过场机位

    [Header("停留时长 (秒)")]
    public float holdTime = 5f;

    [Header("要暂停的脚本")]
    public MonoBehaviour[] scriptsToDisable;

    [SerializeField] float cutsceneIntensity = 0.4f;
    [SerializeField] float gameplayIntensity = 1f;

    [SerializeField] LightBreathingHold _lightBreathingHold;

    [Header("可选：相机静止阈值")]
    [Tooltip("设为 0 禁用额外静止检测")]
    public float settlePosThreshold = 0.05f;   // 米
    public float settleRotThreshold = 1f;      // 角度(°)

    /* ---------- 内部 ---------- */

    CinemachineBrain _brain;
    bool _busy;

    void Awake()
    {
        _brain = Camera.main.GetComponent<CinemachineBrain>();
    }

    public void PlayCutscene()
    {
        if (!_busy) StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        _busy = true;

        _lightBreathingHold.ForceSetIntensity(0.3f); // 见下方方法
        _lightBreathingHold.enabled = false;
        /* ---------- 冻结脚本 ---------- */
        foreach (var s in scriptsToDisable) s.enabled = false;

        /* ---------- 切到 Cutscene ---------- */
        int gameplayPrio = gameplayCam.Priority;
        int cutscenePrio = cutsceneCam.Priority;       // 记录原值

        cutsceneCam.Priority = gameplayPrio + 10;      // 抢镜
        yield return new WaitUntil(() => !_brain.IsBlending);

        /* ---------- 停留 ---------- */
        yield return new WaitForSeconds(holdTime);

        /* ---------- 切回 Gameplay ---------- */
        cutsceneCam.Priority = gameplayPrio - 1;
        // 若想保留 cutsceneCam 原始 Priority，请改成：
        // cutsceneCam.Priority = cutscenePrio;

        /* ---------- 等待镜头归位 ---------- */
        yield return new WaitUntil(() =>
        {
            if (_brain.IsBlending) return false;                       // 仍在 Blend
            if (_brain.ActiveVirtualCamera != (ICinemachineCamera)gameplayCam)
                return false;                                          // 还不是 GameplayCam

            // 若禁用静止检测，直接返回 true
            if (settlePosThreshold <= 0f || settleRotThreshold <= 0f)
                return true;

            // 进一步检查是否“完全贴合”目标位姿
            Transform camTf = Camera.main.transform;
            Vector3 goalPos = gameplayCam.State.FinalPosition;
            Quaternion goalRot = gameplayCam.State.FinalOrientation;

            bool posOK = Vector3.Distance(camTf.position, goalPos) < settlePosThreshold;
            bool rotOK = Quaternion.Angle(camTf.rotation, goalRot) < settleRotThreshold;
            return posOK && rotOK;
        });

        foreach (var s in scriptsToDisable) s.enabled = true;
        _busy = false;
    }
}
