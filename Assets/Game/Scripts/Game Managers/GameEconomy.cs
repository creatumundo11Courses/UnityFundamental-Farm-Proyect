using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameEconomy : MonoBehaviour
{
    public static int Coins;
    public TextMeshProUGUI CoinsTMP;

    private void Start()
    {
        Coins = 100;
    }
    private void Update()
    {
        CoinsTMP.text = Coins.ToString();
    }
}
