using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Photon.Pun;

public class SceneNodeTransform : MonoBehaviourPunCallbacks
{
    public float maxSlide = 1.15f;
    private int selectedPaddle = 0, currentPlayer = 0;
    public int currentPaddle, currentPaddlePlayer;
    private Quaternion defaultRotation;
	PhotonView PV;
    private Vector3 defaultPosition;
    void Start()
    {
		PV = GetComponent<PhotonView>();
        defaultRotation = transform.localRotation;
        defaultPosition = transform.localPosition;
        currentPlayer = PhotonNetwork.IsMasterClient ? 0 : 1;
    }
    // Update is called once per frame
    void Update()
    {
        // Check if current paddle and player
        if (currentPaddle == selectedPaddle && currentPaddlePlayer == currentPlayer)
        {
            if (Input.GetKey(KeyCode.W))
            {
                movePaddle(0.1f);
            }
            if (Input.GetKey(KeyCode.A))
            {
                rotatePaddle(-8.8f);
            }
            if (Input.GetKey(KeyCode.S))
            {
                movePaddle(-0.1f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rotatePaddle(8.8f);
            }
        }

        // Switch selected paddle
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            selectedPaddle = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            selectedPaddle = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            selectedPaddle = 2;
        }
    }
	[PunRPC]
	void movePaddleRPC(float moveAmt) {
		GameObject paddle = PhotonView.Find(this.photonView.ViewID).gameObject;
		Vector3 slide = Vector3.zero;
        slide.z = moveAmt;
        if (Mathf.Abs(paddle.transform.localPosition.z + moveAmt) < maxSlide)
        {
            paddle.transform.localPosition += slide;
        }
	}
	[PunRPC]
	void rotatePaddleRPC(float rotateAmt) {
		GameObject paddle = PhotonView.Find(this.photonView.ViewID).gameObject;
		Quaternion q = Quaternion.AngleAxis(rotateAmt, Vector3.up);
		paddle.transform.localRotation *= q;
	}
    public void movePaddle(float moveAmt)
    {
        moveAmt *= Time.deltaTime * 60;
		if (currentPlayer == 1)
		{
			moveAmt *= -1;
		}
		Vector3 slide = Vector3.zero;
			slide.z = moveAmt;
			if (Mathf.Abs(transform.localPosition.z + moveAmt) < maxSlide)
			{
				this.transform.localPosition += slide;
			}
		if (!PhotonNetwork.IsMasterClient) {
			PV.RPC("movePaddleRPC",RpcTarget.MasterClient, moveAmt);
		}
    }

    public void rotatePaddle(float rotateAmt)
	{
        rotateAmt *= Time.deltaTime * 60;
		if (currentPlayer == 1)
		{
			rotateAmt *= -1;
		}
		Quaternion q = Quaternion.AngleAxis(rotateAmt, Vector3.up);
		this.transform.localRotation *= q;
		if(!PhotonNetwork.IsMasterClient ){
			PV.RPC("rotatePaddleRPC",RpcTarget.MasterClient, rotateAmt);
		}
    }
    public void reset()
    {
        transform.localRotation = defaultRotation;
        transform.localPosition = defaultPosition;
    }
}
