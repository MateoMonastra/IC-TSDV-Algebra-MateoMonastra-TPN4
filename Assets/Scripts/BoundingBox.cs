using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundig : MonoBehaviour
{
    private List<Vector3> vertices = new List<Vector3>();
    private Bounds boundingBox;

    private void Start()
    {
        GetVertices();
        DrawBoundingBox();
    }

    void GetVertices()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        if (meshFilter != null)
        {
            meshFilter.mesh.GetVertices(vertices);

            for (int i = 0; i < vertices.Count; i++)
            {
                vertices[i] = transform.TransformPoint(vertices[i]);
            }

            boundingBox = CalculateBoundingBox(vertices);
        }
    }
    Bounds CalculateBoundingBox(List<Vector3> vertexList)
    {
        if (vertexList.Count == 0)
        {
            return new Bounds(transform.position, Vector3.zero);
        }

        Vector3 min = vertexList[0];
        Vector3 max = vertexList[0];

        for (int i = 1; i < vertexList.Count; i++)
        {
            min = Vector3.Min(min, vertexList[i]);
            max = Vector3.Max(max, vertexList[i]);
        }

        Vector3 size = max - min;
        Vector3 center = (max + min) / 2;

        return new Bounds(center, size);
    }

    void DrawBoundingBox()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boundingBox.center, boundingBox.size);
    }
    private void OnDrawGizmos()
    { 
        DrawBoundingBox();
    }
}
