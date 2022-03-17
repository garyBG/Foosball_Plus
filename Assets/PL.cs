using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class PL : MonoBehaviour
{
    [SerializeField]
    private Text _text;
    public Player _player;
    
    public void SetPlayerInfo (Player player) {
        _player = player;
        _text.text = player.NickName;
    }

}
