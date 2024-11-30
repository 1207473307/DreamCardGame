using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneUI : MonoBehaviour
{
    private Animator animator;
    private bool isBack;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isBack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))//按下esc时返回主页面
        {
            EnableMask();
        }
    }

    //启动遮挡动画
    private void EnableMask()
    {
        isBack = true;
        animator.SetBool("isBack",isBack);
    }

    //在动画关键帧中回到主页面
    private void BackToMainScene()
    {
            SceneManager.LoadScene(1);
    }
}
