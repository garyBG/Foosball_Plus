using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBehavior : MonoBehaviour
{
	private float dir = 1f;
	private bool switched = false;

	public int RotationAxis = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    Vector3 rotateAxis;
	    float curRotation;
	    switch (RotationAxis)
	    {
            case 0:
                rotateAxis = Vector3.right;
                curRotation = transform.rotation.x;
                break;
            case 1:
	            rotateAxis = Vector3.forward;
	            curRotation = transform.rotation.z;
                break;
            case 2:
	            rotateAxis = Vector3.up;
	            curRotation = transform.rotation.y;
                break;
            default:
	            rotateAxis = Vector3.right;
	            curRotation = transform.rotation.x;
	            break;
	    }

        Quaternion newRot = Quaternion.AngleAxis(50 * dir * Time.deltaTime, rotateAxis) * transform.rotation;
        if (Mathf.Abs(curRotation) < 0.5f)
        {
            if (!switched)
            {
                switched = true;
                dir *= -1;
            }
        }
        else
        {
            switched = false;
        }
        transform.rotation = newRot;
    }
}
