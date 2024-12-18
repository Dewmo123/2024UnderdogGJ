using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateablePlayerVIsual : MonoBehaviour
{
    [SerializeField] private Transform upperBody;
    [SerializeField] private Transform upperBodyVisual;
    [SerializeField] float minAngle = 45f;
    [SerializeField] float maxAngle = 45f;
    [SerializeField] Vector2 offset;
    public void Rotate(float angle)
    {
        angle = Mathf.Clamp(angle, -maxAngle, minAngle);
        upperBody.localRotation = Quaternion.Euler(0, 0, angle);

        upperBody.localPosition = offset;
        upperBodyVisual.localPosition = -offset;
    }
#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(upperBodyVisual.position + (Vector3)offset, 0.05f);
    }
#endif
}
