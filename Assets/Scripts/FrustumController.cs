using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrustumController : MonoBehaviour
{

    [SerializeField] int ScreenWidth;
    [SerializeField] int ScreenHeight;

    [SerializeField] float fieldOfViewAngle;
    [SerializeField] float nearClippingPlane;
    [SerializeField] float renderingDistance;


    float verticalfieldOfViewAngle;
    // Start is called before the first frame update
    void Start()
    {
        //verticalfieldOfViewAngle = 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
