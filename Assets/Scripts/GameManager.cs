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
    playerWin, enemyWin
    
}

public class GameManager : MonoSingleton<GameManager>
{
    // public static GameManager Instance;
    public GameObject card;
    public GameObject ActionCard;

    public GameObject buttonObject;
    public GameObject buttonConfirmA;
    public GameObject buttonConfirmB;
    // public GameObject dataManager;

    // private float startPoint = -700.0f;
    private float step;
    private List<GameObject> personcardPool = new List<GameObject>();
    private List<GameObject> handcardPoolA = new List<GameObject>();
    private List<GameObject> handcardPoolB = new List<GameObject>();
    private List<GameObject> BlockcardA = new List<GameObject>();  //for action0\1
    private List<GameObject> BlockcardB = new List<GameObject>();  //for action0\1
    private List<GameObject> BlockcardC1 = new List<GameObject>(); //for action3
    private List<GameObject> BlockcardC2 = new List<GameObject>(); //for action3
    private List<GameObject> BlockcardD = new List<GameObject>();  //for action2
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
    public Transform BlockD;
    public Transform ActionCardsA;
    public Transform ActionCardsB;
    public Transform PersonCards;
    public Transform Pile;
    public Transform DiscardPile;

    public int start_player = 1; //上局开始的玩家

    // for Score
    public int[] playerAScores = new int[7];
    public int[] playerBScores = new int[7];
    public int[] personstate = new int[7]; //-1:平局， 0：玩家A赢得  1：玩家B赢得

    // 整数记录两位玩家的总分
    public int totalScoreA = 0;
    public int totalScoreB = 0;

    public int totalPersonA = 0;
    public int totalPersonB = 0;

    // randombuff
    public int randombuffA = 0; //0~7
    public int randombuffB = 0;

    private readonly float[] probabilities = { 31f, 14f, 4.5f, 1f, 31f, 14f, 4.5f }; // 每种 buff 的概率
    private readonly int[] buffs = { 1, 2, 3, 4, 5, 6, 7 }; // buff 编号


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

