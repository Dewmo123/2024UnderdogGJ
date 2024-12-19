using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    public float maxHp = 1;
    public int damage;
    public float moveSpeed;
    public float attackCool;
    public float attackRange;
    public Bullet bullet;
    public int score;

    private int damageData;
    private int scoreData;

    public void Save()
    {
        damageData = damage;
        scoreData = score;
    }

    public void Load()
    {
        damage = damageData;
        score = scoreData;
    }

    public void IncreaseStats(int value)
    {
        damage += value;
        score += value / 2;
    }

}

