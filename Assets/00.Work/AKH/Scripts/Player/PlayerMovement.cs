using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerComponent
{
    private Player _player;
    private Rigidbody2D _rb;
    [SerializeField] private Transform _groundChker;
    [SerializeField] private Transform _wallChker;
    [SerializeField] private Vector2 _groundCheckerSize;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _wallLayer;
    public bool isGround { get; private set; }
    public bool isWall { get; private set; }
    public void Initialize(Player player)
    {
        _player = player;
        _rb = _player.Rigid;
    }
    public void SetMovement(Vector2 moveVec)
    {
        _rb.velocity = moveVec;
    }
    private void Update()
    {
        CheckGround();
        CheckWall();
    }
    private void CheckGround()
    {
        isGround = Physics2D.OverlapBox(_groundChker.position, _groundCheckerSize,0, _groundLayer);
    }
    private void CheckWall()
    {
        isWall = Physics2D.Raycast(_wallChker.position, IsFacingRight() ? Vector3.right : Vector3.left, 0.3f, _wallLayer);
    }
    #region
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
    #endregion
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_groundChker.position, _groundCheckerSize);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Ray(_wallChker.position, IsFacingRight() ? Vector3.right : Vector3.left));
    }
#endif
}
