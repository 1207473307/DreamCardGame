using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : MonoBehaviour
{
    public List<Card> CardList = new List<Card>(); // 存储卡牌数据的链表
    public List<ActionCard> ActionCardList = new List<ActionCard>(); // 存储行动卡牌数据的链表
    public TextAsset cardListData; // 卡牌数据txt文件
    // Start is called before the first frame update
    void Start()
    {
       LordCardList(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LordCardList()
    {
        string[] dataArray = cardListData.text.Split('\n');
        foreach (var row in dataArray)
        {
            string[] rowArray = row.Split(',');
            if (rowArray[0] == "#")
            {
                continue;
            }
            else if (rowArray[0] == "personcard")  //艺妓卡
            {
                int id = int.Parse(rowArray[1]);
                string name = rowArray[2];
                int value = int.Parse(rowArray[3]);
                string gift = rowArray[4];
                CardList.Add(new PersonCard(id, name, value, gift));

                // Debug.Log("读取到艺妓卡："+ id);
            }
            else if (rowArray[0] == "giftcard")  //礼物卡
            {
                int id = int.Parse(rowArray[1]);
                string name = rowArray[2];
                int value = int.Parse(rowArray[3]);
                string gift = rowArray[4];
                int person = int.Parse(rowArray[5]);
                CardList.Add(new GiftCard(id, name, value, gift, person));
                // Debug.Log("读取到礼物卡："+ id);
            }
        }
    }


    public Card GetPersonCard(int _id)
    {
        return CardList[_id];
    }

    public Card RandomCard()
    {
        Card randCard = CardList[Random.Range(7, CardList.Count)];
        return randCard;
    }

    public Dictionary<string, List<Card>> DistributeCards()
    {

        // 提取 7-27 的卡片索引范围
        List<Card> selectedCards = CardList.GetRange(7, 21); // 包括索引 7 到 27 的 21 张卡

        // 洗牌（确保随机性）
        Shuffle(selectedCards);

        // 分配卡片
        Card discardedCard = selectedCards[0];              // 弃牌 1 张
        List<Card> playerACards = selectedCards.GetRange(1, 6);  // A 玩家手牌 6 张
        List<Card> playerBCards = selectedCards.GetRange(7, 6);  // B 玩家手牌 6 张
        List<Card> remainingDeck = selectedCards.GetRange(13, 8); // 剩余牌堆 8 张

            // 使用字典存储结果
        Dictionary<string, List<Card>> result = new Dictionary<string, List<Card>>
        {
            { "DiscardPile", new List<Card> { discardedCard } },
            { "HandCardsA", playerACards },
            { "HandCardsB", playerBCards },
            { "Pile", remainingDeck }
        };

        return result;
    }

    // Fisher-Yates 洗牌算法
    private void Shuffle(List<Card> cards)
    {
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            Card temp = cards[i];
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }
  

    // 行动卡数据
    public List<ActionCard> GetActionCards()
    {   
        ActionCardList.Clear();
        ActionCardList.Add(new ActionCard(0, "密约"));
        ActionCardList.Add(new ActionCard(1, "取舍"));
        ActionCardList.Add(new ActionCard(2, "赠予"));
        ActionCardList.Add(new ActionCard(3, "竞争"));

        return ActionCardList;

    }

}
