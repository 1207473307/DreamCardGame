using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum GamePhase  //游戏阶段
{
    gameStart, 
    playerDraw, playerActionSelect, playerAction0, playerAction1, playerAction2, playerAction3,  playerReAction2, playerReAction3, 
    enemyDraw, enemyActionSelect, enemyAction0, enemyAction1, enemyAction2, enemyAction3,  enemyReAction2, enemyReAction3,
    
}

public class GameManager : MonoSingleton<GameManager>
{
    // public static GameManager Instance;
    public GameObject card;
    public GameObject ActionCard;
    // public GameObject dataManager;

    // private float startPoint = -700.0f;
    private float step;
    private List<GameObject> personcardPool = new List<GameObject>();
    private List<GameObject> handcardPoolA = new List<GameObject>();
    private List<GameObject> handcardPoolB = new List<GameObject>();
    private List<GameObject> BlockcardA = new List<GameObject>();
    private List<GameObject> BlockcardB = new List<GameObject>();
    private List<GameObject> BlockcardC1 = new List<GameObject>();
    private List<GameObject> BlockcardC2 = new List<GameObject>();
    private List<GameObject> actioncardPoolA = new List<GameObject>();
    private List<GameObject> actioncardPoolB = new List<GameObject>();
    private List<GameObject> pilePool = new List<GameObject>();
    private List<GameObject> discardpilePool = new List<GameObject>();
    private CardData cardData;
    // private PlayerDataManager pdm;

    public Transform HandCardsA;
    public Transform HandCardsB;
    public Transform BlockA;
    public Transform BlockB;
    public Transform BlockC1;
    public Transform BlockC2;
    public Transform ActionCardsA;
    public Transform ActionCardsB;
    public Transform PersonCards;
    public Transform Pile;
    public Transform DiscardPile;


    public GamePhase currentPhase = GamePhase.gameStart;



    public UnityEvent phaseChangeEvent;


