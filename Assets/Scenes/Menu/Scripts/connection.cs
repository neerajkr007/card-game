using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class connection : MonoBehaviourPunCallbacks, IMatchmakingCallbacks
{
    //public TMPro.TMP_Text msg1;
    //public TMPro.TMP_Text msg2;
    public GameObject tobeenabled;
    public GameObject tobedisabled;
    public void Start()
    {
        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        tobedisabled.SetActive(false);
        tobeenabled.SetActive(true);
        print("connected to server.");
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("disconnected from server coz " + cause.ToString());
        //msg2.text = "disconnected coz " + cause;
    }
}
