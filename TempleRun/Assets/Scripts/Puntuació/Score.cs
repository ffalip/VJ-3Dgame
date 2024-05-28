using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreTextReal;
    private bool isColliding;
    PlayerMove pm;
    private void Awake()
    {
        score = 0;
        isColliding = false;
        pm = GetComponentInParent<PlayerMove>();
    }
    private void FixedUpdate()
    {
        if (!pm.getIsDead() && !pm.getIsFall())
        {
            score += 1;
            scoreTextReal.text = score.ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Moneda")
        {
            if (!isColliding)
            {
                isColliding = true;
                score+=100;
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
