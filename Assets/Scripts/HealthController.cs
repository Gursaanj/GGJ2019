using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

    [SerializeField]
    GameObject[] hearts;

    [SerializeField]
    [Range(1, 3)]
    int lives;

    int currentLives;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("lives", 0) != 0)
        {
            currentLives = PlayerPrefs.GetInt("lives");
            for (int i = 0; i < hearts.Length; i++)
            {
                bool isActive = i + 1 <= lives;
                hearts[i].SetActive(isActive);
            }
        }
        else
        {
            currentLives = lives;
            for (int i = 0; i < hearts.Length; i++)
            {
                bool isActive = i + 1 <= lives;
                hearts[i].SetActive(isActive);
            }
        }
    }
   

    public void incremeantHearts()
    {
        currentLives++;
        hearts[currentLives].SetActive(true);
        lives = currentLives;
        PlayerPrefs.SetInt("lives", lives);
    }
    public void decremeantHearts()
    {
        currentLives--;
        currentLives = Mathf.Clamp(currentLives - 1, 0, lives);
        hearts[currentLives].SetActive(false);
        lives = currentLives;
        PlayerPrefs.SetInt("lives", lives);
    }
}
