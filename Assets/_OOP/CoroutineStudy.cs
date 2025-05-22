using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
객체 지향과 직접적인 연관 X
유니티의 기능
시간 흐름 제어가 필요한 경우 유용한 도구

코루틴(Coroutine)
유니티에서 시간의 흐름을 제어할 수 있는 함수
IEnumerator 특수한 반환 자료형

-> 원리가 이해되지 않아도 괜찮다.
-> 쓰다 보면 나중에 이해가 되는 날이 올 것.
-> 지금은 내가 마음대로 사용할 수 있는 Update 같은 함수
 
*/

public class CoroutineStudy : MonoBehaviour
{
    private void Start()
    {
        // PrintMessages() 코루틴 시작
        StartCoroutine(PrintMessages());

        // SpaceKeyRoutine() 코루틴 시작
        StartCoroutine(SpaceKeyRoutine());
    }

    // 메시지를 일정 시간 간격으로 출력하는 코루틴 함수
    IEnumerator PrintMessages()
    {
        Debug.Log("첫 번째 메시지");

        // 1초 대기
        yield return new WaitForSeconds(3.0f);

        Debug.Log("3초 후 메시지");

        // 2초 추가 대기
        yield return new WaitForSeconds(2.0f);

        // 다음 프레임까지 대기
        yield return null;

        // 다음 FixedUpdate 주기까지 대기
        yield return new WaitForFixedUpdate();
    }

    // 스페이스바 키 누름 알림 코루틴
    IEnumerator SpaceKeyRoutine()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("스페이스 누름!");
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("코루틴 중단");

                yield break;
            }
            yield return null;
        }
    }
}
