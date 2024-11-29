using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource bgmAudioSource;//背景音乐播放器
    [SerializeField] private AudioSource sfxAudioSource;//音效播放器

    [SerializeField] private AudioClip bgm;//背景音乐
    [SerializeField] private AudioClip discardCardSfx;//出牌音效
    [SerializeField] private AudioClip distributeCardSfx;//发牌音效
    [SerializeField] private AudioClip buffSfx;//buff出现音效
    [SerializeField] private AudioClip chooseActionCardSfx;//选择行动卡音效
    [SerializeField] private AudioClip chooseCardSfx;//选牌音效
    [SerializeField] private AudioClip newbieSfx;//新手引导音效
    [SerializeField] private AudioClip gameoverSfx;//游戏结束音效


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //游戏开始时播放背景音乐
        bgmAudioSource.clip = bgm;
        bgmAudioSource.Play();
    }


    //播放出牌音效
    public void DiscardAudioPlay()
    {
        sfxAudioSource.PlayOneShot(discardCardSfx);
    }

    //播放发牌音效
    public void DistributeAudioPlay()
    {
        sfxAudioSource.PlayOneShot(distributeCardSfx);
    }

    //播放buff出现音效
    public void BuffAudioPlay()
    {
        sfxAudioSource.PlayOneShot(buffSfx);
    }

    //播放选择行动卡音效
    public void ChooseActionCardAudioPlay()
    {
        sfxAudioSource.PlayOneShot(chooseActionCardSfx);
    }

    //播放选择卡片音效
    public void ChooseCardAudioPlay()
    {
        sfxAudioSource.PlayOneShot(chooseCardSfx);
    }

    //播放新手引导下一步音效
    public void NewbieAudioPlay()
    {
        sfxAudioSource.PlayOneShot(newbieSfx);
    }

    //播放游戏结束音效
    public void GameOverAudioPlay()
    {
        sfxAudioSource.PlayOneShot(gameoverSfx);
    }
}
