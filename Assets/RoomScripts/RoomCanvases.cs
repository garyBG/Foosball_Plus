using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCanvases : MonoBehaviour
{
    [SerializeField]
    private CreateJoinRoom _createJoinRoom;
    public CreateJoinRoom createJoinRoom {get{return _createJoinRoom;}}
  [SerializeField]
    private InRoom _InRoom;
    public InRoom InRoom {get{return _InRoom;}}
    private void Awake() {
        FirstInitialize();
    }
    private void FirstInitialize(){
        createJoinRoom.Initialize(this);
        InRoom.Initialize(this);
    }
}
