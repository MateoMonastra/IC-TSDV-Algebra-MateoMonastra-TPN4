using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class BoundingBox : MonoBehaviour
{
    [SerializeField] private List<Vector3> vertices = new List<Vector3>();
    public struct MyBounds
    {
        private Vector3 m_Center;

        private Vector3 m_Extents;

        // Resumen:
        //     The center of the bounding box.
        public Vector3 center
        {
            get
            {
                return m_Center;
            }
           
            set
            {
                m_Center = value;
            }
        }

        // Resumen:
        //     The total size of the box. This is always twice as large as the extents.
        public Vector3 size
        {
            get
            {
                return m_Extents * 2f;
            }
            
            set
            {
                m_Extents = value * 0.5f;
            }
        }

        public MyBounds(Vector3 center, Vector3 size)
        {
            m_Center = center;
            m_Extents = size * 0.5f;
        }
    }

    private MyBounds boundingBox;

    private void Start()
    {
        SetVertices();
    }

    private void Update()
    {
        SetVertices();
    }
    void SetVertices()
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

    public List<Vector3> GetVertices()
    {
        return vertices;
    }

    MyBounds CalculateBoundingBox(List<Vector3> vertexList)
    {
        if (vertexList.Count == 0)
        {
            return new MyBounds(transform.position, Vector3.zero);
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

        return new MyBounds(center, size);
    }

    void DrawBoundingBox()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boundingBox.center, boundingBox.size);
    }
    void OnDrawGizmos()
    { 
        if (Application.isPlaying)
        {
            DrawBoundingBox();
        }
    }
}
