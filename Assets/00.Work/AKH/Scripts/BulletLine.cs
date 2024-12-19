using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLine : MonoBehaviour, IPlayerComponent
{
    private LineRenderer bulletLine;
    [SerializeField] private float shootDistance;
    [SerializeField] private LayerMask hitLayer;
    private Transform firePoint;
    private Player _player;
    private void Awake()
    {
        bulletLine = GetComponent<LineRenderer>();
    }
    public void DisableLine()
    {
        bulletLine.enabled = false;
    }
    public void EnableLine()
    {
        bulletLine.enabled = true;
    }
    public void DrawLine(Vector2 dir)
    {
        dir = dir.normalized;

        bulletLine.positionCount = 2;
        bulletLine.SetPosition(0, firePoint.position);

        Vector2 endPos = firePoint.position + (Vector3)(dir * shootDistance);

        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, dir, shootDistance, hitLayer);
        if (hit.collider != null)
        {
            endPos = hit.point;
        }
        bulletLine.SetPosition(1, endPos);
    }

    public void Initialize(Player player)
    {
        _player = player;
        firePoint = _player.FirePoint;
    }
}
