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

    public void BeginGame()//��ʼ��Ϸ
    {
        SceneManager.LoadScene(3);
    }

    public void ExitGame()//�˳���Ϸ
    {
        Application.Quit();
    }
}
