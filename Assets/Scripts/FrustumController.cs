using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrustumController : MonoBehaviour
{

    [SerializeField] int screenWidth;
    [SerializeField] int screenHeight;

    [SerializeField] float fieldOfViewAngle;
    public float verticalfieldOfViewAngle;
    [SerializeField] float nearClippingPlane;
    [SerializeField] float renderingDistance;

    [SerializeField] float aspectRatio;
    // Start is called before the first frame update
    public Vector3[] frustumNormals;


    Vector3 farLimit;
    Vector3 nearLimit;

    // Frustum
    // DL "DownLeft" DR "DownRight"
    // UL "UpperLeft" UR "UpperRight"

    Vector3 nearUpperLeftVertex;
    Vector3 nearUpperRightVertex;
    Vector3 nearLowerLeftVertex;
    Vector3 nearLowerRightVertex;

    Vector3 farUpperLeftVertex;
    Vector3 farUpperRightVertex;
    Vector3 farLowerLeftVertex;
    Vector3 farLowerRightVertex;

    public List<Vector3> vertices = new List<Vector3>();

    void Start()
    {
        SetVertices();
        frustumNormals = new Vector3[6];
    }

    void Update()
    {
        UpdateVertices();

        aspectRatio = (float)screenWidth / (float)screenHeight;

        verticalfieldOfViewAngle = fieldOfViewAngle / aspectRatio;

        farLimit = transform.position + transform.forward * renderingDistance;
        nearLimit = transform.position + transform.forward * nearClippingPlane;

        nearUpperLeftVertex = new Vector3(Mathf.Tan((-fieldOfViewAngle / 2) * Mathf.Deg2Rad) * nearClippingPlane, Mathf.Tan((verticalfieldOfViewAngle / 2) * Mathf.Deg2Rad) * nearClippingPlane, nearLimit.z);
        nearUpperRightVertex = new Vector3(Mathf.Tan((fieldOfViewAngle / 2) * Mathf.Deg2Rad) * nearClippingPlane, Mathf.Tan((verticalfieldOfViewAngle / 2) * Mathf.Deg2Rad) * nearClippingPlane, nearLimit.z);
        nearLowerLeftVertex = new Vector3(Mathf.Tan((-fieldOfViewAngle / 2) * Mathf.Deg2Rad) * nearClippingPlane, Mathf.Tan((-verticalfieldOfViewAngle / 2) * Mathf.Deg2Rad) * nearClippingPlane, nearLimit.z);
        nearLowerRightVertex = new Vector3(Mathf.Tan((fieldOfViewAngle / 2) * Mathf.Deg2Rad) * nearClippingPlane, Mathf.Tan((-verticalfieldOfViewAngle / 2) * Mathf.Deg2Rad) * nearClippingPlane, nearLimit.z);

        farUpperLeftVertex = new Vector3(Mathf.Tan((-fieldOfViewAngle / 2) * Mathf.Deg2Rad) * renderingDistance, Mathf.Tan((verticalfieldOfViewAngle / 2) * Mathf.Deg2Rad) * renderingDistance, farLimit.z);
        farUpperRightVertex = new Vector3(Mathf.Tan((fieldOfViewAngle / 2) * Mathf.Deg2Rad) * renderingDistance, Mathf.Tan((verticalfieldOfViewAngle / 2) * Mathf.Deg2Rad) * renderingDistance, farLimit.z);
        farLowerLeftVertex = new Vector3(Mathf.Tan((-fieldOfViewAngle / 2) * Mathf.Deg2Rad) * renderingDistance, Mathf.Tan((-verticalfieldOfViewAngle / 2) * Mathf.Deg2Rad) * renderingDistance, farLimit.z);
        farLowerRightVertex = new Vector3(Mathf.Tan((fieldOfViewAngle / 2) * Mathf.Deg2Rad) * renderingDistance, Mathf.Tan((-verticalfieldOfViewAngle / 2) * Mathf.Deg2Rad) * renderingDistance, farLimit.z);

        int index = 0;
        for (int i = 0; i <= vertices.Count - 3; i += 3)
        {
            frustumNormals[index] = GetFaceNormal(i);
            index++;
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.DrawCube(farLimit, new Vector3(0.3f, 0.3f, 0.3f));
            Gizmos.DrawCube(nearLimit, new Vector3(0.3f, 0.3f, 0.3f));

            DrawFrustumLines();

            for (int i = 0; i < vertices.Count - 3; i += 3)
            {
                Gizmos.DrawLine(vertices[i], vertices[i + 1]);
                Gizmos.DrawLine(vertices[i], vertices[i + 2]);
                Gizmos.DrawLine(vertices[i + 1], vertices[i + 2]);
            }

            for (int faceIndex = 0; faceIndex <= vertices.Count - 3; faceIndex += 3)
            {
                Vector3 point;
                point.x = (vertices[faceIndex].x + vertices[faceIndex + 1].x + vertices[faceIndex + 2].x) / 3;
                point.y = (vertices[faceIndex].y + vertices[faceIndex + 1].y + vertices[faceIndex + 2].y) / 3;
                point.z = (vertices[faceIndex].z + vertices[faceIndex + 1].z + vertices[faceIndex + 2].z) / 3;

                Gizmos.DrawCube(point, new Vector3(0.1f, 0.1f, 0.1f));
                Gizmos.color = new Color(1, 1, 1, 1f);
                Gizmos.DrawLine(point, point + GetFaceNormal(faceIndex));
            }
        }
    }
    void DrawFrustumLines()
    {
        Gizmos.DrawLine(nearUpperLeftVertex, farUpperLeftVertex);
        Gizmos.DrawLine(nearUpperRightVertex, farUpperRightVertex);
        Gizmos.DrawLine(nearLowerLeftVertex, farLowerLeftVertex);
        Gizmos.DrawLine(nearLowerRightVertex, farLowerRightVertex);

        Gizmos.DrawLine(nearUpperLeftVertex, nearUpperRightVertex);
        Gizmos.DrawLine(nearUpperRightVertex, nearLowerRightVertex);
        Gizmos.DrawLine(nearLowerRightVertex, nearLowerLeftVertex);
        Gizmos.DrawLine(nearLowerLeftVertex, nearUpperLeftVertex);

        Gizmos.DrawLine(farUpperLeftVertex, farUpperRightVertex);
        Gizmos.DrawLine(farUpperRightVertex, farLowerRightVertex);
        Gizmos.DrawLine(farLowerRightVertex, farLowerLeftVertex);
        Gizmos.DrawLine(farLowerLeftVertex, farUpperLeftVertex);
    }

    // Update is called once per frame

    void SetVertices()
    {
        vertices.Add(transform.position);
        vertices.Add(farLowerRightVertex);
        vertices.Add(farLowerLeftVertex);

        vertices.Add(transform.position);
        vertices.Add(farUpperLeftVertex);
        vertices.Add(farUpperRightVertex);

        vertices.Add(transform.position);
        vertices.Add(farLowerLeftVertex);
        vertices.Add(farUpperLeftVertex);

        vertices.Add(transform.position);
        vertices.Add(farLowerRightVertex);
        vertices.Add(farUpperRightVertex);

        vertices.Add(nearUpperLeftVertex);
        vertices.Add(nearUpperRightVertex);
        vertices.Add(nearLowerRightVertex);

        vertices.Add(farUpperLeftVertex);
        vertices.Add(farLowerRightVertex);
        vertices.Add(farUpperRightVertex);
    }
    void UpdateVertices()
    {
        vertices[0] = transform.position;
        vertices[1] = farLowerRightVertex;
        vertices[2] = farLowerLeftVertex;

        vertices[3] = transform.position;
        vertices[4] = farUpperLeftVertex;
        vertices[5] = farUpperRightVertex;

        vertices[6] = transform.position;
        vertices[7] = farLowerLeftVertex;
        vertices[8] = farUpperLeftVertex;

        vertices[9] = transform.position;
        vertices[10] = farUpperRightVertex;
        vertices[11] = farLowerRightVertex;

        vertices[12] = nearUpperLeftVertex;
        vertices[13] = nearUpperRightVertex;
        vertices[14] = nearLowerRightVertex;

        vertices[15] = farUpperLeftVertex;
        vertices[16] = farLowerRightVertex;
        vertices[17] = farUpperRightVertex;
    }

    public Vector3 GetFaceNormal(int index)
    {
        // https://www.khronos.org/opengl/wiki/Calculating_a_Surface_Normal#:~:text=A%20surface%20normal%20for%20a,of%20the%20face%20w.r.t.%20winding).
        Vector3 firstVertex = vertices[index];
        Vector3 secondVertex = vertices[index + 1];
        Vector3 thirdVertex = vertices[index + 2];

        Vector3 normal;
        Vector3 firstSecond = secondVertex - firstVertex;
        Vector3 firstThird = thirdVertex - firstVertex;

        //Vector3 normal = Vector3.zero;
        //Vector3 normal = Vector3.Cross(secondVertex - firstVertex, thirdVertex - firstVertex).normalized;

        normal.x = (firstThird.y * firstSecond.z) - (firstThird.z * firstSecond.y);
        normal.y = (firstThird.z * firstSecond.x) - (firstThird.x * firstSecond.z);
        normal.z = (firstThird.x * firstSecond.y) - (firstThird.y * firstSecond.x);

        float magnitude = Mathf.Sqrt(Mathf.Pow(normal.x, 2) + Mathf.Pow(normal.y, 2) + Mathf.Pow(normal.z, 2));
        Vector3 normalizedNormal = normal / magnitude;

        return normalizedNormal;
    }
}
