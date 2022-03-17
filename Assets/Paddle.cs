using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
	public Vector3 velocity = new Vector3(0f, 0f, 0f);
	private Vector3 pos = new Vector3(0f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
	    pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newVelocity = new Vector3(
		    (transform.position.x - pos.x) * Time.deltaTime,
            (transform.position.y - pos.y) * Time.deltaTime,
            (transform.position.z - pos.z) * Time.deltaTime
		    );

        if (newVelocity.magnitude > velocity.magnitude)
        {
	        velocity = newVelocity;
        }
	    pos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
	    if (other.tag == "CBT")
	    {
		    other.GetComponent<Rigidbody>().velocity = velocity;
	    }
    }
}
