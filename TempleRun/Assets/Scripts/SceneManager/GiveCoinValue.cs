using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GiveCoinValue : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]TextMeshProUGUI coinsTextReal;
    void Start()
    {
        string coins = StaticData.coinsToKeep;
        coinsTextReal.text = coins;
    }


}
