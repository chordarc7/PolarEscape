using System;
using System.Collections.Generic;
using UnityEngine;

public class RopeVerlet : MonoBehaviour, IUpdatable, IFixedUpdatable
{
    [SerializeField] private Transform anchor;
    [SerializeField] private Transform attached;
    
    [Header("Rope")]
    [SerializeField] private int segmentsCount = 20;
    [SerializeField] private float segmentLength = 0.225f;
    
    [Header("Physics")]
    [SerializeField] private Vector2 gravity = new(0f, -2f);
    [SerializeField] private float damping = 0.98f;
    
    [Header("Constraints")]
    [SerializeField] private int constraintCount = 50; // balance between performance and quality
    
    private LineRenderer _lineRenderer;
    private List<RopeSegment> _segments = new();
    
    public struct RopeSegment
    {
        public Vector2 CurrentPos;
        public Vector2 OldPos;

        public RopeSegment(Vector2 pos)
        {
            CurrentPos = pos;
            OldPos = pos;
        }
    }
    
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = segmentsCount;
        
        CreateSegments();
    }

    private void CreateSegments()
    {
        var pos = (Vector2)anchor.position;
        for (var i = 0; i < segmentsCount; i++)
        {
            _segments.Add(new RopeSegment(pos));
            pos.y -= segmentLength;
        }
    }

    private void OnEnable()
    {
        UpdateManager.Register((IUpdatable)this);
        UpdateManager.Register((IFixedUpdatable)this);
    }

    private void OnDisable()
    {
        UpdateManager.Unregister((IUpdatable)this);
        UpdateManager.Unregister((IFixedUpdatable)this);
    }

    public void OnUpdate(float deltaTime)
    {
        DrawRope();
    }

    public void OnFixedUpdate(float deltaTime)
    {
        Simulate(deltaTime);
        
        for (var i = 0; i < constraintCount; i++) ApplyConstraints();
    }

    private void DrawRope()
    {
        var ropePositions = new Vector3[segmentsCount];
        for (var i = 0; i < segmentsCount; i++) ropePositions[i] = _segments[i].CurrentPos;
        _lineRenderer.SetPositions(ropePositions);
    }

    private void Simulate(float deltaTime)
    {
        for (var i = 0; i < _segments.Count; i++)
        {
            var seg = _segments[i];
            var velocity = (seg.CurrentPos - seg.OldPos) * damping;

            seg.OldPos = seg.CurrentPos;
            seg.CurrentPos += velocity;
            seg.CurrentPos += gravity * deltaTime;
            _segments[i] = seg;
        }
    }

    private void ApplyConstraints()
    {
        // rope is anchored to anchor
        var firstSeg = _segments[0];
        firstSeg.CurrentPos = anchor.position;
        _segments[0] = firstSeg;
        
        for (var i = 0; i < segmentsCount - 1; i++)
        {
            var currentSeg = _segments[i];
            var nextSeg = _segments[i + 1];
            
            var dist = (currentSeg.CurrentPos - nextSeg.CurrentPos).magnitude;
            var diff = dist - segmentLength;
            
            var changeDir = (currentSeg.CurrentPos - nextSeg.CurrentPos).normalized;
            var changeVector = changeDir * diff;

            if (i != 0)
            {
                currentSeg.CurrentPos -= changeVector * 0.5f;
                nextSeg.CurrentPos += changeVector * 0.5f;
            }
            else
            {
                nextSeg.CurrentPos += changeVector;
            }
            
            _segments[i] = currentSeg;
            _segments[i + 1] = nextSeg;
        }
        
        // attached object is attached to the rope
        if (attached) attached.position = _segments[^1].CurrentPos;
    }
}
