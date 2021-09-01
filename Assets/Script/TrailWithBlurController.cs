//using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TrailRenderer))]
public class TrailWithBlurController : MonoBehaviour
{
    [SerializeField]
    private bool isInit = false;

    public MeshFilter meshFilter;
    public Transform pivotTrans;
    public AnimationCurve mixCurve;
    public bool invertAxis = false;

    private TrailRenderer _trailRenderer;

    private Vector3 lastPos;
    private int lastVertexCount;
    private Vector3 dir;
    private Vector3[] vertices;
    private List<UnityEngine.Color> lastColors = new List<Color>();

    private void OnEnable()
    {
        if (meshFilter == null) return;
        if (pivotTrans == null) return;
        isInit = true;
        lastColors = new List<Color>();
        _trailRenderer = GetComponent<TrailRenderer>();
        meshFilter.sharedMesh = new Mesh();
        lastPos = pivotTrans.position;
        lastVertexCount = meshFilter.sharedMesh.vertexCount;
        meshFilter.sharedMesh.GetColors(lastColors);
    }

    private void OnDisable()
    {
        lastColors = null;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!isInit) return;
        _trailRenderer.BakeMesh(meshFilter.sharedMesh);

        var colors = new List<Color>();
        meshFilter.sharedMesh.GetColors(colors);

        dir = (pivotTrans.position - lastPos).normalized;
        if (invertAxis)
        {
            var tmp = dir.y;
            dir.y = dir.x;
            dir.x = tmp;
        }

        dir += Vector3.one;

        vertices = meshFilter.sharedMesh.vertices;
        var deltaCount = vertices.Length - lastVertexCount;

        if (deltaCount > 0)
        {
            if (lastColors.Count == 0) lastColors = colors;
            for (var i = colors.Count - 1; i > 0; i--)
            {
                if (i - (deltaCount) < 0) break;
                colors[i] = lastColors[i - (deltaCount)];
            }
            for (var i = 0; i < deltaCount; i++)
            {
                var ratio = (float)i / vertices.Length;
                var multiplier = mixCurve.Evaluate(ratio);
                var color = new Color(.5f * dir.x, .5f * dir.y, 1f, 1f);
                color = Color.Lerp(colors[i], color, multiplier);
                colors[i] = color;
            }
        }
        else
        {
            for (var i = 0; i < colors.Count; i++)
            {
                colors[i] = lastColors[i];
            }
        }
        lastVertexCount = meshFilter.sharedMesh.vertexCount;
        lastPos = pivotTrans.position;
        lastColors = colors;
        meshFilter.sharedMesh.SetColors(colors);
    }
}