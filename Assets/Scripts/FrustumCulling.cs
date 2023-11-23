using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrustrumCulling : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] GameObject point;
    FrustumController frustumController;
    void Start()
    {
        frustumController = GetComponent<FrustumController>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject hidableObject in GameObject.FindGameObjectsWithTag("HidableObject"))
        {
            BoundingBox objectBoundingBox = hidableObject.GetComponent<BoundingBox>();

            for (int i = 0; i < objectBoundingBox.GetVertices().Count; i++)
            {
                if (CheckPointInside(objectBoundingBox.GetVertices()[i]))
                {
                    hidableObject.GetComponent<MeshRenderer>().enabled = true;
                    break;
                }
                else
                    hidableObject.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
    int insideOfNormals = 0;
    bool CheckPointInside(Vector3 point)
    {
        insideOfNormals = 0;
        foreach (GameObject hidableObject in GameObject.FindGameObjectsWithTag("HidableObject"))
        {
            for (int faceIndex = 0; faceIndex <= frustumController.vertices.Count - 3; faceIndex += 3)
            {
                if (!frustumController.IsPointInside(point, faceIndex))
                    return false;
            }
        }

        Debug.Log(insideOfNormals + "/" + frustumController.vertices.Count / 3);

        if (insideOfNormals >= frustumController.vertices.Count / 3)
            Debug.Log("Adentro");

        return true;
    }
}
