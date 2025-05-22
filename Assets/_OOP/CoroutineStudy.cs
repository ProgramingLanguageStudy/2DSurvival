using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
��ü ����� �������� ���� X
����Ƽ�� ���
�ð� �帧 ��� �ʿ��� ��� ������ ����

�ڷ�ƾ(Coroutine)
����Ƽ���� �ð��� �帧�� ������ �� �ִ� �Լ�
IEnumerator Ư���� ��ȯ �ڷ���

-> ������ ���ص��� �ʾƵ� ������.
-> ���� ���� ���߿� ���ذ� �Ǵ� ���� �� ��.
-> ������ ���� ������� ����� �� �ִ� Update ���� �Լ�
 
*/

public class CoroutineStudy : MonoBehaviour
{
    private void Start()
    {
        // PrintMessages() �ڷ�ƾ ����
        StartCoroutine(PrintMessages());

        // SpaceKeyRoutine() �ڷ�ƾ ����
        StartCoroutine(SpaceKeyRoutine());
    }

    // �޽����� ���� �ð� �������� ����ϴ� �ڷ�ƾ �Լ�
    IEnumerator PrintMessages()
    {
        Debug.Log("ù ��° �޽���");

        // 1�� ���
        yield return new WaitForSeconds(3.0f);

        Debug.Log("3�� �� �޽���");

        // 2�� �߰� ���
        yield return new WaitForSeconds(2.0f);

        // ���� �����ӱ��� ���
        yield return null;

        // ���� FixedUpdate �ֱ���� ���
        yield return new WaitForFixedUpdate();
    }

    // �����̽��� Ű ���� �˸� �ڷ�ƾ
    IEnumerator SpaceKeyRoutine()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("�����̽� ����!");
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("�ڷ�ƾ �ߴ�");

                yield break;
            }
            yield return null;
        }
    }
}
