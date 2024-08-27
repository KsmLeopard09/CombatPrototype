using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction sprintAction;
    public Vector2 movementValue;
    [SerializeField] Animator animator;
    Rigidbody rb;
    float dampTime;
    float x, y;
    float idleFrames;

    // Start is called before the first frame update
    void Start()
    {
        dampTime = 0.26f;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        if (sprintAction.IsPressed())
        {
            animator.SetBool("isSprinting", true);
        }
        else if(sprintAction.IsPressed() && movementValue != Vector2.zero)
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isSprinting", true);
        }
        if (movementValue == Vector2.zero || movementValue.y < 0.3f)
        {
            animator.SetBool("isSprinting", false);
        }
        if (x > -0.5f && x < -0.1f)
        {
            x = -0.5f;
        }
        if (x < 0.5f && x > 0.1f)
        {
            x = 0.5f;
        }
        if (y > -0.5f && y < -0.1f)
        {
            y = -0.5f;
        }
        if (y < 0.5f && y > 0.1f)
        {
            y = 0.5f;
        }
        if(x >= 0.8 || x < -0.8)
        {
            dampTime = 0.08f;
        }
        else
        {
            dampTime = 0.26f;
        }
        movementValue.x = x;
        movementValue.y = y;
        if(movementValue.x > -1 && movementValue.x < -0.3 && movementValue.y < 1 && movementValue.y > 0)
        {
            movementValue.x = -0.7071f;
            movementValue.y = 0.7071f;
        }
        if (movementValue.x < 1 && movementValue.x > 0.3 && movementValue.y < 1 && movementValue.y > 0)
        {
            movementValue.x = 0.7071f;
            movementValue.y = 0.7071f;
        }
        if (movementValue.x > -1 && movementValue.x < -0.3 && movementValue.y < 0 && movementValue.y > -1)
        {
            movementValue.x = -0.7071f;
            movementValue.y = -0.7071f;
        }
        if (movementValue.x < 1 && movementValue.x > 0.3 && movementValue.y < 0 && movementValue.y > -1)
        {
            movementValue.x = 0.7071f;
            movementValue.y = -0.7071f;
        }
        if (animator.GetBool("isSprinting") == false)
        {
            if (movementValue == Vector2.zero)
            {
                idleFrames += Time.deltaTime;
                if(idleFrames > 0.1f)
                {
                    animator.SetBool("isMoving", false);
                }
            }
            if(sprintAction.IsPressed() && movementValue != Vector2.zero)
            {
                idleFrames = 0;
                animator.SetBool("isMoving", false);
            }
            else if(sprintAction.IsPressed() == false && movementValue != Vector2.zero)
            {
                idleFrames = 0;
                animator.SetBool("isMoving", true);
            }
        }
        if(animator.GetBool("isMoving") == true)
        {
            if(sprintAction.IsPressed())
            {
                idleFrames = 0;
                animator.SetBool("isMoving", false);
                animator.SetBool("isSprinting", true);
            }
        }
        movementValue = Vector2.ClampMagnitude(movementValue, 1);
        if (animator.GetBool("isSprinting") == false)
        {
            animator.SetFloat("Multiplier", 1.45f);
        }
        animator.SetFloat("X", movementValue.x, dampTime, Time.deltaTime);
        animator.SetFloat("Y", movementValue.y, dampTime, Time.deltaTime);
    }
    private void OnEnable()
    {
        sprintAction.Enable();
    }
    private void OnDisable()
    {
        sprintAction.Disable();
    }
}
