using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 무기 하나의 레벨별 스텟 데이터들을 보관하는 ScriptableObject
/// 각 스텟은 WeaponStatType별로 구분되며, 무기 레벨에 따라 값이 달라진다.
/// </summary>
[CreateAssetMenu(fileName = "WeaponData", menuName = "GameSettings/WeaponData")]
public class WeaponData : ScriptableObject
{
    // 무기 이름
    [SerializeField] string _weaponName;

    //스텟 종류별로 레벨별 값을 설정한 배열
    [SerializeField] WeaponLevelStat[] _levelStats;

    [TextArea(3,5)][SerializeField] string _description; // 무기 설명
    [SerializeField] Sprite _iconSprite;  // 무기 아이콘 스프라이트

    // 스텟 종류별로 빠르게 조회할 수 있도록 만든 딕셔너리
    Dictionary<WeaponStatType, WeaponLevelStat> _levelStatMap = new Dictionary<WeaponStatType, WeaponLevelStat>();
    // = new();
    int _maxLevel; // 최대 레벨

    public string WeaponName => _weaponName;
    public string Description => _description;
    public Sprite IconSprite => _iconSprite;
    public int MaxLevel => _maxLevel; // 최대 레벨은 _levelStats 배열 중 최대 레벨을 갖는 WeaponLevelStat의 MaxLevel

    /// <summary>
    /// 주어진 스탯 타입과 레벨에 대한 값을 반환해 주는 함수
    /// </summary>
    /// <param name="statType">무기 스탯 종류</param>
    /// <param name="level">무기 레벨</param>
    /// <returns></returns>
    public float GetStat(WeaponStatType statType, int level)
    {
        // _levelStatMap이 statType에 해당하는 값을 가지고 있는 경우
        if (_levelStatMap.TryGetValue(statType, out var levelStat))
        {
            // statType에 해당하는 값을 반환
            return levelStat.GetValue(level);
        }

        // 없으면 경고 메시지를 출력하고 0을 반환
        Debug.LogWarning($"{_weaponName} 무기는 {statType} 스텟이 없습니다.");
        return 0;
    }

    private void OnValidate()
    {
        Initialize();
    }

    private void OnEnable()
    {
        Initialize();    
    }

    /// <summary>
    /// 에디터(인스펙터뷰)에서 변수(스텟 배열, 이름 등)가 변경될 때
    /// 자동으로 _levelStatMap 딕셔너리를 갱신한다.
    /// </summary>
    void Initialize()
    {
        _levelStatMap.Clear();
        _maxLevel = 0;
        // _levelStats 배열에 있는 모든 WeaponLevelStat을 순회하면서
        foreach (var levelStat in _levelStats)
        {
            _levelStatMap[levelStat.StatType] = levelStat;

            // 무기의 최대 레벨을 WeaponLevelStat 중 최대 레벨이 가장 높은 것의
            // 최대 레벨로 설정
            if (_maxLevel < levelStat.MaxLevel)
            {
                _maxLevel = levelStat.MaxLevel; // 최대 레벨 갱신
            }
        }
    }
}
