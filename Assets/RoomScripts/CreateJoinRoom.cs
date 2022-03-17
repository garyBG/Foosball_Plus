using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateJoinRoom : MonoBehaviour
{
     [SerializeField]
     private RoomListing _roomListings;
    [SerializeField]
    private CreateRoom _createRoom;
    // Start is called before the first frame update
   private RoomCanvases _roomsCanvases;
   public void Initialize (RoomCanvases canvases) {
       _roomsCanvases = canvases;
       _createRoom.Initialize(canvases);
       _roomListings.Initialize(canvases);
   }

   public void Show() {
       gameObject.SetActive(true);
   }
   public void Hide() {
       gameObject.SetActive(false);
   }
}
