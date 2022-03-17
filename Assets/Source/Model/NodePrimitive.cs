using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePrimitive: MonoBehaviour {
    public Color MyColor = new Color(0.1f, 0.1f, 0.2f, 1.0f);
    public Vector3 Pivot;
    public GameObject colliderObj;

	// Use this for initialization
	void Start ()
	{
		Mesh mesh = gameObject.GetComponent<MeshFilter>().mesh;
		Bounds meshBounds = new Bounds(new Vector3(-100f, -100f, -100f), new Vector3(1000f, 1000f, 1000f));
		mesh.bounds = meshBounds;
    }

    void Update()
    {
    }
	
  
	public void LoadShaderMatrix(ref Matrix4x4 nodeMatrix)
    {
        Matrix4x4 p = Matrix4x4.TRS(Pivot, Quaternion.identity, Vector3.one);
        Matrix4x4 invp = Matrix4x4.TRS(-Pivot, Quaternion.identity, Vector3.one);
        Matrix4x4 trs = Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale);
        Matrix4x4 m = nodeMatrix * p * trs * invp;

        // Update collider object position
        if (colliderObj != null)
        {
	        Vector3 pos = m.GetColumn(3);
	        Vector3 scale = new Vector3(
		        m.GetColumn(0).magnitude,
		        m.GetColumn(1).magnitude,
		        m.GetColumn(2).magnitude);

            Vector3 up = m.GetColumn(1).normalized;
            Vector3 forward = m.GetColumn(2).normalized;

            float angle = Mathf.Acos(Vector3.Dot(Vector3.up, up)) * Mathf.Rad2Deg;
            Vector3 axis = Vector3.Cross(Vector3.up, up);

            colliderObj.transform.localRotation = Quaternion.AngleAxis(angle, axis);
            colliderObj.transform.position = pos;
            colliderObj.transform.localScale = scale;

            // Now, align the forward axis
            angle = Mathf.Acos(Vector3.Dot(colliderObj.transform.forward, forward)) * Mathf.Rad2Deg;
            axis = Vector3.Cross(colliderObj.transform.forward, forward);
            colliderObj.transform.localRotation = Quaternion.AngleAxis(angle, axis) * colliderObj.transform.localRotation;
        }

        GetComponent<Renderer>().material.SetMatrix("MyXformMat", m);
        GetComponent<Renderer>().material.SetColor("MyColor", MyColor);
    }
}