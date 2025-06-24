using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ��� �����͹����� ����Ʈ�� ���� �ִ� �����ͺ��̽�
/// </summary>
[CreateAssetMenu(fileName = "HeroDatabase", menuName = "GameSettings/HeroDatabase")]
public class HeroDatabase : ScriptableObject
{
    public List<HeroDataBundle> heroDataBundleList;
}
