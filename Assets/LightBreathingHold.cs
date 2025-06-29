using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class LightBreathingHold : MonoBehaviour
{
    [Header("���ȷ�Χ")]
    [SerializeField] float minIntensity = 0f;   // �����
    [SerializeField] float maxIntensity = 0.3f;     // ��������

    [Header("ʱ������ (��)")]
    [SerializeField] float fadeTime = 0.8f;     // ��->�� / ��->�� ����ʱ��
    [SerializeField] float holdBright = 1.0f;     // ����ʱͣ��
    [SerializeField] float holdDark = 1.0f;     // �ʱͣ��

    [Header("��ֵ���� (��ѡ)")]
    [SerializeField] AnimationCurve ease = AnimationCurve.EaseInOut(0, 0, 1, 1);

    Light2D _light;

    void Awake()
    {
        _light = GetComponentInParent<Light2D>();
        StartCoroutine(BreathLoop());
    }

    IEnumerator BreathLoop()
    {
        while (true)
        {
            // 1) ��������
            yield return Fade(maxIntensity, minIntensity, fadeTime);
            // 2) �ͣ��
            yield return new WaitForSeconds(holdDark);
            // 3) �Ӱ�����
            yield return Fade(minIntensity, maxIntensity, fadeTime);
            // 4) ����ͣ��
            yield return new WaitForSeconds(holdBright);
        }
    }

    IEnumerator Fade(float from, float to, float duration)
    {
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float p = t / duration;               // 0��1
            _light.intensity = Mathf.Lerp(from, to, ease.Evaluate(p));
            yield return null;
        }
        _light.intensity = to;                    // ��֤��β
    }
    public void ForceSetIntensity(float val)
    {
        StopAllCoroutines();      // ����ֹͣ����
        _light.intensity = val;   // ��֡����Ч
    }

    private void OnEnable()
    {
        StartCoroutine(BreathLoop());
    }
}
