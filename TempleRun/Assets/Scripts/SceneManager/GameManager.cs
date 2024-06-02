using System.Collections;
using System.Collections.Generic;
using TMPro;

//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI coins;
    // Start is called before the first frame update

    public void GameScene(string sceneName)
    {
        if (sceneName == "Credits")
        {
            string scoreToKeep = score.text;
            string coinsToKeep = coins.text;
            StaticData.scoreToKeep = scoreToKeep;
            StaticData.coinsToKeep = coinsToKeep;
        }
        SceneManager.LoadScene(sceneName);
      
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
