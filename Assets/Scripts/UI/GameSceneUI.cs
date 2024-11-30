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
        if(Input.GetKeyDown(KeyCode.Escape))//����escʱ������ҳ��
        {
            EnableMask();
        }
    }

    //�����ڵ�����
    private void EnableMask()
    {
        isBack = true;
        animator.SetBool("isBack",isBack);
    }

    //�ڶ����ؼ�֡�лص���ҳ��
    private void BackToMainScene()
    {
            SceneManager.LoadScene(1);
    }
}
