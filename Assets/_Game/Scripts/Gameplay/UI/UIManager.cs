

using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class UIManager : EventEmitter
{
    public GameObject itemSelectionPanel;
    public List<ItemPanelUI> itemPanelUIList;
    public void ShowItemSelection(List<ItemDataSO> itemDataList)
    {
        itemSelectionPanel.SetActive(true);


        for (int i = 0; i < itemPanelUIList.Count; i++)
        {
            itemPanelUIList[i].transform.localScale = new Vector3(0f, 1f, 1f);

            // DOVirtual.DelayedCall(0.1f * i, () =>
            // {
            itemPanelUIList[i].transform.DOScaleX(1f, 0.2f);
            // });
        }

        for (int i = 0; i < itemDataList.Count; i++)
        {
            itemPanelUIList[i].SetInfoFromItem(itemDataList[i]);
        }
    }

    public void CloseItemPanel()
    {
        itemSelectionPanel.SetActive(false);
        
    }
}