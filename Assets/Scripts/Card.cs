// 卡牌类 基础类
public class Card
{
    //卡牌编号
    public int id;
    public string cardName;
    // 魅力值
    public int value;
    // 对应礼物
    public string gift;

    public bool back;

    //实例化函数
    public Card(int _id, string _cardName, int _value, string _gift)
    {
        this.id = _id;
        this.cardName = _cardName;
        this.value = _value;
        this.gift = _gift;
        this.back = false;
    }

}

// 艺妓卡类  继承自卡牌类
public class PersonCard : Card
{
    
    //心之所属  0:平局 1：A玩家  -1：B玩家
    public int heart;
    //A玩家礼物数量
    public int gift_A;
    //B玩家礼物数量
    public int gift_B;


    //实例化函数
    public PersonCard(int _id, string _cardName, int _value, string _gift) : base(_id, _cardName, _value, _gift)
    {
        this.heart = 0;
        this.gift_A = 0;
        this.gift_B = 0;
    }
}

// 礼物卡类 继承自卡牌类
public class GiftCard : Card
{
    // 对应艺妓编号（0~6）
    public int person;
    // 状态编码
    public int state;


    public GiftCard(int _id, string _cardName, int _value, string _gift, int _person) : base(_id, _cardName, _value, _gift)
    {
        this.person = _person;
        this.state = 0;
    }
}

// 行动卡
public class ActionCard
{
    //卡牌编号
    public int id;
    public string cardName;
    public bool back;
    public ActionCard(int _id, string _cardName)
    {
        this.id = _id;
        this.cardName = _cardName;
        this.back = false;
    }
}