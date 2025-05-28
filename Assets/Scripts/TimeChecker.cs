using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChecker : MonoBehaviour
{
    IEnumerator TimeCheckRoutine()
    {
        float timeCount = 15f;
        while (true)
        {
            timeCount -= Time.fixedDeltaTime;
        }
    }
}
