using UnityEngine;
using System.Collections;

public class BasicController02 : MonoBehaviour {


    private Animator animator;
    private CharacterController controller;
    public float transitionTime = 0.25f;


    float accelerator = 1.0f;
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
            if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
            {
                accelerator = 2.0f;
            }
            else if (Input.GetKey(KeyCode.RightAlt) || Input.GetKey(KeyCode.LeftAlt))
            {
                accelerator = 1.5f;
            }
            else
            {
                accelerator = 1.0f;
            }
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
	}
}
