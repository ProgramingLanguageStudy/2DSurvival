using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장갑(글러브) 장비. 주인공 최대 체력을 증가시키는 장비 클래스
/// </summary>
public class GloveGear : Gear
{
    protected override void Apply()
    {
        _heroModel.SetBonusMaxHp(_bonusValue);
    }
}