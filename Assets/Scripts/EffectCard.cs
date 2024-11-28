using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectCard : MonoBehaviour
{
    public static EffectCard Instance;

    private List<PersonCard> enmeyPersonCardList;//����ʱ�з������б�
    private List<PersonCard> playerPersonCardList;//����ʱ��������б�

    private int enemyValue;//����ʱ�з��ܹ�����ֵ
    private int playerValue;//����ʱ����ܹ�����ֵ

    public Image currentBuffImage;//��ǰbuffͼƬ
    public Image currentBuffIcon;//��ǰbuffͼ��

    //buffЧ�����ʶ���
    private float buff1_Probability = 0.31f;
    private float buff2_Probability = 0.14f;
    private float buff3_Probability = 0.045f;
    private float buff4_Probability = 0.01f;
    private float buff5_Probability = 0.31f;
    private float buff6_Probability = 0.14f;
    private float buff7_Probability = 0.045f;

    //buffͼƬ
    [SerializeField] private Image buff1_Image;
    [SerializeField] private Image buff2_Image;
    [SerializeField] private Image buff3_Image;
    [SerializeField] private Image buff4_Image;
    [SerializeField] private Image buff5_Image;
    [SerializeField] private Image buff6_Image;
    [SerializeField] private Image buff7_Image;

    //buffͼ��
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

    public void EnableBuff()//����buff
    {
        //�����ʺ��Ƿ�Ϊ1
        if (buff1_Probability + buff2_Probability + buff1_Probability + buff3_Probability + buff4_Probability + buff5_Probability + buff6_Probability + buff7_Probability == 1)
        {
            RandomBuff();
        }
    }

    private void RandomBuff()//���buff
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

    private void buff1()//���ֽ���׶�ʱ����������ֵ+1,���ʣ�31%
    {
        playerValue+=1;
    }

    private void buff2()//���ֽ���׶�ʱ����������ֵ+2,���ʣ�14%
    {
        playerValue+=2;
    }

    private void buff3()//���ֽ���׶Σ����ʹ�Է�һ���ѻ�������ع鹫������,���ʣ�4.5%
    {
        int listNumber=Random.Range(0, enmeyPersonCardList.Count);
        enmeyPersonCardList.RemoveAt(listNumber);
    }

    private void buff4()//���ֽ���׶Σ��з�ȫ���ѽ�������ȫ���ͷţ���ȫ���ع鹫������,���ʣ�1%
    {
        for(int i = 0; i < enmeyPersonCardList.Count; i++)
        {
            enmeyPersonCardList.RemoveAt(i);
        }
    }

    private void buff5()//���ֽ���׶�ʱ����������ֵ-1,���ʣ�31%
    {
        playerValue -= 1;
    }

    private void buff6()//���ֽ���׶�ʱ����������ֵ-2�����ʣ�14%
    {
        playerValue -= 2;
    }

    private void buff7()//���ֽ���׶Σ��������һ���ѻ�������ع鹫�����������ʣ�4.5%
    {
        int listNumber = Random.Range(0, playerPersonCardList.Count);
        enmeyPersonCardList.RemoveAt(listNumber);
    }
}
