using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LineRenderer))]
public class DragJump : MonoBehaviour, IPlayerComponent
{
    private Player _player;

    [SerializeField] private Vector2 jumpPowerMultiplier = new Vector2(5, 5);
    [SerializeField] private float minPower = 1f;
    [SerializeField] private float maxPower = 10f;

    private Rigidbody2D target;

    private Coroutine draggCoroutine;
    private Vector2 startDragPos => target.position;

    [SerializeField] private JumpGuideLine jumpGuideLine;
    public bool isDrag => draggCoroutine != null;
    private LineRenderer dragLine;
    public void DragStart(Func<Vector2> mousePos)
    {
        // startDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragLine.enabled = true;
        draggCoroutine = StartCoroutine(Dragging(mousePos));
    }
    private IEnumerator Dragging(Func<Vector2> mousePos)
    {
        while (true)
        {
            (Vector2 dir, float distance, Vector2 power) = GetJumpInfo(mousePos());

            jumpGuideLine.DrawGuideLine(target, dir, power);
            DragGuideLine(dir, distance);

            yield return null;
        }
    }

    private void DragGuideLine(Vector3 dir, float distance)
    {
        dir = dir * -1;
        if (_player.isArrow)
            DrawArrow(transform.position + dir * Mathf.Clamp(distance, minPower, maxPower));
        else
        {
            dragLine.positionCount = 2;
            dragLine.SetPosition(0, transform.position);
            dragLine.SetPosition(1, transform.position + dir * Mathf.Clamp(distance, minPower, maxPower));
        }
    }
    public void DrawArrow(Vector3 pointer)
    {
        float arrowheadSize = 1;
        //pointer.y = transform.position.y;

        float percentSize = (float)(arrowheadSize / Vector3.Distance(transform.position, pointer));
        dragLine.positionCount = 4;
        dragLine.SetPosition(0, transform.position);
        dragLine.SetPosition(1, Vector3.Lerp(transform.position, pointer, 0.999f - percentSize));
        dragLine.SetPosition(2, Vector3.Lerp(transform.position, pointer, 1 - percentSize));
        dragLine.SetPosition(3, pointer);
        dragLine.widthCurve = new AnimationCurve(

        new Keyframe(0, 0.4f),
        new Keyframe(0.999f - percentSize, 0.4f),
        new Keyframe(1 - percentSize, 1f),
        new Keyframe(1 - percentSize, 1f),
        new Keyframe(1, 0f));
    }
    public void DragEnd(Func<Vector2> mousePos)
    {
        StopCoroutine(draggCoroutine);
        draggCoroutine = null;

        (Vector2 dir, float distance, Vector2 power) = GetJumpInfo(mousePos());

        Jump(dir, power);

        dragLine.enabled = false;
        jumpGuideLine.Disable();
    }
    public void DragStop(Func<Vector2> mousePos)
    {
        StopCoroutine(draggCoroutine);
        draggCoroutine = null;

        (Vector2 dir, float distance, Vector2 power) = GetJumpInfo(mousePos());

        dragLine.enabled = false;
        jumpGuideLine.Disable();
    }
    private void Jump(Vector2 dir, Vector2 power)
    {
        Vector2 forece = dir * power;
        target.AddForce(forece, ForceMode2D.Impulse);
    }
    public (Vector2, float, Vector2) GetJumpInfo(Vector2 mousePos)
    {
        Vector2 dir = (mousePos - target.position).normalized * -1;
        float distance = Vector2.Distance(mousePos, startDragPos);
        Vector2 power = Mathf.Clamp(distance, minPower, maxPower) * jumpPowerMultiplier;

        return (dir, distance, power);
    }

    public void Initialize(Player player)
    {
        _player = player;
        target = player.Rigid;
        dragLine = GetComponent<LineRenderer>();
    }
}
