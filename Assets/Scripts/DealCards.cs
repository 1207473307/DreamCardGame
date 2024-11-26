using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealCards : MonoBehaviour
{   public GameObject card;
    public GameObject ActionCard;
    // public GameObject dataManager;

    // private float startPoint = -700.0f;
    private float step;
    private List<GameObject> personcardPool = new List<GameObject>();
    private List<GameObject> handcardPoolA = new List<GameObject>();
    private List<GameObject> handcardPoolB = new List<GameObject>();
    private List<GameObject> actioncardPoolA = new List<GameObject>();
    private List<GameObject> actioncardPoolB = new List<GameObject>();
    private List<GameObject> pilePool = new List<GameObject>();
    private List<GameObject> discardpilePool = new List<GameObject>();
    private CardData cardData;
    // private PlayerDataManager pdm;

    public Transform HandCardsA;
    public Transform HandCardsB;
    public Transform ActionCardsA;
    public Transform ActionCardsB;
    public Transform PersonCards;
    public Transform Pile;
    public Transform DiscardPile;
    // Start is called before the first frame update
    void Start()
    {
        cardData = GetComponent<CardData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreatPersonCard()
    {
        for (int i = 0; i < 7; i++)
                {
                    // 生成卡牌实体
                    GameObject newCard = GameObject.Instantiate(card, PersonCards);
                    // newCard.transform.localPosition = new Vector2(startPoint + step * i, 0.0f);

                    // 添加到对象池
                    personcardPool.Add(newCard);
                    // 赋予该实体以数据
                    newCard.GetComponent<CardDisplay>().card = cardData.GetPersonCard(i);
                }

    }

    public void CreatActionCard()
    {
        List<ActionCard> ActionCards = cardData.GetActionCards();
        foreach (ActionCard cardData in ActionCards)
        {
            GameObject newActionCard = GameObject.Instantiate(ActionCard, ActionCardsA);
            actioncardPoolA.Add(newActionCard);
            newActionCard.GetComponent<ActionCardDisplay>().actionCard = cardData;
        }
        foreach (ActionCard cardData in ActionCards)
        {
            GameObject newActionCard = GameObject.Instantiate(ActionCard, ActionCardsB);
            actioncardPoolB.Add(newActionCard);
            newActionCard.GetComponent<ActionCardDisplay>().actionCard = cardData;
        }


    }


    public void OnClickOpen()
    {
        ClearPool(personcardPool);
        ClearPool(handcardPoolA);
        ClearPool(handcardPoolB);
        ClearPool(pilePool);
        ClearPool(discardpilePool);
 
        CreatPersonCard();
        CreatActionCard();

        // 调用分组函数，获取分组结果
        Dictionary<string, List<Card>> result = cardData.DistributeCards();

        // 分配 A 玩家手牌
        List<Card> playerACards = result["HandCardsA"];
        for (int i = 0; i < 6; i++)
        {
            // 生成卡牌实体
            GameObject newCard = GameObject.Instantiate(card, HandCardsA);
            // newCard.transform.localPosition = new Vector2(startPoint + step * i, 0.0f);

            // 添加到对象池
            handcardPoolA.Add(newCard);
            // 赋予该实体以数据
            newCard.GetComponent<CardDisplay>().card = playerACards[i];
        }

        // 分配 B 玩家手牌
        List<Card> playerBCards = result["HandCardsB"];
        for (int i = 0; i < 6; i++)
        {
            // 创建 B 玩家手牌的卡牌实体
            GameObject newCard = GameObject.Instantiate(card, HandCardsB);
            // newCard.transform.localPosition = new Vector2(startPoint + step * i, 0.0f); // 可自定义位置逻辑

            // 添加到 B 玩家手牌池
            handcardPoolB.Add(newCard);

            // 赋予实体卡片数据
            playerBCards[i].back = true;  //背面朝上
            newCard.GetComponent<CardDisplay>().card = playerBCards[i];
        }

        // 分配剩余牌堆
        List<Card> remainingDeck = result["Pile"];
        foreach (Card cardData in remainingDeck)
        {
            GameObject newCard = GameObject.Instantiate(card, Pile);
            pilePool.Add(newCard);
            cardData.back = true;  //背面朝上
            newCard.GetComponent<CardDisplay>().card = cardData;
        }

        // 分配弃牌
        List<Card> discardedCards = result["DiscardPile"];
        foreach (Card cardData in discardedCards)
        {
            GameObject newCard = GameObject.Instantiate(card, DiscardPile);
            discardpilePool.Add(newCard);
            cardData.back = true;  //背面朝上
            newCard.GetComponent<CardDisplay>().card = cardData;
        }

    }

    public void ClearPool(List<GameObject> cardPool)
    {
        foreach (var card in cardPool)
        {
            Destroy(card);
        }
        cardPool.Clear();
    }


}
