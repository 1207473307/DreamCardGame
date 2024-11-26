using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionCardDisplay : MonoBehaviour
{   
    public ActionCard actionCard;
    public Text cardName;
    public Image background;
    public GameObject backImage;
    // Start is called before the first frame update
    void Start()
    {
        if (actionCard != null)
        {
            ShowActionCard();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowActionCard()
    {
        if (actionCard.back)
        {
            backImage.SetActive(true);
        }
        else{
            backImage.SetActive(false);
        }


        cardName.text = actionCard.cardName;
     
    }
}
