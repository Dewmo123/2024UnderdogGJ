using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IPlayerComponent
{
    private Player _player;
    public void Initialize(Player player)
    {
        _player = player;
    }
    public void Aim()
    {
        transform.right = (_player.GetCompo<InputReader>().MouseWorldPos - (Vector2)transform.position).normalized;
    }
    public void Attack()
    {

    }
}
