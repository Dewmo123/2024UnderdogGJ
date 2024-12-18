using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    [SerializeField] PlayerSniper sniper;
    [SerializeField] RotateablePlayerVIsual visual;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 dir = mousePos - transform.position;

            float rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            visual.Rotate(rotation);
            
            sniper.Shoot(dir.normalized);

        }
    }
}
