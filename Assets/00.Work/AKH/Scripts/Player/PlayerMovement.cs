using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerComponent
{
    private Player _player;
    private Rigidbody2D _rb;
    public void Initialize(Player player)
    {
        _player = player;
        _rb = _player.Rigid;
    }
    public void SetMovement(Vector2 moveVec)
    {
        _rb.velocity = moveVec;
    }
    public bool IsFacingRight()
    {
        return Mathf.Approximately(transform.eulerAngles.y, 0);
    }
    public void HandleSpriteFlip(Vector3 targetPosition)
    {
        bool isRight = IsFacingRight();
        if (targetPosition.x < transform.position.x && isRight)
        {
            transform.eulerAngles = new Vector3(0, -180F, 0);
        }
        else if (targetPosition.x > transform.position.x && !isRight)
        {
            transform.eulerAngles = Vector3.zero;
        }

    }
}
