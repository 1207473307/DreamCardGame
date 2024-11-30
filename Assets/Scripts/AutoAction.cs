using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 
using System.Threading;

public class AutoAction : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Active = true; 
    void Start()
    {
        GameManager.Instance.phaseChangeEvent.AddListener(AutoActionSelect);
    }

    // Update is called once per frame
    void AutoActionSelect()
    {
        // 获取当前游戏阶段
        GamePhase currentPhase = GameManager.Instance.currentPhase;
        switch (currentPhase)
        {
            case GamePhase.enemyActionSelect:
                EnemySelectAction();
                break;

            case GamePhase.enemyAction0:
                EnemyAction0();
                break;
            case GamePhase.enemyAction1:
                EnemyAction1();
                break;
            case GamePhase.enemyAction2:
                EnemyAction2();
                break;
            case GamePhase.enemyAction3:
                EnemyAction3();
                break;

            case GamePhase.enemyReAction2:
                EnemyReAction2();
                break;
            case GamePhase.enemyReAction3:
                EnemyReAction3();
                break;

            default:
                break;
        }
    }

    void WaitAndConfirm()
    {
        // 等待卡片操作完成
        Thread.Sleep(1000); // 这里的时间可以根据实际需要调整

        // 模拟点击 buttonConfirmB 按钮
        if (GameManager.Instance.buttonConfirmB != null)
        {
            GameManager.Instance.buttonConfirmB.GetComponent<Button>().onClick.Invoke();
        }
    }

    void EnemySelectAction()
    {
        List<GameObject> actionCardsB = new List<GameObject>();
        Thread.Sleep(1000);
        foreach (Transform cardTransform in GameManager.Instance.ActionCardsB)
        {
            GameObject card = cardTransform.gameObject;
            // 如果卡片未使用（hasUsed == false），则可以选择
            if (!card.GetComponent<ActionSelect>().hasUsed)
            {
                actionCardsB.Add(card);
            }
        }

        // 如果有可用的卡片，随机选择一个
        if (actionCardsB.Count > 0)
        {
            int randomIndex = Random.Range(0, actionCardsB.Count);
            GameObject selectedCard = actionCardsB[randomIndex];

            // 模拟点击选中的卡片，调用OnPointerDown
            
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            selectedCard.GetComponent<ActionSelect>().OnPointerDown(pointerEventData);
            // 可以在这里加入一些调试信息来确认选择
            Debug.Log("Enemy selected action: " + selectedCard.name);
        }
        else
        {
            Debug.Log("No available action cards for enemy.");
        }

    }

    void EnemyAction0()
    {
        // 从 HandCardsB 中随机选择一张卡
        int randomIndex = Random.Range(0, GameManager.Instance.HandCardsB.childCount);
        Transform selectedCard = GameManager.Instance.HandCardsB.GetChild(randomIndex);

        // 确保该卡片是可用的并且符合条件
        CardAction cardAction = selectedCard.GetComponent<CardAction>();
        if (cardAction != null)
        {
            // 触发卡片的点击事件
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
            {
                pointerPress = selectedCard.gameObject,
                pointerDrag = selectedCard.gameObject,
                button = PointerEventData.InputButton.Left
            };

            // 调用卡片的点击方法
            cardAction.OnPointerDown(pointerEventData);

            // 等待一段时间确保卡片点击已经处理完毕（如果需要）
            WaitAndConfirm();
        }
    }

    void EnemyAction1()
    {
         // 随机选择 2 张卡片
        List<Transform> selectedCards = new List<Transform>();
        while (selectedCards.Count < 2)
        {
            int randomIndex = Random.Range(0, GameManager.Instance.HandCardsB.childCount);
            Transform selectedCard = GameManager.Instance.HandCardsB.GetChild(randomIndex);

            // 确保选中的卡片不重复
            if (!selectedCards.Contains(selectedCard))
            {
                selectedCards.Add(selectedCard);
            }
        }

        // 点击选择的卡片
        foreach (Transform card in selectedCards)
        {
            CardAction cardAction = card.GetComponent<CardAction>();
            if (cardAction != null)
            {
                // 模拟点击卡片
                PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
                {
                    pointerPress = card.gameObject,
                    pointerDrag = card.gameObject,
                    button = PointerEventData.InputButton.Left
                };

                // 触发卡片点击事件
                cardAction.OnPointerDown(pointerEventData);
            }
        }

        // 等待卡片操作完成后点击确认
        WaitAndConfirm();
    }

    void EnemyAction2()
    {
        // 从 HandCardsB 中随机选择 3 张卡片
        List<Transform> selectedCards = SelectRandomCards(GameManager.Instance.HandCardsB, 3);

        // 点击选择的卡片
        foreach (Transform card in selectedCards)
        {
            CardAction cardAction = card.GetComponent<CardAction>();
            if (cardAction != null)
            {
                // 模拟点击卡片
                PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
                {
                    pointerPress = card.gameObject,
                    pointerDrag = card.gameObject,
                    button = PointerEventData.InputButton.Left
                };

                // 触发卡片点击事件
                cardAction.OnPointerDown(pointerEventData);
            }
        }

        // 等待卡片操作完成后点击确认
        WaitAndConfirm();

    }

    void EnemyAction3()
    {
        List<Transform> selectedCards = SelectRandomCards(GameManager.Instance.HandCardsB, 4);

        // 点击选择的卡片
        foreach (Transform card in selectedCards)
        {
            CardAction cardAction = card.GetComponent<CardAction>();
            if (cardAction != null)
            {
                // 模拟点击卡片
                PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
                {
                    pointerPress = card.gameObject,
                    pointerDrag = card.gameObject,
                    button = PointerEventData.InputButton.Left
                };

                // 触发卡片点击事件
                cardAction.OnPointerDown(pointerEventData);
            }
        }

        // 等待卡片操作完成后点击确认
        WaitAndConfirm();
    }

    void EnemyReAction2()
    {
        // 从 BlockA 中随机选择 1 张卡片
        Transform selectedCard = SelectRandomCard(GameManager.Instance.BlockA);

        // 点击选择的卡片
        if (selectedCard != null)
        {
            CardAction cardAction = selectedCard.GetComponent<CardAction>();
            if (cardAction != null)
            {
                // 模拟点击卡片
                PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
                {
                    pointerPress = selectedCard.gameObject,
                    pointerDrag = selectedCard.gameObject,
                    button = PointerEventData.InputButton.Left
                };

                // 触发卡片点击事件
                cardAction.OnPointerDown(pointerEventData);
            }
        }

        // 等待卡片操作完成后点击确认
        WaitAndConfirm();
    }

    void EnemyReAction3()
    {
            // 从 BlockC1 和 BlockC2 中随机选择一个块
        Transform selectedBlock = SelectRandomBlock();

        // 如果选中了有效的块，模拟点击
        if (selectedBlock != null)
        {
            BlockSelect blockSelect = selectedBlock.GetComponent<BlockSelect>();
            if (blockSelect != null)
            {
                // 调用 Select 方法模拟选择
                blockSelect.Select();

                // 将 selectedBlock 设置为当前选中的块
                BlockSelect.selectedBlock = blockSelect;
            }
        }

        // 模拟点击确认按钮
        WaitAndConfirm();
    }

    // 辅助方法：从 BlockC1 和 BlockC2 中随机选择一个块
    Transform SelectRandomBlock()
    {
        // 将 BlockC1 和 BlockC2 中的块添加到列表中
        List<Transform> availableBlocks = new List<Transform> { GameManager.Instance.BlockC1, GameManager.Instance.BlockC2 };

        // 随机选择一个块
        int randomIndex = Random.Range(0, availableBlocks.Count);
        return availableBlocks[randomIndex];
    }

    
    // 辅助方法：从指定的 Transform 中随机选择 1 张卡片
    Transform SelectRandomCard(Transform cardPool)
    {
        if (cardPool.childCount > 0)
        {
            int randomIndex = Random.Range(0, cardPool.childCount);
            return cardPool.GetChild(randomIndex);
        }
        return null;
    }

    // 辅助方法：从指定的 Transform 中随机选择 N 张卡片
    List<Transform> SelectRandomCards(Transform cardPool, int numberOfCards)
    {
        List<Transform> selectedCards = new List<Transform>();
        while (selectedCards.Count < numberOfCards)
        {
            int randomIndex = Random.Range(0, cardPool.childCount);
            Transform selectedCard = cardPool.GetChild(randomIndex);

            // 确保选中的卡片不重复
            if (!selectedCards.Contains(selectedCard))
            {
                selectedCards.Add(selectedCard);
            }
        }
        return selectedCards;
    }


}
