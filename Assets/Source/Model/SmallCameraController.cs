using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCameraController : MonoBehaviour
{
	public SceneNode cameraAttachment = null;
	public LineSegment sightLine = null; 

	public float offsetX = 0.0f;
	public float offsetY = 0.0f;
	public float offsetZ = 0.0f;
	// Start is called before the first frame update
	void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
	    if (cameraAttachment != null)
	    {
		    Matrix4x4 mCombinedParentXform = cameraAttachment.getCombinedParentXform();

		    Vector3 newCameraPos = mCombinedParentXform.MultiplyPoint(new Vector3(offsetX, offsetY, offsetZ));
		    transform.position = newCameraPos;

			Vector3 up = mCombinedParentXform.GetColumn(1).normalized;
		    Vector3 forward = mCombinedParentXform.GetColumn(2).normalized;

		    float angle = Mathf.Acos(Vector3.Dot(Vector3.up, up)) * Mathf.Rad2Deg;
		    Vector3 axis = Vector3.Cross(Vector3.up, up);
		    transform.localRotation = Quaternion.AngleAxis(angle, axis);

		    // Now, align the forward axis
		    angle = Mathf.Acos(Vector3.Dot(transform.transform.forward, forward)) * Mathf.Rad2Deg;
		    axis = Vector3.Cross(transform.transform.forward, forward);
		    transform.localRotation = Quaternion.AngleAxis(angle, axis) * transform.localRotation;
		    
		    sightLine.SetPoints(transform.localPosition, new Vector3(transform.localPosition.x + (10f * transform.forward.x), transform.localPosition.y + (10f * transform.forward.y),
			    transform.localPosition.z + (10f * transform.forward.z)));
		}
    }
}
