using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private float dir = 1f;
    private bool switched = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    Quaternion newRot = Quaternion.AngleAxis(50 * dir * Time.deltaTime, Vector3.right) * transform.rotation;
	    if (Mathf.Abs(transform.rotation.x) < 0.5f)
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
