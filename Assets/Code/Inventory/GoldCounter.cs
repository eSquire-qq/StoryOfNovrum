using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GoldCounter : MonoBehaviour
{
    [SerializeField]
    private TMP_Text goldCounter;

    public int currentGold {get { return Int32.Parse(goldCounter.text); } }

    public void UpdateGold(int gold)
    {
        goldCounter.text = gold.ToString();
    }

    public void AddGold(int gold)
    {
        goldCounter.text = (Int32.Parse(goldCounter.text) + gold).ToString();
    }

    public void RemoveGold(int gold)
    {
        goldCounter.text = (Int32.Parse(goldCounter.text) - gold).ToString();
    }
}
