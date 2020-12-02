using UnityEngine;
using Photon.Pun;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System;
using System.Reflection;

public class shuffleCards : MonoBehaviourPunCallbacks
{
    public card_class[] cards;
    public card_class[] player1;
    public card_class[] player2;
    public card_class[] player3;
    public card_class[] player4;
    public cardViz[] cardviz1;
    public GameObject[] showcard1;
    public cardViz[] cardviz2;
    public GameObject[] showcard2;
    public cardViz[] cardviz3;
    public GameObject[] showcard3;
    public cardViz[] cardviz4;
    public GameObject[] showcard4;
    int j, r = 0;
    int a = 0, b = 0, c = 0, d = 0;
    public PhotonView pv;
    public PhotonView pv1;
    private int n = -1, m = -1, o = -1, p = -1;
    private int turn = -1;
    public TMPro.TMP_Text won;


    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            turn = UnityEngine.Random.Range(0, PhotonNetwork.CurrentRoom.PlayerCount);
            pv1.RPC("RPC_initialturn", RpcTarget.AllBuffered, turn);
            //pv2.RPC("RPC_turn", RpcTarget.AllBuffered, turn);
            //pv3.RPC("RPC_turn", RpcTarget.AllBuffered, turn);
            //pv4.RPC("RPC_turn", RpcTarget.AllBuffered, turn);
            for (int i = 0; i < cards.Length; i++)
            {
                j = UnityEngine.Random.Range(0, PhotonNetwork.CurrentRoom.PlayerCount);
                switch (j)
                {
                    case 0:
                        if (a < cards.Length / PhotonNetwork.CurrentRoom.PlayerCount)
                        {
                            Debug.Log((i + 1) + " a");
                            player1[a] = cards[i];
                            pv1.RPC("RPC_sendcards1", RpcTarget.AllBuffered, i); 
                            a++;
                        }
                        else
                        {
                            goto case 1;
                        }
                        break;
                    case 1:
                        if (b < cards.Length / PhotonNetwork.CurrentRoom.PlayerCount)
                        {
                            Debug.Log((i + 1) + " b");
                            //Invoke("wait", 1);
                            player2[b] = cards[i];
                            pv1.RPC("RPC_sendcards2", RpcTarget.AllBuffered, i);
                            b++;
                        }
                        else if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
                        {
                            goto case 0;
                        }
                        else
                        {
                            goto case 2;
                        }
                        break;
                    case 2:
                        if (c < cards.Length / PhotonNetwork.CurrentRoom.PlayerCount)
                        {
                            Debug.Log((i + 1) + " c");
                            player3[c] = cards[i];
                            pv1.RPC("RPC_sendcards3", RpcTarget.AllBuffered, i);
                            c++;
                        }
                        else if (PhotonNetwork.CurrentRoom.PlayerCount == 3)
                        {
                            goto case 0;
                        }
                        else
                        {
                            goto case 3;
                        }
                        break;
                    case 3:
                        if (d < cards.Length / PhotonNetwork.CurrentRoom.PlayerCount)
                        {
                            Debug.Log((i + 1) + " d");
                            pv1.RPC("RPC_sendcards4", RpcTarget.AllBuffered, i);
                            player4[d] = cards[i];
                            d++;
                        }
                        else
                        {
                            goto case 0;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    IEnumerator deleteCards()
    {
        yield return new WaitForSeconds(5);
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            showcard1[r-1].SetActive(false);
            showcard2[r-1].SetActive(false);
        }

        if (PhotonNetwork.CurrentRoom.PlayerCount == 3)
        {
            showcard1[r-1].SetActive(false);
            showcard2[r-1].SetActive(false);
            showcard3[r-1].SetActive(false);
        }

        if (PhotonNetwork.CurrentRoom.PlayerCount == 4)
        {
            showcard1[r - 1].SetActive(false);
            showcard2[r - 1].SetActive(false);
            showcard3[r - 1].SetActive(false);
            showcard4[r - 1].SetActive(false);
        }
        won.text = "";
        Debug.Log("cards deleted");
        Debug.Log(r);
        pv1.RPC("RPC_nextturn0", RpcTarget.All, turn, r);
        /*if (PhotonNetwork.PlayerList[0].IsLocal)
        {
            pv1.RPC("RPC_nextturn0", RpcTarget.All, turn, r);
        }
        if (PhotonNetwork.PlayerList[1].IsLocal)
        {
            pv1.RPC("RPC_nextturn1", RpcTarget.All, turn, r);
        }
        if(PhotonNetwork.CurrentRoom.PlayerCount == 3 | PhotonNetwork.CurrentRoom.PlayerCount == 4)
        {
            if (PhotonNetwork.PlayerList[2].IsLocal)
            {
                pv1.RPC("RPC_nextturn2", RpcTarget.All, turn, r);
            }
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount == 4)
        {
            if (PhotonNetwork.PlayerList[3].IsLocal)
            {
                pv1.RPC("RPC_nextturn3", RpcTarget.All, turn, r);
            }
        }
        */
        Debug.Log("next turn sent");
        Debug.Log(turn + " t " + r);
    }

    [PunRPC]
    void RPC_roundtoall(int round, int i)
    {
        Debug.Log(round + " round");
        //Debug.Log(r + " r before");
        r = round;
        turn = i;
    }

    [PunRPC]
    public void RPC_compareturn(bool sa, bool sb, bool life, bool fc)
    {
        r++;
        Debug.Log("rpcturn");
        if (sa)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                //max = new[] { cards[m].strength, cards[n].strength, cards[o].strength}.Max();
                float[] a = new float[2] { cards[m].strength, cards[n].strength};
                var b = a.ToList();
                float max = b.Max();
                //Debug.Log(b.IndexOf(max));
                turn = (int)b.IndexOf(max);
                pv.RPC("RPC_wonmessage", RpcTarget.AllBuffered, PhotonNetwork.PlayerList[(int)b.IndexOf(max)].NickName + " won!!!");
            }

            if (PhotonNetwork.CurrentRoom.PlayerCount == 3)
            {
                //max = new[] { cards[m].strength, cards[n].strength, cards[o].strength}.Max();
                float[] a = new float[3] { cards[m].strength, cards[n].strength, cards[o].strength };
                var b = a.ToList();
                float max = b.Max();
                //Debug.Log(b.IndexOf(max));
                turn = (int)b.IndexOf(max);
                pv.RPC("RPC_wonmessage", RpcTarget.AllBuffered, PhotonNetwork.PlayerList[(int)b.IndexOf(max)].NickName + " won!!!");
            }

            if (PhotonNetwork.CurrentRoom.PlayerCount == 4)
            {
                //max = new[] { cards[m].strength, cards[n].strength, cards[o].strength}.Max();
                float[] a = new float[4] { cards[m].strength, cards[n].strength, cards[o].strength, cards[p].strength };
                var b = a.ToList();
                float max = b.Max();
                //Debug.Log(b.IndexOf(max));
                turn = (int)b.IndexOf(max);
                pv.RPC("RPC_wonmessage", RpcTarget.AllBuffered, PhotonNetwork.PlayerList[(int)b.IndexOf(max)].NickName + " won!!!");
                
            }
        }

        if (sb)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                //max = new[] { cards[m].strength, cards[n].strength, cards[o].strength}.Max();
                float[] a = new float[2] { cards[m].speed, cards[n].speed};
                var b = a.ToList();
                float max = b.Max();
                //Debug.Log(b.IndexOf(max));
                turn = (int)b.IndexOf(max);
                pv.RPC("RPC_wonmessage", RpcTarget.AllBuffered, PhotonNetwork.PlayerList[(int)b.IndexOf(max)].NickName + " won!!!");

                
            }

