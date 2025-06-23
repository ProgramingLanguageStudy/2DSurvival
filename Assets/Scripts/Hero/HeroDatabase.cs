using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroDatabase", menuName = "GameSettings/HeroDatabase")]
public class HeroDatabase : ScriptableObject
{
    public List<HeroData> heroDataList;
}
