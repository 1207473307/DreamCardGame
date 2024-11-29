using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;
    public Text cardName;
    public Text Value;
    public Text Gift;

    public Image background;

    public Color PersonColor;
    public Color GiftColor;
  

    
    public GameObject backImage;
    // Start is called before the first frame update
    void Start()
    {
        ColorUtility.TryParseHtmlString("#387445", out PersonColor);
        ColorUtility.TryParseHtmlString("#556874", out GiftColor);
        
        if (card != null)
        {
            ShowCard();

        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShowCard()
    {
        if (card.back)
        {
            backImage.SetActive(true);
        }
        else{
            backImage.SetActive(false);
        }


        cardName.text = card.cardName;

        if (card is PersonCard) // 如果是艺妓卡
        {
            var itemcard = card as PersonCard;
            Value.text = "<color=red>" + itemcard.value.ToString() + "</color>";
            Gift.text = "<color=blue>" + itemcard.gift.ToString() + "</color>";
            background.color = PersonColor;
            // infoText.gameObject.SetActive(false);

        }
        // 如果礼物牌
        else if (card is GiftCard)
        {
            var itemcard = card as GiftCard;
            Value.text = "<color=red>" + itemcard.value.ToString() + "</color>";
            Gift.text = "<color=blue>" + itemcard.gift.ToString() + "</color>";
            background.color = GiftColor;
            // infoText.text = itemcard.effect;
        }
        
    }

}
