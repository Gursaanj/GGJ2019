﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameController : MonoBehaviour {

    [SerializeField]
    float speed;
    Vector3 destinationScale = new Vector3(0.75f, 0.75f, 1);
    [SerializeField]
    bool startMainMenu = false;

    bool isShrinking;

    // Update is called once per frame
    void Update () {
        animateButton();
        buttonListen();
    }

    void buttonListen()
    {
        if(Input.anyKeyDown)
        {
            int index = startMainMenu ? 0 : 1;
            if (startMainMenu) { 
                Destroy(AudioManager.instance.gameObject);
            }
            SceneManager.LoadScene(index);
        }
    }

    void animateButton()
    {
        Vector3 currentDestination = destinationScale;

        if (!isShrinking)
        {
            currentDestination = Vector3.one;
        }

        transform.localScale = Vector3.MoveTowards(transform.localScale, currentDestination, speed * Time.deltaTime);

        if (transform.localScale == destinationScale)
        {
            isShrinking = false;
        }

        if (transform.localScale == Vector3.one)
        {
            isShrinking = true;
        }
    }
}
