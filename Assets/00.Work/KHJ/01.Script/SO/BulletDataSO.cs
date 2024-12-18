using UnityEngine;

[CreateAssetMenu(menuName = "SO/BulletData")]
public class BulletDataSO : ScriptableObject
{
    public float damage;
    public float moveSpeed;
    public bool canWellTouched;
}