using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateablePlayerVIsual : MonoBehaviour, IPlayerComponent
{
    private Player _player;
    [SerializeField] private Transform upperBody;
    [SerializeField] private Transform upperBodyVisual;
    [SerializeField] float minAngle = 45f;
    [SerializeField] float maxAngle = 45f;
    [SerializeField] Vector2 offset;

    public void Initialize(Player player)
    {
        _player = player;
        gameObject.SetActive(false);

    }

    public void Rotate(float angle)
    {
        Debug.Log(angle);
        angle = Mathf.Clamp(angle, -minAngle, maxAngle);
        upperBody.localRotation = Quaternion.Euler(0, 0, angle);

        upperBody.localPosition = offset;
        upperBodyVisual.localPosition = -offset;
    }
    public void ReverseRotate(float angle)
    {
        angle = ReverseAngle(angle);
        Debug.Log(angle);

        angle = Mathf.Clamp(angle, -minAngle, maxAngle);
        upperBody.localRotation = Quaternion.Euler(0, 0, angle);

        upperBody.localPosition = offset;
        upperBodyVisual.localPosition = -offset;
    }

    private float ReverseAngle(float angle)
    {
        return angle > 0 ? 180 - angle : -180 - angle;
    }
#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(upperBodyVisual.position + (Vector3)offset, 0.05f);
    }
#endif
}
