using Photon.Pun;
using System;
using UnityEngine;

public class Joined_names : MonoBehaviour
{
    public TMPro.TMP_Text[] player;
    private int i;
    //private int maxplayers = 4; 
    public void Update()
    {
        //Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.InRoom)
        {
            for (i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
            {
                player[i].text = PhotonNetwork.PlayerList[i].NickName + " has joined!";
                
                for (int j = PhotonNetwork.CurrentRoom.PlayerCount ; j<4 ; j++)
                {
                    player[j].text ="";
                }
            }
        }
    }
}
