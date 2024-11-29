using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeginSceneManager : MonoBehaviour
{

    [SerializeField] private AudioSource sfxAudioSource;//音效播放器
    [SerializeField] private AudioClip beginGameSfx;//出牌音效

    private float delayTimer=1.5f;
    private float delayTime=0;
    private bool isBegin;//是否开始游戏
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

    public void BeginGame()//开始游戏
    {
        sfxAudioSource.PlayOneShot(beginGameSfx);//播放开始游戏按键音效
        isBegin = true;
    }

    public void ExitGame()//退出游戏
    {
        Application.Quit();
    }
}
