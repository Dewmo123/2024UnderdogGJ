using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected BulletDataSO _bulletData;
    [SerializeField] private float _speed;
    private void Start()
    {
        if (TryGetComponent(out Rigidbody2D rigid))
            rigid.velocity = transform.right * _bulletData.moveSpeed;
        StartCoroutine(DestroyBulletCoroutine());
    }

    private IEnumerator DestroyBulletCoroutine()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_bulletData.canWellTouched)
        {
            if (collision.transform.CompareTag("Well"))
            {
                Destroy(gameObject);
            }
        }

        if (collision.transform.CompareTag("Player"))
        {
            if (collision.TryGetComponent(out IHittable hit))
            {
                hit.OnHit(_bulletData.damage);
                Destroy(gameObject);
            }
        }
    }
}
