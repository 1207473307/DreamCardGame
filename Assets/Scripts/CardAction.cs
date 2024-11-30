using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum CardState //位置和所属状态
{
    inPlayerHand, inPlayerBlock, inEnemyHand, inEnemyBlock, inBlockC1, inBlockC2, inBlockD, People, inPile
}

public class CardAction : MonoBehaviour, IPointerDownHandler
{
    public CardState cardState = CardState.inPlayerHand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameManager.Instance.currentPhase == GamePhase.playerAction0 || GameManager.Instance.currentPhase == GamePhase.playerAction1 ||GameManager.Instance.currentPhase == GamePhase.playerAction2)
        {
            GameManager.Instance.MoveRequest(gameObject, 0);
        }
        else if (GameManager.Instance.currentPhase == GamePhase.playerAction3)
        {
            GameManager.Instance.MoveRequestC(gameObject, 0);
        }
        else if (GameManager.Instance.currentPhase == GamePhase.playerReAction2)
        {
            GameManager.Instance.MoveRequestD(gameObject, 0);
        }

        if (GameManager.Instance.currentPhase == GamePhase.enemyAction0 || GameManager.Instance.currentPhase == GamePhase.enemyAction1 ||GameManager.Instance.currentPhase == GamePhase.enemyAction2)
        {
            GameManager.Instance.MoveRequest(gameObject, 1);
        }

        else if (GameManager.Instance.currentPhase == GamePhase.enemyAction3)
        {
            GameManager.Instance.MoveRequestC(gameObject, 1);
        }
        else if (GameManager.Instance.currentPhase == GamePhase.enemyReAction2)
        {
            GameManager.Instance.MoveRequestD(gameObject, 1);
        }


    }
}
