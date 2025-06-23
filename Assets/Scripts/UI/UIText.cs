using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI에 적을 텍스트들을 미리 적어놓고 필요할 때마다 연결해서 사용할 SO
/// UIManager에 연결함.
/// </summary>
[CreateAssetMenu(fileName = "UIText", menuName = "UI/UIText")]
public class UIText : ScriptableObject
{
    [Header("----- 캐릭터 선택창 -----")]
    public string buyingText;
    public string afterBuyingText;
    public string clickLockedHero;
    public string failedBuyingText;
    public string successBuyingText;
}
