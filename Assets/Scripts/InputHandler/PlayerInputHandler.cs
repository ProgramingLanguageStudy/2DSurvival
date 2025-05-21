using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// ����Ƽ Input System�� PlayerInput�� Ȱ���� �Է��� �޾� �˸��� ����
/// </summary>
public class PlayerInputHandler : InputHandler
{
    public override event UnityAction<Vector2> OnMoveInput;

    Vector2 _moveInput;

    void OnMove(InputValue inputValue)
    {
        _moveInput = inputValue.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        OnMoveInput?.Invoke(_moveInput);
    }
}
