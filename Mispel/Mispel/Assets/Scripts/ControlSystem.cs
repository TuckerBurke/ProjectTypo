using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSystem : MonoBehaviour
{
    public PlayerControls controls;

    private void Awake()
    {
        // Destroy all other versions of this when changing scenes
        GameObject[] controlSystems = GameObject.FindGameObjectsWithTag("Control System");
        if (controlSystems.Length <= 1)
        {
            controls = new PlayerControls();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            for(int i = 0; i < controlSystems.Length; i ++)
            {
                Destroy(controlSystems[i]);
            }
        }

    }
}
