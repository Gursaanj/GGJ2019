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
        currentLives = lives;
        for (int i = 0; i < hearts.Length; i++)
        {
            bool isActive = i+1 <= lives;
            hearts[i].SetActive(isActive);
        }
    }

    public void decremeantHearts()
    {
        currentLives--;
        hearts[currentLives].SetActive(false);
    }
}
