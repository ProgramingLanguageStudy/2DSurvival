using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeChecker : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _timeText;
    
    [SerializeField] int _timeCount = 0;
    int _min = 0;
    int _sec = 0;

    // 초기화
    private void Start()
    {
        // 널방지
        if (_timeText == null)
        {
            Debug.LogError("Time Text is not assigned in the inspector.");
            return;
        }
        // 시간 0이하 방지
        if (_timeCount <= 0)
        {
            Debug.LogError("Time Count must be greater than 0.");
            return;
        }

        // 초단위가 아닌 분단위를 입력받았다고 가정하고 60 곱함
        _timeCount *= 60;

        // 초기 시간 설정
        _min = _timeCount / 60;
        _sec = _timeCount % 60;

        // 텍스트 초기화
        _timeText.text = string.Format("{0:D2} : {1:D2}", _min, _sec);

        // 코루틴 시작
        StartCoroutine(TimeCheckRoutine());
    }

    IEnumerator TimeCheckRoutine()
    {
        yield return new WaitForSeconds(1f); // 처음부터 1초가 줄어드는 것을 방지하기 위해 대기

        if (_timeText == null)
        {
            yield break; // null 방지 2중으로 하는거긴 한데...
        }

        while (true)
        {
            // 1초에 1씩 감소
            _timeCount -= 1;

            // Total시간(덩어리)에서 분과 초를 나누어 계산
            _min = _timeCount / 60;
            _sec = _timeCount % 60;

            // 매 초마다 시간 텍스트 업데이트
            _timeText.text = string.Format("{0:D2} : {1:D2}", _min, _sec);

            if (_timeCount <= 0)
            {
                _timeText.text = "00 : 00";
                yield break; // 0초되면 더이상 카운트 x
            }

            yield return new WaitForSeconds(1f); // 1초마다 반복
        }
    }
}
