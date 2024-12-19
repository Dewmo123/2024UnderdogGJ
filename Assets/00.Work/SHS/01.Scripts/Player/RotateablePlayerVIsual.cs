using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateablePlayerVIsual : MonoBehaviour, IPlayerComponent
{
    private Player _player;
    public Animator Anim;
    [SerializeField] private Transform upperBody;
    [SerializeField] private Transform upperBodyVisual;
    [SerializeField] float minAngle = 45f;
    public float Angle { get; private set; }
    public float ReversedAngle => Mathf.Abs(Angle) > 90f ? 369 - ReverseAngle(Angle) : Angle;
    [SerializeField] float maxAngle = 45f;
    [field: SerializeField] public Vector2 offset { get; private set; }
    [SerializeField] float angleOffset;

    public void Initialize(Player player)
    {
        _player = player;
        gameObject.SetActive(false);
        Anim = GetComponent<Animator>();
    }

    public void Rotate(float angle)
    {
        angle = Mathf.Clamp(angle, -minAngle, maxAngle);
        angle += angleOffset;
        upperBody.localRotation = Quaternion.Euler(0, 0, angle);

        upperBody.localPosition = offset;
        upperBodyVisual.localPosition = -offset;

        Angle = angle;
    }
    public void ReverseRotate(float angle)
    {
        angle = ReverseAngle(angle);
        angle = Mathf.Clamp(angle, -minAngle, maxAngle);
        angle += angleOffset;

        upperBody.localRotation = Quaternion.Euler(0, 0, angle);

        upperBody.localPosition = offset;
        upperBodyVisual.localPosition = -offset;

        Angle = ReverseAngle(angle);
    }

    private float ReverseAngle(float angle)
    {
        return angle > 0 ? 180 - angle : -180 - angle;
    }
    public void EndTriggerCalled()
    {
        _player.GetCompo<PlayerAnimator>().EndTriggerCalled();
    }
    public void Attack()
    {
        _player.GetCompo<PlayerAnimator>().Attack();
    }
#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(upperBodyVisual.position + (Vector3)offset, 0.05f);
    }
#endif
}