    public void Reset(bool s=false)
    {
        if (s)
        {
            for (int i = 0; i < 7; i++)
            {
                personstate[i] = -1;
            }

            totalPersonA = 0;
            totalPersonB = 0;
  
        }

        if (currentPhase == GamePhase.gameStart)
        {
            ClearPool(personcardPool);
            ClearPool(handcardPoolA);
            ClearPool(handcardPoolB);
            ClearPool(pilePool);
            ClearPool(discardpilePool);
            ClearPool(actioncardPoolA);
            ClearPool(actioncardPoolB);
            
            // 第二局不保留总分
            totalScoreA = 0;
            totalScoreB = 0;
            for (int i = 0; i < 7; i++)
            {
                playerAScores[i] = 0;
                playerBScores[i] = 0;
            }
            
    
            CreatPersonCard();
            CreatActionCard();
            DealCards();
        }


        // get buff
        getbuff();

        //进入抽牌阶段
        if (start_player == 1)
        {
            start_player = 0;
            currentPhase = GamePhase.playerDraw;
            phaseChangeEvent.Invoke();
            DrawCard(0, 1);
        }
        else if (start_player == 0)
        {
            start_player = 1;
            currentPhase = GamePhase.enemyDraw;
            phaseChangeEvent.Invoke();
            DrawCard(1, 1);
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
            playerACards[i].back = false;  //背面朝下
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
            newCard.GetComponent<CardAction>().cardState = CardState.inPile;
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
                pilePool[0].transform.SetParent(DiscardPile); 
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
                pilePool[0].transform.SetParent(DiscardPile); 
                pilePool.RemoveAt(0); // 从牌堆中移除

                // 获取卡牌数据
                Card topCard = topCardObject.GetComponent<CardDisplay>().card;

                // 生成新卡牌对象
                GameObject newCard = GameObject.Instantiate(card, HandCardsB);

                // 显示卡背

                topCard.back = true;
                


                // 赋值卡牌数据
                newCard.GetComponent<CardAction>().cardState = CardState.inEnemyHand;
                newCard.GetComponent<CardDisplay>().card = topCard;
                newCard.GetComponent<CardDisplay>().ShowCard();
                // newCard.GetComponent<BattleCard>().cardState = CardState.inEnemyHand;

                if (_state)
                {
                    currentPhase = GamePhase.enemyActionSelect;
                    phaseChangeEvent.Invoke();
                }
            }
        }     
    }
    
    private int GenerateRandomBuff()
    {
        // 累积概率表
        float[] cumulativeProbabilities = new float[probabilities.Length];
        cumulativeProbabilities[0] = probabilities[0];

        for (int i = 1; i < probabilities.Length; i++)
        {
            cumulativeProbabilities[i] = cumulativeProbabilities[i - 1] + probabilities[i];
        }

        // 随机生成一个 0 ~ 100 的浮点数
        float randomValue = Random.Range(0f, 100f);

        // 查找随机值属于哪个范围
        for (int i = 0; i < cumulativeProbabilities.Length; i++)
        {
            if (randomValue <= cumulativeProbabilities[i])
            {
                return buffs[i];
            }
        }

        // 默认返回 0 (无 buff)，理论上不会到这里
        return 0;
    }



    public void getbuff()
    {
        randombuffA = GenerateRandomBuff();
        randombuffB = GenerateRandomBuff();
        Debug.Log($"Player A Buff: {randombuffA}, Player B Buff: {randombuffB}");
    }
    public virtual void GameStart() // 游戏开始
    {
        Reset(true);

        buttonObject.SetActive(false);
        buttonConfirmA.SetActive(true);
        buttonConfirmB.SetActive(true);
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
                if (currentPhase != GamePhase.playerAction2)
                {
                    _card.GetComponent<CardDisplay>().card.back = true;
                    _card.GetComponent<CardDisplay>().ShowCard();
                }
                
                _card.GetComponent<CardAction>().cardState = CardState.inPlayerBlock;
            }

            else if (_card.GetComponent<CardAction>().cardState == CardState.inPlayerBlock) // 如果卡片在 A 的 Block 区域
            {
                BlockcardA.Remove(_card); // 从 A 的 Block 区域移除
                handcardPoolA.Add(_card); // 添加到 A 的手牌
                _card.transform.SetParent(HandCardsA); // 设置父级为 HandCardsA
                _card.GetComponent<CardDisplay>().card.back = false;
                _card.GetComponent<CardDisplay>().ShowCard();
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
                if (currentPhase == GamePhase.enemyAction2)
                {
                    _card.GetComponent<CardDisplay>().card.back = false;
                    _card.GetComponent<CardDisplay>().ShowCard();
                }
                
                _card.GetComponent<CardAction>().cardState = CardState.inEnemyBlock;
            }

            else if (_card.GetComponent<CardAction>().cardState == CardState.inEnemyBlock) // 如果卡片在 B 的 Block 区域
            {
                BlockcardB.Remove(_card); // 从 B 的 Block 区域移除
                handcardPoolB.Add(_card); // 添加到 B 的手牌
                _card.transform.SetParent(HandCardsB); // 设置父级为 HandCardsB

                _card.GetComponent<CardDisplay>().card.back = true;
                _card.GetComponent<CardDisplay>().ShowCard();       
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
                    _card.GetComponent<CardDisplay>().card.back = false;
                    _card.GetComponent<CardDisplay>().ShowCard();
                    _card.GetComponent<CardAction>().cardState = CardState.inBlockC1;
                }
            

                else if (BlockcardC2.Count < 2)
                {
                    handcardPoolB.Remove(_card); 
                    BlockcardC2.Add(_card); 
                    _card.transform.SetParent(BlockC2); 
                    _card.GetComponent<CardDisplay>().card.back = false;
                    _card.GetComponent<CardDisplay>().ShowCard();
                    _card.GetComponent<CardAction>().cardState = CardState.inBlockC2;
                }
            }
            else if (_card.GetComponent<CardAction>().cardState == CardState.inBlockC1) 
            {
                BlockcardC1.Remove(_card); 
                handcardPoolB.Add(_card); 
                _card.transform.SetParent(HandCardsB); 
                _card.GetComponent<CardDisplay>().card.back = true;
                _card.GetComponent<CardDisplay>().ShowCard();
                _card.GetComponent<CardAction>().cardState = CardState.inEnemyHand;
            }
            else if (_card.GetComponent<CardAction>().cardState == CardState.inBlockC2)
            {
                BlockcardC2.Remove(_card); 
                handcardPoolB.Add(_card); 
                _card.transform.SetParent(HandCardsB); 
                _card.GetComponent<CardDisplay>().card.back = true;
                _card.GetComponent<CardDisplay>().ShowCard();
                _card.GetComponent<CardAction>().cardState = CardState.inEnemyHand;

            }
        }


    }

    public void MoveRequestD(GameObject _card, int playerid)
    {
        if (playerid == 0)
        {
            if (_card.GetComponent<CardAction>().cardState == CardState.inEnemyBlock)
            {
                BlockcardB.Remove(_card); 
                BlockcardD.Add(_card); 
                _card.transform.SetParent(BlockD); 
                _card.GetComponent<CardAction>().cardState = CardState.inBlockD;
            }

            else if (_card.GetComponent<CardAction>().cardState == CardState.inBlockD) 
            {
                BlockcardD.Remove(_card); 
                BlockcardB.Add(_card); 
                _card.transform.SetParent(BlockB); 
                _card.GetComponent<CardAction>().cardState = CardState.inEnemyBlock;
            }
        }

        if (playerid == 1)
        {
            if (_card.GetComponent<CardAction>().cardState == CardState.inPlayerBlock)
            {
                BlockcardA.Remove(_card); 
                BlockcardD.Add(_card); 
                _card.transform.SetParent(BlockD); 
                _card.GetComponent<CardAction>().cardState = CardState.inBlockD;
            }

            else if (_card.GetComponent<CardAction>().cardState == CardState.inBlockD) 
            {
                BlockcardD.Remove(_card); 
                BlockcardA.Add(_card); 
                _card.transform.SetParent(BlockA); 
                _card.GetComponent<CardAction>().cardState = CardState.inPlayerBlock;
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
                currentPhase = GamePhase.enemyDraw;
                phaseChangeEvent.Invoke();

                if (pilePool.Count > 0)
                {
                    DrawCard(1, 1);
                }
                else
                {
                    CheckOver();
                }
            }
        }

        if (currentPhase == GamePhase.playerAction1)
        {
            if (BlockcardA.Count == 2)
            {
                Cal_Score();
                ClearPool(BlockcardA);
                currentPhase = GamePhase.enemyDraw;
                phaseChangeEvent.Invoke();

                if (pilePool.Count > 0)
                {
                    DrawCard(1, 1);
                }
                else
                {
                    CheckOver();
                }
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
        if (currentPhase == GamePhase.playerReAction2)
        {
            if (BlockcardD.Count == 1)
            {
                Cal_Score();
                ClearPool(BlockcardD);
                ClearPool(BlockcardB);
                currentPhase = GamePhase.playerDraw;
                phaseChangeEvent.Invoke();

                if (pilePool.Count > 0)
                {
                    DrawCard(0, 1);
                }
                else
                {
                    CheckOver();
                }
            }
        }

        if (currentPhase == GamePhase.playerReAction3)
        {
            if (BlockSelect.selectedBlock != null)
            {
                Cal_Score();
                ClearPool(BlockcardC1);
                ClearPool(BlockcardC2);

                BlockC1.GetComponent<BlockSelect>().Reset();
                BlockC2.GetComponent<BlockSelect>().Reset();
                // BlockSelect.selectedBlock = null;
                // BlockC1.GetComponent<BlockSelect>().selectionIndicator.gameObject.SetActive(false);
                // BlockC2.GetComponent<BlockSelect>().selectionIndicator.gameObject.SetActive(false);
                currentPhase = GamePhase.playerDraw;
                phaseChangeEvent.Invoke();

                if (pilePool.Count > 0)
                {
                    DrawCard(0, 1);
                }
                else
                {
                    CheckOver();
                }
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
                currentPhase = GamePhase.playerDraw;
                phaseChangeEvent.Invoke();

                if (pilePool.Count > 0)
                {
                    DrawCard(0, 1);
                }
                else
                {
                    CheckOver();
                }
            }
        }

        if (currentPhase == GamePhase.enemyAction1)
        {
            if (BlockcardB.Count == 2)
            {
                Cal_Score();
                ClearPool(BlockcardB);
                currentPhase = GamePhase.playerDraw;
                phaseChangeEvent.Invoke();

                if (pilePool.Count > 0)
                {
                    DrawCard(0, 1);
                }
                else
                {
                    CheckOver();
                }
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

        if (currentPhase == GamePhase.enemyReAction2)
        {
            if (BlockcardD.Count == 1)
            {
                Cal_Score();
                ClearPool(BlockcardD);
                ClearPool(BlockcardA);
                currentPhase = GamePhase.enemyDraw;
                phaseChangeEvent.Invoke();

                if (pilePool.Count > 0)
                {
                    DrawCard(1, 1);
                }
                else
                {
                    CheckOver();
                }
            }
        }

        if (currentPhase == GamePhase.enemyReAction3)
        {
            if (BlockSelect.selectedBlock != null)
            {
                Cal_Score();
                ClearPool(BlockcardC1);
                ClearPool(BlockcardC2);
                // BlockSelect.selectedBlock = null;
                // BlockC1.GetComponent<BlockSelect>().selectionIndicator.color = BlockC1.GetComponent<BlockSelect>().normalColor;
                // BlockC2.GetComponent<BlockSelect>().selectionIndicator.color = BlockC1.GetComponent<BlockSelect>().normalColor;
                // BlockC1.GetComponent<BlockSelect>().selectionIndicator.gameObject.SetActive(false);
                // BlockC2.GetComponent<BlockSelect>().selectionIndicator.gameObject.SetActive(false);
                BlockC1.GetComponent<BlockSelect>().Reset();
                BlockC2.GetComponent<BlockSelect>().Reset();
                currentPhase = GamePhase.enemyDraw;
                phaseChangeEvent.Invoke();

                if (pilePool.Count > 0)
                {
                    DrawCard(1, 1);
                }
                else
                {
                    CheckOver();
                }
            }
        }

    }

    public void Cal_Score()
    {
        if (currentPhase == GamePhase.playerAction0)
        {
            CardDisplay cardDisplay = BlockcardA[0].GetComponent<CardDisplay>();
            AddScore(0, cardDisplay.card);
        }
        else if (currentPhase == GamePhase.enemyAction0)
        {
            CardDisplay cardDisplay = BlockcardB[0].GetComponent<CardDisplay>();  
            AddScore(1, cardDisplay.card);
        }

        else if (currentPhase == GamePhase.enemyReAction2)
        {
            CardDisplay cardDisplay = BlockcardA[0].GetComponent<CardDisplay>();
            CardDisplay cardDisplay1 = BlockcardA[1].GetComponent<CardDisplay>();
            AddScore(0, cardDisplay.card);
            AddScore(0, cardDisplay1.card);

            CardDisplay cardDisplay2 = BlockcardD[0].GetComponent<CardDisplay>();
            AddScore(1, cardDisplay2.card);
        }

        else if (currentPhase == GamePhase.playerReAction2)
        {
            CardDisplay cardDisplay = BlockcardB[0].GetComponent<CardDisplay>();
            CardDisplay cardDisplay1 = BlockcardB[1].GetComponent<CardDisplay>();
            AddScore(1, cardDisplay.card);
            AddScore(1, cardDisplay1.card);

            CardDisplay cardDisplay2 = BlockcardD[0].GetComponent<CardDisplay>();
            AddScore(0, cardDisplay2.card);
        }

        else if (currentPhase == GamePhase.enemyReAction3)
        {
            if (BlockSelect.selectedBlock == BlockC1.GetComponent<BlockSelect>())
            {
                CardDisplay cardDisplay = BlockcardC2[0].GetComponent<CardDisplay>();
                CardDisplay cardDisplay1 = BlockcardC2[1].GetComponent<CardDisplay>();
                AddScore(0, cardDisplay.card);
                AddScore(0, cardDisplay1.card);

                CardDisplay cardDisplay2 = BlockcardC1[0].GetComponent<CardDisplay>();
                CardDisplay cardDisplay3 = BlockcardC1[1].GetComponent<CardDisplay>();
                AddScore(1, cardDisplay2.card);
                AddScore(1, cardDisplay3.card);
            }

            else if (BlockSelect.selectedBlock == BlockC2.GetComponent<BlockSelect>())
            {
                CardDisplay cardDisplay = BlockcardC1[0].GetComponent<CardDisplay>();
                CardDisplay cardDisplay1 = BlockcardC1[1].GetComponent<CardDisplay>();
                AddScore(0, cardDisplay.card);
                AddScore(0, cardDisplay1.card);

                CardDisplay cardDisplay2 = BlockcardC2[0].GetComponent<CardDisplay>();
                CardDisplay cardDisplay3 = BlockcardC2[1].GetComponent<CardDisplay>();
                AddScore(1, cardDisplay2.card);
                AddScore(1, cardDisplay3.card);
            }

        }

        else if (currentPhase == GamePhase.playerReAction3)
        {
            if (BlockSelect.selectedBlock == BlockC1.GetComponent<BlockSelect>())
            {
                CardDisplay cardDisplay = BlockcardC1[0].GetComponent<CardDisplay>();
                CardDisplay cardDisplay1 = BlockcardC1[1].GetComponent<CardDisplay>();
                AddScore(0, cardDisplay.card);
                AddScore(0, cardDisplay1.card);

                CardDisplay cardDisplay2 = BlockcardC2[0].GetComponent<CardDisplay>();
                CardDisplay cardDisplay3 = BlockcardC2[1].GetComponent<CardDisplay>();
                AddScore(1, cardDisplay2.card);
                AddScore(1, cardDisplay3.card);
            }

            else if (BlockSelect.selectedBlock == BlockC2.GetComponent<BlockSelect>())
            {
                CardDisplay cardDisplay = BlockcardC2[0].GetComponent<CardDisplay>();
                CardDisplay cardDisplay1 = BlockcardC2[1].GetComponent<CardDisplay>();
                AddScore(0, cardDisplay.card);
                AddScore(0, cardDisplay1.card);

                CardDisplay cardDisplay2 = BlockcardC1[0].GetComponent<CardDisplay>();
                CardDisplay cardDisplay3 = BlockcardC1[1].GetComponent<CardDisplay>();
                AddScore(1, cardDisplay2.card);
                AddScore(1, cardDisplay3.card);
            }

        }

    }

    public void AddScore(int playerid, Card _card, int extre=0)
    {
        if (_card is GiftCard giftCard)
        {   
            int person = giftCard.person;
            if (playerid == 0)
            {
                playerAScores[person] += (1+extre);
                // totalScoreA += (1+extre);
            }
            else
            {
                playerBScores[person] += (1+extre);
                // totalScoreB += (1+extre);
            }
        }
    }


    public void reback(int playerid, bool all=false)
    {
        int targetState = playerid == 0 ? 0 : 1;

        // 找到所有 personstate 为目标状态的索引
        List<int> targetStates = new List<int>();
        for (int i = 0; i < personstate.Length; i++)
        {
            if (personstate[i] == targetState)
            {
                targetStates.Add(i);
            }
        }

        if (targetStates.Count == 0)
        {
            Debug.LogWarning($"No personstate with value {targetState} found.");
            return;
        }

        if (all)
        {
            // 如果 all 为 true，将所有目标状态置为 -1，并清空对应的分数
            foreach (int index in targetStates)
            {
                personstate[index] = -1;
                if (playerid == 0)
                {
                    totalPersonA -= playerAScores[index];
                    playerAScores[index] = 0;
                }
                else if (playerid == 1)
                {
                    totalPersonB -= playerBScores[index];
                    playerBScores[index] = 0;
                }
            }
        }
        else
        {
            // 随机选择一个目标状态的索引
            int randomIndex = targetStates[Random.Range(0, targetStates.Count)];
            personstate[randomIndex] = -1;

            // 清空对应玩家的分数
            if (playerid == 0)
            {
                totalPersonA -= playerAScores[randomIndex];
                playerAScores[randomIndex] = 0;
            }
            else if (playerid == 1)
            {
                totalPersonB -= playerBScores[randomIndex];
                playerBScores[randomIndex] = 0;
            }
        }
    }

    public void Dealbuff()  //buff效果
    {
        if (randombuffA==1)
        {
            totalScoreA += 1;
        }
        else if (randombuffA==2)
        {
            totalScoreA += 2;
        }
        else if (randombuffA==3)
        {
            reback(1);
        }
        else if (randombuffA==4)
        {
            reback(1, true);
        }
        else if (randombuffA==5)
        {
            totalScoreA -= 1;
        }
        else if (randombuffA==6)
        {
            totalScoreA -= 2;
        }
        else if (randombuffA==7)
        {
            reback(0);
        }


        if (randombuffB==1)
        {
            totalScoreB += 1;
        }
        else if (randombuffB==2)
        {
            totalScoreB += 2;
        }
        else if (randombuffB==3)
        {
            reback(0);
        }
        else if (randombuffB==4)
        {
            reback(0, true);
        }
        else if (randombuffB==5)
        {
            totalScoreB -= 1;
        }
        else if (randombuffB==6)
        {
            totalScoreA -= 2;
        }
        else if (randombuffB==7)
        {
            reback(0);
        }

    }


    public void CheckOver()
    {
        for (int i = 0; i < 7; i++)
        {
            Debug.Log($"CheckPerson{i}");
            if (personstate[i] == -1)
            {
                if (playerAScores[i] > playerBScores[i])  //A玩家赢得艺妓i
                {
                    personstate[i] = 0;
                    totalScoreA += playerAScores[i];
                    totalPersonA += 1;
                }
                else if (playerAScores[i] < playerBScores[i])  //B玩家赢得艺妓i
                {
                    personstate[i] = 1;
                    totalScoreB += playerBScores[i];
                    totalPersonB += 1;
                }
            }

            else if (personstate[i] == 0)
            {
                if (playerAScores[i] >= playerBScores[i])  //A玩家赢得艺妓i
                {
                    personstate[i] = 0;
                    totalScoreA += playerAScores[i];
                }
                else if (playerAScores[i] < playerBScores[i])  //B玩家赢得艺妓i
                {
                    personstate[i] = 1;
                    totalScoreB += playerBScores[i];
                    totalPersonA -= 1;
                    totalPersonB += 1;
                }
            }
            else if (personstate[i] == 1)
            {
                if (playerAScores[i] > playerBScores[i])  //A玩家赢得艺妓i
                {
                    personstate[i] = 0;
                    totalScoreA += playerAScores[i];
                    totalPersonA += 1;
                    totalPersonB -= 1;
                }
                else if (playerAScores[i] <= playerBScores[i])  //B玩家赢得艺妓i
                {
                    personstate[i] = 1;
                    totalScoreB += playerBScores[i];
                }
            }
        }

        Dealbuff(); // buff apply


        if (totalScoreA >= 13)  //A win
        {
            currentPhase = GamePhase.playerWin;
            phaseChangeEvent.Invoke();
            buttonObject.SetActive(true);
        }
        else if (totalScoreB >= 13) //B win
        {
            currentPhase = GamePhase.enemyWin;
            phaseChangeEvent.Invoke();
            buttonObject.SetActive(true);
        }
        else if (totalPersonA >= 4)  //A win
        {
            currentPhase = GamePhase.playerWin;
            phaseChangeEvent.Invoke();
            buttonObject.SetActive(true);
        }
        else if (totalPersonA >= 4)  //B win
        {
            currentPhase = GamePhase.playerWin;
            phaseChangeEvent.Invoke();
            buttonObject.SetActive(true);
        }
        else  //游戏继续
        {
            currentPhase = GamePhase.gameStart;
            phaseChangeEvent.Invoke();
            Reset();
        }

    }
}
