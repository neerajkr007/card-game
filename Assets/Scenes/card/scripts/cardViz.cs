using UnityEngine;
using UnityEngine.UI;

public class cardViz : MonoBehaviour
{
    public TMPro.TMP_Text id;
    public TMPro.TMP_Text name;
    //public Image img;
    public TMPro.TMP_Text strength;
    public TMPro.TMP_Text speed;
    public TMPro.TMP_Text life;
    public TMPro.TMP_Text food_chain;

    public card_class card;

    private void Start()
    {
        loadcard(card);
    }
    public void loadcard(card_class c)
    {
        //Debug.Log(" loaded");
        if (c == null)
            return;
        card = c;
        id.text = c.cardId.ToString();
        name.text = c.cardName;
        //img.sprite = c.cardImg;
        strength.text = c.strength.ToString();
        speed.text = c.speed.ToString();
        life.text = c.life.ToString();
        food_chain.text = c.food_chain.ToString();
    }
}
