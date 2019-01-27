using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

    [SerializeField]
    GameObject[] hearts;

    //0 , 1, 2

    [SerializeField]
    [Range(1, 3)]
    int lives;

    int currentLives;
    //1, 2, 

    private void Awake()
    {
        currentLives = PlayerPrefs.GetInt("lives", lives);
        if (currentLives == 0)
        {
            currentLives = lives;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            bool isActive = i + 1 <= currentLives;
            hearts[i].SetActive(isActive);
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
        int index = Mathf.Clamp(currentLives, 0, hearts.Length-1);
        hearts[index].SetActive(false);
        PlayerPrefs.SetInt("lives", lives);
    }
}
