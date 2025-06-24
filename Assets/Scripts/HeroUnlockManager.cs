using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 영웅의 잠금 여부를 관리하는 클래스
/// 해쉬셋을 이용해서 키가 있는지 여부를 저장
/// 즉 해쉬셋에서 0번 해쉬셋이 있으면 0번영웅은 해제 되었다는 뜻
/// 3번 해쉬셋이 비어있으면 3번 영웅은 잠겨 있다는 뜻
/// </summary>
public class HeroUnlockManager : MonoBehaviour
{
    public static HeroUnlockManager Instance;

    // 잠금 해제된 영웅의 해쉬셋
    private HashSet<int> _unlockedHeroIds = new();

    public bool IsUnlocked(int heroId) => _unlockedHeroIds.Contains(heroId);

    public event Action<int> OnHeroUnlocked;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 영웅을 해제했을 때 해쉬셋에 추가하는 함수
    /// </summary>
    /// <param name="heroId">해제한 영웅의 ID</param>
    public void Unlock(int heroId)
    {
        _unlockedHeroIds.Add(heroId);
        //PlayerPrefs.SetInt($"HeroUnlocked_{heroId}", 1);

        OnHeroUnlocked?.Invoke(heroId);
    }

    /// <summary>
    /// 게임 시작 시 해제된 영웅을 불러오는 함수
    /// 나중에 구현할 일이 있을수도 있어서 만들어놓음
    /// </summary>
    /// <param name="heroDatabase"></param>
    public void LoadUnlockData(HeroDatabase heroDatabase)
    {
        foreach (var heroDataBundle in heroDatabase.heroDataBundleList)
        {
            if (PlayerPrefs.GetInt($"HeroUnlocked_{heroDataBundle.HeroId}", 0) == 1)
            {
                _unlockedHeroIds.Add(heroDataBundle.HeroId);
            }
        }
    }

    // 임시 테스트용
    // 잠금 해제한 영웅들을 다시 잠금
    public void ResetUnlocks()
    {
        _unlockedHeroIds.Clear();
    }
}
