using System.Collections;
using UnityEngine;
using Cinemachine;

public class SimpleCutsceneTrigger : MonoBehaviour
{
    [Header("�������")]
    public CinemachineVirtualCamera gameplayCam;
    public CinemachineVirtualCamera cutsceneCam;

    [Header("ͣ��ʱ��")]
    public float holdTime = 2f;

    [Header("Ҫ��ͣ�Ľű�")]
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

        /* ---------- �е� Cutscene ---------- */
        int gameplayPrio = gameplayCam.Priority;
        int cutscenePrio = cutsceneCam.Priority;          // ��סԭֵ

        cutsceneCam.Priority = gameplayPrio + 10;           // һ������
        yield return new WaitUntil(() => !_brain.IsBlending);

        /* ---------- ͣ�� ---------- */
        yield return new WaitForSeconds(holdTime);

        /* ---------- ���� ---------- */
        // *ȷ��* Cutscene ���ȼ��� Gameplay �ͣ��ٵ� 1 ���㹻
        cutsceneCam.Priority = gameplayPrio - 1;

        // ������뱣������ԭʼ���ȼ�����������ٻ�ԭ��cutsceneCam.Priority = cutscenePrio;

        // �ȵ����治���� Cutscene ��λ����
        yield return new WaitUntil(() =>
            !_brain.IsBlending &&
            _brain.ActiveVirtualCamera != (ICinemachineCamera)cutsceneCam);

        /* ---------- �ָ� ---------- */
        foreach (var s in scriptsToDisable) s.enabled = true;
        _busy = false;
    }
}
