using Photon.Pun;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Collections;

public class networkScript : MonoBehaviour
{
    private int turn = -1, p = 0, l = -1;
    public card_class[] ogCards;
    public cardViz[] cardviz;
    public GameObject[] displayableCard;
    public GameObject traits;
    public Button[] traitButtons;
    public TMPro.TMP_Text[] names;
    public bool[] clicked = new bool[4] { false, false, false, false };
    public PhotonView pv;
    public card_class[] randomcards = new card_class[0];
    public int round = 0, n = -1;
    //TimeSpan timeout = new TimeSpan(0, 0, 2);

    private void Start()
    {
        //Debug.Log(cardsperplayer);
        placeNames();
    }

    public void distribute(int j)
    {
        //Debug.Log("distribute clear");
        Array.Resize(ref randomcards, randomcards.Length + 1);
        randomcards[p] = ogCards[j];
        //randomCardvalues.Add(j);
        p++;
        //Debug.Log(p + " p");
        if(p == ogCards.Length / PhotonNetwork.CurrentRoom.PlayerCount)
        {
            //Debug.Log("runs");
            StartCoroutine(displayCards(0));

        }
    }

    IEnumerator displayCards(int t)
    {
        yield return new WaitForSeconds(t);
        Debug.Log("displaycards clear");
        l = UnityEngine.Random.Range(0, (ogCards.Length / PhotonNetwork.CurrentRoom.PlayerCount)-round);
        Debug.Log(l + " l");
        if (PhotonNetwork.PlayerList[1].IsLocal)
        {
            cardviz[round].card = randomcards[l];
            displayableCard[round].SetActive(true);
        }
            Debug.Log("rpc just about to be sent");

            sendrpcs();
        
    }
    
    void sendrpcs()
    {
        Debug.Log("rpcs clear");
        if (PhotonNetwork.PlayerList[0].IsLocal)
        {
            pv.RPC("RPC_compareothers0", RpcTarget.AllBuffered, cardviz[round].card.cardId);
        }

        if (PhotonNetwork.PlayerList[1].IsLocal)
        {
            pv.RPC("RPC_compareothers1", RpcTarget.AllBuffered, cardviz[round].card.cardId);
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount > 2)
        {
            if (PhotonNetwork.PlayerList[2].IsLocal)
            {
                pv.RPC("RPC_compareothers2", RpcTarget.AllBuffered, cardviz[round].card.cardId);
            }
        }

        if (PhotonNetwork.CurrentRoom.PlayerCount > 3)
        {
            if (PhotonNetwork.PlayerList[3].IsLocal)
            {
                pv.RPC("RPC_compareothers3", RpcTarget.AllBuffered, cardviz[round].card.cardId);
            }
        }
    }

    void turns()
    {
        if (PhotonNetwork.PlayerList[turn].IsLocal)
        {
            traits.SetActive(true);
        }
        else
        {
            traits.SetActive(false);
        }
    }

    void traitclicks()
    {
        traitButtons[0].onClick.AddListener(strength);
        traitButtons[1].onClick.AddListener(speed);
        traitButtons[2].onClick.AddListener(life);
        traitButtons[3].onClick.AddListener(fc);
    }

    void nextCards()
    {
        Debug.Log(round+" round " + PhotonNetwork.LocalPlayer.NickName);
        var num = randomcards.ToList();
        if(PhotonNetwork.PlayerList[1].IsLocal)
        {
            n = cardviz[round - 1].card.cardId;
            Debug.Log(n);
        }
        for (int q = 0; q <= (ogCards.Length / PhotonNetwork.CurrentRoom.PlayerCount) - round; q++)
        {
            Debug.Log(randomcards[q]);
            Debug.Log(" q " + q);
            if(randomcards[q].cardId == n)
            {
                Debug.Log("found " + q);
                num.RemoveAt(q);
                //randomCardvalues.Remove(q);
                randomcards = num.ToArray();
                //Debug.Log(randomcards.Length);
                Debug.Log("displaycards about to be run");
                StartCoroutine(displayCards(0));
                return;
            }
            
        }
        //Debug.Log("nextcards runs");
        //Debug.Log(n);
        //for (int i = 0; i < (ogCards.Length / PhotonNetwork.CurrentRoom.PlayerCount); i++)
        //foreach (int i in randomCardvalues)
        //{
        //Debug.Log(randomCardvalues.ToArray());
        //Debug.Log(i);
        //if (i == n-1)
        //{

        //}
        //}
    }

    void placeNames()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            if (PhotonNetwork.PlayerList[0].IsLocal)
            {
                names[0].text = PhotonNetwork.PlayerList[0].NickName;
                names[1].text = PhotonNetwork.PlayerList[1].NickName;
            }

