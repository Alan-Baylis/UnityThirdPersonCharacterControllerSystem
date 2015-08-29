using UnityEngine;
using System.Collections;

public class BasicController03 : MonoBehaviour {

    Animator animator;


	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetBool("Grenade", true);
        }
        else
        {
            animator.SetBool("Grenade", false);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("Fire", true);
        }
        else
        {
            animator.SetBool("Fire", false);
        }

	}
}
