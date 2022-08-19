using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackDataStorer", menuName = "Data/Storage/AttackDataStorer", order = 1)]
public class AttackStorer : ScriptableObject
{
    [SerializeField]
    List<AttackData> _attackDatas = new List<AttackData>();

    public AttackData GetAttackData(string attackName)
    {
        return _attackDatas.Find(x=> x.name == attackName);
    }
}
