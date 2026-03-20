using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public enum UIEvent
{
    HoverStat,
    HoverDesc,
    HoverItem
}

[Serializable]
public class UIManager : EventEmitter
{
    public List<ItemPanelUI> itemPanelUIList;
    public TextMeshProUGUI playerStatsLabelText;
    public TextMeshProUGUI playerStatsValueText;
    public TextMeshProUGUI nameText;

    public GameObject skipButton;

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

            DOVirtual.DelayedCall(0.1f * index, () => { itemPanelUIList[index].transform.DOScaleX(1f, 0.2f); });
        }

        for (int i = 0; i < itemDataList.Count; i++)
        {
            itemPanelUIList[i].SetInfoFromItem(itemDataList[i]);
        }

        skipButton.SetActive(true);
    }

    public void CloseItemPanel()
    {
        for (int i = 0; i < itemPanelUIList.Count; i++)
        {
            itemPanelUIList[i].gameObject.SetActive(false);
        }

        skipButton.SetActive(false);
    }

    public void SetUIStats(Stats stats)
    {
        nameText.text = "";
        
        playerStatsLabelText.text = GetStatsLabels(stats);
        playerStatsValueText.text = GetStatsValues(stats);
    }

    public void SetItemStats(Item item)
    {
        List<string> classNames = new List<string>();
        foreach (var itemClass in item.itemClassTypes)
        {
            switch (itemClass)
            {
                case ItemClassType.DienNang:
                    classNames.Add("Điện Năng");
                    break;
                case ItemClassType.TaDien:
                    classNames.Add("Tà Điển");
                    break;
                case ItemClassType.BaoHo:
                    classNames.Add("Bảo Hộ");
                    break;
                case ItemClassType.CoKhi:
                    classNames.Add("Cơ Khí");
                    break;
                case ItemClassType.XayDung:
                    classNames.Add("Xây Dựng");
                    break;
                case ItemClassType.DoDac:
                    classNames.Add("Đồ Đạc");
                    break;
                case ItemClassType.NhietNang:
                    classNames.Add("Nhiệt Năng");
                    break;
                case ItemClassType.NangDo:
                    classNames.Add("Nặng Đô");
                    break;
                case ItemClassType.SacLem:
                    classNames.Add("Sắc Lẻm");
                    break;
            }
        }

        nameText.text = "[" + string.Join(", ", classNames) + "]";
        playerStatsLabelText.text = GetStatsLabels(item.stats);
        playerStatsValueText.text = GetStatsValues(item.stats);
    }

    public void SetDescription(string desc)
    {
        nameText.text = "";
        playerStatsValueText.text = "";
        playerStatsLabelText.text = desc;
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

    public static string GetStatsLabels(Stats stats)
    {
        List<string> labels = new List<string>();

        if (stats.physicalDamage > 0) labels.Add("Sát Thương Vật Lý");
        if (stats.armor > 0) labels.Add("Giáp");
        if (stats.criticalChance > 0) labels.Add("Tỉ Lệ Bạo Kích");
        if (stats.criticalDamage > 0) labels.Add("Sát Thương Bạo Kích");
        if (stats.lifeSteal > 0) labels.Add("Hút Máu");
        if (stats.magicDamage > 0) labels.Add("Sát Thương Phép");
        if (stats.poisonous > 0) labels.Add("Kích Điện");
        if (stats.stunChance > 0) labels.Add("Gây Choáng");
        if (stats.luck > 0) labels.Add("May Mắn");
        if (stats.increaseDamage > 0) labels.Add("Khuếch Đại ST Vật Lý");
        if (stats.increaseMagic > 0) labels.Add("Khuếch Đại ST Phép");
        if (stats.thorn > 0) labels.Add("Phản Đòn");
        if (stats.dodgeChance > 0) labels.Add("Né Đòn");
        if (stats.speed > 0) labels.Add("Linh Hoạt");
        if (stats.rage > 0) labels.Add("Cuồng Nộ");
        if (stats.regeneration > 0) labels.Add("Hồi Phục");

        return string.Join("\n", labels);
    }

    public static string GetStatsValues(Stats stats)
    {
        List<string> values = new List<string>();

        if (stats.physicalDamage > 0) values.Add($"{stats.physicalDamage}");
        if (stats.armor > 0) values.Add($"{stats.armor}");
        if (stats.criticalChance > 0) values.Add($"{stats.criticalChance}%");
        if (stats.criticalDamage > 0) values.Add($"{stats.criticalDamage}%");
        if (stats.lifeSteal > 0) values.Add($"{stats.lifeSteal}%");
        if (stats.magicDamage > 0) values.Add($"{stats.magicDamage}");
        if (stats.poisonous > 0) values.Add($"{stats.poisonous}");
        if (stats.stunChance > 0) values.Add($"{stats.stunChance}%");
        if (stats.luck > 0) values.Add($"{stats.luck}");
        if (stats.increaseDamage > 0) values.Add($"{stats.increaseDamage}%");
        if (stats.increaseMagic > 0) values.Add($"{stats.increaseMagic}%");
        if (stats.thorn > 0) values.Add($"{stats.thorn}%");
        if (stats.dodgeChance > 0) values.Add($"{stats.dodgeChance}%");
        if (stats.speed > 0) values.Add($"{stats.speed}");
        if (stats.rage > 0) values.Add($"{stats.rage}");
        if (stats.regeneration > 0) values.Add($"{stats.regeneration}");

        return string.Join("\n", values);
    }
}