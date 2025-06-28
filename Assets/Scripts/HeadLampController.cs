using UnityEngine;

/// <summary>
/// �ֵ�Ͳƽ��׷����꣬��������ͼ����ƫ��
/// </summary>
public class HeadLampController : MonoBehaviour
{
    [Header("Hierarchy")]
    [SerializeField] Transform headLampPivot;
    [SerializeField] float headOffsetY = 0.6f;

    [Header("Rotation Tuning")]
    [Range(0f, 20f)]
    [SerializeField] float smoothness = 8f;          // ָ��ƽ��������
    [Min(0f)]
    [SerializeField] float maxTurnSpeed = 120f;      // ��/s
    [Range(0f, 5f)]
    [SerializeField] float snapThreshold = 0.3f;     // ���ֱ��������ֵ

    [Header("Sprite Forward Offset")]
    [Tooltip("����ͼ�泯 +Y����Ҫ�� -90���泯 -Y �� 90���泯 +X �� 0")]
    [SerializeField] float spriteForwardOffset = -90f;

    Camera mainCam;
    PlayerInputReader input;

    void Awake()
    {
        mainCam = Camera.main;
        input = GetComponentInParent<PlayerInputReader>();

        var lp = headLampPivot.localPosition;   // �� pivot �ŵ�ͷ��
        lp.y = headOffsetY;
        headLampPivot.localPosition = lp;
    }

    void LateUpdate()
    {
        /* ---------- 1) Ŀ��Ƕȣ�һ���Լ���ƫ�ƣ� ---------- */
        Vector2 mouseWorld = mainCam.ScreenToWorldPoint(input.AimScreen);
        Vector2 dir = mouseWorld - (Vector2)headLampPivot.position;
        if (dir.sqrMagnitude < 0.0001f) return;

        float targetZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg
                        + spriteForwardOffset;   // ����һ��

        float currentZ = headLampPivot.eulerAngles.z;

        /* ---------- 2) ָ��ƽ�� ---------- */
        float lerpedZ = smoothness > 0f
            ? Mathf.LerpAngle(currentZ, targetZ,
                              1f - Mathf.Exp(-smoothness * Time.deltaTime))
            : targetZ;

        /* ---------- 3) ���ת������ ---------- */
        if (maxTurnSpeed > 0f)
            lerpedZ = Mathf.MoveTowardsAngle(currentZ, lerpedZ,
                                             maxTurnSpeed * Time.deltaTime);

        /* ---------- 4) ���С������ ---------- */
        if (Mathf.Abs(Mathf.DeltaAngle(lerpedZ, targetZ)) < snapThreshold)
            lerpedZ = targetZ;

        headLampPivot.rotation = Quaternion.Euler(0, 0, lerpedZ);
    }
}
