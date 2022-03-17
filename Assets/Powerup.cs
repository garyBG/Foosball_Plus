using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Powerup : MonoBehaviour
{
    private bool selectPaddle = false;
    private int currentPlayer;
    private GameObject currentHit;
    public Camera cam;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Paddle")
        {
            powerUp(other.GetComponent<Paddlecollider>().parentNode, other.GetComponent<Paddlecollider>().currentPlayer);
        }
    }

    private void powerUp(SceneNode node, int currentPlayer) {
        if ((currentPlayer == 0 && PhotonNetwork.IsMasterClient)
         || (currentPlayer == 1 && !PhotonNetwork.IsMasterClient))
        {
            selectPaddle = true;
            this.currentPlayer = 1;
        }
        this.GetComponent<MeshRenderer>().enabled = false;

        // // Apply scale
        // node.transform.localScale = new Vector3(
        //     node.transform.localScale.x * 2f, 
        //     node.transform.localScale.y, 
        //     node.transform.localScale.z
        //     );
    }

    void Start() {
        cam = GameObject.Find("CameraP1").GetComponent<Camera>();
    }
    void Update()
    {
        // Logic for selecting and expanding the paddles
        if (selectPaddle)
        {
            // Raycast to get the new ball position
		    RaycastHit cast = new RaycastHit();
		    LayerMask mask = LayerMask.GetMask("Paddle");
		    bool hit = Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out cast, Mathf.Infinity, mask);

		    if (hit 
            && !cast.collider.gameObject.Equals(currentHit)
            && cast.collider.gameObject.GetComponent<Paddlecollider>().currentPlayer == this.currentPlayer)
		    {
                selectNewPaddle(cast.collider.gameObject);
		    }
            else if (!hit && currentHit != null)
            {
                deselectPaddle();
            }

            // Lock in paddle choice
            if (currentHit != null && Input.GetMouseButtonDown(0))
            {
                selectPaddle = false;
                Destroy(this.gameObject);
            }
        }
    }
    void selectNewPaddle(GameObject newHit) {
        deselectPaddle();
        currentHit = newHit;
        transformPaddle(currentHit.GetComponent<Paddlecollider>().parentNode, 2f);        
    }

    void deselectPaddle() {
        if (currentHit != null)
        {
            transformPaddle(currentHit.GetComponent<Paddlecollider>().parentNode, 0.5f);
            currentHit = null;
        }
    }

    void transformPaddle(SceneNode node, float scale) {
        node.transform.localScale = new Vector3(
            node.transform.localScale.x * scale, 
            node.transform.localScale.y, 
            node.transform.localScale.z
        );
    }
}
