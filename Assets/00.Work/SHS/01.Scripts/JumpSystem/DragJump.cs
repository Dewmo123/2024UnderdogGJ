using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LineRenderer))]
public class DragJump : MonoBehaviour
{
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private float minPower = 1f;
    [SerializeField] private float maxPower = 10f;
    [SerializeField] Rigidbody2D target;

    private Coroutine draggCoroutine;
    private Vector2 startDragPos => target.position;

    [SerializeField] private JumpGuideLine jumpGuideLine;
    private LineRenderer dragLine;
    void Awake()
    {
        dragLine = GetComponent<LineRenderer>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DragStart(() => Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        if (Input.GetMouseButtonUp(0))
        {
            DragEnd(() => Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private void DragStart(Func<Vector2> mousePos)
    {
        // startDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        draggCoroutine = StartCoroutine(Dragging(mousePos));
    }
    private IEnumerator Dragging(Func<Vector2> mousePos)
    {
        while (true)
        {
            (Vector2 dir, float distance, float power) = GetJumpInfo(mousePos());

            jumpGuideLine.DrawGuideLine(target, dir, power);
            DragGuideLine(dir, distance);

            yield return null;
        }
    }

    private void DragGuideLine(Vector2 dir, float distance)
    {
        dir = dir * -1;
        dragLine.positionCount = 2;
        dragLine.SetPosition(0, target.position);
        dragLine.SetPosition(1, target.position + dir * distance);
    }

    private void DragEnd(Func<Vector2> mousePos)
    {
        StopCoroutine(draggCoroutine);

        (Vector2 dir, float distance, float power) = GetJumpInfo(mousePos());

        Jump(dir, power);
    }
    private void Jump(Vector2 dir, float power)
    {
        Vector2 forece = dir * power;
        target.AddForce(forece, ForceMode2D.Impulse);
    }
    public (Vector2, float, float) GetJumpInfo(Vector2 mousePos)
    {
        Vector2 dir = (mousePos - target.position).normalized * -1;
        float distance = Vector2.Distance(mousePos, startDragPos);
        float power = Mathf.Clamp(distance, minPower, maxPower) * jumpPower;

        return (dir, distance, power);
    }
}
