using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListing : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private PL _PlayerListing;
    public List<PL> _listings = new List<PL>();
    private RoomCanvases _roomCanvas;
    private bool _ready = false;
    public override void OnEnable() {
        base.OnEnable();
        //SetReadyUp(false);
        getCurrentRoomPlayers();
    }
     public override void OnDisable() {
        base.OnDisable();
        for(int i = 0; i < _listings.Count; i++) {
            Destroy(_listings[i].gameObject);
        }
        _listings.Clear();
        
    }
    public void Initialize(RoomCanvases canvases)
    {
        _roomCanvas = canvases;
    }
    private void getCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected || PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
            return;
        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }
    }
    private void AddPlayerListing(Player player)
    {
        int index = _listings.FindIndex(x => x._player == player);
        if (index != -1) {
            _listings[index].SetPlayerInfo(player);
        } 
        PL listing = Instantiate(_PlayerListing, _content);
        if (listing != null)
            listing.SetPlayerInfo(player);
        _listings.Add(listing);

    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);

    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listings.FindIndex(x => x._player == otherPlayer);
        if (index != -1)
        {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
    }
    public void OnClick_StartGame() {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel(1);
        // foreach (Player p in PhotonNetwork.PlayerList)
        //     Debug.Log(p);
    }
}
