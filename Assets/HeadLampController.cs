using UnityEngine;

/// <summary>
/// 手电筒平滑追随鼠标，可配置贴图朝向偏移
/// </summary>
public class HeadLampController : MonoBehaviour
{
    [Header("Hierarchy")]
    [SerializeField] Transform headLampPivot;
    [SerializeField] float headOffsetY = 0.6f;

    [Header("Rotation Tuning")]
    [Range(0f, 20f)]
    [SerializeField] float smoothness = 8f;          // 指数平滑锐利度
    [Min(0f)]
    [SerializeField] float maxTurnSpeed = 120f;      // °/s
    [Range(0f, 5f)]
    [SerializeField] float snapThreshold = 0.3f;     // 误差直接锁定阈值

    [Header("Sprite Forward Offset")]
    [Tooltip("若贴图面朝 +Y，需要填 -90；面朝 -Y 填 90；面朝 +X 填 0")]
    [SerializeField] float spriteForwardOffset = -90f;

    Camera mainCam;
    PlayerInputReader input;

    void Awake()
    {
        mainCam = Camera.main;
        input = GetComponentInParent<PlayerInputReader>();

        var lp = headLampPivot.localPosition;   // 把 pivot 放到头顶
        lp.y = headOffsetY;
        headLampPivot.localPosition = lp;
    }

    void LateUpdate()
    {
        /* ---------- 1) 目标角度（一次性加入偏移） ---------- */
        Vector2 mouseWorld = mainCam.ScreenToWorldPoint(input.AimScreen);
        Vector2 dir = mouseWorld - (Vector2)headLampPivot.position;
        if (dir.sqrMagnitude < 0.0001f) return;

        float targetZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg
                        + spriteForwardOffset;   // 仅此一次

        float currentZ = headLampPivot.eulerAngles.z;

        /* ---------- 2) 指数平滑 ---------- */
        float lerpedZ = smoothness > 0f
            ? Mathf.LerpAngle(currentZ, targetZ,
                              1f - Mathf.Exp(-smoothness * Time.deltaTime))
            : targetZ;

        /* ---------- 3) 最大转速限制 ---------- */
        if (maxTurnSpeed > 0f)
            lerpedZ = Mathf.MoveTowardsAngle(currentZ, lerpedZ,
                                             maxTurnSpeed * Time.deltaTime);

        /* ---------- 4) 误差小则锁定 ---------- */
        if (Mathf.Abs(Mathf.DeltaAngle(lerpedZ, targetZ)) < snapThreshold)
            lerpedZ = targetZ;

        headLampPivot.rotation = Quaternion.Euler(0, 0, lerpedZ);
    }
}
