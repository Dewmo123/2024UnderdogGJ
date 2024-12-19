using UnityEngine;

[CreateAssetMenu(menuName = "SO/BulletData")]
public class BulletDataSO : ScriptableObject
{
    public int damage;
    public float moveSpeed;
    public bool canWellTouched;

    private int damageData;

    public void Save()
    {
        damageData = damage;
    }

    public void Load()
    {
        damage = damageData;
    }

    public void IncreaseStats(int value)
    {
        damage += value;
    }
}