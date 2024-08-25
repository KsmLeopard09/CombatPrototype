using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float hSensitivity = 1.0f;
    public float vSensitivity;
    public Transform target;
    public Transform player;

    float mouseX;
    float mouseY;

    // Start is called before the first frame update
    void Start()
    {
        hSensitivity = 30.0f;
        vSensitivity = 5.0f;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        ControlCamera();
    }

    void ControlCamera()
    {
        mouseX += Input.GetAxis("Mouse X") * hSensitivity;
        mouseY += Input.GetAxis("Mouse Y") * vSensitivity;

        transform.LookAt(target);

        player.rotation = Quaternion.Euler(0, mouseX, 0);
        target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
    }
}
