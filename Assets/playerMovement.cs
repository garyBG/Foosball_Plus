using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    CharacterController cc;
    float pitch = 0f;
    public Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        cc = this.GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
        // Debug.Log(IsLocalPlayer);
        // if (!IsLocalPlayer) {
        //     cameraTransform.GetComponent<AudioListener>().enabled = false;
        //     cameraTransform.GetComponent<Camera>().enabled = false;

        // }
        // else {
        //     cc = this.GetComponent<CharacterController>();
        //     Cursor.lockState = CursorLockMode.Locked;
        // }

        
    }
    
    // Update is called once per frame
    void Update()
    {
        Look();
        // if (IsOwner) {
        //     //MovePlayer();
        //     LookClientRpc();
        // }
    }

    void MovePlayer() {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = Vector3.ClampMagnitude(move,1f);
        cc.SimpleMove(move);

    }
    void Look () {
        Debug.Log(Input.GetMouseButtonDown(0));
        float mousex = Input.GetAxis("Mouse X") * 3f;
        transform.Rotate(0,mousex,0);
        pitch -= Input.GetAxis("Mouse Y") * 3f;
        pitch = Mathf.Clamp(pitch, -45f,45f);
        cameraTransform.localRotation = Quaternion.Euler(pitch,0,0);
        

    }
}
