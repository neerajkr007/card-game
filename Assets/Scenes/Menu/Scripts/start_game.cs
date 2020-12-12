using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class start_game : MonoBehaviour
{
    public Slider slider;
    public GameObject tobeenabled;
    public GameObject tobedisabled;
    public GameObject button;

    private void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            button.SetActive(true);
        }
    }
    public void check()
    {
        //if (PhotonNetwork.CurrentRoom.PlayerCount == slider.value)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            tobedisabled.SetActive(false);
            tobeenabled.SetActive(true);
        }
    }
}
