using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장비 데이터 클래스
/// </summary>
[CreateAssetMenu(fileName = "GearData", menuName = "GameSettings/GearData")]
public class GearData : ScriptableObject
{
    [SerializeField] string _gearName;      // 장비 이름
    [SerializeField] float[] _levelValues;  // 레벨별 능력치 값들

    public string GearName => _gearName;
    
    /// <summary>
    /// 레벨에 따른 보너스 수치를 반환하는 함수
    /// </summary>
    /// <param name="level"></param>
    /// <returns>음수 레벨이면 0, 최대 레벨 초과하면 최대 레벨 보너스 수치</returns>
    public float GetValue(int level)
    {
        if (level < 0) return 0;
        if (level >= _levelValues.Length)
        {
            // 최대 레벨을 초과하면 최대 레벨로 설정
            level = _levelValues.Length - 1; 
        }
        return _levelValues[level];
    }
}
