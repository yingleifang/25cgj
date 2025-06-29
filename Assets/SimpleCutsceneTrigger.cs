using System.Collections;
using UnityEngine;
using Cinemachine;

public class SimpleCutsceneTrigger : MonoBehaviour
{
    [Header("虚拟相机")]
    public CinemachineVirtualCamera gameplayCam;
    public CinemachineVirtualCamera cutsceneCam;

    [Header("停留时长")]
    public float holdTime = 2f;

    [Header("要暂停的脚本")]
    public MonoBehaviour[] scriptsToDisable;

    CinemachineBrain _brain;
    bool _busy;

    void Awake() => _brain = Camera.main.GetComponent<CinemachineBrain>();
    public void PlayCutscene()
    {
        if (!_busy) StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        _busy = true;

        // Freeze
        foreach (var s in scriptsToDisable) s.enabled = false;

        /* ---------- 切到 Cutscene ---------- */
        int gameplayPrio = gameplayCam.Priority;
        int cutscenePrio = cutsceneCam.Priority;          // 记住原值

        cutsceneCam.Priority = gameplayPrio + 10;           // 一定更高
        yield return new WaitUntil(() => !_brain.IsBlending);

        /* ---------- 停留 ---------- */
        yield return new WaitForSeconds(holdTime);

        /* ---------- 淡回 ---------- */
        // *确保* Cutscene 优先级比 Gameplay 低，再低 1 就足够
        cutsceneCam.Priority = gameplayPrio - 1;

        // 如果你想保留它的原始优先级，可在最后再还原：cutsceneCam.Priority = cutscenePrio;

        // 等到画面不再由 Cutscene 机位控制
        yield return new WaitUntil(() =>
            !_brain.IsBlending &&
            _brain.ActiveVirtualCamera != (ICinemachineCamera)cutsceneCam);

        /* ---------- 恢复 ---------- */
        foreach (var s in scriptsToDisable) s.enabled = true;
        _busy = false;
    }
}
