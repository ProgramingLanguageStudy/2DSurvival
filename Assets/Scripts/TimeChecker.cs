using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ���� Ŭ������� �����ð��� üũ�Ͽ� ���ѽð��� ������ ���
/// Ŭ���� ȭ���� ����ִ� ��ũ��Ʈ
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
