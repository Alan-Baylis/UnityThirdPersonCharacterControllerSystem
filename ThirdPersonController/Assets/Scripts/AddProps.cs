using UnityEngine;
using System.Collections;
//Get the prop(guns, badge etc) when trigger with those
public class AddProps : MonoBehaviour {

    public GameObject prop; //Get the prop game object
    public string propName; //Get the prop game object name
    public Transform targetBone;    //Get the target bone which the prop has to be attached
    public Vector3 propOffset;  //Get the prop offset to place the prop at the location
    public bool destroyTrigger = true;  //Get the bool variable 

    //On trigger enter method when someone enter in trigger zone
    void OnTriggerEnter(Collider collision)
    {
        //Return true if target bone is a child of collision.transform (SWAT)
        if (targetBone.IsChildOf(collision.transform))
        {
            bool checkProp = false;
            foreach (Transform child in targetBone)
            {
                if (child.name == propName)
                {
                    checkProp = true;
                }
            }
            if (!checkProp)
            {
                GameObject newProp;
                newProp = Instantiate(prop, targetBone.position, targetBone.rotation) as GameObject;
                newProp.name = propName;
                newProp.transform.parent = targetBone;
                newProp.transform.localPosition += propOffset;
                if (destroyTrigger)
                {
                    Destroy(gameObject);
                }
            }
        } 
    } 
}
