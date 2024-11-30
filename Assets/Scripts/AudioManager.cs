using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource bgmAudioSource;//�������ֲ�����
    [SerializeField] private AudioSource sfxAudioSource;//��Ч������

    [SerializeField] private AudioClip bgm;//��������
    [SerializeField] private AudioClip discardCardSfx;//������Ч
    [SerializeField] private AudioClip distributeCardSfx;//������Ч
    [SerializeField] private AudioClip buffSfx;//buff������Ч
    [SerializeField] private AudioClip chooseActionCardSfx;//ѡ���ж�����Ч
    [SerializeField] private AudioClip chooseCardSfx;//ѡ����Ч
    [SerializeField] private AudioClip newbieSfx;//����������Ч
    [SerializeField] private AudioClip gameoverSfx;//��Ϸ������Ч


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //��Ϸ��ʼʱ���ű�������
        bgmAudioSource.clip = bgm;
        bgmAudioSource.Play();
    }


    //���ų�����Ч
    public void DiscardAudioPlay()
    {
        sfxAudioSource.PlayOneShot(discardCardSfx);
    }

    //���ŷ�����Ч
    public void DistributeAudioPlay()
    {
        sfxAudioSource.PlayOneShot(distributeCardSfx);
    }

    //����buff������Ч
    public void BuffAudioPlay()
    {
        sfxAudioSource.PlayOneShot(buffSfx);
    }

    //����ѡ���ж�����Ч
    public void ChooseActionCardAudioPlay()
    {
        sfxAudioSource.PlayOneShot(chooseActionCardSfx);
    }

    //����ѡ��Ƭ��Ч
    public void ChooseCardAudioPlay()
    {
        sfxAudioSource.PlayOneShot(chooseCardSfx);
    }

    //��������������һ����Ч
    public void NewbieAudioPlay()
    {
        sfxAudioSource.PlayOneShot(newbieSfx);
    }

    //������Ϸ������Ч
    public void GameOverAudioPlay()
    {
        sfxAudioSource.PlayOneShot(gameoverSfx);
    }
}