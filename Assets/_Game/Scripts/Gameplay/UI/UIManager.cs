

using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class UIManager : EventEmitter
{
    public List<ItemPanelUI> itemPanelUIList;
    public void ShowItemSelection(List<ItemDataSO> itemDataList)
    {
        for (int i = 0; i < itemPanelUIList.Count; i++)
        {
            itemPanelUIList[i].gameObject.SetActive(true);
            int index = i;

            itemPanelUIList[index].transform.localScale = new Vector3(0f, 1f, 1f);

            DOVirtual.DelayedCall(0.1f * index, () =>
            {
                itemPanelUIList[index].transform.DOScaleX(1f, 0.2f);
            });
        }

        for (int i = 0; i < itemDataList.Count; i++)
        {
            itemPanelUIList[i].SetInfoFromItem(itemDataList[i]);
        }
    }

    public void CloseItemPanel()
    {
        for (int i = 0; i < itemPanelUIList.Count; i++)
        {
            itemPanelUIList[i].gameObject.SetActive(false);
        }
    }
}