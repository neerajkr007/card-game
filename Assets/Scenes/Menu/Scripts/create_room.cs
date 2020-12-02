using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class create_room : MonoBehaviourPunCallbacks
{
    private string id;
    public TMPro.TMP_Text disp_id;
    public TMPro.TMP_Text playername;
    public TMPro.TMP_Text value;
    public Slider slider;
    public GameObject tobeenabled;
    public GameObject tobedisabled;

    //public TMPro.TMP_Text msg1;
    //public TMPro.TMP_Text msg2;

    public void gen_id()
    {
        id = Random.Range(1000, 9999).ToString();
        disp_id.text = "room id - " + id.ToString();
    }
    public void createroom()
    {
       
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = (byte)slider.value;
        PhotonNetwork.CreateRoom(id, options, TypedLobby.Default);
        //Debug.Log(id);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("created successfully");
        PhotonNetwork.LocalPlayer.NickName = playername.GetComponent<TMPro.TMP_Text>().text;
        tobedisabled.SetActive(false);
        tobeenabled.SetActive(true);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("room creation failed coz " + message);
    }

    public void Update()
    {
        value.text = slider.value.ToString();
    }

}
