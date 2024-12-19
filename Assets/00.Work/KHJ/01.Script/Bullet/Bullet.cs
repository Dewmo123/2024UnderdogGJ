using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletDataSO _bulletData;
    [SerializeField] private float _speed;
    private void Start()
    {
        if (TryGetComponent(out Rigidbody2D rigid))
            rigid.velocity = transform.right * _bulletData.moveSpeed;
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
            Destroy(gameObject); // 데미지 코드 추가해야함
        }
    }
}
