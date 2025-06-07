using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 현재 클리어까지 남은시간을 체크하여 제한시간을 버텼을 경우
/// 클리어 화면을 띄워주는 스크립트
/// </summary>
public class TimeChecker : MonoBehaviour
{
    public void CheckRemainingTime(float remainingTime)
    {
        if (remainingTime <= 1)
        {
            SceneManager.LoadScene("Clear");
        }
    }
}
