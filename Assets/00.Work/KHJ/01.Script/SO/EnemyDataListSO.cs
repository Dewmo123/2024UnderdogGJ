using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemyDataList")]
public class EnemyDataListSO : ScriptableObject
{
    public List<EnemyDataSO> EnemyDataList = new List<EnemyDataSO>();
}