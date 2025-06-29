using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class LightBreathingHold : MonoBehaviour
{
    [Header("亮度范围")]
    [SerializeField] float minIntensity = 0f;   // 最暗亮度
    [SerializeField] float maxIntensity = 0.3f;     // 最亮亮度

    [Header("时间设置 (秒)")]
    [SerializeField] float fadeTime = 0.8f;     // 亮->暗 / 暗->亮 过渡时长
    [SerializeField] float holdBright = 1.0f;     // 最亮时停留
    [SerializeField] float holdDark = 1.0f;     // 最暗时停留

    [Header("插值曲线 (可选)")]
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
            // 1) 从亮到暗
            yield return Fade(maxIntensity, minIntensity, fadeTime);
            // 2) 最暗停留
            yield return new WaitForSeconds(holdDark);
            // 3) 从暗到亮
            yield return Fade(minIntensity, maxIntensity, fadeTime);
            // 4) 最亮停留
            yield return new WaitForSeconds(holdBright);
        }
    }

    IEnumerator Fade(float from, float to, float duration)
    {
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float p = t / duration;               // 0→1
            _light.intensity = Mathf.Lerp(from, to, ease.Evaluate(p));
            yield return null;
        }
        _light.intensity = to;                    // 保证收尾
    }
    public void ForceSetIntensity(float val)
    {
        StopAllCoroutines();      // 立即停止呼吸
        _light.intensity = val;   // 当帧就生效
    }

    private void OnEnable()
    {
        StartCoroutine(BreathLoop());
    }
}
