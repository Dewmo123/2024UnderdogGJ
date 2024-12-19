using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Bullet
{
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _range;


    


    public void PlayBomb()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, _range, _targetLayer);
        if (targets != null)
        {
            foreach (Collider2D target in targets)
            {
                if (target.TryGetComponent(out IHittable item))
                    item.OnHit(_bulletData.damage);
            }
        }
    }

    public void EndAnimation()
    {
        Destroy(gameObject);
    }
}
