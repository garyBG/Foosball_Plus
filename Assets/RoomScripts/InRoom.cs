using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRoom : MonoBehaviour
{
    [SerializeField]
    private PlayerListing _playerListingMenu;
    [SerializeField]
    private LeaveRoom _LeaveRoom;

    // Start is called before the first frame update
   private RoomCanvases _roomsCanvases;
   public void Initialize (RoomCanvases canvases) {
       _roomsCanvases = canvases;
       _playerListingMenu.Initialize(canvases);
       _LeaveRoom.Initialize(canvases);
   }
   public void Show() {
       gameObject.SetActive(true);
   }
   public void Hide() {
       gameObject.SetActive(false);
   }
}
