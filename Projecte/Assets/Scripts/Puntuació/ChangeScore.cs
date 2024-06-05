using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScore : MonoBehaviour
{
    TextMeshProUGUI scoreText;

    public void modifyText(int score)
    {
        scoreText.text = score.ToString();
    }
}
