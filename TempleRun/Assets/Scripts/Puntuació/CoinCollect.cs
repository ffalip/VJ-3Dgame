using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollect : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;
    private bool isColliding;

    public AudioSource collectSound;

    private void Awake()
    {
        score = 0;
        isColliding = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Moneda")
        {
            Destroy(other.gameObject);
            if (!isColliding)
            {
                collectSound.Play();
                //collectSound.pitch += 0.02f;
                isColliding = true;
                ++score;
                scoreText.text = score.ToString();
                StartCoroutine(ResetCollisionFlag());
            }
        }
    }

    private IEnumerator ResetCollisionFlag()
    {
        yield return new WaitForSeconds(0.1f); // espera 0.1 segundos
        isColliding = false;
    }


}
