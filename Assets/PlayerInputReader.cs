using UnityEngine;
using UnityEngine.InputSystem;

/// 负责把 Input System 数据转成简单字段，供别的脚本读取
public class PlayerInputReader : MonoBehaviour, PlayerControls.IPlayerActions
{
    public Vector2 MoveInput { get; private set; }   // 键盘方向
    public Vector2 AimScreen { get; private set; }   // 鼠标屏幕坐标

    PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();          // 生成的 C# 包装类
        controls.Player.SetCallbacks(this);
        controls.Enable();
    }

    // === IPlayerActions 回调（名字由 InputActions 自动生成） ===
    public void OnMove(InputAction.CallbackContext ctx) => MoveInput = ctx.ReadValue<Vector2>();
    public void OnAim(InputAction.CallbackContext ctx) => AimScreen = ctx.ReadValue<Vector2>();
}
