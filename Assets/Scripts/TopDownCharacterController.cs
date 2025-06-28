using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInputReader))]
public class TopDownCharacterController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;

    Rigidbody2D rb;
    PlayerInputReader input;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInputReader>();
    }

    /* ---------- 只处理位移 ---------- */
    void FixedUpdate()
    {
        Vector2 dir = input.MoveInput.normalized;
        Vector2 target = rb.position + dir * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(target);
    }
}
