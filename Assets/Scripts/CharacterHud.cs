using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 1. 주인공 캐릭터를 따라다니는 월드 UI 체력바를 만들어 주세요.
// 2. HpBar의 fillAmount 값을 조절하는 체력바 기능을 만들어 주세요.
// 3. Hero의 부품으로 CharacterHud를 추가해서 HeroModel의 Hp 이벤트를 활용해
//    체력이 변경될 때마다 체력바 UI도 갱신되게 해 주세요.

public class CharacterHud : MonoBehaviour
{
    [SerializeField] Image _image;
    
    public void Initialize(HeroModel model)
    {
        model.OnHpChanged += OnHpChanged;
    }

    public void OnHpChanged(float currentHp, float maxHp)
    {
        _image.fillAmount = currentHp / maxHp;
    }
}
