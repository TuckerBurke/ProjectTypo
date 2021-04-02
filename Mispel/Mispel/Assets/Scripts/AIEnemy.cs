using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : Character
{
    private float walkinTimer;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        horizontalSpeed = 3;
        runPotentialAirSpeed = horizontalAirSpeed = horizontalSpeed;
        currentState = CharacterStates.Falling;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if(currentState == CharacterStates.Standing)
        {
            currentState = CharacterStates.Running;
            
            
        }
        if(walkinTimer <= 2)
        {
            if (currentState == CharacterStates.Running)
                RunLeft();
        }
        else if(walkinTimer <= 4)
        {
            if (currentState == CharacterStates.Running)
                RunRight();
        }
        else
        {
            walkinTimer = 0;
            currentState = CharacterStates.Squatting;
            shortHopping = true;
        }

        walkinTimer += Time.deltaTime;
    }
}
