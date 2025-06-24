using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HeroDatabase에 있는 HeroData 리스트를 이용하여
/// 캐릭터 선택창을 생성하는 클래스
/// </summary>
public class HeroSelectionUIBuilder : MonoBehaviour
{
    [SerializeField] HeroDatabase _heroDatabase;

    [SerializeField] UIManager _uiManager;
    [SerializeField] Transform _contentParent;
    [SerializeField] GameObject _heroSelectionViewPrefab;

    void Start()
    {
        // HeroDatabase에 있는 HeroData리스트 수를 파악해서
        foreach (var heroDataBundle in _heroDatabase.heroDataBundleList)
        {
            // HeroSelectionView프리펩을 생성해서 저장(foreach니까 반복될예정)
            GameObject viewPrefab = Instantiate(_heroSelectionViewPrefab, _contentParent);

            // 생성된 뷰 프리펩에서 HeroSelectionView 컴포넌트 가져와서 저장(마찬가지로 반복예정)
            HeroSelectionView view = viewPrefab.GetComponent<HeroSelectionView>();

            // 가져온 HeroSelectionView에서 초기화함수 실행(HeroData넘겨주고 UIText들 넘겨주고)
            view.Initialize(heroDataBundle, _uiManager.UIText);
            
            // 영웅 0번과 1번은 처음부터 잠금해제
            HeroUnlockManager.Instance.Unlock(0);
            HeroUnlockManager.Instance.Unlock(1);
        }
    }
}
