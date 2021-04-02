using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicEnemy : Character
{
    [SerializeField] private GameObject player;
    // Detection zone is an empty GameObject with a 2D box collider on it 
    // Also make sure that the box collider's offset is 0
    [SerializeField] private List<GameObject> detectionZones;
    [SerializeField] private GameObject mobileDetectionZone;
    [SerializeField] private float movementSpeed;

    [SerializeField] private GameObject healthBar;
    public bool shouldRespawn;

    private bool playerFound;

    private float deathTimer;

    private float fadeOutTimer;
    private float currentAlpha;
    private float fadeOutLength;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        horizontalSpeed = movementSpeed;
        currentState = CharacterStates.Falling;

        // Create a health bar
        healthBar = GameObject.Instantiate(healthBar);
        healthBar.transform.SetParent(GameObject.Find("Canvas").transform);
        healthBar.name = gameObject.name + " Health Bar";

        availableForms.Add(Forms.Cell);

        BoxManager boxManager = GameObject.Find("GameManager").GetComponent<BoxManager>();
        //neutralAirHitboxSet = boxManager.playerNeutralAirHitboxSet;
        if (transform.root.name == "Basic Enemy")
        {
            forwardAirHitboxSet = boxManager.enemy1ForwardTiltHitboxSet;
        }
        else if (transform.root.name == "Basic Enemy (1)")
        {
            forwardAirHitboxSet = boxManager.enemy2ForwardTiltHitboxSet;
        }
        else if (transform.root.name == "Basic Enemy (2)")
        {
            forwardAirHitboxSet = boxManager.enemy3ForwardTiltHitboxSet;
        }
        else if (transform.root.name == "Basic Enemy (3)")
        {
            forwardAirHitboxSet = boxManager.enemy4ForwardTiltHitboxSet;
        }
        else if (transform.root.name == "Basic Enemy (4)")
        {
            forwardAirHitboxSet = boxManager.enemy5ForwardTiltHitboxSet;
        }
        else if (transform.root.name == "Basic Enemy (5)")
        {
            forwardAirHitboxSet = boxManager.enemy6ForwardTiltHitboxSet;
        }
        else if (transform.root.name == "Basic Enemy (6)")
        {
            forwardAirHitboxSet = boxManager.enemy7ForwardTiltHitboxSet;
        }
        else if (transform.root.name == "Basic Enemy (7)")
        {
            forwardAirHitboxSet = boxManager.enemy8ForwardTiltHitboxSet;
        }

        //upAirHitboxSet = boxManager.playerUpAirHitboxSet;
        //backAirHitboxSet = boxManager.playerBackAirHitboxSet;
        //downAirHitboxSet = boxManager.playerDownAirHitboxSet;
        //
        //jabHitboxSet = boxManager.playerJabHitboxSet;
        //forwardTiltHitboxSet = boxManager.playerForwardTiltHitboxSet;
        //upTiltHitboxSet = boxManager.playerUpTiltHitboxSet;
        //downTiltHitboxSet = boxManager.playerDownAirHitboxSet;
        //
        //armsNeutralBHitboxSet = boxManager.playerARMSNeutralBHitboxSet;
        //armsSideBHitboxSet = boxManager.playerARMSSideBHitboxSet;
        //armsUpBHitboxSet = boxManager.playerARMSUpBHitboxSet;
        //
        //fireNeutralBHitboxSet = boxManager.playerFIRENeutralBHitboxSet;
        //fireSideBHitboxSet = boxManager.playerFIRESideBHitboxSet;
        //fireUpBHitboxSet = boxManager.playerFIREUpBHitboxSet;
    }

    protected override void Initialize()
    {
        base.Initialize();

        horizontalSpeed = movementSpeed;
        currentState = CharacterStates.Falling;
        armor = 0;
        currentAlpha = 1.0f;
        fadeOutLength = 2.0f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        switch(currentState)
        {
            #region Standing
            case CharacterStates.Standing:

                // If the player is found and is not within attacking range
                if (playerFound && Vector2.Distance(transform.position, player.transform.position) > 1.0f)
                {
                    // If the player is farther than 0.2 away in x direction and less an 1 away in y direction 
                    if (Mathf.Abs(player.transform.position.x - transform.position.x) > 0.2f && Mathf.Abs(player.transform.position.y - transform.position.y) <= 1.0f)
                    {
                        currentState = CharacterStates.Running;
                    }
                    else
                    {
                        currentState = CharacterStates.Standing;
                    }
                }
                // If the player is found and is within attacking range
                else if (playerFound)
                {
                    currentState = CharacterStates.GroundAttacking;
                    currentAttackID = AttackIDs.AttackID.ForwardTilt1;
                }

                // If the player is not found
                if (playerFound == false)
                {
                    // If the enemy is not at their respawn position
                    if (Mathf.Abs(transform.position.x - respawnPoint.x) >= 0.1f)
                    {
                        currentState = CharacterStates.Running;
                    }
                }

                break;
            #endregion

            #region Running
            case CharacterStates.Running:

                // If the player is found and is not within attacking range
                if (playerFound && Vector2.Distance(transform.position, player.transform.position) > 1.0f)
                {
                    // If the player is just about directly above them
                    if (Mathf.Abs(player.transform.position.x - transform.position.x) <= 0.2f && Mathf.Abs(player.transform.position.y - transform.position.y) > 1.0f)
                    {
                        currentState = CharacterStates.Standing;
                    }
                    else
                    {
                        // If the player is to the left, run left
                        if (player.transform.position.x < transform.position.x)
                        {
                            RunLeft();
                        }
                        else
                        {
                            RunRight();
                        }
                    }
                }
                // If the player is found and is within attacking range
                else if (playerFound)
                {
                    currentState = CharacterStates.GroundAttacking;
                    currentAttackID = AttackIDs.AttackID.ForwardTilt1;
                }

                if (playerFound == false)
                {
                    // If the enemy is not at their respawn position
                    if (Mathf.Abs(transform.position.x - respawnPoint.x) >= 0.1f)
                    {
                        // If the player is to the left of the respawn point
                        if (transform.position.x < respawnPoint.x)
                        {
                            RunRight();
                        }
                        else
                        {
                            RunLeft();
                        }
                    }
                    else
                    {
                        currentState = CharacterStates.Standing;
                    }
                }

                break;

            #endregion

            #region Death
            case CharacterStates.Death:

                healthBar.SetActive(false);
                if (deathTimer >= fadeOutLength)
                {
                    //Color thisColor = transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.color;
                    //transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.color = new Color(thisColor.r, thisColor.g, thisColor.b, currentAlpha);
                    currentAlpha -= Time.deltaTime / fadeOutLength;
                    fadeOutTimer += Time.deltaTime;
                }
                if (deathTimer >= fadeOutLength*2f)
                {
                    if (shouldRespawn)
                    {
                        transform.position = respawnPoint;
                        Initialize();
                        transform.GetChild(0).gameObject.SetActive(true);
                        healthBar.SetActive(true);
                        currentState = CharacterStates.Standing;
                        shouldRespawn = false;
                        deathTimer = 0.0f;
                    }
                    else
                    {
                        //transform.GetChild(0).gameObject.SetActive(false);
                        healthBar.SetActive(false);
                        deathTimer = 0.0f;
                    }
                }

                deathTimer += Time.deltaTime;
             
                break;
                #endregion

        }

        // Position the health bar to where the enemy is
        healthBar.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position + new Vector3(0, 1.1f, 0));
        healthBar.transform.localScale = new Vector3(0.2f, 0.2f, 1);

        // Set the health bar fill amount to how much health the player has.
        healthBar.transform.Find("Bar").GetComponent<Image>().fillAmount = health * (1.0f / maxHealth);

        // If you're not dead, then you better be looking for the player
        if (currentState != CharacterStates.Death)
        {
            bool playerFoundThisFrame = false;
            // Loop through the detection zones
            for (int i = 0; i < detectionZones.Count; i++)
            {
                // If the player is within a zone
                Bounds currentDetectionZone = detectionZones[i].GetComponent<BoxCollider2D>().bounds;
                if (player.transform.position.x >= currentDetectionZone.min.x
                    && player.transform.position.x <= currentDetectionZone.max.x
                    && player.transform.position.y >= currentDetectionZone.min.y
                    && player.transform.position.y <= currentDetectionZone.max.y)
                {
                    playerFound = true;
                    playerFoundThisFrame = true;
                }
                // If all detection zones were checked and the player was not found this frame
                else if(i == detectionZones.Count -1 && playerFoundThisFrame == false)
                {
                    playerFound = false;
                }
            }

            // If the player is not in a static detection zone
            if (playerFound == false)
            {
                // See if the player is within the mobile detection zone
                Bounds mobileBounds = mobileDetectionZone.GetComponent<BoxCollider2D>().bounds;
                if (player.transform.position.x >= mobileBounds.min.x
                    && player.transform.position.x <= mobileBounds.max.x
                    && player.transform.position.y >= mobileBounds.min.y
                    && player.transform.position.y <= mobileBounds.max.y)
                {
                    playerFound = true;
                }
            }

            // If the player is not found and the enemy is on the ground
            if(playerFound == false && CheckIfOnGround())
            {
                // Check if the enemy is too far away vertically from its respawn point
                if(Mathf.Abs(transform.position.y - respawnPoint.y) >= 1.0f)
                {
                    currentState = CharacterStates.Standing;
                }
            }
        }

        //if(playerFound && Vector2.Distance(transform.position,player.transform.position) > 1.0f)
        //{
        //    currentState = CharacterStates.Running;
        //    if(player.transform.position.x < transform.position.x)
        //    {
        //        RunLeft();
        //    }
        //    else
        //    {
        //        RunRight();
        //    }
        //
        //    health -= 0.001f;
        //}
        //else if(playerFound)
        //{
        //    //currentState = CharacterStates.GroundAttacking;
        //    //currentAttackID = AttackIDs.AttackID.ForwardTilt1;
        //    currentState = CharacterStates.Standing;
        //    Debug.Log("Gotcha!");
        //}

        //if(playerFound == false)
        //{
        //    if(Mathf.Abs(transform.position.x - respawnPoint.x) >= 0.1f)
        //    {
        //        currentState = CharacterStates.Running;
        //        if(transform.position.x < respawnPoint.x)
        //        {
        //            RunRight();
        //        }
        //        else
        //        {
        //            RunLeft();
        //        }
        //    }
        //    else
        //    {
        //        if(CheckIfOnGround() == false)
        //        {
        //            currentState = CharacterStates.Falling;
        //        }
        //        else
        //            currentState = CharacterStates.Standing;
        //    }
        //}
    }
}
