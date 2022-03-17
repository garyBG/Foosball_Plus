using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
/*using UnityEngine.Windows.WebCam;*/
using Slider = UnityEngine.UI.Slider;
using Toggle = UnityEngine.UI.Toggle;

public class CameraControl : MonoBehaviour
{

	private Vector3 mouseDelta = Vector3.zero;
	private Vector3 mouseDownPos = Vector3.zero;
	private float mouseScrollDelta = 0.0f;
	private bool clickedInMe = false;
	public Vector3 target = new Vector3(0f, 0f, 0f);
	public bool followBall = false;

	private Color originalObjectColor;
	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftAlt))
		{
			mouseDownPos = Input.mousePosition;
			mouseDelta = Vector3.zero;
			clickedInMe = this.GetComponent<Camera>().pixelRect.Contains(mouseDownPos);
		}

		if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftAlt) && clickedInMe) // Adjust rotation
		{
			mouseDelta = mouseDownPos - Input.mousePosition;
			mouseDownPos = Input.mousePosition;
			ComputeVerticalOrbit(mouseDelta);
			ComputeHorizontalOrbit(mouseDelta);
		}

		mouseScrollDelta = -Input.mouseScrollDelta.y;

		if (mouseScrollDelta != 0.0f && this.GetComponent<Camera>().pixelRect.Contains(mouseDownPos))
		{
			ProcesssZoom(mouseScrollDelta);
		}

		if (followBall)
		{
			target = GameObject.Find("Hashish").transform.position;
		}

		UpdateCameraRotation();
	}


	private void ComputeVerticalOrbit(Vector3 mouseDelta)
	{
		//Vector3 target = new Vector3(18f, 0f, 0f);

		Quaternion q = Quaternion.AngleAxis(mouseDelta.y * 0.2f, this.transform.right);

		// 2. we need to rotate the camera position
		Matrix4x4 r = Matrix4x4.Rotate(q);
		Matrix4x4 invP = Matrix4x4.TRS(-target, Quaternion.identity, Vector3.one);
		r = invP.inverse * r * invP;
		Vector3 newCameraPos = r.MultiplyPoint(this.transform.localPosition);

		Quaternion newCameraRot = q * this.transform.rotation;
		if (newCameraRot.eulerAngles.x < 80 || newCameraRot.eulerAngles.x > 280)
		{
			this.transform.localPosition = newCameraPos;
			this.transform.rotation = newCameraRot;
		}
	}

	private void ComputeHorizontalOrbit(Vector3 mouseDelta)
	{
		//Vector3 target = new Vector3(0f, 0f, 0f);
		Quaternion q = Quaternion.AngleAxis(-mouseDelta.x * 0.2f, Vector3.up);

		// 2. we need to rotate the camera position
		Matrix4x4 r = Matrix4x4.Rotate(q);
		Matrix4x4 invP = Matrix4x4.TRS(-target, Quaternion.identity, Vector3.one);
		r = invP.inverse * r * invP;
		Vector3 newCameraPos = r.MultiplyPoint(this.transform.localPosition);
		this.transform.localPosition = newCameraPos;

		this.transform.rotation = q * this.transform.rotation;
	}

	private void ProcesssZoom(float delta)
	{
		//Vector3 target = new Vector3(0f, 0f, 0f);
		Vector3 v = target - this.transform.localPosition;
		float dist = v.magnitude;
		dist += delta;
		this.transform.localPosition = target - dist * v.normalized;
	}

	private void UpdateCameraRotation()
	{
		//Vector3 target = new Vector3(0f, 0f, 0f);
		Vector3 cameradir = target - this.transform.position;
		this.transform.forward = cameradir;
	}
}