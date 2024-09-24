using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction sprintAction;
    public Vector2 movementValue, actualValue;
    [SerializeField] Animator animator;
    Rigidbody rb;
    float dampTime;
    float x, y;
    float idleFrames;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        actualValue = new Vector2(x, y);
        if (sprintAction.IsPressed())
        {
            animator.SetBool("isSprinting", true);
        }
        else if (sprintAction.IsPressed() && movementValue != Vector2.zero)
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
        movementValue.x = x;
        movementValue.y = y;
        dampTime = 0.1f;
        if(y == 0 && Vector2.SqrMagnitude(actualValue) >= 0.25 && Vector2.SqrMagnitude(actualValue) < 1)
        {
            if(x > 0)
            {
                movementValue.x = 0.5f;
            }
            else
            {
                movementValue.x = -0.5f;
            }
        }
        #region WalkLogic
        if (x < -0.4 && y < -0.2 && Vector2.SqrMagnitude(actualValue) < 0.25)
        {
            movementValue.x = -0.3f;
            movementValue.y = -0.3f;
        }
        if (x > 0.2 && y > 0.2 && Vector2.SqrMagnitude(actualValue) < 0.25)
        {
            movementValue.x = 0.3f;
            movementValue.y = 0.3f;
        }
        if (x < -0.4 && y > 0.2 && Vector2.SqrMagnitude(actualValue) < 0.25)
        {
            movementValue.x = -0.3f;
            movementValue.y = 0.3f;
        }
        if (x > 0.2 && y < -0.2 && Vector2.SqrMagnitude(actualValue) < 0.25)
        {
            movementValue.x = 0.3f;
            movementValue.y = -0.3f;
        }
        #endregion
        #region JogLogic
        if (x < -0.4 && y < -0.2 && Vector2.SqrMagnitude(actualValue) > 0.25)
        {
            dampTime = 0.1f;
            movementValue.x = -0.7071f;
            movementValue.y = -0.7071f;
        }
        if (x > 0.2 && y > 0.2 && Vector2.SqrMagnitude(actualValue) > 0.25)
        {
            movementValue.x = 0.7071f;
            movementValue.y = 0.7071f;
        }
        if (x < -0.4 && y > 0.2 && Vector2.SqrMagnitude(actualValue) > 0.25)
        {
            movementValue.x = -0.7071f;
            movementValue.y = 0.7071f;
        }
        if (x > 0.2 && y < -0.2 && Vector2.SqrMagnitude(actualValue) > 0.25)
        {
            dampTime = 0.1f;
            movementValue.x = 0.7071f;
            movementValue.y = -0.7071f;
        }
        #endregion
        if (animator.GetBool("isSprinting") == false)
        {
            if (movementValue == Vector2.zero)
            {
                idleFrames += Time.deltaTime;
                if (idleFrames > 0.1f)
                {
                    animator.SetBool("isMoving", false);
                }
            }
            if (sprintAction.IsPressed() && movementValue != Vector2.zero)
            {
                idleFrames = 0;
                animator.SetBool("isMoving", false);
            }
            else if (sprintAction.IsPressed() == false && movementValue != Vector2.zero)
            {
                idleFrames = 0;
                animator.SetBool("isMoving", true);
            }
        }
        if (animator.GetBool("isMoving") == true)
        {
            if (sprintAction.IsPressed())
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
