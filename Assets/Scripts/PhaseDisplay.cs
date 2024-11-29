using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseDisplay : MonoBehaviour
{
    public Text PhaseInfo;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.phaseChangeEvent.AddListener(UpdateText);
    }

    // Update is called once per frame
    void Update()
    {
        // UpdateText();
    }

    void UpdateText()
    {
        PhaseInfo.text = GameManager.Instance.currentPhase.ToString();
    }
}
