using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeginSceneManager : MonoBehaviour
{

    [SerializeField] private AudioSource sfxAudioSource;//��Ч������
    [SerializeField] private AudioClip beginGameSfx;//������Ч

    private float delayTimer=1.5f;
    private float delayTime=0;
    private bool isBegin;//�Ƿ�ʼ��Ϸ
    // Start is called before the first frame update
    void Start()
    {
        isBegin=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isBegin==true)
        {
            delayTime += Time.deltaTime;
            if (delayTime > delayTimer)
            {
                delayTime = 0;
                SceneManager.LoadScene(3);
            }
        }
    }

    public void BeginGame()//��ʼ��Ϸ
    {
        sfxAudioSource.PlayOneShot(beginGameSfx);//���ſ�ʼ��Ϸ������Ч
        isBegin = true;
    }

    public void ExitGame()//�˳���Ϸ
    {
        Application.Quit();
    }
}
