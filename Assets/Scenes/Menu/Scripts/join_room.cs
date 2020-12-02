using Photon.Pun;
using UnityEngine;


public class join_room : MonoBehaviourPunCallbacks
{
    public TMPro.TMP_InputField id;
    public TMPro.TMP_Text playername;
    public TMPro.TMP_Text status;
    public TMPro.TMP_Text disp_id;
    public GameObject tobeenabled01;
    public GameObject tobeenabled02;
    public GameObject tobedisabled0;

    public void join()
    {
        Debug.Log(id.text);
        PhotonNetwork.JoinRoom(id.text);
        disp_id.text = "room id - " + id.text;
        Debug.Log(disp_id.text);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("joined the room");
        status.text = "joined the room";
        PhotonNetwork.LocalPlayer.NickName = playername.GetComponent<TMPro.TMP_Text>().text;
        tobedisabled0.SetActive(false);
        tobeenabled01.SetActive(true);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("failed coz " + message);
        status.text = message;
        //msg2.text = "failed coz " + message;
        tobedisabled0.SetActive(false);
        tobeenabled02.SetActive(true);
    }
}