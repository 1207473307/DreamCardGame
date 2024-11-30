using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffDisplay : MonoBehaviour
{
    public Text BuffA;
    public Text BuffB;

    private Dictionary<int, string> buffTexts = new Dictionary<int, string>();

    // Start is called before the first frame update
    void Start()
    {   
        InitializeBuffTexts();
        GameManager.Instance.phaseChangeEvent.AddListener(UpdateText);
    }

    // Update is called once per frame
    void Update()
    {
        // UpdateText();
    }

    void InitializeBuffTexts()
    {
        buffTexts[0] = "No Buff";
        buffTexts[1] = "梦境紊乱：己方梦珍值+1";
        buffTexts[2] = "梦境紊乱：己方梦珍值+2";
        buffTexts[3] = "梦境紊乱：随机释放一名敌方神明";
        buffTexts[4] = "梦境紊乱：敌方全部已解梦神明全部释放";
        buffTexts[5] = "梦境紊乱：己方梦珍值-1";
        buffTexts[6] = "梦境紊乱：己方梦珍值-2";
        buffTexts[7] = "梦境紊乱：随机释放一名己方神明";
    }
    void UpdateText()
    {
        BuffA.text = buffTexts.TryGetValue(GameManager.Instance.randombuffA, out string buffAText) ? buffAText : "Unknown Buff";
        BuffB.text = buffTexts.TryGetValue(GameManager.Instance.randombuffB, out string buffBText) ? buffBText : "Unknown Buff";
    }
}
