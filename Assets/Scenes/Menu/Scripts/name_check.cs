using UnityEngine;
using Photon.Pun;

public class name_check : MonoBehaviour
{
    public TMPro.TMP_Text name;
    public GameObject tobedisabled;
    public GameObject tobeenabled1;
    public GameObject tobeenabled2;
    public TMPro.TMP_Text setMessage;

    public void check()
    {
        if (name.text == "")
        {
            tobedisabled.SetActive(false);
            tobeenabled1.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetString("PlayerName", name.GetComponent<TMPro.TMP_Text>().text);
            PhotonNetwork.LocalPlayer.NickName = name.GetComponent<TMPro.TMP_Text>().text;
            setMessage.text = PlayerPrefs.GetString("PlayerName") + " , choose a option";
            tobedisabled.SetActive(false);
            tobeenabled2.SetActive(true);
        }

    }
}