            if (PhotonNetwork.PlayerList[1].IsLocal)
            {
                names[0].text = PhotonNetwork.PlayerList[1].NickName;
                names[1].text = PhotonNetwork.PlayerList[0].NickName;
            }
        }

        if (PhotonNetwork.CurrentRoom.PlayerCount == 3)
        {
            if (PhotonNetwork.PlayerList[0].IsLocal)
            {
                names[0].text = PhotonNetwork.PlayerList[0].NickName;
                names[1].text = PhotonNetwork.PlayerList[1].NickName;
                names[2].text = PhotonNetwork.PlayerList[2].NickName;
            }
            if (PhotonNetwork.PlayerList[1].IsLocal)
            {
                names[0].text = PhotonNetwork.PlayerList[1].NickName;
                names[1].text = PhotonNetwork.PlayerList[2].NickName;
                names[2].text = PhotonNetwork.PlayerList[0].NickName;
            }
            if (PhotonNetwork.PlayerList[2].IsLocal)
            {
                names[0].text = PhotonNetwork.PlayerList[2].NickName;
                names[1].text = PhotonNetwork.PlayerList[0].NickName;
                names[2].text = PhotonNetwork.PlayerList[1].NickName;
            }
        }

        if (PhotonNetwork.CurrentRoom.PlayerCount == 4)
        {
            if (PhotonNetwork.PlayerList[0].IsLocal)
            {
                names[0].text = PhotonNetwork.PlayerList[0].NickName;
                names[1].text = PhotonNetwork.PlayerList[1].NickName;
                names[2].text = PhotonNetwork.PlayerList[2].NickName;
                names[3].text = PhotonNetwork.PlayerList[3].NickName;
            }
            if (PhotonNetwork.PlayerList[1].IsLocal)
            {
                names[0].text = PhotonNetwork.PlayerList[1].NickName;
                names[1].text = PhotonNetwork.PlayerList[2].NickName;
                names[2].text = PhotonNetwork.PlayerList[3].NickName;
                names[3].text = PhotonNetwork.PlayerList[0].NickName;
            }
            if (PhotonNetwork.PlayerList[2].IsLocal)
            {
                names[0].text = PhotonNetwork.PlayerList[2].NickName;
                names[1].text = PhotonNetwork.PlayerList[3].NickName;
                names[2].text = PhotonNetwork.PlayerList[0].NickName;
                names[3].text = PhotonNetwork.PlayerList[1].NickName;
            }
            if (PhotonNetwork.PlayerList[3].IsLocal)
            {
                names[0].text = PhotonNetwork.PlayerList[3].NickName;
                names[1].text = PhotonNetwork.PlayerList[0].NickName;
                names[2].text = PhotonNetwork.PlayerList[1].NickName;
                names[3].text = PhotonNetwork.PlayerList[2].NickName;
            }
        }
    }

    void strength()
    {
        string playerName = PhotonNetwork.PlayerList[turn].NickName, trait = "strength";
        traits.SetActive(false);
        pv.RPC("RPC_showallcards", RpcTarget.All);
        if (PhotonNetwork.PlayerList[turn].IsLocal)
        {
            clicked[0] = true;
            pv.RPC("RPC_chosenTrait", RpcTarget.AllBuffered, playerName, trait);
            pv.RPC("RPC_compareturn", RpcTarget.MasterClient, clicked[0], clicked[1], clicked[2], clicked[3]);
            clicked[0] = false;
        }
    }

    void speed()
    {
        string playerName = PhotonNetwork.PlayerList[turn].NickName, trait = "speed";
        pv.RPC("RPC_showallcards", RpcTarget.All);
        if (PhotonNetwork.PlayerList[turn].IsLocal)
        {
            clicked[1] = true;
            pv.RPC("RPC_chosenTrait", RpcTarget.AllBuffered, playerName, trait);
            traits.SetActive(false);
            //Thread.Sleep(timeout);
            pv.RPC("RPC_compareturn", RpcTarget.MasterClient, clicked[0], clicked[1], clicked[2], clicked[3]);
            clicked[1] = false;
        }
    }

    void life()
    {
        string playerName = PhotonNetwork.PlayerList[turn].NickName, trait = "life";
        pv.RPC("RPC_showallcards", RpcTarget.All);
        if (PhotonNetwork.PlayerList[turn].IsLocal)
        {
            clicked[2] = true;
            pv.RPC("RPC_chosenTrait", RpcTarget.AllBuffered, playerName, trait);
            traits.SetActive(false);
            //Thread.Sleep(timeout);
            pv.RPC("RPC_compareturn", RpcTarget.MasterClient, clicked[0], clicked[1], clicked[2], clicked[3]);
            clicked[2] = false;
        }
    }

    void fc()
    {
        string playerName = PhotonNetwork.PlayerList[turn].NickName , trait = "fc";
        traits.SetActive(false);
        pv.RPC("RPC_showallcards", RpcTarget.All);
        if (PhotonNetwork.PlayerList[turn].IsLocal)
        {
            clicked[3] = true;
            pv.RPC("RPC_chosenTrait", RpcTarget.AllBuffered, playerName, trait);
            //Thread.Sleep(timeout);
            pv.RPC("RPC_compareturn", RpcTarget.MasterClient, clicked[0], clicked[1], clicked[2], clicked[3]);
            clicked[3] = false;
        }
    }

    IEnumerator gameover()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    [PunRPC]
    void RPC_initialturn(int n)
    {
        //Debug.Log("initialturn clear");
        traitclicks();
        turn = n;
        if (PhotonNetwork.PlayerList[turn].IsLocal)
        {
            turns();
            
        }
    }

    [PunRPC]
    void RPC_nextturn0(int n, int r)
    {
        if (PhotonNetwork.PlayerList[0].IsLocal)
        {
            Debug.Log("next card about to run");
            round = r;
            turn = n;
            if (round == ogCards.Length / PhotonNetwork.CurrentRoom.PlayerCount)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                nextCards();
                if (PhotonNetwork.PlayerList[turn].IsLocal)
                {
                    turns();
                }
            }
        }

        if (PhotonNetwork.PlayerList[1].IsLocal)
        {
            Debug.Log("next card about to run");
            round = r;
            turn = n;
            if (round == ogCards.Length / PhotonNetwork.CurrentRoom.PlayerCount)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                nextCards();
                if (PhotonNetwork.PlayerList[turn].IsLocal)
                {
                    turns();
                }
            }
        }

        if (PhotonNetwork.PlayerList[2].IsLocal)
        {
            Debug.Log("next card about to run");
            round = r;
            turn = n;
            if (round == ogCards.Length / PhotonNetwork.CurrentRoom.PlayerCount)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                nextCards();
                if (PhotonNetwork.PlayerList[turn].IsLocal)
                {
                    turns();
                }
            }
        }
    }

    [PunRPC]
    void RPC_nextturn1(int n, int r)
    {
        if (PhotonNetwork.PlayerList[1].IsLocal)
        {
            Debug.Log("next card about to run");
            round = r;
            turn = n;
            if (round == ogCards.Length / PhotonNetwork.CurrentRoom.PlayerCount)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                nextCards();
                if (PhotonNetwork.PlayerList[turn].IsLocal)
                {
                    turns();
                }
            }
        }
    }

    [PunRPC]
    void RPC_nextturn2(int n, int r)
    {
        if (PhotonNetwork.PlayerList[2].IsLocal)
        {
            Debug.Log("next card about to run");
            round = r;
            turn = n;
            if (round == ogCards.Length / PhotonNetwork.CurrentRoom.PlayerCount)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                nextCards();
                if (PhotonNetwork.PlayerList[turn].IsLocal)
                {
                    turns();
                }
            }
        }
    }

    [PunRPC]
    void RPC_nextturn3(int n, int r)
    {
        if (PhotonNetwork.PlayerList[3].IsLocal)
        {
            Debug.Log("next card about to run");
            round = r;
            turn = n;
            if (round == ogCards.Length / PhotonNetwork.CurrentRoom.PlayerCount)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                nextCards();
                if (PhotonNetwork.PlayerList[turn].IsLocal)
                {
                    turns();
                }
            }
        }
    }

    [PunRPC]
    void RPC_sendcards1(int id)
    {
        if(PhotonNetwork.PlayerList[0].IsLocal)
        {
            //Debug.Log("sendcards clear");
            distribute(id);
        }
    }

    [PunRPC]
    void RPC_sendcards2(int id)
    {
        if (PhotonNetwork.PlayerList[1].IsLocal)
        {
            //Debug.Log("sendcards clear");
            distribute(id);
        }
    }

    [PunRPC]
    void RPC_sendcards3(int id)
    {
        if (PhotonNetwork.PlayerList[2].IsLocal)
        {
            //Debug.Log("sendcards clear");
            distribute(id);
        }
    }

    [PunRPC]
    void RPC_sendcards4(int id)
    {
        if (PhotonNetwork.PlayerList[3].IsLocal)
        {
            //Debug.Log("sendcards clear");
            distribute(id);
        }
    }
    
}
