using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoleAniController : MonoBehaviour {

    private Animation ani;
	// Use this for initialization
	void Start () {
        ani = this.gameObject.GetComponent<Animation>();
        AnimationClip clip = ani.GetClip("pose");
        AnimationEvent ae = new AnimationEvent();
        ae.time = clip.length - 0.1f;
        ae.functionName = "Switch2Wait";
        clip.AddEvent(ae);
	}
	
    void Switch2Wait()
    {
        ani.CrossFade("wait");
    }

    bool canRotate = false;
    // Update is called once per frame
    void Update () {
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.transform.name == "Models")
                {
                    canRotate = true;
                    
                }

            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            canRotate = false;
        }

        if (canRotate)
        {
            this.transform.parent.Rotate(Vector3.up * -Input.GetAxis("Mouse X")*10);
        }
	}

     
}
