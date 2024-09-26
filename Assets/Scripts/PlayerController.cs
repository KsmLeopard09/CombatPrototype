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
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Gets the values of the Horizontal and the vertical axes
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        //Sets sprinting to true when sprint button is pressed
        if (sprintAction.IsPressed())
        {
            animator.SetBool("isSprinting", true);
        }
        //Sets sprinting to true right from idle instead of having to move first
        else if (sprintAction.IsPressed() && movementValue != Vector2.zero)
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isSprinting", true);
        }
        //sets sprinting to false if players stops moving or moves too much to the side
        if (movementValue == Vector2.zero || movementValue.y < 0.4f)
        {
            animator.SetBool("isSprinting", false);
        }
        //does not register movement to the side if it's very minimal
        if(Mathf.Abs(movementValue.x) < 0.3f)
        {
            movementValue.x = 0f;
        }
        //stores movement values in variables to compare later on
        movementValue.x = x;
        movementValue.y = y;
        //sets a blending period in between animations without having to have exit time allowing for quick and smooth blending
        if(y > 0.3f)
        {
            dampTime = 0.075f;
        }
        else
        {
            dampTime = 0.12f;
        }



        #region WalkLogic
        //checks to see if the overall squared magnitude of the movement vector is less than or equal to 0.25 to see if walking conditions need to be checked
        if(Vector2.SqrMagnitude(movementValue) > 0.0001 && Vector2.SqrMagnitude(movementValue) < 0.49)
        {
            //takes the overall squared magnitude of the movement vector and sets the x value depending on direction
            if (y == 0)
            {
                if (x > 0)
                {
                    movementValue.x = 0.5f;
                }
                else
                {
                    movementValue.x = -0.5f;
                }
            }
            //takes the overall squared magnitude of the movement vector and sets the y value depending on direction
            if (x == 0)
            {
                if (y > 0)
                {
                    movementValue.y = 0.5f;
                }
                else
                {
                    movementValue.y = -0.5f;
                }
            }
            //takes the overall squared magnitude of the movement vector and sets the x and y value depending on direction for diagonal movement
            if (x < 0 && y < 0)
            {
                movementValue.x = -0.3f;
                movementValue.y = -0.3f;
            }
            if (x > 0 && y > 0)
            {
                movementValue.x = 0.3f;
                movementValue.y = 0.3f;
            }
            if (x < 0 && y > 0)
            {
                movementValue.x = -0.3f;
                movementValue.y = 0.3f;
            }
            if (x > 0 && y < 0)
            {
                movementValue.x = 0.3f;
                movementValue.y = -0.3f;
            }
        }
        #endregion




        #region JogLogic
        if(Vector2.SqrMagnitude(movementValue) >= 0.49)
        {
            //takes the overall squared magnitude of the movement vector and sets the x value depending on direction
            if (y == 0)
            {
                if (x > 0)
                {
                    movementValue.x = 1f;
                }
                else
                {
                    movementValue.x = -1f;
                }
            }
            //takes the overall squared magnitude of the movement vector and sets the y value depending on direction
            if (x == 0)
            {
                if (y > 0)
                {
                    movementValue.y = 1f;
                }
                else
                {
                    movementValue.y = -1f;
                }
            }
            if (x < -0.4 && y < -0.2)
            {
                dampTime = 0.1f;
                movementValue.x = -0.7071f;
                movementValue.y = -0.7071f;
            }
            if (x > 0.2 && y > 0.2)
            {
                movementValue.x = 0.7071f;
                movementValue.y = 0.7071f;
            }
            if (x < -0.4 && y > 0.2)
            {
                movementValue.x = -0.7071f;
                movementValue.y = 0.7071f;
            }
            if (x > 0.2 && y < -0.2)
            {
                dampTime = 0.08f;
                movementValue.x = 0.7071f;
                movementValue.y = -0.7071f;
            }
        }
        #endregion



        #region IdleAndSprint
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
        #endregion



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
