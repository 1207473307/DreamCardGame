using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseDisplay : MonoBehaviour
{
    public Text PhaseInfo;

    private Dictionary<GamePhase, string> phaseTexts = new Dictionary<GamePhase, string>();

    // Start is called before the first frame update
    void Start()
    {
        InitializePhaseTexts();
        GameManager.Instance.phaseChangeEvent.AddListener(UpdateText);
    }

    // Update is called once per frame
    void Update()
    {
        // UpdateText();
    }

        // 初始化字典
    void InitializePhaseTexts()
    {
        phaseTexts[GamePhase.gameStart] = "游戏开始!";
        phaseTexts[GamePhase.playerDraw] = "玩家A抽卡";
        phaseTexts[GamePhase.playerActionSelect] = "玩家A请选择唤醒行动";
        phaseTexts[GamePhase.playerAction0] = "玩家A: 梦隐";
        phaseTexts[GamePhase.playerAction1] = "玩家A: 梦碎";
        phaseTexts[GamePhase.playerAction2] = "玩家A: 梦华";
        phaseTexts[GamePhase.playerAction3] = "玩家A: 梦凌";
        phaseTexts[GamePhase.playerReAction2] = "玩家A: 请选择一张卡牌";
        phaseTexts[GamePhase.playerReAction3] = "玩家A: 请选择一组卡牌";
        phaseTexts[GamePhase.enemyDraw] = "玩家B抽卡";
        phaseTexts[GamePhase.enemyActionSelect] = "玩家B请选择唤醒行动";
        phaseTexts[GamePhase.enemyAction0] = "玩家B: 梦隐";
        phaseTexts[GamePhase.enemyAction1] = "玩家B: 梦碎";
        phaseTexts[GamePhase.enemyAction2] = "玩家B: 梦华";
        phaseTexts[GamePhase.enemyAction3] = "玩家B: 梦凌";
        phaseTexts[GamePhase.enemyReAction2] = "玩家B: 请选择一张卡牌";
        phaseTexts[GamePhase.enemyReAction3] = "玩家B: 请选择一组卡牌";
        phaseTexts[GamePhase.playerWin] = "玩家A胜利!";
        phaseTexts[GamePhase.enemyWin] = "玩家B胜利!";
    }
    void UpdateText()
    {
        // 获取当前游戏阶段
        GamePhase currentPhase = GameManager.Instance.currentPhase;

        // 检查字典中是否存在该阶段的文本
        if (phaseTexts.TryGetValue(currentPhase, out string text))
        {
            PhaseInfo.text = text; // 更新 UI
        }
        else
        {
            PhaseInfo.text = "Unknown Phase"; // 备用文本
        }
    }
}
