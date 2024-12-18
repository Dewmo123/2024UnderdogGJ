using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnTable", menuName = "SpawnTable")]
public class SpawnTableSO : ScriptableObject
{
    public List<PieChartData<GameObject>> spawnables = new();
    public ISpawnable GetRandomSpawnable()
    {
        float randomValue = Random.Range(0, 100);
        foreach (var spawnable in spawnables)
        {
            if (randomValue < spawnable.percentage)
            {
                ISpawnable spawnableComponent = spawnable.Value.GetComponent<ISpawnable>();
                return spawnableComponent;
            }
        }
        Debug.Log("SpawneTableSO : No spawnable found");
        return null;
    }

    private void OnValidate()
    {
        for (int i = 0; i < spawnables.Count; i++)
        {
            if (spawnables[i].Value == null)
            {
                Debug.Log("SpawnTableSO : spawnable " + i + " is null");
                continue;
            }
            if (!spawnables[i].Value.TryGetComponent(out ISpawnable spawnable))
            {
                Debug.LogError("SpawnTableSO : spawnable " + i + " does not have ISpawnable component");
                spawnables[i].Value = null;
            }
        }
    }
}