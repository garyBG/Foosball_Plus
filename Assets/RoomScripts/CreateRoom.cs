using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
public class CreateRoom : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text _roomName;

      private CreateRoom _createRoom;
    // Start is called before the first frame update
   private RoomCanvases _roomsCanvases;
   public void Initialize (RoomCanvases canvases) {
       _roomsCanvases = canvases;
   }
    public void OnClick_CreateRoom () {
        RoomOptions rm = new RoomOptions();
        rm.MaxPlayers = 2; 
        if (!PhotonNetwork.IsConnected)
            return;
        if (_roomName.text == string.Empty)
            return;
        PhotonNetwork.JoinOrCreateRoom(_roomName.text,rm,TypedLobby.Default); 
    }
    public override void OnCreatedRoom() {
        Debug.Log("Created Room Succesfully");
        _roomsCanvases.InRoom.Show();
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Created Room Failed");
       
    }
}
