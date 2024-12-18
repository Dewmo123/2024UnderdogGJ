using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField] private MapInfoSO _mapInfo;
    [SerializeField] private Tilemap _floorTile, _colliderTile;

    private void Awake()
    {
        _mapInfo.Initialize(_floorTile, _colliderTile);
    }
}
