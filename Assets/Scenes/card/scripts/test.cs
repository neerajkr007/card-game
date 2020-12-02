using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System;

public class test : MonoBehaviour
{
    public cardViz[] cardviz;
    public card_class[] card;
    public card_class[] randomcards;
    public GameObject[] cards;
    public PhotonView pv;
    public Button[] trait_buttons;
    private int turn = -1;
    private int nextturn = -1;
    public TMPro.TMP_Text[] names;
    private bool showcardstoall = false;
     
    private int p = 0, l = -1;
    public GameObject traits;
    private bool[] clicked = new bool[4] { false, false, false, false};


    [PunRPC]
    void RPC_turn(int n)
    {
        turn = n;
        Debug.Log("hmmm");
        if (PhotonNetwork.PlayerList[turn].IsLocal)
        {
            Debug.Log("gg");
            Invoke("turns", 0f);
            Invoke("traitclicks", 0f);
        }
    }

    public void Start()
    {
        Invoke("placeNames", 0.5f);
        //Debug.Log(p + " p");
    }
    public void placeNames()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            names[0].text = PhotonNetwork.PlayerList[0].NickName;
            names[1].text = PhotonNetwork.PlayerList[1].NickName;
        }

        if (PhotonNetwork.CurrentRoom.PlayerCount == 3)
        {
            names[0].text = PhotonNetwork.PlayerList[0].NickName;
            names[1].text = PhotonNetwork.PlayerList[1].NickName;
            names[2].text = PhotonNetwork.PlayerList[2].NickName;
        }

        if (PhotonNetwork.CurrentRoom.PlayerCount == 4)
        {
            names[0].text = PhotonNetwork.PlayerList[0].NickName;
            names[1].text = PhotonNetwork.PlayerList[1].NickName;
            names[2].text = PhotonNetwork.PlayerList[2].NickName;
            names[2].text = PhotonNetwork.PlayerList[3].NickName;
        }
    }
    public void distribute(int j)
    {
        randomcards[p] = card[j];
        p++;
    }

    public void traitclicks()
    {
        if (PhotonNetwork.PlayerList[turn].IsLocal)
        {
            //Debug.Log(turn);
            trait_buttons[0].onClick.AddListener(strength);
            trait_buttons[1].onClick.AddListener(speed);
            trait_buttons[2].onClick.AddListener(life);
            trait_buttons[3].onClick.AddListener(fc);
        }
    }
    
    [PunRPC]
    void RPC_randomizecards()
    {
        if (PhotonNetwork.LocalPlayer.IsLocal)
        {
            //Debug.Log(p + " p2");
            Debug.Log(" randomcards");
            if (p == card.Length / PhotonNetwork.CurrentRoom.PlayerCount)
            {
                //dispCards();
                Invoke("dispCards", 0f);
            }
        }
        
    }

    public void dispCards()
    {
        if (PhotonNetwork.LocalPlayer.IsLocal)
        {
        found:
            l = UnityEngine.Random.Range(0, card.Length / PhotonNetwork.CurrentRoom.PlayerCount);
            if (randomcards[l] != null)
            {
                Debug.Log(randomcards[l].cardId+" l id");
                cardviz[0].card = randomcards[l];
                cards[0].SetActive(true);
                Debug.Log(" dispcardscards");
                sendrpcs();
                Invoke("hidecards", 1);
            }
            else
            {
                goto found;
            }
        }
    }

    public void hidecards()
    {
        cards[0].SetActive(false);
    }

    public void sendrpcs()
    {
        if (PhotonNetwork.PlayerList[0].IsLocal)// & !PhotonNetwork.PlayerList[turn].IsLocal)
        {
            pv.RPC("RPC_compareothers0", RpcTarget.AllBuffered, cardviz[0].card.cardId);
            //cards[0].SetActive(false);
        }

        if (PhotonNetwork.PlayerList[1].IsLocal)// & !PhotonNetwork.PlayerList[turn].IsLocal)
        {
            pv.RPC("RPC_compareothers1", RpcTarget.AllBuffered, cardviz[0].card.cardId);
            //cards[0].SetActive(false);
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount > 2)
        {
            if (PhotonNetwork.PlayerList[2].IsLocal)// & !PhotonNetwork.PlayerList[turn].IsLocal)
            {
                pv.RPC("RPC_compareothers2", RpcTarget.AllBuffered, cardviz[0].card.cardId);
                //[0].SetActive(false);
            }
        }

        if (PhotonNetwork.CurrentRoom.PlayerCount > 3)
        {
            if (PhotonNetwork.PlayerList[3].IsLocal)// & !PhotonNetwork.PlayerList[turn].IsLocal)
            {
                pv.RPC("RPC_compareothers3", RpcTarget.AllBuffered, cardviz[0].card.cardId);
                //cards[0].SetActive(false);
            }
        }
    }

    public void testing()
    {
        if(PhotonNetwork.LocalPlayer.IsLocal)
        {
            var num = randomcards.ToList();
            //var b = cardviz.ToList();
            int n = cardviz[0].card.cardId;
            Debug.Log(n);
            try 
            {
                for (int i = 0; i < card.Length / PhotonNetwork.CurrentRoom.PlayerCount; i++)
                {
                    if (randomcards[i].cardId == n)
                    {
                        num.RemoveAt(i);
                        randomcards = num.ToArray();
                        RPC_randomizecards();
                        if (PhotonNetwork.PlayerList[turn].IsLocal)
                        {
                            Debug.Log("gg");
                            Invoke("turns", 0f);
                            Invoke("traitclicks", 0f);
                        }
                        return;
                    }
                }
            }

            catch (NullReferenceException) { }
            catch (IndexOutOfRangeException) { }
            
            
        }
    }

    public void turns()
    {
        //Debug.Log(turn);
        if (PhotonNetwork.PlayerList[turn].IsLocal)
        {
            traits.SetActive(true);
        }
        else
        {
            traits.SetActive(false);
        }
    }

    public void strength()
    {
        pv.RPC("RPC_showallcards", RpcTarget.AllBuffered);
        if (PhotonNetwork.PlayerList[turn].IsLocal)
        {
            clicked[0] = true;
            pv.RPC("RPC_compareturn", RpcTarget.MasterClient, clicked[0], clicked[1], clicked[2], clicked[3]);
            traits.SetActive(false);
            clicked[0] = false;
        }
        
    }

    public void speed()
    {
        pv.RPC("RPC_showallcards", RpcTarget.AllBuffered);
        if (PhotonNetwork.PlayerList[turn].IsLocal)
        {
            clicked[1] = true;
            pv.RPC("RPC_compareturn", RpcTarget.MasterClient, clicked[0], clicked[1], clicked[2], clicked[3]);
            traits.SetActive(false);
            clicked[1] = false;
        }
    }

    public void life()
    {
        pv.RPC("RPC_showallcards", RpcTarget.AllBuffered);
        if (PhotonNetwork.PlayerList[turn].IsLocal)
        {
            clicked[2] = true;
            pv.RPC("RPC_compareturn", RpcTarget.MasterClient, clicked[0], clicked[1], clicked[2], clicked[3]);
            traits.SetActive(false);
            clicked[2] = false;
        }
    }

    public void fc()
    {
        pv.RPC("RPC_showallcards", RpcTarget.AllBuffered);
        if (PhotonNetwork.PlayerList[turn].IsLocal)
        {
            clicked[3] = true;
            pv.RPC("RPC_compareturn", RpcTarget.MasterClient, clicked[0], clicked[1], clicked[2], clicked[3]);
            traits.SetActive(false);
            clicked[3] = false;
        }
    }

    [PunRPC]
    void RPC_sendcards1(int cardid)
    {
        //Debug.Log(cardid + " 1");
        if (PhotonNetwork.PlayerList[0].IsLocal)
        {
            distribute(cardid);
            //distribute(cardid);
        }
    }

    [PunRPC]
    void RPC_sendcards2(int cardid)
    {
        
        if (PhotonNetwork.PlayerList[1].IsLocal)
        {
            distribute(cardid);
        }

    }

    [PunRPC]
    void RPC_sendcards3(int cardid)
    {
        if (PhotonNetwork.PlayerList[2].IsLocal)
        {
            distribute(cardid);
        }

    }

    [PunRPC]
    void RPC_sendcards4(int cardid)
    { 
        if (PhotonNetwork.PlayerList[3].IsLocal)
        {
            distribute(cardid);
        }

    }

    [PunRPC]
    void RPC_nextturn(int n)
    {
        turn = n;
        Debug.Log(turn + " turn2");
        testing();
    }
    
}
