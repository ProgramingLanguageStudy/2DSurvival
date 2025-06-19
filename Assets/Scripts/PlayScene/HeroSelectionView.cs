using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Intro���� ���ӽ��� ��ư�� ������ ������ ���� ����â UI
/// </summary>
public class HeroSelectionView : MonoBehaviour
{
    [Header("----- ������Ʈ ���� -----")]
    [SerializeField] Image _icon;                   // ���� ������
    [SerializeField] TextMeshProUGUI _nameText;     // ���� �̸� �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI _descText;     // ���� �ɷ�ġ �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI _skillNameText;// ���� ��ų�̸� �ؽ�Ʈ
    [SerializeField] Image _skillIcon;              // ���� ��ų ������

    HeroData _heroData;

    public void Initialize(HeroData heroData)
    {
        
    }

    
}
