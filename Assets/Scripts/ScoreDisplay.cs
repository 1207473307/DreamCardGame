using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text TotalScoreA;
    public Text TotalScoreB;
    public Text ScoreA0;
    public Text ScoreA1;
    public Text ScoreA2;
    public Text ScoreA3;
    public Text ScoreA4;
    public Text ScoreA5;
    public Text ScoreA6;
    public Text ScoreB0;
    public Text ScoreB1;
    public Text ScoreB2;
    public Text ScoreB3;
    public Text ScoreB4;
    public Text ScoreB5;
    public Text ScoreB6;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.phaseChangeEvent.AddListener(UpdateScore);
    }

    // Update is called once per frame
    void Update()
    {
        // UpdateText();
    }

    void UpdateScore()
    {
        TotalScoreA.text = GameManager.Instance.totalScoreA.ToString();
        TotalScoreB.text = GameManager.Instance.totalScoreB.ToString();
        ScoreA0.text = GameManager.Instance.playerAScores[0].ToString();
        ScoreA1.text = GameManager.Instance.playerAScores[1].ToString();
        ScoreA2.text = GameManager.Instance.playerAScores[2].ToString();
        ScoreA3.text = GameManager.Instance.playerAScores[3].ToString();
        ScoreA4.text = GameManager.Instance.playerAScores[4].ToString();
        ScoreA5.text = GameManager.Instance.playerAScores[5].ToString();
        ScoreA6.text = GameManager.Instance.playerAScores[6].ToString();
        ScoreB0.text = GameManager.Instance.playerBScores[0].ToString();
        ScoreB1.text = GameManager.Instance.playerBScores[1].ToString();
        ScoreB2.text = GameManager.Instance.playerBScores[2].ToString();
        ScoreB3.text = GameManager.Instance.playerBScores[3].ToString();
        ScoreB4.text = GameManager.Instance.playerBScores[4].ToString();
        ScoreB5.text = GameManager.Instance.playerBScores[5].ToString();
        ScoreB6 .text= GameManager.Instance.playerBScores[6].ToString();
    }
}
