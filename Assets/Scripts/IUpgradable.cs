using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 업그레이드 가능한 대상들의 공통 기능 인터페이스
/// </summary>
public interface IUpgradable
{
    /// <summary>
    /// 업그레이드를 실행하는 함수
    /// </summary>
    void Upgrade();
}
