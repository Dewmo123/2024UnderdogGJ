using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class SpawnRegion : MonoBehaviour
{
    private PolygonCollider2D regionCollider;
    [SerializeField] private SpawnTableSO spawnTable;
    private float _maxTime = 3;
    private float _curTime;
    private int _spawnCount = 3;
    void Awake()
    {
        regionCollider = GetComponent<PolygonCollider2D>();
    }
    
    [ContextMenu("Spawn Object")]
    public GameObject SpawnObject()
    {
        ISpawnable spawnable = spawnTable.GetRandomSpawnable();
        Vector2 spawnPosition = GetRandomPointInRegion();

        GameObject gameObject =  Instantiate(spawnable.gameObject, spawnPosition, Quaternion.identity);
        gameObject.TryGetComponent(out ISpawnable spawnableComponent);
        spawnableComponent.OnSpawn(spawnPosition);

        return gameObject;
    }

    public bool CheckRegion(Vector2 position)
    {
        return regionCollider.OverlapPoint(position);
    }
    private Vector2 GetRandomPointInRegion()
    {
        Bounds bounds = regionCollider.bounds;
        Vector2 randomPoint;
        do
        {
            randomPoint = new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y)
            );
        } while (!regionCollider.OverlapPoint(randomPoint));
        return randomPoint;
    }
}
