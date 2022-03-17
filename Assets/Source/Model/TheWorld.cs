using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
public class TheWorld : MonoBehaviour  {
    Player[] pList;
    public GameObject playerCam;
    public SceneNode TheRoot;

    private void Start()
    {
	    if (PhotonNetwork.IsMasterClient) {
            // playerCam.SetActive(true);
            // playerCam1.SetActive(false);
            playerCam.transform.position = new Vector3(18,30,-30);
          //  Instantiate(playerCam, new Vector3(18,30,-30), Quaternion.identity);    
        }
        else{
            playerCam.transform.position = new Vector3(18,30,30);
            //  playerCam.SetActive(false);
            // playerCam1.SetActive(true);
           //  Instantiate(playerCam, new Vector3(18,30,30), Quaternion.identity);    
        }
    }

    private void Update()
    {
        Matrix4x4 i = Matrix4x4.identity;
        TheRoot.CompositeXform(ref i);
    }
}
