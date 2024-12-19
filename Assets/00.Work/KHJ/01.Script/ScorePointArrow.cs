using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePointArrow : MonoBehaviour
{
    
    private void Update()
    {
        Vector3 direction = CreateScorePoint.Instance.Score.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle + 90);
    }
}
