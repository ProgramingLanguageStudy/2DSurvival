using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 버튼을 눌렀을 때 나오는 메세지를 출력하는 클래스
/// </summary>
public class InfoPopup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _messageText;

    public void Show(string message)
    {
        _messageText.text = message;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
