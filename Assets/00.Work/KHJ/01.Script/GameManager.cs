using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private EnemyDataListSO _enemyDataListSO;
    [SerializeField] private BulletDataListSO _bulletDataListSO;

    [SerializeField] private List<SpawnRegion> _spawnRegions;

    private float _currentTime;
    private float _maxTime = 10;

    private int _score;

    private void Start()
    {
        SaveAllData();
        for (int i = 0; 3 < i; i++)
        {
            _spawnRegions[Random.Range(0, _spawnRegions.Count)].SpawnObject();
        }
    }


    public void SetScore(int value)
    {
        _score += value;
        print($"현재 스코어 : {_score}점");
        // 스코어 UI 변경
    }




    
    ////////////////////////////////////////// 적 웨이브
    private void Update()
    {
                _currentTime += Time.deltaTime;
        if (_maxTime <= _currentTime)
        {
            _spawnRegions[Random.Range(0, _spawnRegions.Count)].SpawnObject();
            ChangeStat(1);
            _currentTime = 0;
            _maxTime -= 0.05f;
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

    public void ChangeStat(int value)
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
