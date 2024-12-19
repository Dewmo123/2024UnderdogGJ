using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/BulletDataList")]
public class BulletDataListSO : ScriptableObject
{
    public List<BulletDataSO> BulletDataList = new List<BulletDataSO>();
}
