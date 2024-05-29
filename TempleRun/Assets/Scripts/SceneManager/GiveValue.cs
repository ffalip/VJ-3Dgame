using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GiveValue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreTextReal;
    void Start()
    {
        string score = StaticData.scoreToKeep;
        scoreTextReal.text = score;
    }

}
