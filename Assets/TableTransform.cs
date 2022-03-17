using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TableTransform : MonoBehaviour
{
    public float maxMovement = 0.05f;
    private float maxVelocity = 0.05f;
    private float velocityStep = 0.0005f;

    private float velocity = 0f;
    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Quaternion q = setAngle();
            float curVelocity = setVelocity();
            if (Mathf.Abs((transform.localRotation * q).x) < maxMovement)
            {
                // Trigger Rotation
                if (Mathf.Abs(curVelocity) >= 0.001f)
                {
                    this.transform.localRotation *= q;
                }

                // Wiggle the table
                if (Input.GetKey(KeyCode.UpArrow) && velocity + velocityStep < maxVelocity)
                {
                    velocity = updateVelocity(velocityStep);
                }
                if (Input.GetKey(KeyCode.DownArrow) && velocity - velocityStep > -maxVelocity)
                {
                    velocity = updateVelocity(-1 * velocityStep);
                }

                // Return to home
                if (Input.GetKey(KeyCode.Alpha0))
                {
                    // Rotate table back towards zero
                    transform.localRotation = setQuart(new Quaternion(
                    transform.localRotation.x / 1.01f,
                    transform.localRotation.y,
                    transform.localRotation.z,
                    transform.localRotation.w
                    ));
                }

                // No keys pressed
                if (!Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.Alpha0))
                {
                    // Reduce velocity until 0
                     velocity = reduceVelocity(velocityStep);
                }
            }
            else
            {
                velocity = 0f;
            }
        }
        else
        {
            // Quaternion q = setAngleRPC();
            // float curVelocity = setVelocityRPC();
            // if (Mathf.Abs((transform.localRotation * q).x) < maxMovement)
            // {
            //     // Trigger Rotation
            //     if (Mathf.Abs(curVelocity) >= 0.001f)
            //     {
            //         this.transform.localRotation *= q;
            //     }

            //     // Wiggle the table
            //     if (Input.GetKey(KeyCode.UpArrow) && velocity + velocityStep < maxVelocity)
            //     {
            //         velocity = updateVelocityRPC(velocityStep);
            //     }
            //     if (Input.GetKey(KeyCode.DownArrow) && velocity - velocityStep > -maxVelocity)
            //     {
            //         velocity = updateVelocityRPC(-1 * velocityStep);
            //     }

            //     // Return to home
            //     if (Input.GetKey(KeyCode.Alpha0))
            //     {
            //         // Rotate table back towards zero
            //         transform.localRotation = setQuartRPC(new Quaternion(
            //         transform.localRotation.x / 1.01f,
            //         transform.localRotation.y,
            //         transform.localRotation.z,
            //         transform.localRotation.w
            //         ));
            //     }

            //     // No keys pressed
            //     if (!Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.Alpha0))
            //     {
            //         // Reduce velocity until 0
            //          velocity = reduceVelocity(velocityStep);
            //     }
            // }
            // else
            // {
            //     velocity = 0f;
            // }
        }

    }
    float setVelocity()
    {
        return velocity * Time.deltaTime * 60;

    }
    Quaternion setAngle()
    {
        return Quaternion.AngleAxis(setVelocity(), Vector3.right);
    }
    float updateVelocity(float velocityStep)
    {
        return velocity + velocityStep;
    }
    Quaternion setQuart(Quaternion q)
    {
		return q;
    }
	float reduceVelocity(float velocityStep) {
		return velocity - Mathf.Sign(velocity) * velocityStep;
	}

	[PunRPC]
	 float setVelocityRPC()
    {
        return setVelocity();

    }
	[PunRPC]
    Quaternion setAngleRPC()
    {
        return setAngle();
    }
	[PunRPC]
    float updateVelocityRPC(float velocityStep)
    {
        return updateVelocity(velocityStep);
    }
	[PunRPC]
    Quaternion setQuartRPC(Quaternion q)
    {
		return setQuart(q);
    }
	[PunRPC]
	float reduceVelocityRPC(float velocityStep) {
		return reduceVelocity(velocityStep);
	}

}

