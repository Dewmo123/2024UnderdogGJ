using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent(out Player player))
            {
                GameManager.Instance.SetScore(25);
                player.GetCompo<PlayerHealth>().ChangeValue(25);
                CreateScorePoint.Instance.PosChange();
            }
        }
    }
}
