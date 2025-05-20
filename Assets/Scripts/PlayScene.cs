using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Play 씬을 총괄하는 역할
/// </summary>
public class PlayScene : MonoBehaviour
{
    [SerializeField] InputHandler _inputHandler;
    [SerializeField] Hero _hero;

    void Start()
    {
        _hero.Initialize();

        // 이동 입력 이벤트 구독
        _inputHandler.OnMoveInput += OnMoveInput;
    } 

    /// <summary>
    /// 이동 입력이 들어왔을 때 실행하는 함수
    /// </summary>
    /// <param name="inputVec"></param>
    public void OnMoveInput(Vector2 inputVec)
    {
        _hero.Move(inputVec);
    }
}
