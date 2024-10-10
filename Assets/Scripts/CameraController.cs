using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float hSensitivity = 1.0f;
    public float vSensitivity;
    public Transform target;
    public Transform player;
    public InputAction rotateAction;

    float mouseX;
    float mouseY;

    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        hSensitivity = 2.0f;
        vSensitivity = 0.4f;
        Cursor.visible = false;
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        ControlCamera();
    }
    void ControlCamera()
    {
        mouseX += rotateAction.ReadValue<Vector2>().x * hSensitivity;
        mouseY += rotateAction.ReadValue<Vector2>().y * -vSensitivity;
        mouseY = Mathf.Clamp(mouseY, -25, 25);

        transform.LookAt(target);

        player.rotation = Quaternion.Euler(0, mouseX, 0);
        target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
    }
    private void OnEnable()
    {
        rotateAction.Enable();
    }
    private void OnDisable()
    {
        rotateAction.Disable();
    }
}
