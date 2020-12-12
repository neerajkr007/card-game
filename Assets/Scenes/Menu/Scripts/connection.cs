using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class connection : MonoBehaviourPunCallbacks, IMatchmakingCallbacks
{
    //public TMPro.TMP_Text msg1;
    //public TMPro.TMP_Text msg2;
    public GameObject tobeenabled1;
    public GameObject tobeenabled2;
    public GameObject tobedisabled;
    int hasPlayed;
    public TMPro.TMP_Text setMessage;


    public void Start()
    {
        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.ConnectUsingSettings();
        hasPlayed = PlayerPrefs.GetInt("HasPlayed");
    }

    public override void OnConnectedToMaster()
    {
        //print(hasPlayed);
        print("connected to server.");
        PhotonNetwork.AutomaticallySyncScene = true;
        if (hasPlayed == 0)
        {
            PlayerPrefs.SetInt("HasPlayed", 1);
            tobedisabled.SetActive(false);
            tobeenabled1.SetActive(true);
        }
        else
        {
            if (PlayerPrefs.GetString("PlayerName") == "")
                PlayerPrefs.SetString("PlayerName", "PCdebug");
            PhotonNetwork.LocalPlayer.NickName = PlayerPrefs.GetString("PlayerName");
            print(PhotonNetwork.LocalPlayer.NickName);
            setMessage.text = PlayerPrefs.GetString("PlayerName") + " , choose a option";
            tobedisabled.SetActive(false);
            tobeenabled2.SetActive(true);
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("disconnected from server coz " + cause.ToString());
        //msg2.text = "disconnected coz " + cause;
    }
}
