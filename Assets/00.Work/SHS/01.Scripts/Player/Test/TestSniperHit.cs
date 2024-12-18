using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSniperHit : MonoBehaviour, IPlayerSniperHitable
{
    public void OnHit()
    {
        Debug.Log("Hit");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
