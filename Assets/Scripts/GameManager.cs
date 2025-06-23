using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // 캐릭터 보유 골드
    // 나중에 데이터로 저장할 수도 있음
    [SerializeField] int _gold;
    
    HeroData _selectedHero;

    // 골드 프로퍼티 함수
    public int Gold => _gold;

    public HeroData SelectedHero => _selectedHero;

    // 골드 변화 이벤트
    public event Action<int> OnGoldChanged;

    // 구매 성공 이벤트
    public event Action BuySuccessed;

    // 구매 실패 이벤트
    public event Action BuyFailed;

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
    /// 각각의 Hero 선택창에 달려있는 선택버튼에 연결될 함수
    /// 선택창에서 Hero 선택에 성공했을 때 해당 Hero의 HeroData를 SelectedHero에 저장한다.
    /// </summary>
    /// <param name="heroData">선택창에서 선택된 Hero의 HeroData</param>
    public void SelectHero(HeroData heroData)
    {
        _selectedHero = heroData;
        Debug.Log($"{heroData.Name} 선택 완료");
        
        SceneManager.LoadScene(1);
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
    public void TryBuy(int price)
    {
        if (_gold < price)
        {
            BuyFailed?.Invoke();
            return;
        }
     
        _gold -= price;
        BuySuccessed?.Invoke();
        OnGoldChanged?.Invoke(_gold);
    }

    /// <summary>
    /// 임시로 쓸 골드 치트 함수
    /// </summary>
    public void CheatGold()
    {
        AddGold(10000);
    }
}
