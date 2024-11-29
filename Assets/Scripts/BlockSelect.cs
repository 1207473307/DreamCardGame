using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlockSelect : MonoBehaviour, IPointerDownHandler
{
    public static BlockSelect selectedBlock = null; // Tracks the currently selected block
    public int blockId; // 1 for BlockC1, 2 for BlockC2
    public Color selectedColor = Color.red;
    public Color normalColor = Color.white;
    public Image selectionIndicator; // 选中时显示的 Image 对象
    void Start()
    {
        if (selectionIndicator != null)
        {
            selectionIndicator.color = normalColor; // 默认隐藏选中图标
        }
        else
        {
            Debug.LogError($"SelectionIndicator is not assigned on {gameObject.name}!");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (selectedBlock == this) // 如果当前块已选中
        {
            Deselect();
            selectedBlock = null;
        }
        else
        {
            if (selectedBlock != null) // 如果有其他块被选中，取消其选中状态
            {
                selectedBlock.Deselect();
            }

            Select();
            selectedBlock = this;
        }
    }

    private void Select()
    {
        if (selectionIndicator != null)
        {
            selectionIndicator.color = selectedColor; // 显示选中图标
        }
        Debug.Log($"BlockC{blockId} selected");
    }

    private void Deselect()
    {
        if (selectionIndicator != null)
        {
            selectionIndicator.color = normalColor; // 隐藏选中图标
        }
        Debug.Log($"BlockC{blockId} deselected");
    }
}