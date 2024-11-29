using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


// public enum CardState //位置和所属状态
// {
//     inPlayerHand, inPlayerBlock, inEnemyHand, inEnemyBlock
// }

public class ActionSelect : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    public bool hasUsed = false;
    // public CardState cardState = CardState.inPlayerHand;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (hasUsed == false && GetComponent<ActionCardDisplay>().actionCard.playerid == 0 && GameManager.Instance.currentPhase == GamePhase.playerActionSelect)
        {
            if (GetComponent<ActionCardDisplay>().actionCard.back == false)
            {
                GameManager.Instance.ActionRequst(gameObject, GetComponent<ActionCardDisplay>().actionCard.id, 0);
                GetComponent<ActionCardDisplay>().actionCard.back = true;
                GetComponent<ActionCardDisplay>().ShowActionCard();
            }
        }

        if (hasUsed == false && GetComponent<ActionCardDisplay>().actionCard.playerid == 1 && GameManager.Instance.currentPhase == GamePhase.enemyActionSelect)
        {
            if (GetComponent<ActionCardDisplay>().actionCard.back == false)
            {
                GameManager.Instance.ActionRequst(gameObject, GetComponent<ActionCardDisplay>().actionCard.id, 1);
                GetComponent<ActionCardDisplay>().actionCard.back = true;
                GetComponent<ActionCardDisplay>().ShowActionCard();
            }
        }

    }
}
