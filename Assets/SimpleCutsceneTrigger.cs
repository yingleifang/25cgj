using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering.Universal;

public class SimpleCutsceneTrigger : MonoBehaviour
{
    /* ---------- Inspector ���� ---------- */

    [Header("�������")]
    public CinemachineVirtualCamera gameplayCam;   // ������Ϸ��λ
    public CinemachineVirtualCamera cutsceneCam;   // ������λ

    [Header("ͣ��ʱ�� (��)")]
    public float holdTime = 5f;

    [Header("Ҫ��ͣ�Ľű�")]
    public MonoBehaviour[] scriptsToDisable;

    [SerializeField] float cutsceneIntensity = 0.4f;
    [SerializeField] float gameplayIntensity = 1f;

    [SerializeField] LightBreathingHold _lightBreathingHold;

    [Header("��ѡ�������ֹ��ֵ")]
    [Tooltip("��Ϊ 0 ���ö��⾲ֹ���")]
    public float settlePosThreshold = 0.05f;   // ��
    public float settleRotThreshold = 1f;      // �Ƕ�(��)

    /* ---------- �ڲ� ---------- */

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

        _lightBreathingHold.ForceSetIntensity(0.3f); // ���·�����
        _lightBreathingHold.enabled = false;
        /* ---------- ����ű� ---------- */
        foreach (var s in scriptsToDisable) s.enabled = false;

        /* ---------- �е� Cutscene ---------- */
        int gameplayPrio = gameplayCam.Priority;
        int cutscenePrio = cutsceneCam.Priority;       // ��¼ԭֵ

        cutsceneCam.Priority = gameplayPrio + 10;      // ����
        yield return new WaitUntil(() => !_brain.IsBlending);

        /* ---------- ͣ�� ---------- */
        yield return new WaitForSeconds(holdTime);

        /* ---------- �л� Gameplay ---------- */
        cutsceneCam.Priority = gameplayPrio - 1;
        // ���뱣�� cutsceneCam ԭʼ Priority����ĳɣ�
        // cutsceneCam.Priority = cutscenePrio;

        /* ---------- �ȴ���ͷ��λ ---------- */
        yield return new WaitUntil(() =>
        {
            if (_brain.IsBlending) return false;                       // ���� Blend
            if (_brain.ActiveVirtualCamera != (ICinemachineCamera)gameplayCam)
                return false;                                          // ������ GameplayCam

            // �����þ�ֹ��⣬ֱ�ӷ��� true
            if (settlePosThreshold <= 0f || settleRotThreshold <= 0f)
                return true;

            // ��һ������Ƿ���ȫ���ϡ�Ŀ��λ��
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
