using UnityEngine;
using UnityEngine.InputSystem;

/// ����� Input System ����ת�ɼ��ֶΣ�����Ľű���ȡ
public class PlayerInputReader : MonoBehaviour, PlayerControls.IPlayerActions
{
    public Vector2 MoveInput { get; private set; }   // ���̷���
    public Vector2 AimScreen { get; private set; }   // �����Ļ����

    PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();          // ���ɵ� C# ��װ��
        controls.Player.SetCallbacks(this);
        controls.Enable();
    }

    // === IPlayerActions �ص��������� InputActions �Զ����ɣ� ===
    public void OnMove(InputAction.CallbackContext ctx) => MoveInput = ctx.ReadValue<Vector2>();
    public void OnAim(InputAction.CallbackContext ctx) => AimScreen = ctx.ReadValue<Vector2>();
}
