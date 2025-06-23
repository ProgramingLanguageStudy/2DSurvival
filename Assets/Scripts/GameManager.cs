using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // ĳ���� ���� ���
    // ���߿� �����ͷ� ������ ���� ����
    [SerializeField] int _gold;
    
    HeroData _selectedHero;

    // ��� ������Ƽ �Լ�
    public int Gold => _gold;

    public HeroData SelectedHero => _selectedHero;

    // ��� ��ȭ �̺�Ʈ
    public event Action<int> OnGoldChanged;

    // ���� ���� �̺�Ʈ
    public event Action BuySuccessed;

    // ���� ���� �̺�Ʈ
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
        // �ϴ� �ʱ�ȭ�� 0���� ������ �����Ͱ� ������ �����͸� �ҷ��;���.
        _gold = 0;
    }

    /// <summary>
    /// ������ Hero ����â�� �޷��ִ� ���ù�ư�� ����� �Լ�
    /// ����â���� Hero ���ÿ� �������� �� �ش� Hero�� HeroData�� SelectedHero�� �����Ѵ�.
    /// </summary>
    /// <param name="heroData">����â���� ���õ� Hero�� HeroData</param>
    public void SelectHero(HeroData heroData)
    {
        _selectedHero = heroData;
        Debug.Log($"{heroData.Name} ���� �Ϸ�");
        
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// �������� ��忡 ���� �����ִ� �Լ�
    /// </summary>
    /// <param name="amount">������ ��差</param>
    public void AddGold(int amount)
    {
        _gold += amount;
        OnGoldChanged?.Invoke(_gold);
    }

    /// <summary>
    /// �����Ϸ��� �ϴ� �Լ�
    /// </summary>
    /// <param name="amount">���Ű���</param>
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
    /// �ӽ÷� �� ��� ġƮ �Լ�
    /// </summary>
    public void CheatGold()
    {
        AddGold(10000);
    }
}
