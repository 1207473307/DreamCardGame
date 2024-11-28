using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectCardDisplay : MonoBehaviour
{
    public static EffectCardDisplay Instance;

    public Image buffImage;
    public Image buffIcon;
    private Animator animator;


    private bool isPlay=false;//�Ƿ񲥷Ŷ���

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*  ��������
        if(Input.GetKeyDown(KeyCode.F))
        {
            EffectDisplay();
        }
        */
    }

    public void EffectDisplay()//����buff���ֶ���
    {
        buffImage=EffectCard.Instance.currentBuffImage;
        buffIcon=EffectCard.Instance.currentBuffIcon;
        isPlay = true;
        animator.SetBool("isPlay", isPlay);
    }

    private void SetDisplayBool()//�����������
    {
        isPlay=false;
    }


}
