using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    public float maxHp;
    public int damage;
    public float moveSpeed;
    public float attackCool;
    public float attackRange;
    public Bullet bullet;


    private float maxHpData;
    private int damageData;
    private float moveSpeedData;
    private float attackCoolData;
    private float attackRangeData;
    private Bullet bulletData;

    public void Save()
    {
        maxHpData = maxHp;
        damageData = damage;
    }

    public void Load()
    {
        maxHp = maxHpData;
        damage = damageData;
    }

    public void IncreaseStats(int value)
    {
        damage += value;
    }

}

