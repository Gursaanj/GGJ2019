using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager {

    public static Vector3 lastPosition = Vector3.left;

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
    private readonly static string platform = "_Windows";
#else
    private readonly static string platform = "_OSX";
#endif

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
        return Input.GetButtonDown("Dash"+ platform);
    }

    public static bool isMelee()
    {
        float r = Input.GetAxis("Melee" + platform);
        return r > 0 || Input.GetButtonDown("Melee_KB");
    }
    
    public static bool isShooting()
    {
        float r = Input.GetAxis("Fire" + platform);
        return r > 0 || Input.GetButtonDown("Fire_KB");
    }
}
