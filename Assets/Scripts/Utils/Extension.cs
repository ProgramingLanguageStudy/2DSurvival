using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    /// <summary>
    /// � layer�� � layerMask�� ���ԵǴ��� ���θ� ��ȯ�� �ִ� �Լ�
    /// </summary>
    /// <param name="layerMask"></param>
    /// <param name="layer"></param>
    /// <returns></returns>
    public static bool Contains(this LayerMask layerMask, int layer)
    {
        return Util.Contains(layerMask, layer);
    }
}
