using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager {

    public static float MainHorizontal()
    {
        float r = Input.GetAxis("Horizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f); //If both joystick and keyboard are being used, clamp between values
    }

    public static float MainVertical()
    {
        float r = Input.GetAxis("Vertical");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static Vector3 MainInput()
    {
        return new Vector3(MainHorizontal(), MainVertical(), 0.0f);
    }

    /*
    void Update()
    {
        Vector3 direction = InputManager.MainInput(); //Get input
        Move(direction);
    }

    private void Move(Vector3 direction)
    {
        playerRigidbody.velocity = (Vector3.Normalize(direction) * base.moveSpeed);
    }
    */
}
