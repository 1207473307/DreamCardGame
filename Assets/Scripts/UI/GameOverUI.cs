using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI instance;

    private Animator animator;
    private bool isPlayAnim;//是否播放动画
    private bool isWin;//是否胜利
    private bool isLoss;//是否失败

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
        /* 游戏结束动画测试
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

    private void PlayGameOverWinAnim()//播放游戏胜利结束动画
    {
        isPlayAnim = true;
        isWin=true;
        animator.SetBool("isPlay",isPlayAnim);
        animator.SetBool("isWin", isWin);
    }

    private void PlayGameOverLossAnim()//播放游戏失败结束动画
    {
        isPlayAnim = true;
        isLoss = true;
        animator.SetBool("isPlay", isPlayAnim);
        animator.SetBool("isLoss", isLoss);
    }

    public void ContinueGame()//继续游戏按钮点击事件，重新载入场景
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGame()//返回主菜单按钮点击事件，载入主菜单场景
    {
        Application.Quit();
    }
}
