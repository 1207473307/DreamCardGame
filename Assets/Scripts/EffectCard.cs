using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectCard : MonoBehaviour
{
    public static EffectCard Instance;

    private List<PersonCard> enmeyPersonCardList;//结算时敌方忆者列表
    private List<PersonCard> playerPersonCardList;//结算时玩家忆者列表

    private int enemyValue;//结算时敌方总共魅力值
    private int playerValue;//结算时玩家总共魅力值

    public Image currentBuffImage;//当前buff图片
    public Image currentBuffIcon;//当前buff图标

    //buff效果概率定义
    private float buff1_Probability = 0.31f;
    private float buff2_Probability = 0.14f;
    private float buff3_Probability = 0.045f;
    private float buff4_Probability = 0.01f;
    private float buff5_Probability = 0.31f;
    private float buff6_Probability = 0.14f;
    private float buff7_Probability = 0.045f;

    //buff图片
    [SerializeField] private Image buff1_Image;
    [SerializeField] private Image buff2_Image;
    [SerializeField] private Image buff3_Image;
    [SerializeField] private Image buff4_Image;
    [SerializeField] private Image buff5_Image;
    [SerializeField] private Image buff6_Image;
    [SerializeField] private Image buff7_Image;

    //buff图标
    [SerializeField] private Image buff1_Icon;
    [SerializeField] private Image buff2_Icon;
    [SerializeField] private Image buff3_Icon;
    [SerializeField] private Image buff4_Icon;
    [SerializeField] private Image buff5_Icon;
    [SerializeField] private Image buff6_Icon;
    [SerializeField] private Image buff7_Icon;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableBuff()//启用buff
    {
        //检查概率合是否为1
        if (buff1_Probability + buff2_Probability + buff1_Probability + buff3_Probability + buff4_Probability + buff5_Probability + buff6_Probability + buff7_Probability == 1)
        {
            RandomBuff();
        }
    }

    private void RandomBuff()//随机buff
    {
        float probability = Random.Range(0, 1);
        if(probability < buff1_Probability)
        {
            buff1();
            currentBuffImage = buff1_Image;
            currentBuffIcon = buff1_Icon;
        }
        else if(probability < buff1_Probability+buff2_Probability)
        {
            buff2();
            currentBuffImage = buff2_Image;
            currentBuffIcon = buff2_Icon;
        }
        else if(probability < buff1_Probability + buff2_Probability+buff3_Probability)
        {
            buff3();
            currentBuffImage = buff3_Image;
            currentBuffIcon = buff3_Icon;
        }
        else if(probability < buff1_Probability + buff2_Probability + buff3_Probability+buff4_Probability)
        {
            buff4();
            currentBuffImage = buff4_Image;
            currentBuffIcon = buff4_Icon;
        }
        else if (probability < buff1_Probability + buff2_Probability + buff3_Probability + buff4_Probability+buff5_Probability)
        {
            buff5();
            currentBuffImage = buff5_Image;
            currentBuffIcon = buff5_Icon;
        }
        else if (probability < buff1_Probability + buff2_Probability + buff3_Probability + buff4_Probability + buff5_Probability+buff6_Probability)
        {
            buff6();
            currentBuffImage = buff6_Image;
            currentBuffIcon = buff6_Icon;
        }
        else
        {
            buff7();
            currentBuffImage = buff7_Image;
            currentBuffIcon = buff7_Icon;
        }

    }

    private void buff1()//本局结算阶段时，己方梦珍值+1,概率：31%
    {
        playerValue+=1;
    }

    private void buff2()//本局结算阶段时，己方梦珍值+2,概率：14%
    {
        playerValue+=2;
    }

    private void buff3()//本局结算阶段，随机使对方一名已获得神明回归公共牌区,概率：4.5%
    {
        int listNumber=Random.Range(0, enmeyPersonCardList.Count);
        enmeyPersonCardList.RemoveAt(listNumber);
    }

    private void buff4()//本局结算阶段，敌方全部已解梦神明全部释放，即全部回归公共卡牌,概率：1%
    {
        for(int i = 0; i < enmeyPersonCardList.Count; i++)
        {
            enmeyPersonCardList.RemoveAt(i);
        }
    }

    private void buff5()//本局结算阶段时，己方梦珍值-1,概率：31%
    {
        playerValue -= 1;
    }

    private void buff6()//本局结算阶段时，己方梦珍值-2，概率：14%
    {
        playerValue -= 2;
    }

    private void buff7()//本局结算阶段，己方随机一名已获得神明回归公共牌区，概率：4.5%
    {
        int listNumber = Random.Range(0, playerPersonCardList.Count);
        enmeyPersonCardList.RemoveAt(listNumber);
    }
}
