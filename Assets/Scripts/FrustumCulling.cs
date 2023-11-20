using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrustrumCulling : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject point;
    FrustumController frustumController;
    void Start()
    {
        frustumController = GetComponent<FrustumController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckPointInside())
            point.GetComponent<MeshRenderer>().enabled = true;
        else
            point.GetComponent<MeshRenderer>().enabled = false;
    }
    int insideOfNormals = 0;
    bool CheckPointInside()
    {
        insideOfNormals = 0;
        for (int faceIndex = 0; faceIndex <= frustumController.vertices.Count - 3; faceIndex += 3)
        {
            if (frustumController.IsPointInside(point.transform.position, faceIndex))
                insideOfNormals++;
            else
                return false;
        }

        Debug.Log(insideOfNormals + "/" + frustumController.vertices.Count / 3);

        if (insideOfNormals >= frustumController.vertices.Count / 3)
            Debug.Log("Adentro");

        return true;
    }
}
