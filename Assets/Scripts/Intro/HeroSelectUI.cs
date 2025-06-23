using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HeroDatabase에 있는 HeroData 리스트를 이용하여
/// 캐릭터 선택창을 생성하는 클래스
/// </summary>
public class HeroSelectUI : MonoBehaviour
{
    [SerializeField] HeroDatabase _heroDatabase;

    [SerializeField] UIManager _uiManager;
    [SerializeField] Transform _contentParent;
    [SerializeField] GameObject _heroSelectionViewPrefab;

    void Start()
    {
        // HeroDatabase에 있는 HeroData리스트 수를 파악해서
        foreach (var heroData in _heroDatabase.heroDataList)
        {
            // HeroSelectionView프리펩을 생성해서 저장(foreach니까 반복될예정)
            GameObject viewPrefab = Instantiate(_heroSelectionViewPrefab, _contentParent);

            // 생성된 뷰 프리펩에서 HeroSelectionView 컴포넌트 가져와서 저장(마찬가지로 반복예정)
            HeroSelectionView view = viewPrefab.GetComponent<HeroSelectionView>();

            // 가져온 HeroSelectionView에서 초기화함수 실행(HeroData넘겨주고 UIText들 넘겨주고)
            view.Initialize(heroData, _uiManager.UIText);

            // view에서 캐릭터 선택시 발생되는 이벤트 함수 구독
            view.OnClickSelected += HandleHeroSelected;
        }
    }

    /// <summary>
    /// 캐릭터 선택 시 발생하는 이벤트를 처리하는 함수.
    /// 선택된 영웅 정보를 GameManager에 전달하고,
    /// 잠겨있는 경우에는 안내 메시지를 출력함.
    /// </summary>
    /// <param name="selectedHero">선택된 영웅의 데이터</param>
    void HandleHeroSelected(HeroData selectedHero)
    {
        // 여기에 선택 후 처리 로직: 예시
        if (selectedHero.IsUnlocked)
        {
            GameManager.Instance.SelectHero(selectedHero);
        }
        else
        {
            //구매 실패 시 UI 띄우기
            Debug.Log($"{selectedHero.Name}{_uiManager.UIText.clickLockedHero}");
        }
    }
}
