using UnityEngine;
using System.Linq;
using System;
using System.Text;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Card")]
//[System.Serializable]
public class card_class : ScriptableObject
{
    public int cardId;
    public string cardName;
    //public Sprite cardImg;
    public float strength;
    public float speed;
    public float life;
    public float food_chain;
    /*public static byte[] Serialize(object obj)
    {
        card_class data = (card_class)obj;
        byte[] cardIdByte = BitConverter.GetBytes(data.cardId);

    }*/
}
