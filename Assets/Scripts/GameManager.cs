using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 게임 진행중 항상 유지될 필요가 있는 정보들이 담겨 있는 클래스
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    // 캐릭터 보유 골드(확인용)
    // 나중에 데이터로 저장할 수도 있음
    [SerializeField] int _gold;
    int _heroId;

    [SerializeField] HeroDatabase _heroDatabase;
    HeroDataBundle _heroDataBundle;

    public HeroDataBundle HeroDataBundle => _heroDataBundle;

    public int Gold => _gold;

    public int HeroId => _heroId;

    // 골드 변화 이벤트
    public event Action<int> OnGoldChanged;

    // 구매 성공 이벤트
    public event Action HeroBuySuccessed;

    // 구매 실패 이벤트
    public event Action HeroBuyFailed;

    // 영웅 선택 성공 이벤트
    public event Action<int> HeroSelectSuccessed;

    // 영웅 선택 실패 이벤트
    public event Action HeroSelectFailed;

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

    private void Start()
    {
        // 일단 초기화를 0으로 하지만 데이터가 있으면 데이터를 불러와야함.
        _gold = 0;
    }

    /// <summary>
    /// 보유중인 골드에 값을 더해주는 함수
    /// </summary>
    /// <param name="amount">증가할 골드량</param>
    public void AddGold(int amount)
    {
        _gold += amount;
        OnGoldChanged?.Invoke(_gold);
    }

    /// <summary>
    /// 구매하려고 하는 함수
    /// </summary>
    /// <param name="amount">구매가격</param>
    public void TryBuy(HeroDataBundle heroDataBundle, int price)
    {
        if (heroDataBundle == null)
        {
            Debug.LogWarning($"heroDataBundle이 없습니다.");
            return;
        }

        _heroId = heroDataBundle.HeroId;

        if (_gold < price)
        {
            HeroBuyFailed?.Invoke();
            return;
        }
     
        _gold -= price;
        HeroUnlockManager.Instance.Unlock(_heroId);
        HeroBuySuccessed?.Invoke();
        OnGoldChanged?.Invoke(_gold);
    }

    /// <summary>
    /// 캐릭터 선택 시 발생하는 이벤트를 처리하는 함수.
    /// 선택된 영웅 정보를 이벤트로 알리고,
    /// 잠겨있는 경우에는 안내 메시지를 출력함.
    /// </summary>
    /// <param name="heroId">입력받은 영웅의 heroId</param>
    public void OnHeroSelected(int heroId)
    {
        _heroId = heroId;
        // 열려 있는 캐릭터면
        if (HeroUnlockManager.Instance.IsUnlocked(_heroId))
        {
            SceneManager.LoadScene(1);
            HeroSelectSuccessed?.Invoke(_heroId);
        }
        // 잠겨 있는 캐릭터면
        else
        {
            HeroSelectFailed?.Invoke();
        }
    }

    /// <summary>
    /// 임시로 쓸 골드 치트 함수
    /// </summary>
    public void CheatGold()
    {
        AddGold(10000);
    }
}
