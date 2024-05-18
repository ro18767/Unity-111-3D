using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraAnchor;

    float angleX;  // Кути повороту камери, що є 
    float angleY;  // накопиченням зрушень миші
    Vector3 rod;  // накопиченням зрушень миші
    int rodFactor = -20;  // накопиченням зрушень миші

    void Start()
    {
        angleX = transform.eulerAngles.x;   // початкові значення - 
        angleY = transform.eulerAngles.y;   // як у редакторі

        rod = this.transform.position - cameraAnchor.transform.position;
        Debug.Log("rod: " + rod);
    }

    void LateUpdate()
    {
        //float sd = ;  // delta X
        if(Input.mouseScrollDelta.y < 0f)
        {
            rodFactor++;
            if (rodFactor > 10) rodFactor = 10;
        }
        if (Input.mouseScrollDelta.y > 0f)
        {
            rodFactor--;
            if (rodFactor < -50) rodFactor = -50;
        }
        // Debug.Log("sd: " + rodFactor);

        float mx = Input.GetAxis("Mouse X");  // delta X
        float my = Input.GetAxis("Mouse Y");  // delta Y
        angleX -= my;
        angleY += mx;
        angleY %= 360;
        //Debug.Log("angleX: " + angleX + " angleY: " + angleY);

        angleX = Mathf.Max(angleX, -35);
        angleX = Mathf.Min(angleX, 35);


        this.transform.eulerAngles = new Vector3(angleX, angleY, 0);

        this.transform.position = cameraAnchor.transform.position + (Quaternion.Euler(0, angleY, 0) * rod * (Mathf.Pow(1.2f, rodFactor) + ((rodFactor + 50) * 0.07f)) );
    }
}
