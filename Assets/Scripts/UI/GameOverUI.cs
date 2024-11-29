using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI instance;

    private Animator animator;
    private bool isPlayAnim;//�Ƿ񲥷Ŷ���
    private bool isWin;//�Ƿ�ʤ��
    private bool isLoss;//�Ƿ�ʧ��

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        isPlayAnim=false;
        isWin=false;
        isLoss=false;
    }

    // Update is called once per frame
    void Update()
    {
        /* ��Ϸ������������
        if(Input.GetKeyDown(KeyCode.F))
        {
            PlayGameOverWinAnim();
        }

        if(Input.GetKeyUp(KeyCode.V))
        {
            PlayGameOverLossAnim();
        }
        */
    }

    private void PlayGameOverWinAnim()//������Ϸʤ����������
    {
        isPlayAnim = true;
        isWin=true;
        animator.SetBool("isPlay",isPlayAnim);
        animator.SetBool("isWin", isWin);
    }

    private void PlayGameOverLossAnim()//������Ϸʧ�ܽ�������
    {
        isPlayAnim = true;
        isLoss = true;
        animator.SetBool("isPlay", isPlayAnim);
        animator.SetBool("isLoss", isLoss);
    }

    public void ContinueGame()//������Ϸ��ť����¼����������볡��
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGame()//�������˵���ť����¼����������˵�����
    {
        Application.Quit();
    }
}
