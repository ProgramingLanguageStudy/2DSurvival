using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClearController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _clearText;
    [TextArea(3,5)][SerializeField] string _message;
    [SerializeField] float _messageSpan;

    private void Start()
    {
        StartCoroutine(ClearTextRoutine(_message));
    }

    IEnumerator ClearTextRoutine(string message)
    {
        _clearText.text = "";

        foreach (char c in message)
        {
            _clearText.text += c;
            yield return new WaitForSeconds(_messageSpan);
        }
    }

}
