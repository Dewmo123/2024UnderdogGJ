using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(LineRenderer))]
public class PlayerSniper : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] float shootDistance = 10f;
    [SerializeField] LayerMask hitLayer;
    [SerializeField] AnimationCurve bulletLineFadeOutCurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 0));
    [SerializeField] float bulletLineFadeOutTime = 0.5f;
    private LineRenderer bulletLine;
    void Awake()
    {
        bulletLine = GetComponent<LineRenderer>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 dir = mousePos - firePoint.position;
            Shoot(dir.normalized);
        }
    }
    public void Shoot(Vector2 dir)
    {
        dir = dir.normalized;

        bulletLine.positionCount = 2;
        bulletLine.SetPosition(0, firePoint.position);

        Vector2 endPos = firePoint.position + (Vector3)(dir * shootDistance);

        Debug.Log($"{firePoint.position} {endPos} {dir} {shootDistance}");

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

        StartCoroutine(BulletLineFadeOut());
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
}
