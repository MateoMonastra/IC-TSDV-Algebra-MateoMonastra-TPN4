using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrustrumCulling : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject point;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckPointInside();
    }
    int insideOfNormals = 0;
    void CheckPointInside()
    {
        insideOfNormals = 0;
        for (int i = 0; i < GetComponent<FrustumController>().frustumNormals.Length; i++)
        {
            if (Vector3.Dot(GetComponent<FrustumController>().frustumNormals[i], point.transform.position) > 0)
                insideOfNormals++;
        }

        Debug.Log(insideOfNormals + "/" + GetComponent<FrustumController>().frustumNormals.Length);

        if (insideOfNormals >= GetComponent<FrustumController>().frustumNormals.Length)
            Debug.Log("Adentro");
    }
}
