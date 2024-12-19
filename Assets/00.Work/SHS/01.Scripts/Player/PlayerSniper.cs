using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(LineRenderer))]
public class PlayerSniper : MonoBehaviour, IPlayerComponent
{
    [SerializeField] Transform firePoint;
    [SerializeField] float shootDistance = 10f;
    [SerializeField] LayerMask hitLayer;
    [SerializeField] AnimationCurve bulletLineFadeOutCurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 0));
    [SerializeField] float bulletLineFadeOutTime = 0.5f;
    private Player _player;
    private LineRenderer bulletLine;

    [SerializeField] private float _shootCool;
    [SerializeField] private float _curCool;

    public bool isCoolTime { get; private set; }
    public float Ratio => _curCool / _shootCool;

    public void Shoot(Vector2 dir)
    {
        if (isCoolTime) return;
        dir = dir.normalized;

        bulletLine.positionCount = 2;
        bulletLine.SetPosition(0, firePoint.position);

        Vector2 endPos = firePoint.position + (Vector3)(dir * shootDistance);

        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, dir, shootDistance, hitLayer);
        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent<IPlayerSniperHitable>(out var hitable))
            {
                hitable.OnHit();
            }

            endPos = hit.point;
        }
        bulletLine.SetPosition(1, endPos);
        StartCoroutine(CalcCoolTime());
        StartCoroutine(BulletLineFadeOut());
    }

    private IEnumerator CalcCoolTime()
    {
        isCoolTime = true;
        while (true)
        {
            _curCool += Time.deltaTime;
            if (_curCool >= _shootCool)
                break;
            yield return null;
        }
        isCoolTime = false;
        _curCool = 0;
    }

    private IEnumerator BulletLineFadeOut()
    {
        float time = 0f;
        while (true)
        {
            time += Time.deltaTime;
            float alpha = bulletLineFadeOutCurve.Evaluate(time);
            bulletLine.startColor = new Color(1, 1, 1, alpha);
            bulletLine.endColor = new Color(1, 1, 1, alpha);

            if (time >= bulletLineFadeOutTime)
            {
                break;
            }
            
            yield return null;
        }
    }

    public void Initialize(Player player)
    {
        _player = player;
        bulletLine = GetComponent<LineRenderer>();
    }
}
