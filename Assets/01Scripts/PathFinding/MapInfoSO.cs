using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "SO/PathFinding/MapInfo")]
public class MapInfoSO : ScriptableObject
{
    private Tilemap _floor, _collider;
    private List<Vector3Int> _directions;

    public void Initialize(Tilemap floorTile, Tilemap colliderTile)
    {
        _floor = floorTile;
        _collider = colliderTile;
        _directions = new List<Vector3Int>(8); //8방향 입력
        for(int i = -1; i <= 1; i++)
            for(int j = -1; j <=1; j++)
            {
                if (i == 0 && j == 0) continue; 
                _directions.Add(new Vector3Int(j, i));
            }
    }

    public Vector3 GetRandomOneTile(Vector3 from)
    {
        Vector3Int fromTilePos = GetTilePosFromWorldPos(from);
        for(int i = _directions.Count -1; i >= 0; i--)
        {
            int idx = Random.Range(0, i + 1);
            Vector3Int nextPos = fromTilePos + _directions[idx];
            if (CanMove(nextPos))
            {
                return GetCellCenterToWorld(nextPos); //월드좌표로 변경해서 준다.
            }

            (_directions[idx], _directions[i]) = (_directions[i], _directions[idx]);
        }

        return from;
    }

    public bool CanMove(Vector3Int nextPos)
    {
        //이동하고자 하는 타일에 타일이 존재하고 그리고 충돌타일에는 타일 없고
        return _floor.GetTile(nextPos) != null && _collider.GetTile(nextPos) == null;
    }

    public Vector3Int GetTilePosFromWorldPos(Vector3 from) => _floor.WorldToCell(from);
    public Vector3 GetCellCenterToWorld(Vector3Int cellCenter) 
                    => _floor.GetCellCenterWorld(cellCenter);
}
