

using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

[Serializable]
public class UIManager : EventEmitter
{
    public List<ItemPanelUI> itemPanelUIList;
    public TextMeshProUGUI playerStatsLabelText;
    public TextMeshProUGUI playerStatsValueText;

    public void ShowItemSelection(List<ItemDataSO> itemDataList)
    {
        if (AudioController.Instance != null)
        {
            AudioController.Instance.PlaySfx(AudioController.AudioKeys.UiChestOpen);
        }

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

    public void SetUIStats(Stats stats)
    {
        playerStatsLabelText.text = GetStatsLabels();
        playerStatsValueText.text = GetStatsValues(stats);
    }

    public static string GetStatsInfo(Stats stats)
    {
        return
            $"Sát Thương Vật Lý: {stats.physicalDamage}\n" +
            $"Giáp: {stats.armor}\n" +
            $"Tỉ Lệ Bạo Kích: {stats.criticalChance}%\n" +
            $"Sát Thương Bạo Kích: {stats.criticalDamage}%\n" +
            $"Hút Máu: {stats.lifeSteal}%\n" +
            $"Sát Thương Phép: {stats.magicDamage}\n" +
            $"Kích Điện: {stats.poisonous}\n" +
            $"Gây Choáng: {stats.stunChance}%\n" +
            $"May Mắn: {stats.luck}\n" +
            $"Khuếch Đại ST Vật Lý: {stats.increaseDamage}%\n" +
            $"Khuếch Đại ST Phép: {stats.increaseMagic}%\n" +
            $"Phản Đòn: {stats.thorn}%\n" +
            $"Né Đòn: {stats.dodgeChance}%\n" +
            $"Linh Hoạt: {stats.speed}\n" +
            $"Cuồng Nộ: {stats.rage}\n" +
            $"Hồi Phục: {stats.regeneration}";
    }

    public static string GetStatsLabels()
    {
        return
            "Sát Thương Vật Lý\n" +
            "Giáp\n" +
            "Tỉ Lệ Bạo Kích\n" +
            "Sát Thương Bạo Kích\n" +
            "Hút Máu\n" +
            "Sát Thương Phép\n" +
            "Kích Điện\n" +
            "Gây Choáng\n" +
            "May Mắn\n" +
            "Khuếch Đại ST Vật Lý\n" +
            "Khuếch Đại ST Phép\n" +
            "Phản Đòn\n" +
            "Né Đòn\n" +
            "Linh Hoạt\n" +
            "Cuồng Nộ\n" +
            "Hồi Phục";
    }

    public static string GetStatsValues(Stats stats)
    {
        return
            $"{stats.physicalDamage}\n" +
            $"{stats.armor}\n" +
            $"{stats.criticalChance}%\n" +
            $"{stats.criticalDamage}%\n" +
            $"{stats.lifeSteal}%\n" +
            $"{stats.magicDamage}\n" +
            $"{stats.poisonous}\n" +
            $"{stats.stunChance}%\n" +
            $"{stats.luck}\n" +
            $"{stats.increaseDamage}%\n" +
            $"{stats.increaseMagic}%\n" +
            $"{stats.thorn}%\n" +
            $"{stats.dodgeChance}%\n" +
            $"{stats.speed}\n" +
            $"{stats.rage}\n" +
            $"{stats.regeneration}";
    }
}