using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveSkill", menuName = "Hero/PassiveSkill")]
public class PassiveSkill : ScriptableObject, IPassiveSkill
{
    [SerializeField]
    public void Activate(GameObject owner)
    {
        throw new System.NotImplementedException();
    }
}
