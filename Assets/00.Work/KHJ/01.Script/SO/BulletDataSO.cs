using UnityEngine;

[CreateAssetMenu(menuName = "SO/BulletData")]
public class BulletDataSO : ScriptableObject
{
    public float damage;
    public float moveSpeed;
    public bool canWellTouched;

    private float damageData;

    public void Save()
    {
        damageData = damage;
    }

    public void Load()
    {
        damage = damageData;
    }

    public void IncreaseStats(float value)
    {
        damage += value;
    }
}