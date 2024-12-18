using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    public float maxHp;
    public float damage;
    public float moveSpeed;
    public float attackCool;
    public float attackRange;
    public Bullet bullet;
}


