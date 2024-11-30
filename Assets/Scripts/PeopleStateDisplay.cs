using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PeopleStateDisplay : MonoBehaviour
{   
    public Image[] AImages = new Image[7]; // 用数组存储 A 组的 Image
    public Image[] BImages = new Image[7]; // 用数组存储 B 组的 Image

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.phaseChangeEvent.AddListener(UpdateImages);
    }


    void UpdateImages()
    {
        for (int i = 0; i < GameManager.Instance.personstate.Length; i++)
        {
            if (GameManager.Instance.personstate[i] == -1)
            {
                // -1 表示双方的 Image 都不显示
                AImages[i].gameObject.SetActive(false);
                BImages[i].gameObject.SetActive(false);
            }
            else if (GameManager.Instance.personstate[i] == 0)
            {
                // 0 表示显示 A 的 Image，隐藏 B 的 Image
                AImages[i].gameObject.SetActive(true);
                BImages[i].gameObject.SetActive(false);
            }
            else if (GameManager.Instance.personstate[i] == 1)
            {
                // 1 表示显示 B 的 Image，隐藏 A 的 Image
                AImages[i].gameObject.SetActive(false);
                BImages[i].gameObject.SetActive(true);
            }
        }
    }

}
