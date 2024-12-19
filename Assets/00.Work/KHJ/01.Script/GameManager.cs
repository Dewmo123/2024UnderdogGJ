using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private EnemyDataListSO _enemyDataListSO;
    [SerializeField] private BulletDataListSO _bulletDataListSO;

    [SerializeField] private List<SpawnRegion> _spawnRegions;

    private float _currentTime;
    private float _maxTime = 9;

    private void Start()
    {
        SaveAllData();
    }

    private void Update()
    {
                _currentTime += Time.deltaTime;
        if (_maxTime <= _currentTime)
        {
            _spawnRegions[Random.Range(0, _spawnRegions.Count)].SpawnObject();
            ChangeStat(1.3f);
            _currentTime = 0;
        }
    }

    public void SaveAllData()
    {
        foreach (EnemyDataSO data in _enemyDataListSO.EnemyDataList)
        {
            data.Save();
        }
        foreach (BulletDataSO data in _bulletDataListSO.BulletDataList)
        {
            data.Save();
        }
    }

    public void ResetAllData()
    {
        foreach (EnemyDataSO data in _enemyDataListSO.EnemyDataList)
        {
            data.Load();
        }
        foreach (BulletDataSO data in _bulletDataListSO.BulletDataList)
        {
            data.Load();
        }
    }

    public void ChangeStat(float value)
    {
        foreach (EnemyDataSO data in _enemyDataListSO.EnemyDataList)
        {
            data.IncreaseStats(value);
        }
        foreach (BulletDataSO data in _bulletDataListSO.BulletDataList)
        {
            data.IncreaseStats(value);
        }
    }


    private void OnApplicationQuit()
    {
        ResetAllData();
    }

    private void OnDisable()
    {
        ResetAllData();
    }
}
