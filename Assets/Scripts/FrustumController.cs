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
    void Start()
    {

    }

    Vector3 farLimit;
    Vector3 nearLimit;

    // Frustum
    // DL "DownLeft" DR "DownRight"
    // UL "UpperLeft" UR "UpperRight"

    Vector3 nearUpperLeftVertex;
    Vector3 nearUpperRightVertex;
    Vector3 nearDownLeftVertex;
    Vector3 nearDownRightVertex;

    Vector3 farUpperLeftVertex;
    Vector3 farUpperRightVertex;
    Vector3 farDownLeftVertex;
    Vector3 farDownRightVertex;

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.DrawCube(farLimit, new Vector3(0.3f, 0.3f, 0.3f));
            Gizmos.DrawCube(nearLimit, new Vector3(0.3f, 0.3f, 0.3f));

            Gizmos.DrawLine(nearUpperLeftVertex, farUpperLeftVertex);
            Gizmos.DrawLine(nearUpperRightVertex, farUpperRightVertex);
            Gizmos.DrawLine(nearDownLeftVertex, farDownLeftVertex);
            Gizmos.DrawLine(nearDownRightVertex, farDownRightVertex);

        }
    }
    // Update is called once per frame
    void Update()
    {
        aspectRatio = (float)screenWidth / (float)screenHeight;

        verticalfieldOfViewAngle = fieldOfViewAngle / aspectRatio;

        farLimit = transform.position + transform.forward * renderingDistance;
        nearLimit = transform.position + transform.forward * nearClippingPlane;

        nearUpperLeftVertex = new Vector3(Mathf.Tan((-fieldOfViewAngle / 2) * Mathf.Deg2Rad) * nearClippingPlane, Mathf.Tan((verticalfieldOfViewAngle / 2) * Mathf.Deg2Rad) * nearClippingPlane, nearLimit.z);
        nearUpperRightVertex = new Vector3(Mathf.Tan((fieldOfViewAngle / 2) * Mathf.Deg2Rad) * nearClippingPlane, Mathf.Tan((verticalfieldOfViewAngle / 2) * Mathf.Deg2Rad) * nearClippingPlane, nearLimit.z);
        nearDownLeftVertex = new Vector3(Mathf.Tan((-fieldOfViewAngle / 2) * Mathf.Deg2Rad) * nearClippingPlane, Mathf.Tan((-verticalfieldOfViewAngle / 2) * Mathf.Deg2Rad) * nearClippingPlane, nearLimit.z);
        nearDownRightVertex = new Vector3(Mathf.Tan((fieldOfViewAngle / 2) * Mathf.Deg2Rad) * nearClippingPlane, Mathf.Tan((-verticalfieldOfViewAngle / 2) * Mathf.Deg2Rad) * nearClippingPlane, nearLimit.z);

        farUpperLeftVertex = new Vector3(Mathf.Tan((-fieldOfViewAngle / 2) * Mathf.Deg2Rad) * renderingDistance, Mathf.Tan((verticalfieldOfViewAngle / 2) * Mathf.Deg2Rad) * renderingDistance, farLimit.z);
        farUpperRightVertex = new Vector3(Mathf.Tan((fieldOfViewAngle / 2) * Mathf.Deg2Rad) * renderingDistance, Mathf.Tan((verticalfieldOfViewAngle / 2) * Mathf.Deg2Rad) * renderingDistance, farLimit.z);
        farDownLeftVertex = new Vector3(Mathf.Tan((-fieldOfViewAngle / 2) * Mathf.Deg2Rad) * renderingDistance, Mathf.Tan((-verticalfieldOfViewAngle / 2) * Mathf.Deg2Rad) * renderingDistance, farLimit.z);
        farDownRightVertex = new Vector3(Mathf.Tan((fieldOfViewAngle / 2) * Mathf.Deg2Rad) * renderingDistance, Mathf.Tan((-verticalfieldOfViewAngle / 2) * Mathf.Deg2Rad) * renderingDistance, farLimit.z);

    }
}
