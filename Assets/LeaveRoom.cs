using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;

public class LeaveRoom : MonoBehaviour
{
    private RoomCanvases _roomCanvas;
    public PlayerListing pl;
    public void Initialize(RoomCanvases canvases) {
        _roomCanvas = canvases;
    }
    public void OnClick_LeaveRoom() {
    //    // pl._listings.Clear();
    //     Destroy(pl._listings.Last());
        PhotonNetwork.LeaveRoom(true);
        _roomCanvas.InRoom.Hide();
    }
}
