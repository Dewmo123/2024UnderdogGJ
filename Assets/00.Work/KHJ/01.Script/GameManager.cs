using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private EnemyDataListSO _enemyDataListSO;
    [SerializeField] private BulletDataListSO _bulletDataListSO;

    [SerializeField] private List<SpawnRegion> _spawnRegions;

    [SerializeField] private TextMeshProUGUI _scoreTxt;

    private int _spawnCount = 0;

    private float _currentTime;
    private float _maxTime = 10;


    public int score { get; private set; }
    public int killedCount { get; private set; }

    private void Start()
    {
        SaveAllData();
        for (int i = 0; 3 < i; i++)
        {
            _spawnRegions[Random.Range(0, _spawnRegions.Count)].SpawnObject();
        }
    }

    public void KillEnemy(int value)
    {
        score += value;
        killedCount++;
    }


    public void SetScore(int value)
    {
        score += value;
    }




    
    ////////////////////////////////////////// 적 웨이브
    private void Update()
    {
         _currentTime += Time.deltaTime;
        if (_maxTime <= _currentTime)
        {
            _spawnRegions[Random.Range(0, _spawnRegions.Count)].SpawnObject();
            
            _currentTime = 0;
            _maxTime -= 0.1f;
            _spawnCount++;
            if (_spawnCount % 3 == 0) ChangeStat(1); 
        }
        _scoreTxt.text = "Score: \n" + score;
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
