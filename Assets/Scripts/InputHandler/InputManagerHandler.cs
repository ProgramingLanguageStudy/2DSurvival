using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 유니티 InputManager를 사용해 입력을 받아 알리는 역할
/// </summary>
public class InputManagerHandler : InputHandler
{
    public override event UnityAction<Vector2> OnMoveInput;
    Vector2 _inputVector = Vector2.zero;

    private void Update()
    {
        _inputVector.x = Input.GetAxisRaw("Horizontal");
        _inputVector.y = Input.GetAxisRaw("Vertical");

        // 이동 입력 이벤트 발생
        // 구독자들에게 이동 입력 알림
        OnMoveInput?.Invoke(_inputVector);
    }
}