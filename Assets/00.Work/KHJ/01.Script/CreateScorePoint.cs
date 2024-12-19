using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreateScorePoint : MonoSingleton<CreateScorePoint>
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Score _score;

    public Transform Score { get; private set; }

    private void Start()
    {
        SpawnScore();
    }

    public void SpawnScore()
    {
        Score = Instantiate(_score.transform, _spawnPoints[Random.Range(0, 
            _spawnPoints.Count)].position ,Quaternion.identity, transform);
    }

    public void PosChange()
    {
        Score.position = _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;
    }

}
