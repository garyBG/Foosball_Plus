using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListing : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private RL _roomListing;
    private List<RL> _listings = new List<RL>();
    private RoomCanvases _roomsCanvases;
    public void Initialize(RoomCanvases canvases)
    {
        _roomsCanvases = canvases;
    }
    public override void OnJoinedRoom()
    {
        _roomsCanvases.InRoom.Show();
        _content.DestroyChildren();
        _listings.Clear();

    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index != -1)
                {
                    Destroy(_listings[index].gameObject);
                    _listings.RemoveAt(index);
                }
            }
            else
            {
                int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index == -1)
                {
                    RL listing = Instantiate(_roomListing, _content);
                    if (listing != null)
                        listing.SetRoomInfo(info);
                    _listings.Add(listing);
                }

            }
        }

    }
}
