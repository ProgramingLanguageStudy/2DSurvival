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

    // �ʱ�ȭ
    private void Start()
    {
        // �ι���
        if (_timeText == null)
        {
            Debug.LogError("Time Text is not assigned in the inspector.");
            return;
        }
        // �ð� 0���� ����
        if (_timeCount <= 0)
        {
            Debug.LogError("Time Count must be greater than 0.");
            return;
        }

        // �ʴ����� �ƴ� �д����� �Է¹޾Ҵٰ� �����ϰ� 60 ����
        _timeCount *= 60;

        // �ʱ� �ð� ����
        _min = _timeCount / 60;
        _sec = _timeCount % 60;

        // �ؽ�Ʈ �ʱ�ȭ
        _timeText.text = string.Format("{0:D2} : {1:D2}", _min, _sec);

        // �ڷ�ƾ ����
        StartCoroutine(TimeCheckRoutine());
    }

    IEnumerator TimeCheckRoutine()
    {
        yield return new WaitForSeconds(1f); // ó������ 1�ʰ� �پ��� ���� �����ϱ� ���� ���

        if (_timeText == null)
        {
            yield break; // null ���� 2������ �ϴ°ű� �ѵ�...
        }

        while (true)
        {
            // 1�ʿ� 1�� ����
            _timeCount -= 1;

            // Total�ð�(���)���� �а� �ʸ� ������ ���
            _min = _timeCount / 60;
            _sec = _timeCount % 60;

            // �� �ʸ��� �ð� �ؽ�Ʈ ������Ʈ
            _timeText.text = string.Format("{0:D2} : {1:D2}", _min, _sec);

            if (_timeCount <= 0)
            {
                _timeText.text = "00 : 00";
                yield break; // 0�ʵǸ� ���̻� ī��Ʈ x
            }

            yield return new WaitForSeconds(1f); // 1�ʸ��� �ݺ�
        }
    }
}
