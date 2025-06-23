using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HeroDatabase�� �ִ� HeroData ����Ʈ�� �̿��Ͽ�
/// ĳ���� ����â�� �����ϴ� Ŭ����
/// </summary>
public class HeroSelectUI : MonoBehaviour
{
    [SerializeField] HeroDatabase _heroDatabase;

    [SerializeField] UIManager _uiManager;
    [SerializeField] Transform _contentParent;
    [SerializeField] GameObject _heroSelectionViewPrefab;

    void Start()
    {
        // HeroDatabase�� �ִ� HeroData����Ʈ ���� �ľ��ؼ�
        foreach (var heroData in _heroDatabase.heroDataList)
        {
            // HeroSelectionView�������� �����ؼ� ����(foreach�ϱ� �ݺ��ɿ���)
            GameObject viewPrefab = Instantiate(_heroSelectionViewPrefab, _contentParent);

            // ������ �� �����鿡�� HeroSelectionView ������Ʈ �����ͼ� ����(���������� �ݺ�����)
            HeroSelectionView view = viewPrefab.GetComponent<HeroSelectionView>();

            // ������ HeroSelectionView���� �ʱ�ȭ�Լ� ����(HeroData�Ѱ��ְ� UIText�� �Ѱ��ְ�)
            view.Initialize(heroData, _uiManager.UIText);

            // view���� ĳ���� ���ý� �߻��Ǵ� �̺�Ʈ �Լ� ����
            view.OnClickSelected += HandleHeroSelected;
        }
    }

    /// <summary>
    /// ĳ���� ���� �� �߻��ϴ� �̺�Ʈ�� ó���ϴ� �Լ�.
    /// ���õ� ���� ������ GameManager�� �����ϰ�,
    /// ����ִ� ��쿡�� �ȳ� �޽����� �����.
    /// </summary>
    /// <param name="selectedHero">���õ� ������ ������</param>
    void HandleHeroSelected(HeroData selectedHero)
    {
        // ���⿡ ���� �� ó�� ����: ����
        if (selectedHero.IsUnlocked)
        {
            GameManager.Instance.SelectHero(selectedHero);
        }
        else
        {
            //���� ���� �� UI ����
            Debug.Log($"{selectedHero.Name}{_uiManager.UIText.clickLockedHero}");
        }
    }
}
