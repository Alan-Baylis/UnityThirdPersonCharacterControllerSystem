using UnityEngine;
using System.Collections;

public class BasicController02 : MonoBehaviour {


    private Animator animator;
    private CharacterController controller;
    public float transitionTime = 0.25f;

    public float jumpSpeed = 4.0f;
    public float gravity = 20.0f;
    public float jumpPos = 0.0f;
    private float verticalSpeed = 0;
    private float xVelocity = 0.0f;
    private float zVelocity = 0.0f;

    float accelerator;
    float horizontal;
    float vertical;
    float xSpeed;
    float zSpeed;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        if (animator.layerCount >= 2)
        {
            animator.SetLayerWeight(1, 1);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                animator.SetBool("Jump", true);
                verticalSpeed = jumpSpeed;
            }
            else
            {
                animator.SetBool("Jump", false);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                animator.SetBool("TurnLeft", true);
                transform.Rotate(Vector3.up * (Time.deltaTime * -45.0f), Space.World);
            }
            else
            {
                animator.SetBool("TurnLeft", false);
            }
            if (Input.GetKey(KeyCode.E))
            {
                animator.SetBool("TurnRight", true);
                transform.Rotate(Vector3.up * (Time.deltaTime * 45.0f), Space.World);
            }
            else
            {
                animator.SetBool("TurnRight", false);
            }
            if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
            {
                accelerator = 5.0f;
            }
            else if (Input.GetKey(KeyCode.RightAlt) || Input.GetKey(KeyCode.LeftAlt))
            {
                accelerator = 4.0f;
            }
            else
            {
                accelerator = 3.0f;
            }

            //Moving A Character
            //Get horizontal Axis
            horizontal = Input.GetAxis("Horizontal");
            //Get vertical Axis
            vertical = Input.GetAxis("Vertical");
            //Calculate xSpeed
            xSpeed = horizontal * accelerator;
            //Calculate zSpeed
            zSpeed = vertical * accelerator;
            //Set xSpeed to the animator controller
            animator.SetFloat("xSpeed", xSpeed, transitionTime, Time.deltaTime);
            //Set zSpeed to the animator controller
            animator.SetFloat("zSpeed", zSpeed, transitionTime, Time.deltaTime);
            //Calculate Speed and Set to the animator controller
            animator.SetFloat("Speed", Mathf.Sqrt(horizontal * horizontal + vertical * vertical), transitionTime, Time.deltaTime);
            //
            //transform.Rotate(Vector3.up * (Time.deltaTime * vertical * Input.GetAxis("Mouse X") * 90), Space.World);
        }
        //Firing and throwing Grenade
        if (Input.GetKey(KeyCode.F) && animator.layerCount >= 2)
        {
            
            animator.SetBool("Grenade", true);
        }
        else
        {
            animator.SetBool("Grenade", false);
        }
        if (Input.GetButtonDown("Fire1") && animator.layerCount >= 2)
        {
            Debug.Log("Firing");
            animator.SetBool("Fire", true);
        }
        else
        {
            animator.SetBool("Fire", false);
        }
	}
    //Method for overriding the animation's original root motion (Callback function)
    //Not clear yet 
    void OnAnimatorMove()
    {
        Vector3 deltaPosition = animator.deltaPosition;
        if (controller.isGrounded)
        {
            xVelocity = animator.GetFloat("Speed") * controller.velocity.x * 0.25f;
            zVelocity = animator.GetFloat("Speed") * controller.velocity.z * 0.25f;
        }
        verticalSpeed += Physics.gravity.y * Time.deltaTime;
        if (verticalSpeed <= 0)
        {
            animator.SetBool("Jump", false);
        }
        deltaPosition.y = verticalSpeed * Time.deltaTime;
        if (!controller.isGrounded)
        {
            deltaPosition.x = xVelocity * Time.deltaTime;
            deltaPosition.z = zVelocity * Time.deltaTime;
        }
        controller.Move(deltaPosition);
        if ((controller.collisionFlags & CollisionFlags.Below) != 0)
        {
            verticalSpeed = 0;
        }
        transform.rotation = animator.rootRotation;
    }
     
}
