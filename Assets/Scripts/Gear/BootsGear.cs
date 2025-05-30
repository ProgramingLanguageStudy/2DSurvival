using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 신발(부츠) 장비. 주인공 이동 속력을 증가시키는 장비 클래스
/// </summary>
public class BootsGear : Gear
{
    protected override void Apply()
    {
        _heroModel.SetBonusSpeedRation(_bonusValue);
    }
}
