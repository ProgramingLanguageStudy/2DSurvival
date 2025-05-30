using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrader : MonoBehaviour
{
    [SerializeField] Gear[] _gears;      // ���׷��̵� ������ ����

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _gears[0].Upgrade();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _gears[1].Upgrade();
        }
    }
}
