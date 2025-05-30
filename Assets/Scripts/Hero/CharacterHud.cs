using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 캐릭터의 HUD UI를 담당하는 역할
/// </summary>
public class CharacterHud : MonoBehaviour
{
    [SerializeField] Image _hpbar;
    
    public void SetHpBar(float currentHp, float maxHp)
    {
        _hpbar.fillAmount = currentHp / maxHp;
    }
}
