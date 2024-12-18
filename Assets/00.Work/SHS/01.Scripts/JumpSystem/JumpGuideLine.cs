using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class JumpGuideLine : MonoBehaviour
{
    private LineRenderer lineRendererCompo;
    [SerializeField] private float lineLength = 4f;
    [SerializeField] private int vertexCount = 40;
    [SerializeField] AnimationCurve widthCurve = new AnimationCurve(new Keyframe(0, 1f), new Keyframe(1,1f));
    [SerializeField] LayerMask groundLayer;
    void Awake()
    {
        lineRendererCompo = GetComponent<LineRenderer>();
        lineRendererCompo.positionCount = vertexCount;
        lineRendererCompo.widthCurve = widthCurve;
    }
    public void DrawGuideLine(Rigidbody2D target, Vector2 dir, float power)
    {
        dir = dir.normalized;
        Vector2 point = target.position;
        Vector2 gravity = target.gravityScale * Physics2D.gravity;
        Vector2 velocity = dir * power + target.velocity;

        lineRendererCompo.positionCount = vertexCount;


        float timeStep = lineLength / vertexCount * 0.2f;

        for (int i = 0; i < vertexCount; i++)
        {
            lineRendererCompo.SetPosition(i, point);

            velocity += gravity * timeStep;
            point += velocity * timeStep;

            RaycastHit2D hit = Physics2D.Raycast(point, velocity, velocity.magnitude * timeStep, groundLayer);
            if(hit.collider != null)
            {
                lineRendererCompo.positionCount = i + 2;
                lineRendererCompo.SetPosition(i + 1, hit.point);
                break;
            }
        }
    }
    [SerializeField] private float power = 10f;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = (Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized;
            Debug.Log(mousePos);
            DrawGuideLine(GetComponent<Rigidbody2D>(), mousePos, power);
        }
    }
}
