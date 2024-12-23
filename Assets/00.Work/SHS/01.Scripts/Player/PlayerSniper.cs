using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(LineRenderer))]
public class PlayerSniper : MonoBehaviour, IPlayerComponent
{
    [SerializeField] Transform firePoint;
    [SerializeField] float shootDistance = 10f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] AnimationCurve bulletLineFadeOutCurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 0));
    [SerializeField] float bulletLineFadeOutTime = 0.5f;
    private Player _player;
    private LineRenderer bulletLine;

    [SerializeField] private float _shootCool;
    [SerializeField] private float _curCool;

    public bool isCoolTime { get; private set; }
    public float Ratio => _curCool / _shootCool;

    [SerializeField] UnityEvent onShoot;
    [SerializeField] UnityEvent onCoolTime;
    public void Shoot(Vector2 dir)
    {
        if (isCoolTime)
        {
            onCoolTime.Invoke();
            return;
        }
        dir = dir.normalized;

        bulletLine.positionCount = 2;
        bulletLine.SetPosition(0, firePoint.position);

        Vector2 endPos = firePoint.position + (Vector3)(dir * shootDistance);

        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, dir, shootDistance, groundLayer);
        if (hit.collider != null)
        {
            endPos = hit.point;
        }
        bulletLine.SetPosition(1, endPos);
        dir = (Vector3)endPos - _player.GetCompo<RotateablePlayerVIsual>().transform.position;
        RaycastHit2D[] ray = Physics2D.RaycastAll(firePoint.position, dir, shootDistance, enemyLayer);
        foreach (var item in ray)
        {
            Debug.Log("asd");
            if (item.collider.TryGetComponent<IHittable>(out var hitable))
                hitable.OnHit(1);
        }

        StartCoroutine(CalcCoolTime());
        StartCoroutine(BulletLineFadeOut());

        onShoot.Invoke();
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
