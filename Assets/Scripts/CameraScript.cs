using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraAnchor;

    float angleX;  // Кути повороту камери, що є 
    float angleY;  // накопиченням зрушень миші

    void Start()
    {
        angleX = transform.eulerAngles.x;   // початкові значення - 
        angleY = transform.eulerAngles.y;   // як у редакторі
    }

    void LateUpdate()
    {
        float mx = Input.GetAxis("Mouse X");  // delta X
        float my = Input.GetAxis("Mouse Y");  // delta Y
        angleX -= my;
        angleY += mx;
        angleY %= 360;
        //Debug.Log("angleX: " + angleX + " angleY: " + angleY);

        angleX = Mathf.Max(angleX, -35);
        angleX = Mathf.Min(angleX, 35);


        this.transform.eulerAngles = new Vector3(angleX, angleY, 0);

        this.transform.position = cameraAnchor.transform.position;
    }
}
