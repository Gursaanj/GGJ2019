using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour {

    [SerializeField]
    BaseEnemyController deadCharacter;

    [SerializeField]
    GameObject Restart;

    private GameObject player;

    public static TransitionManager instance = null;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        deadCharacter.onDeathDelegate += startTransition;
        StartCoroutine(fadeInScene());
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnPlayerDeath()
    {
        Restart.SetActive(true);
    }

    void startTransition()
    {
        StartCoroutine(fadeOutScene());
    }

    IEnumerator fadeOutScene()
    {
        for (float i = 0; i <= 1f; i += 0.2f) {
            yield return new WaitForSeconds(0.1f);
            GetComponent<Image>().color = new Color(0, 0, 0, i);
        }
        changeScene();
    }

    IEnumerator fadeInScene()
    {
        GetComponent<Image>().color = new Color(0, 0, 0, 1);
        for (float i = 1; i >= 0f; i -= 0.2f)
        {
            yield return new WaitForSeconds(0.1f);
            GetComponent<Image>().color = new Color(0, 0, 0, i);
        }
    }

    void changeScene()
    {
        int num = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(num);
    }
}
