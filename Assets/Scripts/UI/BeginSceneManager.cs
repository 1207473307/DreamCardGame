using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeginSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginGame()//开始游戏
    {
        SceneManager.LoadScene(3);
    }

    public void ExitGame()//退出游戏
    {
        Application.Quit();
    }
}
