using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager {

    public static Vector3 lastPosition = Vector3.left;

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
        Vector3 input = new Vector3(MainHorizontal(), MainVertical(), 0.0f);
        if (input != Vector3.zero)
        {
            lastPosition = input;
        }
        return input;
    }

    public static bool isDashing()
    { 
        return Input.GetButtonDown("Dash");
    }
}