    // Start is called before the first frame update
    void Start()
    {
        cardData = GetComponent<CardData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void ClearPool(List<GameObject> cardPool)
    {
        foreach (var card in cardPool)
        {
            Destroy(card);
        }
        cardPool.Clear();
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
                    newCard.GetComponent<CardAction>().cardState = CardState.People;
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

        List<ActionCard> ActionCards2 = cardData.GetActionCards();
        foreach (ActionCard cardData in ActionCards2)
        {
            GameObject newActionCard = GameObject.Instantiate(ActionCard, ActionCardsB);
            actioncardPoolB.Add(newActionCard);
            newActionCard.GetComponent<ActionCardDisplay>().actionCard = cardData;
            newActionCard.GetComponent<ActionCardDisplay>().actionCard.playerid = 1;
        }


    }

    public void DealCards()
    {
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
            newCard.GetComponent<CardAction>().cardState = CardState.inPlayerHand;
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
            newCard.GetComponent<CardAction>().cardState = CardState.inEnemyHand;
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

    public void DrawCard(int _player, int _number, bool _back = false, bool _state=true)  //抽卡
    {
        if (_player == 0)  //玩家抽卡
        {
            for (int i = 0; i < _number; i++)
            {   
                if (pilePool.Count == 0) return;

                GameObject topCardObject = pilePool[0];
                pilePool.RemoveAt(0); // 从牌堆中移除

                // 获取卡牌数据
                Card topCard = topCardObject.GetComponent<CardDisplay>().card;

                // 生成新卡牌对象
                GameObject newCard = GameObject.Instantiate(card, HandCardsA);

                // 显示卡背
                if (_back)
                {
                    topCard.back = true;
                }
                else
                {
                    topCard.back = false;
                }

                // 赋值卡牌数据
                newCard.GetComponent<CardAction>().cardState = CardState.inPlayerHand;
                newCard.GetComponent<CardDisplay>().card = topCard;
                // newCard.GetComponent<BattleCard>().cardState = CardState.inPlayerHand;
                

                if (_state)
                {
                    currentPhase = GamePhase.playerActionSelect;
                    phaseChangeEvent.Invoke();
                }
            }
        }
        else if (_player == 1)
        {
            for (int i = 0; i < _number; i++)
            {
                if (pilePool.Count == 0) return;

                GameObject topCardObject = pilePool[0];
                pilePool.RemoveAt(0); // 从牌堆中移除

                // 获取卡牌数据
                Card topCard = topCardObject.GetComponent<CardDisplay>().card;

                // 生成新卡牌对象
                GameObject newCard = GameObject.Instantiate(card, HandCardsB);

                // 显示卡背
                if (_back)
                {
                    topCard.back = true;
                }
                else
                {
                    topCard.back = false;
                }


                // 赋值卡牌数据
                newCard.GetComponent<CardAction>().cardState = CardState.inEnemyHand;
                newCard.GetComponent<CardDisplay>().card = topCard;
                // newCard.GetComponent<BattleCard>().cardState = CardState.inEnemyHand;

                if (_state)
                {
                    currentPhase = GamePhase.enemyActionSelect;
                    phaseChangeEvent.Invoke();
                }
            }
        }     
    }
    
    
    public virtual void GameStart() // 游戏开始，抽6张手牌
    {
        if (currentPhase == GamePhase.gameStart)
        {
            

            ClearPool(personcardPool);
            ClearPool(handcardPoolA);
            ClearPool(handcardPoolB);
            ClearPool(pilePool);
            ClearPool(discardpilePool);
    
            CreatPersonCard();
            CreatActionCard();
            DealCards();
    
            // PhaseInfo.text = "A玩家抽卡";
            currentPhase = GamePhase.playerDraw;
            phaseChangeEvent.Invoke();
            DrawCard(0, 1);
        }
        //Debug.Log(currentPhase);
    }

    public void ActionRequst(GameObject _actioncard, int action_id, int playerid)
    {
        _actioncard.GetComponent<ActionSelect>().hasUsed = true;
        if (action_id == 0)
        {
            Action0(playerid);
        }
        if (action_id == 1)
        {
            Action1(playerid);
        }
        if (action_id == 2)
        {
            Action2(playerid);
        }
        if (action_id == 3)
        {
            Action3(playerid);
        }
    }

    public void Action0(int playerid)
    {
        if (playerid == 0)
        {
            currentPhase = GamePhase.playerAction0;
            phaseChangeEvent.Invoke();
        }
        else
        {
            currentPhase = GamePhase.enemyAction0;
            phaseChangeEvent.Invoke();
        }
    }
    public void Action1(int playerid)
    {
        if (playerid == 0)
        {
            currentPhase = GamePhase.playerAction1;
            phaseChangeEvent.Invoke();
        }
        else
        {
            currentPhase = GamePhase.enemyAction1;
            phaseChangeEvent.Invoke();
        }

    }

    public void Action2(int playerid)
    {
        if (playerid == 0)
        {
            currentPhase = GamePhase.playerAction2;
            phaseChangeEvent.Invoke();
        }
        else
        {
            currentPhase = GamePhase.enemyAction2;
            phaseChangeEvent.Invoke();
        }
    }
    public void Action3(int playerid)
    {
        if (playerid == 0)
        {
            currentPhase = GamePhase.playerAction3;
            phaseChangeEvent.Invoke();
        }
        else
        {
            currentPhase = GamePhase.enemyAction3;
            phaseChangeEvent.Invoke();
        }
        BlockC1.GetComponent<BlockSelect>().selectionIndicator.gameObject.SetActive(true);
        BlockC2.GetComponent<BlockSelect>().selectionIndicator.gameObject.SetActive(true);
    }

    public void MoveRequest(GameObject _card, int playerid)
    {
        if (playerid == 0)
        {
            if (_card.GetComponent<CardAction>().cardState == CardState.inPlayerHand)
            {
                handcardPoolA.Remove(_card); // 从 A 的手牌中移除
                BlockcardA.Add(_card); // 添加到 A 的 Block 区域
                _card.transform.SetParent(BlockA); // 设置父级为 BlockA
                _card.GetComponent<CardAction>().cardState = CardState.inPlayerBlock;
            }

            else if (_card.GetComponent<CardAction>().cardState == CardState.inPlayerBlock) // 如果卡片在 A 的 Block 区域
            {
                BlockcardA.Remove(_card); // 从 A 的 Block 区域移除
                handcardPoolA.Add(_card); // 添加到 A 的手牌
                _card.transform.SetParent(HandCardsA); // 设置父级为 HandCardsA
                _card.GetComponent<CardAction>().cardState = CardState.inPlayerHand;
            }
        }

        if (playerid == 1)
        {
            if (_card.GetComponent<CardAction>().cardState == CardState.inEnemyHand)
            {
                handcardPoolB.Remove(_card); // 从 B 的手牌中移除
                BlockcardB.Add(_card); // 添加到 B 的 Block 区域
                _card.transform.SetParent(BlockB); // 设置父级为 BlockB
                _card.GetComponent<CardAction>().cardState = CardState.inEnemyBlock;
            }

            else if (_card.GetComponent<CardAction>().cardState == CardState.inEnemyBlock) // 如果卡片在 B 的 Block 区域
            {
                BlockcardB.Remove(_card); // 从 B 的 Block 区域移除
                handcardPoolB.Add(_card); // 添加到 B 的手牌
                _card.transform.SetParent(HandCardsB); // 设置父级为 HandCardsB
                _card.GetComponent<CardAction>().cardState = CardState.inEnemyHand;
            }
        }
    }

    public void MoveRequestC(GameObject _card, int playerid)
    {
        
        if (playerid == 0)
        {
            
            if (_card.GetComponent<CardAction>().cardState == CardState.inPlayerHand)
            {
                if (BlockcardC1.Count < 2)
                {
                    handcardPoolA.Remove(_card); 
                    BlockcardC1.Add(_card); 
                    _card.transform.SetParent(BlockC1); 
                    _card.GetComponent<CardAction>().cardState = CardState.inBlockC1;
                }
            

                else if (BlockcardC2.Count < 2)
                {
                    handcardPoolA.Remove(_card); 
                    BlockcardC2.Add(_card); 
                    _card.transform.SetParent(BlockC2); 
                    _card.GetComponent<CardAction>().cardState = CardState.inBlockC2;
                }
            }
            else if (_card.GetComponent<CardAction>().cardState == CardState.inBlockC1) 
            {
                BlockcardC1.Remove(_card); 
                handcardPoolA.Add(_card); 
                _card.transform.SetParent(HandCardsA); 
                _card.GetComponent<CardAction>().cardState = CardState.inPlayerHand;
            }
            else if (_card.GetComponent<CardAction>().cardState == CardState.inBlockC2)
            {
                BlockcardC2.Remove(_card); 
                handcardPoolA.Add(_card); 
                _card.transform.SetParent(HandCardsA); 
                _card.GetComponent<CardAction>().cardState = CardState.inPlayerHand;

            }
        }

        if (playerid == 1)
        {
            
            if (_card.GetComponent<CardAction>().cardState == CardState.inEnemyHand)
            {
                if (BlockcardC1.Count < 2)
                {
                    handcardPoolB.Remove(_card); 
                    BlockcardC1.Add(_card); 
                    _card.transform.SetParent(BlockC1); 
                    _card.GetComponent<CardAction>().cardState = CardState.inBlockC1;
                }
            

                else if (BlockcardC2.Count < 2)
                {
                    handcardPoolB.Remove(_card); 
                    BlockcardC2.Add(_card); 
                    _card.transform.SetParent(BlockC2); 
                    _card.GetComponent<CardAction>().cardState = CardState.inBlockC2;
                }
            }
            else if (_card.GetComponent<CardAction>().cardState == CardState.inBlockC1) 
            {
                BlockcardC1.Remove(_card); 
                handcardPoolB.Add(_card); 
                _card.transform.SetParent(HandCardsB); 
                _card.GetComponent<CardAction>().cardState = CardState.inPlayerHand;
            }
            else if (_card.GetComponent<CardAction>().cardState == CardState.inBlockC2)
            {
                BlockcardC2.Remove(_card); 
                handcardPoolB.Add(_card); 
                _card.transform.SetParent(HandCardsB); 
                _card.GetComponent<CardAction>().cardState = CardState.inPlayerHand;

            }
        }


    }
    public void CheckPlayerAction()
    {
        if (currentPhase == GamePhase.playerAction0)
        {
            if (BlockcardA.Count == 1)
            {
                Cal_Score();
                ClearPool(BlockcardA);
                currentPhase = GamePhase.enemyActionSelect;
                phaseChangeEvent.Invoke();
            }
        }

        if (currentPhase == GamePhase.playerAction1)
        {
            if (BlockcardA.Count == 2)
            {
                Cal_Score();
                ClearPool(BlockcardA);
                currentPhase = GamePhase.enemyActionSelect;
                phaseChangeEvent.Invoke();
            }
        }

        if (currentPhase == GamePhase.playerAction2)
        {
            if (BlockcardA.Count == 3)
            {
                currentPhase = GamePhase.enemyReAction2;
                phaseChangeEvent.Invoke();
            }
        }

        if (currentPhase == GamePhase.playerAction3)
        {
            if (BlockcardC1.Count == 2 && BlockcardC2.Count == 2)
            {
                currentPhase = GamePhase.enemyReAction3;
                phaseChangeEvent.Invoke();
            }
        }
    }

    public void CheckEnemyAction()
    {
        if (currentPhase == GamePhase.enemyAction0)
        {
            if (BlockcardB.Count == 1)
            {
                Cal_Score();
                ClearPool(BlockcardB);
                currentPhase = GamePhase.playerActionSelect;
                phaseChangeEvent.Invoke();
            }
        }

        if (currentPhase == GamePhase.enemyAction1)
        {
            if (BlockcardB.Count == 2)
            {
                Cal_Score();
                ClearPool(BlockcardB);
                currentPhase = GamePhase.playerActionSelect;
                phaseChangeEvent.Invoke();
            }
        }

        if (currentPhase == GamePhase.enemyAction2)
        {
            if (BlockcardB.Count == 3)
            {
                currentPhase = GamePhase.playerReAction2;
                phaseChangeEvent.Invoke();
            }
        }

        if (currentPhase == GamePhase.enemyAction3)
        {
            if (BlockcardC1.Count == 2 && BlockcardC2.Count == 2)
            {
                currentPhase = GamePhase.playerReAction3;
                phaseChangeEvent.Invoke();
            }
        }
    }

    public void Cal_Score()
    {

    }
}
