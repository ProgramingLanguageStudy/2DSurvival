using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.Characters;
using System;

public class NamespaceExample : MonoBehaviour
{
    void Start()
    {
        // Player 클래스로 객체를 만든 것.
        Player player = new Player();
        player.Move();

        // 사용 불가
        // System 네임스페이스와 UnityEngine 네임스페이스에
        // 모두 Random 클래스가 존재
        // 이름이 겹쳐서 뭘 쓸지 몰라서 생기는 오류
        //int randomNumber = Random.Range(0, 10);

        // 클래스명이 겹치면 앞에 네임스페이명을 적어 줘서
        // 어떤 네임스페이스의 클래스를 사용하는 건지
        // 명시적으로 알려 주면 된다.
        int randomNumber = UnityEngine.Random.Range(0, 10);
    }
}
