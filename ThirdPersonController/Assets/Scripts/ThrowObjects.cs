using UnityEngine;
using System.Collections;

public class ThrowObjects : MonoBehaviour {


    public GameObject projectile;
    public Vector3 projectileOffset;
    public Vector3 projectileForce;
    public Transform characterHand;
    public float lengthPrepare;
    public float lengthThrow;
    public float compensationYAngle = 20.0f;
    private bool prepared = false;
    private bool threw = false;
    private Animator animator;


	// Use this for initialization
	public void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	public void LateUpdate () {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(1);


        if (stateInfo.IsName("UpperBody.Grenade"))
        {
            
            if (stateInfo.normalizedTime >= lengthPrepare * 0.01 && !prepared)
            {
                //Debug.Log("Prepare function");
                Prepare();
            }
            if (stateInfo.normalizedTime >= lengthThrow * 0.01 && !threw)
            {
                //Debug.Log("Throw function");
                Throw();
            }
            else
            {
                prepared = false;
                threw = false;
            }
        }
	}
    public void Prepare()
    {
        prepared = true;
        projectile = Instantiate(projectile, characterHand.position, characterHand.rotation) as GameObject;
        if (projectile.GetComponent<Rigidbody>())   
            Destroy(projectile.GetComponent<Rigidbody>());
        projectile.GetComponent<SphereCollider>().enabled = false;
        projectile.name = "projectile";
        projectile.transform.parent = characterHand;
        projectile.transform.localPosition = projectileOffset;
        projectile.transform.localEulerAngles = Vector3.zero;
    }
    public void Throw()
    {
        threw = true;
        Vector3 direction = transform.rotation.eulerAngles;
        direction.y += compensationYAngle;
        projectile.transform.rotation = Quaternion.Euler(direction);
        projectile.transform.parent = null;
        projectile.GetComponent<SphereCollider>().enabled = true;
        projectile.AddComponent<Rigidbody>();
        Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponent<Collider>());
        projectile.GetComponent<Rigidbody>().AddRelativeForce(projectileForce);
    }
}