            if (PhotonNetwork.CurrentRoom.PlayerCount == 3)
            {
                //max = new[] { cards[m].speed, cards[n].speed, cards[o].speed}.Max();
                float[] a = new float[3] { cards[m].speed, cards[n].speed, cards[o].speed};
                var b = a.ToList();
                float max = b.Max();
                //Debug.Log(b.IndexOf(max));
                turn = (int)b.IndexOf(max);
                pv.RPC("RPC_wonmessage", RpcTarget.AllBuffered, PhotonNetwork.PlayerList[(int)b.IndexOf(max)].NickName + " won!!!");
                
            }

            if (PhotonNetwork.CurrentRoom.PlayerCount == 4)
            {
                //max = new[] { cards[m].speed, cards[n].speed, cards[o].speed}.Max();
                float[] a = new float[4] { cards[m].speed, cards[n].speed, cards[o].speed, cards[p].speed };
                var b = a.ToList();
                float max = b.Max();
                //Debug.Log(b.IndexOf(max));
                turn = (int)b.IndexOf(max);
                pv.RPC("RPC_wonmessage", RpcTarget.AllBuffered, PhotonNetwork.PlayerList[(int)b.IndexOf(max)].NickName + " won!!!");
                
            }
        }

        if (fc)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                //max = new[] { cards[m].strength, cards[n].strength, cards[o].strength}.Max();
                float[] a = new float[2] { cards[m].food_chain, cards[n].food_chain };
                var b = a.ToList();
                float max = b.Min();
                //Debug.Log(b.IndexOf(max));
                turn = (int)b.IndexOf(max);
                pv.RPC("RPC_wonmessage", RpcTarget.AllBuffered, PhotonNetwork.PlayerList[(int)b.IndexOf(max)].NickName + " won!!!");
                
            }

            if (PhotonNetwork.CurrentRoom.PlayerCount == 3)
            {
                //max = new[] { cards[m].food_chain, cards[n].food_chain, cards[o].food_chain}.Max();
                float[] a = new float[3] { cards[m].food_chain, cards[n].food_chain, cards[o].food_chain };
                var b = a.ToList();
                float max = b.Min();
                //Debug.Log(b.IndexOf(max));
                turn = (int)b.IndexOf(max);
                pv.RPC("RPC_wonmessage", RpcTarget.AllBuffered, PhotonNetwork.PlayerList[(int)b.IndexOf(max)].NickName + " won!!!");
                
            }

            if (PhotonNetwork.CurrentRoom.PlayerCount == 4)
            {
                //max = new[] { cards[m].food_chain, cards[n].food_chain, cards[o].food_chain}.Max();
                float[] a = new float[4] { cards[m].food_chain, cards[n].food_chain, cards[o].food_chain, cards[p].food_chain };
                var b = a.ToList();
                float max = b.Min();
                //Debug.Log(b.IndexOf(max));
                turn = (int)b.IndexOf(max);
                pv.RPC("RPC_wonmessage", RpcTarget.AllBuffered, PhotonNetwork.PlayerList[(int)b.IndexOf(max)].NickName + " won!!!");
                
            }
        }

        if (life)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                //max = new[] { cards[m].strength, cards[n].strength, cards[o].strength}.Max();
                float[] a = new float[2] { cards[m].life, cards[n].life };
                var b = a.ToList();
                float max = b.Max();
                //Debug.Log(b.IndexOf(max));
                turn = (int)b.IndexOf(max);
                pv.RPC("RPC_wonmessage", RpcTarget.AllBuffered, PhotonNetwork.PlayerList[(int)b.IndexOf(max)].NickName + " won!!!");
                
            }

            if (PhotonNetwork.CurrentRoom.PlayerCount == 3)
            {
                //max = new[] { cards[m].life, cards[n].life, cards[o].life}.Max();
                float[] a = new float[3] { cards[m].life, cards[n].life, cards[o].life };
                var b = a.ToList();
                float max = b.Max();
                //Debug.Log(b.IndexOf(max));
                turn = (int)b.IndexOf(max);
                pv.RPC("RPC_wonmessage", RpcTarget.AllBuffered, PhotonNetwork.PlayerList[(int)b.IndexOf(max)].NickName + " won!!!");
                
            }

            if (PhotonNetwork.CurrentRoom.PlayerCount == 4)
            {
                //max = new[] { cards[m].life, cards[n].life, cards[o].life}.Max();
                float[] a = new float[4] { cards[m].life, cards[n].life, cards[o].life, cards[p].life };
                var b = a.ToList();
                float max = b.Max();
                //Debug.Log(b.IndexOf(max));
                turn = (int)b.IndexOf(max);
                pv.RPC("RPC_wonmessage", RpcTarget.AllBuffered, PhotonNetwork.PlayerList[(int)b.IndexOf(max)].NickName + " won!!!");
                
            }
        }

        pv.RPC("RPC_roundtoall", RpcTarget.AllBuffered, r, turn);
    }       

    [PunRPC]
    void RPC_chosenTrait(string playerName, string trait)
    {
        
        won.text = playerName + " chose " + trait;
        //Debug.Log(won.text);
    }

    [PunRPC]
    public void RPC_wonmessage(string s)
    {
        StartCoroutine(wonMsg(s));
        StartCoroutine(deleteCards());
        //Invoke("deleteCards", 5);
    }

    IEnumerator wonMsg(string s)
    {
        yield return new WaitForSeconds(3);
        won.text = s;
        //Debug.Log(won.text);
    }

    [PunRPC]
    public void RPC_compareothers0(int i)
    {
        Debug.Log("rpcother0 "+(i));
        m = i - 1;
    }

    [PunRPC]
    public void RPC_compareothers1(int i)
    {
        Debug.Log("rpcother1 " + (i ));
        n = i - 1;
    }

    [PunRPC]
    public void RPC_compareothers2(int i)
    {
        Debug.Log("rpcother2" + (i));
        o = i - 1;
    }

    [PunRPC]
    public void RPC_compareothers3(int i)
    {
        Debug.Log("rpcother3");
        p = i - 1;
    }

    [PunRPC]
    public void RPC_showallcards()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            if (PhotonNetwork.PlayerList[0].IsLocal)
            {
                cardviz1[r].card = cards[m];
                showcard1[r].SetActive(true);
                cardviz2[r].card = cards[n];
                showcard2[r].SetActive(true);
            }

            if (PhotonNetwork.PlayerList[1].IsLocal)
            {
                cardviz1[r].card = cards[n];
                showcard1[r].SetActive(true);
                cardviz2[r].card = cards[m];
                showcard2[r].SetActive(true);
            }
        }

        if (PhotonNetwork.CurrentRoom.PlayerCount == 3)
        {
            if (PhotonNetwork.PlayerList[0].IsLocal)
            {
                cardviz1[r].card = cards[m];
                showcard1[r].SetActive(true);
                cardviz2[r].card = cards[n];
                showcard2[r].SetActive(true);
                cardviz3[r].card = cards[o];
                showcard3[r].SetActive(true);
            }
            if (PhotonNetwork.PlayerList[1].IsLocal)
            {
                cardviz2[r].card = cards[n];
                showcard2[r].SetActive(true);
                cardviz3[r].card = cards[o];
                showcard3[r].SetActive(true);
                cardviz1[r].card = cards[m];
                showcard1[r].SetActive(true);
            }
            if (PhotonNetwork.PlayerList[2].IsLocal)
            {
                cardviz3[r].card = cards[o];
                showcard3[r].SetActive(true);
                cardviz1[r].card = cards[m];
                showcard1[r].SetActive(true);
                cardviz2[r].card = cards[n];
                showcard2[r].SetActive(true);
            }
        }

        if (PhotonNetwork.CurrentRoom.PlayerCount == 4)
        {
            if (PhotonNetwork.PlayerList[0].IsLocal)
            {
                cardviz1[r].card = cards[m];
                showcard1[r].SetActive(true);
                cardviz2[r].card = cards[n];
                showcard2[r].SetActive(true);
                cardviz3[r].card = cards[o];
                showcard3[r].SetActive(true);
                cardviz4[r].card = cards[p];
                showcard4[r].SetActive(true);
            }
            if (PhotonNetwork.PlayerList[1].IsLocal)
            {
                cardviz2[r].card = cards[n];
                showcard2[r].SetActive(true);
                cardviz3[r].card = cards[o];
                showcard3[r].SetActive(true);
                cardviz4[r].card = cards[p];
                showcard4[r].SetActive(true);
                cardviz1[r].card = cards[m];
                showcard1[r].SetActive(true);
            }
            if (PhotonNetwork.PlayerList[2].IsLocal)
            {
                cardviz3[r].card = cards[o];
                showcard3[r].SetActive(true);
                cardviz4[r].card = cards[p];
                showcard4[r].SetActive(true);
                cardviz1[r].card = cards[m];
                showcard1[r].SetActive(true);
                cardviz2[r].card = cards[n];
                showcard2[r].SetActive(true);
            }
            if (PhotonNetwork.PlayerList[3].IsLocal)
            {
                cardviz4[r].card = cards[p];
                showcard4[r].SetActive(true);
                cardviz1[r].card = cards[m];
                showcard1[r].SetActive(true);
                cardviz2[r].card = cards[n];
                showcard2[r].SetActive(true);
                cardviz3[r].card = cards[o];
                showcard3[r].SetActive(true);
            }
        }

        //Thread.Sleep(timeout);
    }

}
