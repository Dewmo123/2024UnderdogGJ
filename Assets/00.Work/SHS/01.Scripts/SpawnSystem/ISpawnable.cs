using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ISpawnable
{
    public GameObject gameObject { get; }
    public void OnSpawn(Vector2 position);
}
