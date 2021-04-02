using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{

    private PlayerControls controls;

    #region Left Right Movement
    private bool moveLeftPressed;
    private bool moveRightPressed;
    private bool moveLeftReleased;
    private bool moveRightReleased;

    private bool moveLeftPressedLastFrame;
    private bool moveRightPressedLastFrame;

    private float moveLeftValue;
    private float moveRightValue;

    #endregion

    #region Jump
    private bool jumpPressed;
    private bool jumpPressedDown;

    private bool shortHopPressed;
    private bool shortHopPressedDown;

    private bool fullHopPressed;
    private bool fullHopPressedDown;

    private bool jumpPressedLastFrame;
    private bool shortHopPressedLastFrame;
    private bool fullHopPressedLastFrame;
    #endregion

    #region Down
    private bool downPressed;
    private bool downPressedDown;
    private float downValue;

    private float timeSinceDownPressed;
    private float doubleTapWindow;

    private bool downHasLeftZone;

    private bool downPressedLastFrame;
    #endregion

    #region Up

    private bool upPressed;

    #endregion

    #region Attack
    private bool attackPressed;
    private bool attackPressedDown;

    private bool attackPressedLastFrame;

    private bool specialAttackPressed;
    private bool specialAttackPressedDown;

    private bool specialAttackPressedLastFrame;
    #endregion

    #region Block
    private bool blockPressed;
    private bool blockPressedDown;

    private bool blockPressedLastFrame;
    #endregion

    #region FormSwap

    private bool swapFormPressed;
    private bool swapFormPressedDown;

    private bool swapFormPressedLastFrame;

    #endregion

    private float zoneValue;
    private float fullZoneValue;

    public float cameraSpeed;
    private float cameraHorizontalOffset;
    private float cameraVerticalOffset;
    private bool cameraMoveCompleted;
    private float cameraVerticalDestination;
    private bool cameraFalling;
    public bool normalCameraMode;

    public bool npcNearBy;

    private bool fadeIn;

    public bool canBeControlled;

    [SerializeField] private GameObject staminaBar;
    [SerializeField] private GameObject healthBar;

    private SoundManager soundManager;

    private bool firstFrameOfStanding;
    private bool firstFrameOfSquatting;
    private bool firstFrameOfJumping;
    private bool firstFrameOfRunning;
    private bool firstFrameOfCrouching;
    private bool firstFrameOfDashing;
    private bool firstFrameOfLanding;
    private bool firstFrameOfFalling;
    private bool firstFrameOfDoubleJumping;
    private bool firstFrameOfGroundAttacking;
    private bool firstFrameOfAirAttacking;
    private bool firstFrameOfParrying;
    private bool firstFrameOfBlocking;
    private bool firstFrameOfGettingHit;
    private bool firstFrameOfLyingDown;
    private bool firstFrameOfTeching;
    private bool firstFrameOfDeath;

    public bool ActionPressed
    {
        get { return attackPressed; }
    }
    public bool ActionPressedDown
    {
        get { return attackPressedDown; }
    }
    public bool ActionPressedLastFrame
    {
        get { return attackPressedLastFrame; }
    }

    [SerializeField] private GameObject activeFormIcon;
    [SerializeField] private GameObject nextUpFormIcon;
    [SerializeField] private GameObject baseFormIcon;
    [SerializeField] private GameObject armFormIcon;
    [SerializeField] private GameObject fireFormIcon;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        zoneValue = 0.3f;
        fullZoneValue = 0.7f;
        doubleTapWindow = 0.5f;
        //shortHopWindow = 0.05f; //3 frames @ 60fps (was 4 but it felt like smash and I hated it) lollllll fair (I tried to make it work as 8... :'c)
        jumpTypeDecided = false;
        cameraSpeed = 4.0f;
        cameraHorizontalOffset = 2.0f;
        cameraVerticalOffset = 1.0f;
        cameraFalling = false;
        normalCameraMode = true;

        canBeControlled = true;

        armFormAttacks = GetComponent<Arm_Form_Attacks>();
        fireFormAttacks = GetComponent<Fire_Form_Attacks>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        availableForms.Add(Forms.Base);

        BoxManager boxManager = GameObject.Find("GameManager").GetComponent<BoxManager>();
        neutralAirHitboxSet = boxManager.playerNeutralAirHitboxSet;
        forwardAirHitboxSet = boxManager.playerForwardAirHitboxSet;
        upAirHitboxSet = boxManager.playerUpAirHitboxSet;
        backAirHitboxSet = boxManager.playerBackAirHitboxSet;
        downAirHitboxSet = boxManager.playerDownAirHitboxSet;

        jabHitboxSet = boxManager.playerJabHitboxSet;
        forwardTiltHitboxSet = boxManager.playerForwardTiltHitboxSet;
        upTiltHitboxSet = boxManager.playerUpTiltHitboxSet;
        downTiltHitboxSet = boxManager.playerDownAirHitboxSet;

        armsNeutralBHitboxSet = boxManager.playerARMSNeutralBHitboxSet;
        armsSideBHitboxSet = boxManager.playerARMSSideBHitboxSet;
        armsUpBHitboxSet = boxManager.playerARMSUpBHitboxSet;

        fireNeutralBHitboxSet = boxManager.playerFIRENeutralBHitboxSet;
        fireSideBHitboxSet = boxManager.playerFIRESideBHitboxSet;
        fireUpBHitboxSet = boxManager.playerFIREUpBHitboxSet;
    }

    private void Awake()
    {
        controls = GameObject.FindGameObjectWithTag("Control System").GetComponent<ControlSystem>().controls;

        #region Left Right Movement
        controls.Gameplay.MoveLeft.performed += ctx => moveLeftPressed = true;
        controls.Gameplay.MoveLeft.canceled += ctx => moveLeftPressed = false;

        controls.Gameplay.MoveLeft.performed += ctx => moveLeftValue = ctx.ReadValue<float>();
        controls.Gameplay.MoveLeft.canceled += ctx => moveLeftValue = 0.0f;

        controls.Gameplay.MoveRight.performed += ctx => moveRightPressed = true;
        controls.Gameplay.MoveRight.canceled += ctx => moveRightPressed = false;

        controls.Gameplay.MoveRight.performed += ctx => moveRightValue = ctx.ReadValue<float>();
        controls.Gameplay.MoveRight.canceled += ctx => moveRightValue = 0.0f;
        #endregion

        #region Jump
        controls.Gameplay.Jump.performed += ctx => jumpPressed = true;
        controls.Gameplay.Jump.canceled += ctx => jumpPressed = false;

        controls.Gameplay.ShortHop.performed += ctx => shortHopPressed = true;
        controls.Gameplay.ShortHop.canceled += ctx => shortHopPressed = false;

        controls.Gameplay.FullHop.performed += ctx => fullHopPressed = true;
        controls.Gameplay.FullHop.canceled += ctx => fullHopPressed = false;
        #endregion

        #region Down
        controls.Gameplay.Down.performed += ctx => downPressed = true;
        controls.Gameplay.Down.canceled += ctx => downPressed = false;

        controls.Gameplay.Down.performed += ctx => downValue = ctx.ReadValue<float>();
        controls.Gameplay.Down.canceled += ctx => downValue = 0.0f;
        #endregion

        #region Up
        controls.Gameplay.Up.performed += ctx => upPressed = true;
        controls.Gameplay.Up.canceled += ctx => upPressed = false;
        #endregion

        #region Attack
        controls.Gameplay.NormalAttack.performed += ctx => attackPressed = true;
        controls.Gameplay.NormalAttack.canceled += ctx => attackPressed = false;

        controls.Gameplay.SpecialAttack.performed += ctx => specialAttackPressed = true;
        controls.Gameplay.SpecialAttack.canceled += ctx => specialAttackPressed = false;

        #endregion

        #region Block
        controls.Gameplay.Block.performed += ctx => blockPressed = true;
        controls.Gameplay.Block.canceled += ctx => blockPressed = false;

        #endregion

        #region FormSwap

        controls.Gameplay.SwapForm.performed += ctx => swapFormPressed = true;
        controls.Gameplay.SwapForm.canceled += ctx => swapFormPressed = false;

        #endregion
}

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (moveLeftValue < zoneValue)
            moveLeftPressed = false;
        else
            moveLeftPressed = true;
        if (moveRightValue < zoneValue)
            moveRightPressed = false;
        else
            moveRightPressed = true;
        if (downValue < fullZoneValue)
            downPressed = false;
        else
            downPressed = true;

        if (moveLeftPressedLastFrame == true && moveLeftPressed == false)
            moveLeftReleased = true;
        else
            moveLeftReleased = false;

        if (moveRightPressedLastFrame == true && moveRightPressed == false)
            moveRightReleased = true;
        else
            moveRightReleased = false;

        if (jumpPressedLastFrame == false && jumpPressed == true)
            jumpPressedDown = true;
        else
            jumpPressedDown = false;

        if (shortHopPressedLastFrame == false && shortHopPressed == true)
            shortHopPressedDown = true;
        else
            shortHopPressedDown = false;

        if (fullHopPressedLastFrame == false && fullHopPressed == true)
            fullHopPressedDown = true;
        else
            fullHopPressedDown = false;

        if (downPressedLastFrame == false && downPressed == true)
            downPressedDown = true;
        else
            downPressedDown = false;

        if (attackPressedLastFrame == false && attackPressed == true)
            attackPressedDown = true;
        else
            attackPressedDown = false;

        if (specialAttackPressedLastFrame == false && specialAttackPressed == true)
            specialAttackPressedDown = true;
        else
            specialAttackPressedDown = false;

        if (blockPressedLastFrame == false && blockPressed == true)
            blockPressedDown = true;
        else
            blockPressedDown = false;

        if (swapFormPressedLastFrame == false && swapFormPressed == true)
            swapFormPressedDown = true;
        else
            swapFormPressedDown = false;

        if (timeSinceDownPressed != 0)
        {
            timeSinceDownPressed += Time.deltaTime;
        }

        if (canBeControlled)
        {
            switch (currentState)
            {

                #region Standing
                case CharacterStates.Standing:

                    // If the player is in the standing state

                    jumpForward = true;

                    if (moveLeftPressed && jumpPressedDown)
                    {
                        if (facingRight)
                            jumpForward = false;
                        else
                            jumpForward = true;
                    }
                    else if (moveRightPressed && jumpPressedDown)
                    {
                        if (facingRight)
                            jumpForward = true;
                        else
                            jumpForward = false;
                    }

                    // Running code
                    // GetKey
                    if (moveLeftPressed && moveRightPressed)
                        currentState = CharacterStates.Standing;
                    else if (moveLeftPressed || moveRightPressed)
                        currentState = CharacterStates.Running;

                    // Jump Code
                    // GetKeyDown
                    if (jumpPressedDown)
                    {
                        jumpTypeDecided = false;
                        currentState = CharacterStates.Squatting;
                    }
                    else if (shortHopPressedDown)
                    {
                        jumpTypeDecided = true;
                        shortHopping = true;
                        currentState = CharacterStates.Squatting;
                    }
                    else if (fullHopPressedDown)
                    {
                        jumpTypeDecided = true;
                        shortHopping = false;
                        currentState = CharacterStates.Squatting;
                    }

                    //CrouchAndFallThrough();
                    //
                    //// If down is pressed and the player is on the ground
                    //if (downPressed && HandleGroundCollisions() == true)
                    //{
                    //    currentState = CharacterStates.Crouching;
                    //}

                    // If on the ground
                    if (CheckIfOnGround() == true)
                    {
                        // If down is pressed
                        if (downPressed)
                        {
                            currentState = CharacterStates.Crouching;
                        }
                    }
                    else
                        CrouchAndFallThrough();

                    //if(downPressed && downValue >= zoneValue)
                    //{
                    //    if(HandlePlatformCollisions() == true)
                    //    {
                    //        transform.Translate(0, -platformHelpAmount, 0);
                    //        currentState = CharacterStates.Falling;
                    //        if (downPressedDown == true)
                    //        {
                    //            terminalVelocity = defaultTerminalVelocity;
                    //            jumpVelocitySlowRate = defaultJumpVelocitySlowRate;
                    //        }
                    //    }
                    //}

                    if ((attackPressedDown || specialAttackPressedDown) && stamina > 0 && !npcNearBy)
                    {
                        // If attack and left is pressed at the same time
                        if (moveLeftPressed)
                        {
                            if (attackPressedDown)
                            {
                                currentAttackID = AttackIDs.AttackID.ForwardTilt1;
                            }
                            else if (specialAttackPressedDown)
                            {
                                switch (currentForm)
                                {
                                    case Forms.Base:
                                        currentAttackID = AttackIDs.AttackID.ForwardTilt1;
                                        break;
                                    case Forms.Arms:
                                        currentAttackID = AttackIDs.AttackID.ArmsSideSpecial;
                                        break;
                                    case Forms.Fire:
                                        currentAttackID = AttackIDs.AttackID.FireSideSpecial;
                                        break;
                                }
                            }

                            // Face left and do a forward tilt
                            //attackMissGround.Play();
                            soundManager.PlayPlayerAttackMissGround();
                            currentState = CharacterStates.GroundAttacking;
                            facingRight = false;
                            transform.GetChild(0).transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                        }
                        // If attack and right is pressed at the same time
                        else if (moveRightPressed)
                        {
                            if (attackPressedDown)
                            {
                                currentAttackID = AttackIDs.AttackID.ForwardTilt1;
                            }
                            else if (specialAttackPressedDown)
                            {
                                switch (currentForm)
                                {
                                    case Forms.Base:
                                        currentAttackID = AttackIDs.AttackID.ForwardTilt1;
                                        break;
                                    case Forms.Arms:
                                        currentAttackID = AttackIDs.AttackID.ArmsSideSpecial;
                                        break;
                                    case Forms.Fire:
                                        currentAttackID = AttackIDs.AttackID.FireSideSpecial;
                                        break;
                                }
                            }
                            // Face right and do a forward tilt
                            //attackMissGround.Play();
                            soundManager.PlayPlayerAttackMissGround();
                            currentState = CharacterStates.GroundAttacking;
                            facingRight = true;
                            transform.GetChild(0).transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                        }
                        else if (downPressed)
                        {
                            if (attackPressedDown)
                            {
                                currentAttackID = AttackIDs.AttackID.DownTilt1;
                            }
                            else if (specialAttackPressedDown)
                            {
                                switch (currentForm)
                                {
                                    case Forms.Base:
                                        currentAttackID = AttackIDs.AttackID.DownTilt1;
                                        break;
                                    case Forms.Arms:
                                        currentAttackID = AttackIDs.AttackID.ArmsDownSpecial;
                                        break;
                                    case Forms.Fire:
                                        currentAttackID = AttackIDs.AttackID.FireDownSpecial;
                                        break;
                                }
                            }
                            soundManager.PlayPlayerAttackMissGround();
                            //attackMissGround.Play();
                            currentState = CharacterStates.GroundAttacking;
                        }
                        else if (upPressed)
                        {
                            if (attackPressedDown)
                            {
                                currentAttackID = AttackIDs.AttackID.UpTilt1;
                            }
                            else if (specialAttackPressedDown)
                            {
                                switch (currentForm)
                                {
                                    case Forms.Base:
                                        currentAttackID = AttackIDs.AttackID.UpTilt1;
                                        break;
                                    case Forms.Arms:
                                        currentAttackID = AttackIDs.AttackID.ArmsUpSpecial;
                                        break;
                                    case Forms.Fire:
                                        currentAttackID = AttackIDs.AttackID.FireUpSpecial;
                                        break;
                                }
                            }
                            soundManager.PlayPlayerAttackMissGround();
                            //attackMissGround.Play();
                            currentState = CharacterStates.GroundAttacking;
                        }
                        else
                        {
                            if (attackPressedDown)
                            {
                                currentAttackID = AttackIDs.AttackID.Jab1;
                            }
                            else if (specialAttackPressedDown)
                            {
                                switch (currentForm)
                                {
                                    case Forms.Base:
                                        currentAttackID = AttackIDs.AttackID.Jab1;
                                        break;
                                    case Forms.Arms:
                                        currentAttackID = AttackIDs.AttackID.ArmsNeutralSpecial;
                                        break;
                                    case Forms.Fire:
                                        currentAttackID = AttackIDs.AttackID.FireNeutralSpecial;
                                        break;
                                }
                            }
                            soundManager.PlayPlayerAttackMissGround();
                            //attackMissGround.Play();
                            currentState = CharacterStates.GroundAttacking;
                        }
                    }

                    if (blockPressed)
                    {
                        currentState = CharacterStates.Blocking;
                    }

                    break;

                #endregion

                #region Squatting
                case CharacterStates.Squatting:

                    //Player can input up and attack during squat to cancel their jump and perform an upward attack
                    if ((attackPressedDown || specialAttackPressedDown) && stamina > 0 && !npcNearBy)
                    {
                        if (upPressed)
                        {
                            if (attackPressedDown)
                            {
                                currentAttackID = AttackIDs.AttackID.UpTilt1;
                            }
                            else if (specialAttackPressedDown)
                            {
                                switch (currentForm)
                                {
                                    case Forms.Base:
                                        currentAttackID = AttackIDs.AttackID.UpTilt1;
                                        break;
                                    case Forms.Arms:
                                        currentAttackID = AttackIDs.AttackID.ArmsUpSpecial;
                                        break;
                                    case Forms.Fire:
                                        currentAttackID = AttackIDs.AttackID.FireUpSpecial;
                                        break;
                                }
                            }
                            soundManager.PlayPlayerAttackMissAir();
                            currentState = CharacterStates.GroundAttacking;
                            jumpSquatTimer = 0;
                        }
                    }

                    break;

                #endregion

                #region Jumping
                case CharacterStates.Jumping:
                    if (firstFrameOfJumping == true)
                    {
                        soundManager.PlayPlayerJump();
                        firstFrameOfJumping = false;
                    }
                    // GetKey
                    if (moveLeftPressed)
                        AerialDriftLeft();

                    // GetKey
                    if (moveRightPressed)
                        AerialDriftRight();

                    // GetKeyDown
                    if (jumpPressedDown)
                    {
                        //jump2.Play();
                        DoubleJumpNewAirVelocity();
                        currentState = CharacterStates.DoubleJumping;
                    }

                    //Check if jump has been held through squat
                    if (!jumpTypeDecided)
                    {
                        //jump1.Play();
                        //Full Hop
                        if (jumpPressed)
                        {
                            shortHopping = false;
                        }

                        //Short Hop
                        else
                        {
                            shortHopping = true;
                        }

                        jumpTypeDecided = true;
                    }

                    if ((attackPressedDown || specialAttackPressedDown) && stamina > 0 && !npcNearBy && jumpVelocity.y > 0)
                    {
                        // If the player is trying to attack in the direction they are facing
                        if (facingRight && moveRightPressed || !facingRight && moveLeftPressed)
                        {
                            if (attackPressedDown)
                            {
                                currentAttackID = AttackIDs.AttackID.ForwardAir1;
                            }
                            else if (specialAttackPressedDown)
                            {
                                switch (currentForm)
                                {
                                    case Forms.Base:
                                        currentAttackID = AttackIDs.AttackID.ForwardAir1;
                                        break;
                                    case Forms.Arms:
                                        currentAttackID = AttackIDs.AttackID.ArmsSideSpecial;
                                        break;
                                    case Forms.Fire:
                                        currentAttackID = AttackIDs.AttackID.FireSideSpecial;
                                        break;
                                }
                            }
                            // Perform a forward air
                            soundManager.PlayPlayerAttackMissAir();
                            //attackMissAir.Play();
                            currentState = CharacterStates.AirAttacking;
                        }
                        // Else if the player is trying to attack in the opposite direction they are facing
                        else if (facingRight && moveLeftPressed || !facingRight && moveRightPressed)
                        {
                            if (attackPressedDown)
                            {
                                currentAttackID = AttackIDs.AttackID.BackAir1;
                            }
                            else if (specialAttackPressedDown)
                            {
                                switch (currentForm)
                                {
                                    case Forms.Base:
                                        currentAttackID = AttackIDs.AttackID.BackAir1;
                                        break;
                                    case Forms.Arms:
                                        currentAttackID = AttackIDs.AttackID.ArmsSideSpecial;
                                        break;
                                    case Forms.Fire:
                                        currentAttackID = AttackIDs.AttackID.FireSideSpecial;
                                        break;
                                }
                            }
                            // Perform a back air
                            //attackMissAir.Play();
                            soundManager.PlayPlayerAttackMissAir();
                            currentState = CharacterStates.AirAttacking;
                        }
                        else if (downPressed)
                        {
                            if (attackPressedDown)
                            {
                                currentAttackID = AttackIDs.AttackID.DownAir1;
                            }
                            else if (specialAttackPressedDown)
                            {
                                switch (currentForm)
                                {
                                    case Forms.Base:
                                        currentAttackID = AttackIDs.AttackID.DownAir1;
                                        break;
                                    case Forms.Arms:
                                        currentAttackID = AttackIDs.AttackID.ArmsDownSpecial;
                                        break;
                                    case Forms.Fire:
                                        currentAttackID = AttackIDs.AttackID.FireDownSpecial;
                                        break;
                                }
                            }
                            // Perform a down air
                            //attackMissAir.Play();
                            soundManager.PlayPlayerAttackMissAir();
                            currentState = CharacterStates.AirAttacking;
                        }
                        else if (upPressed)
                        {
                            if (attackPressedDown)
                            {
                                currentAttackID = AttackIDs.AttackID.UpAir1;
                            }
                            else if (specialAttackPressedDown)
                            {
                                switch (currentForm)
                                {
                                    case Forms.Base:
                                        currentAttackID = AttackIDs.AttackID.UpAir1;
                                        break;
                                    case Forms.Arms:
                                        currentAttackID = AttackIDs.AttackID.ArmsUpSpecial;
                                        break;
                                    case Forms.Fire:
                                        currentAttackID = AttackIDs.AttackID.FireUpSpecial;
                                        break;
                                }
                            }
                            // Perform an up air
                            //attackMissAir.Play();
                            soundManager.PlayPlayerAttackMissAir();
                            currentState = CharacterStates.AirAttacking;
                        }
                        else
                        {
                            if (attackPressedDown)
                            {
                                currentAttackID = AttackIDs.AttackID.NeutralAir1;
                            }
                            else if (specialAttackPressedDown)
                            {
                                switch (currentForm)
                                {
                                    case Forms.Base:
                                        currentAttackID = AttackIDs.AttackID.NeutralAir1;
                                        break;
                                    case Forms.Arms:
                                        currentAttackID = AttackIDs.AttackID.ArmsNeutralSpecial;
                                        break;
                                    case Forms.Fire:
                                        currentAttackID = AttackIDs.AttackID.FireNeutralSpecial;
                                        break;
                                }
                            }
                            // Perform a neutral air
                            //attackMissAir.Play();
                            soundManager.PlayPlayerAttackMissAir();
                            currentState = CharacterStates.AirAttacking;
                        }
                        stateBeforeAttacking = CharacterStates.Jumping;
                    }

                    break;

                #endregion

                #region Running
                case CharacterStates.Running:

                    jumpForward = true;

                    if(soundManager.PlayerSFXIsPlaying == false)
                    {
                        soundManager.PlayPlayerWalk();
                    }

                    if (moveLeftPressed && jumpPressedDown)
                    {
                        if (facingRight)
                            jumpForward = false;
                        else
                            jumpForward = true;
                    }
                    else if (moveRightPressed && jumpPressedDown)
                    {
                        if (facingRight)
                            jumpForward = true;
                        else
                            jumpForward = false;
                    }

                    // GetKey
                    if (moveLeftPressed && moveRightPressed)
                        currentState = CharacterStates.Standing;

                    // GetKey
                    if (moveLeftPressed)
                        RunLeft();
                    else if (moveRightPressed)
                        RunRight();

                    // GetKeyUp
                    if (moveLeftReleased || moveRightReleased)
                        currentState = CharacterStates.Standing;

                    // Jump Code
                    // GetKeyDown
                    if (jumpPressedDown)
                    {
                        jumpTypeDecided = false;
                        currentState = CharacterStates.Squatting;
                    }
                    else if (shortHopPressedDown)
                    {
                        jumpTypeDecided = true;
                        shortHopping = true;
                        currentState = CharacterStates.Squatting;
                    }
                    else if (fullHopPressedDown)
                    {
                        jumpTypeDecided = true;
                        shortHopping = false;
                        currentState = CharacterStates.Squatting;
                    }

                    // If on the ground
                    if (CheckIfOnGround() == true)
                    {
                        // If down was just pressed
                        if (downPressed && !downPressedLastFrame)
                        {
                            currentState = CharacterStates.Crouching;
                        }
                    }
                    else
                        CrouchAndFallThrough();

                    if ((attackPressedDown || specialAttackPressedDown) && stamina > 0 && !npcNearBy)
                    {
                        currentState = CharacterStates.GroundAttacking;
                        if (upPressed)
                        {
                            if (attackPressedDown)
                            {
                                currentAttackID = AttackIDs.AttackID.UpTilt1;
                            }
                            else if (specialAttackPressedDown)
                            {
                                switch (currentForm)
                                {
                                    case Forms.Base:
                                        currentAttackID = AttackIDs.AttackID.UpTilt1;
                                        break;
                                    case Forms.Arms:
                                        currentAttackID = AttackIDs.AttackID.ArmsUpSpecial;
                                        break;
                                    case Forms.Fire:
                                        currentAttackID = AttackIDs.AttackID.FireUpSpecial;
                                        break;
                                }
                            }
                            //attackMissGround.Play();
                            soundManager.PlayPlayerAttackMissGround();
                        }
                        else if (moveLeftPressed)
                        {
                            if (attackPressedDown)
                            {
                                currentAttackID = AttackIDs.AttackID.ForwardTilt1;
                            }
                            else if (specialAttackPressedDown)
                            {
                                switch (currentForm)
                                {
                                    case Forms.Base:
                                        currentAttackID = AttackIDs.AttackID.ForwardTilt1;
                                        break;
                                    case Forms.Arms:
                                        currentAttackID = AttackIDs.AttackID.ArmsSideSpecial;
                                        break;
                                    case Forms.Fire:
                                        currentAttackID = AttackIDs.AttackID.FireSideSpecial;
                                        break;
                                }
                            }
                            //attackMissGround.Play();
                            soundManager.PlayPlayerAttackMissGround();
                            //facingRight = false;
                        }
                        else if (moveRightPressed)
                        {
                            if (attackPressedDown)
                            {
                                currentAttackID = AttackIDs.AttackID.ForwardTilt1;
                            }
                            else if (specialAttackPressedDown)
                            {
                                switch (currentForm)
                                {
                                    case Forms.Base:
                                        currentAttackID = AttackIDs.AttackID.ForwardTilt1;
                                        break;
                                    case Forms.Arms:
                                        currentAttackID = AttackIDs.AttackID.ArmsSideSpecial;
                                        break;
                                    case Forms.Fire:
                                        currentAttackID = AttackIDs.AttackID.FireSideSpecial;
                                        break;
                                }
                            }
                            //attackMissGround.Play();
                            soundManager.PlayPlayerAttackMissGround();
                            //facingRight = true;
                        }
                        else if (downPressed)
                        {
                            if (attackPressedDown)
                            {
                                currentAttackID = AttackIDs.AttackID.DownTilt1;
                            }
                            else if (specialAttackPressedDown)
                            {
                                switch (currentForm)
                                {
                                    case Forms.Base:
                                        currentAttackID = AttackIDs.AttackID.DownTilt1;
                                        break;
                                    case Forms.Arms:
                                        currentAttackID = AttackIDs.AttackID.ArmsDownSpecial;
                                        break;
                                    case Forms.Fire:
                                        currentAttackID = AttackIDs.AttackID.FireDownSpecial;
                                        break;
                                }
                            }
                            //attackMissGround.Play();
                            soundManager.PlayPlayerAttackMissGround();
                            currentState = CharacterStates.GroundAttacking;
                        }
                        else
                        {
                            if (attackPressedDown)
                            {
                                currentAttackID = AttackIDs.AttackID.Jab1;
                            }
                            else if (specialAttackPressedDown)
                            {
                                switch (currentForm)
                                {
                                    case Forms.Base:
                                        currentAttackID = AttackIDs.AttackID.Jab1;
                                        break;
                                    case Forms.Arms:
                                        currentAttackID = AttackIDs.AttackID.ArmsNeutralSpecial;
                                        break;
                                    case Forms.Fire:
                                        currentAttackID = AttackIDs.AttackID.FireNeutralSpecial;
                                        break;
                                }
                            }
                            //attackMissGround.Play();
                            soundManager.PlayPlayerAttackMissGround();
                            currentState = CharacterStates.GroundAttacking;
                        }
                    }

                    if (blockPressed)
                    {
                        currentState = CharacterStates.Blocking;
                    }

                    break;

                #endregion

                #region Crouching
                case CharacterStates.Crouching:

                    if (downPressed == false)
                    {
                        currentState = CharacterStates.Standing;
                    }

                    // Jump Code
                    // GetKeyDown
                    if (jumpPressedDown)
                    {
                        jumpTypeDecided = false;
                        currentState = CharacterStates.Squatting;
                    }
                    else if (shortHopPressedDown)
                    {
                        jumpTypeDecided = true;
                        shortHopping = true;
                        currentState = CharacterStates.Squatting;
                    }
                    else if (fullHopPressedDown)
                    {
                        jumpTypeDecided = true;
                        shortHopping = false;
                        currentState = CharacterStates.Squatting;
                    }

                    // If left or right was just pressed
                    if ((moveLeftPressed && !moveLeftPressedLastFrame) || (moveRightPressed && !moveRightPressedLastFrame))
                        currentState = CharacterStates.Running;

                    if ((attackPressedDown || specialAttackPressedDown) && stamina > 0 && !npcNearBy)
                    {
                        if (attackPressedDown)
                        {
                            currentAttackID = AttackIDs.AttackID.DownTilt1;
                        }
                        else if (specialAttackPressedDown)
                        {
                            switch (currentForm)
                            {
                                case Forms.Base:
                                    currentAttackID = AttackIDs.AttackID.DownTilt1;
                                    break;
                                case Forms.Arms:
                                    currentAttackID = AttackIDs.AttackID.ArmsDownSpecial;
                                    break;
                                case Forms.Fire:
                                    currentAttackID = AttackIDs.AttackID.FireDownSpecial;
                                    break;
                            }
                        }
                        //attackMissGround.Play();
                        soundManager.PlayPlayerAttackMissGround();
                        currentState = CharacterStates.GroundAttacking;
                    }

                    if (blockPressed)
                    {
                        currentState = CharacterStates.Blocking;
                    }


                    break;

                #endregion

                #region Dashing
                case CharacterStates.Dashing:

                    break;

                #endregion

                #region Landing
                case CharacterStates.Landing:

                    break;

                #endregion

                #region Falling
                case CharacterStates.Falling:

                    if (moveLeftPressed)
                    {
                        if (facingRight)
                            fallForward = false;
                        else
                            fallForward = true;
                    }
                    else if (moveRightPressed)
                    {
                        if (facingRight)
                            fallForward = true;
                        else
                            fallForward = false;
                    }

                    // GetKey
                    if (moveLeftPressed)
                        AerialDriftLeft();

                    // GetKey
                    if (moveRightPressed)
                        AerialDriftRight();

                    //FastFall activation
                    if (downPressedDown)
                    {
                        fastFalling = true;
                    }

                    // GetKeyDown
                    if ((jumpPressedDown || shortHopPressedDown || fullHopPressedDown) && !hasDoubleJumped)
                    {
                        DoubleJumpNewAirVelocity();
                        currentState = CharacterStates.DoubleJumping;
                        // Negate fast falling if the player double jumps while fast falling
                        terminalVelocity = defaultTerminalVelocity;
                        jumpVelocitySlowRate = defaultJumpVelocitySlowRate;
                    }

                    if ((attackPressedDown || specialAttackPressedDown) && stamina > 0 && !npcNearBy)
                    {
                        // If the player is trying to attack in the direction they are facing
                        if (facingRight && moveRightPressed || !facingRight && moveLeftPressed)
                        {
                            if (attackPressedDown)
                            {
                                currentAttackID = AttackIDs.AttackID.ForwardAir1;
                            }
                            else if (specialAttackPressedDown)
                            {
                                switch (currentForm)
                                {
                                    case Forms.Base:
                                        currentAttackID = AttackIDs.AttackID.ForwardAir1;
                                        break;
                                    case Forms.Arms:
                                        currentAttackID = AttackIDs.AttackID.ArmsSideSpecial;
                                        break;
                                    case Forms.Fire:
                                        currentAttackID = AttackIDs.AttackID.FireSideSpecial;
                                        break;
                                }
                            }
                            // Perform a forward air
                            //attackMissAir.Play();
                            soundManager.PlayPlayerAttackMissAir();
                            currentState = CharacterStates.AirAttacking;
                        }
                        // Else if the player is trying to attack in the opposite direction they are facing
                        else if (facingRight && moveLeftPressed || !facingRight && moveRightPressed)
                        {
                            if (attackPressedDown)
                            {
                                currentAttackID = AttackIDs.AttackID.BackAir1;
                            }
                            else if (specialAttackPressedDown)
                            {
                                switch (currentForm)
                                {
                                    case Forms.Base:
                                        currentAttackID = AttackIDs.AttackID.BackAir1;
                                        break;
                                    case Forms.Arms:
                                        currentAttackID = AttackIDs.AttackID.ArmsSideSpecial;
                                        break;
                                    case Forms.Fire:
                                        currentAttackID = AttackIDs.AttackID.FireSideSpecial;
                                        break;
                                }
                            }
                            // Perform a back air
                            //attackMissAir.Play();
                            soundManager.PlayPlayerAttackMissAir();
                            currentState = CharacterStates.AirAttacking;
                        }
                        else if (downPressed)
                        {
                            if (attackPressedDown)
                            {
                                currentAttackID = AttackIDs.AttackID.DownAir1;
                            }
                            else if (specialAttackPressedDown)
                            {
                                switch (currentForm)
                                {
                                    case Forms.Base:
                                        currentAttackID = AttackIDs.AttackID.DownAir1;
                                        break;
                                    case Forms.Arms:
                                        currentAttackID = AttackIDs.AttackID.ArmsDownSpecial;
                                        break;
                                    case Forms.Fire:
                                        currentAttackID = AttackIDs.AttackID.FireDownSpecial;
                                        break;
                                }
                            }
                            // Perform a down air
                            //attackMissAir.Play();
                            soundManager.PlayPlayerAttackMissAir();
                            currentState = CharacterStates.AirAttacking;
                        }
                        else if (upPressed)
                        {
                            if (attackPressedDown)
                            {
                                currentAttackID = AttackIDs.AttackID.UpAir1;
                            }
                            else if (specialAttackPressedDown)
                            {
                                switch (currentForm)
                                {
                                    case Forms.Base:
                                        currentAttackID = AttackIDs.AttackID.UpAir1;
                                        break;
                                    case Forms.Arms:
                                        currentAttackID = AttackIDs.AttackID.ArmsUpSpecial;
                                        break;
                                    case Forms.Fire:
                                        currentAttackID = AttackIDs.AttackID.FireUpSpecial;
                                        break;
                                }
                            }
                            // Perform an up air
                            //attackMissAir.Play();
                            soundManager.PlayPlayerAttackMissAir();
                            currentState = CharacterStates.AirAttacking;
                        }
                        else
                        {
                            if (attackPressedDown)
                            {
                                currentAttackID = AttackIDs.AttackID.NeutralAir1;
                            }
                            else if (specialAttackPressedDown)
                            {
                                switch (currentForm)
                                {
                                    case Forms.Base:
                                        currentAttackID = AttackIDs.AttackID.NeutralAir1;
                                        break;
                                    case Forms.Arms:
                                        currentAttackID = AttackIDs.AttackID.ArmsNeutralSpecial;
                                        break;
                                    case Forms.Fire:
                                        currentAttackID = AttackIDs.AttackID.FireNeutralSpecial;
                                        break;
                                }
                            }
                            // Perform a neutral air
                            //attackMissAir.Play();
                            soundManager.PlayPlayerAttackMissAir();
                            currentState = CharacterStates.AirAttacking;
                        }
                        stateBeforeAttacking = CharacterStates.Falling;
                    }

                    if (downPressed)
                    {
                        fallThroughPlatform = true;
                    }
                    else
                    {
                        fallThroughPlatform = false;
                    }

                    break;

                #endregion

                #region Double Jumping
                case CharacterStates.DoubleJumping:

                    if (firstFrameOfDoubleJumping == true)
                    {
                        soundManager.PlayPlayerDoubleJump();
                        firstFrameOfDoubleJumping = false;
                    }

                    if (moveLeftPressed)
                        AerialDriftLeft();

                    if (moveRightPressed)
                        AerialDriftRight();

                    if ((attackPressedDown || specialAttackPressedDown) && stamina > 0 && !npcNearBy && jumpVelocity.y > 0)
                    {
                        // If the player is trying to attack in the direction they are facing
                        if (facingRight && moveRightPressed || !facingRight && moveLeftPressed)
                        {
                            if (attackPressedDown)
                            {
                                currentAttackID = AttackIDs.AttackID.ForwardAir1;
                            }
                            else if (specialAttackPressedDown)
                            {
                                switch (currentForm)
                                {
                                    case Forms.Base:
                                        currentAttackID = AttackIDs.AttackID.ForwardAir1;
                                        break;
                                    case Forms.Arms:
                                        currentAttackID = AttackIDs.AttackID.ArmsSideSpecial;
                                        break;
                                    case Forms.Fire:
                                        currentAttackID = AttackIDs.AttackID.FireSideSpecial;
                                        break;
                                }
                            }
                            // Perform a forward air
                            //attackMissAir.Play();
                            soundManager.PlayPlayerAttackMissAir();
                            currentState = CharacterStates.AirAttacking;
                        }
                        // Else if the player is trying to attack in the opposite direction they are facing
                        else if (facingRight && moveLeftPressed || !facingRight && moveRightPressed)
                        {
                            if (attackPressedDown)
                            {
                                currentAttackID = AttackIDs.AttackID.BackAir1;
                            }
                            else if (specialAttackPressedDown)
                            {
                                switch (currentForm)
                                {
                                    case Forms.Base:
                                        currentAttackID = AttackIDs.AttackID.BackAir1;
                                        break;
                                    case Forms.Arms:
                                        currentAttackID = AttackIDs.AttackID.ArmsSideSpecial;
                                        break;
                                    case Forms.Fire:
                                        currentAttackID = AttackIDs.AttackID.FireSideSpecial;
                                        break;
                                }
                            }
                            // Perform a back air
                            //attackMissAir.Play();
                            soundManager.PlayPlayerAttackMissAir();
                            currentState = CharacterStates.AirAttacking;
                        }
                        else if (downPressed)
                        {
                            if (attackPressedDown)
                            {
                                currentAttackID = AttackIDs.AttackID.DownAir1;
                            }
                            else if (specialAttackPressedDown)
                            {
                                switch (currentForm)
                                {
                                    case Forms.Base:
                                        currentAttackID = AttackIDs.AttackID.DownAir1;
                                        break;
                                    case Forms.Arms:
                                        currentAttackID = AttackIDs.AttackID.ArmsDownSpecial;
                                        break;
                                    case Forms.Fire:
                                        currentAttackID = AttackIDs.AttackID.FireDownSpecial;
                                        break;
                                }
                            }
                            // Perform a down air
                            //attackMissAir.Play();
                            soundManager.PlayPlayerAttackMissAir();
                            //attackMissAir.Play();
                            currentState = CharacterStates.AirAttacking;
                        }
                        else if (upPressed)
                        {
                            if (attackPressedDown)
                            {
                                currentAttackID = AttackIDs.AttackID.UpAir1;
                            }
                            else if (specialAttackPressedDown)
                            {
                                switch (currentForm)
                                {
                                    case Forms.Base:
                                        currentAttackID = AttackIDs.AttackID.UpAir1;
                                        break;
                                    case Forms.Arms:
                                        currentAttackID = AttackIDs.AttackID.ArmsDownSpecial;
                                        break;
                                    case Forms.Fire:
                                        currentAttackID = AttackIDs.AttackID.FireDownSpecial;
                                        break;
                                }
                            }
                            // Perform an up air
                            //attackMissAir.Play();
                            soundManager.PlayPlayerAttackMissAir();
                            currentState = CharacterStates.AirAttacking;
                        }
                        else
                        {
                            if (attackPressedDown)
                            {
                                currentAttackID = AttackIDs.AttackID.NeutralAir1;
                            }
                            else if (specialAttackPressedDown)
                            {
                                switch (currentForm)
                                {
                                    case Forms.Base:
                                        currentAttackID = AttackIDs.AttackID.NeutralAir1;
                                        break;
                                    case Forms.Arms:
                                        currentAttackID = AttackIDs.AttackID.ArmsNeutralSpecial;
                                        break;
                                    case Forms.Fire:
                                        currentAttackID = AttackIDs.AttackID.FireNeutralSpecial;
                                        break;
                                }
                            }
                            // Perform a neutral air
                            //attackMissAir.Play();
                            soundManager.PlayPlayerAttackMissAir();
                            currentState = CharacterStates.AirAttacking;
                        }
                        stateBeforeAttacking = CharacterStates.DoubleJumping;
                    }

                    break;

                #endregion

                #region Ground Attacking
                case CharacterStates.GroundAttacking:

                    break;

                #endregion

                #region Air Attacking
                case CharacterStates.AirAttacking:

                    // GetKey
                    if (moveLeftPressed)
                        AerialDriftLeft();

                    // GetKey
                    if (moveRightPressed)
                        AerialDriftRight();

                    break;

                #endregion

                #region Parrying
                case CharacterStates.Parrying:

                    break;

                #endregion

                #region Blocking
                case CharacterStates.Blocking:

                    if (!blockPressed)
                    {
                        currentState = CharacterStates.Standing;
                    }

                    // Jump Code
                    // GetKeyDown
                    if (jumpPressedDown)
                    {
                        jumpTypeDecided = false;
                        currentState = CharacterStates.Squatting;
                    }
                    else if (shortHopPressedDown)
                    {
                        jumpTypeDecided = true;
                        shortHopping = true;
                        currentState = CharacterStates.Squatting;
                    }
                    else if (fullHopPressedDown)
                    {
                        jumpTypeDecided = true;
                        shortHopping = false;
                        currentState = CharacterStates.Squatting;
                    }

                    break;

                #endregion

                #region Getting Hit
                case CharacterStates.GettingHit:

                    break;
                #endregion

                #region Lying Down
                case CharacterStates.LyingDown:

                    break;

                #endregion

                #region Teching
                case CharacterStates.Teching:

                    break;

                #endregion

                #region Death
                case CharacterStates.Death:

                    if (fadeIn == false)
                    {
                        if (GameObject.Find("GameManager").GetComponent<Game_Manager>().PlayerDeathFadeOut() == true)
                        {
                            transform.position = respawnPoint;
                            Initialize();
                            fadeIn = true;
                        }
                    }
                    else
                    {
                        if (GameObject.Find("GameManager").GetComponent<Game_Manager>().PlayerDeathFadeIn() == true)
                        {
                            currentState = CharacterStates.Standing;
                            fadeIn = false;
                        }
                    }


                    break;

                    #endregion
            }
        }

        if (timeSinceDownPressed > doubleTapWindow)
        {
            timeSinceDownPressed = 0.0f;
            downHasLeftZone = false;
        }

        if (currentState != CharacterStates.Jumping && currentState != CharacterStates.DoubleJumping && currentState != CharacterStates.Squatting)
        {
            //Reset values for next grounded jump
            jumpTypeDecided = false;
            jumpVelocitySlowRate = defaultJumpVelocitySlowRate;
        }

        if(currentState != CharacterStates.GettingHit && currentState != CharacterStates.Death && currentState != CharacterStates.GroundAttacking && currentState != CharacterStates.AirAttacking)
        {
            if(swapFormPressedDown && availableForms.Count > 1)
            {
                swapForm = true;
                soundManager.PlayPlayerChangeForm();
            }

            if (swapForm == true)
            {
                SwapForm();
            }
        }

        // If not running and the player is playing
        if(currentState != CharacterStates.Running && soundManager.PlayerSFXIsPlaying)
        {
            // Stop the walk if the track is the walk track
            soundManager.StopPlayerWalk();
        }

        staminaBar.transform.Find("Bar").GetComponent<Image>().fillAmount = stamina * (1.0f / maxStamina);
        healthBar.transform.Find("Bar").GetComponent<Image>().fillAmount = health * (1.0f / maxHealth);

        //Camera follows player most of the time
        CameraFollow();

        UpdateUI();

        moveLeftPressedLastFrame = moveLeftPressed;
        moveRightPressedLastFrame = moveRightPressed;

        jumpPressedLastFrame = jumpPressed;
        shortHopPressedLastFrame = shortHopPressed;
        fullHopPressedLastFrame = fullHopPressed;

        downPressedLastFrame = downPressed;

        attackPressedLastFrame = attackPressed;

        specialAttackPressedLastFrame = specialAttackPressed;

        swapFormPressedLastFrame = swapFormPressed;

        lastState = currentState;

        #region First Frame of State Setter
        if(currentState != CharacterStates.Standing)
        {
            firstFrameOfStanding = true;
        }
        if (currentState != CharacterStates.Squatting)
        {
            firstFrameOfSquatting = true;
        }
        if (currentState != CharacterStates.Jumping)
        {
            firstFrameOfJumping = true;
        }
        if (currentState != CharacterStates.Running)
        {
            firstFrameOfRunning = true;
        }
        if (currentState != CharacterStates.Crouching)
        {
            firstFrameOfCrouching = true;
        }
        if (currentState != CharacterStates.Dashing)
        {
            firstFrameOfDashing = true;
        }
        if (currentState != CharacterStates.Landing)
        {
            firstFrameOfLanding = true;
        }
        if (currentState != CharacterStates.Falling)
        {
            firstFrameOfFalling = true;
        }
        if (currentState != CharacterStates.DoubleJumping)
        {
            firstFrameOfDoubleJumping = true;
        }
        if (currentState != CharacterStates.GroundAttacking)
        {
            firstFrameOfGroundAttacking = true;
        }
        if (currentState != CharacterStates.AirAttacking)
        {
            firstFrameOfAirAttacking = true;
        }
        if (currentState != CharacterStates.Parrying)
        {
            firstFrameOfParrying = true;
        }
        if (currentState != CharacterStates.Blocking)
        {
            firstFrameOfBlocking = true;
        }
        if (currentState != CharacterStates.GettingHit)
        {
            firstFrameOfGettingHit = true;
        }
        if (currentState != CharacterStates.LyingDown)
        {
            firstFrameOfLyingDown = true;
        }
        if (currentState != CharacterStates.Teching)
        {
            firstFrameOfTeching = true;
        }
        if (currentState != CharacterStates.Death)
        {
            firstFrameOfDeath = true;
        }
        #endregion
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    private void CrouchAndFallThrough()
    {
        // If down is pressed and down has been double tapped and the stick has left the zone
        if (downPressed && timeSinceDownPressed != 0 && timeSinceDownPressed <= doubleTapWindow && downHasLeftZone)
        {
            // Check if standing on a platform
            if (CheckIfOnPlatform() == true)
            {
                // Move the player down through the platform enough so that it stops colliding with the platform
                //transform.Translate(0, -platformHelpAmount-0.05f, 0);
                fallThroughPlatform = true;
                // Set the current state to falling
                currentState = CharacterStates.Falling;
                // Reset the double tap down timer
                timeSinceDownPressed = 0.0f;
                downHasLeftZone = false;
            }
        }
        // If just down is pressed, no double tapping
        else if (downPressed && currentState != CharacterStates.Running)
        {
            // Set the players state to crouching
            currentState = CharacterStates.Crouching;
            // Start the double tap timer if it has not been started yet
            if (timeSinceDownPressed == 0)
                timeSinceDownPressed += Time.deltaTime;
        }
        else if(downPressed && !downPressedLastFrame)
        {
            // Set the players state to crouching
            currentState = CharacterStates.Crouching;
            // Start the double tap timer if it has not been started yet
            if (timeSinceDownPressed == 0)
                timeSinceDownPressed += Time.deltaTime;
        }

        // If the timer is active and you have left the zone
        if (timeSinceDownPressed != 0 && downValue < zoneValue)
        {
            // Set it to true
            downHasLeftZone = true;
        }
        // Else if the timer has stopped
        else if (timeSinceDownPressed == 0)
        {
            // Set it to false
            downHasLeftZone = false;
        }
    }

    //Double jumping resets the player's air velocity to a new value based on directional input at time of double jump
    private void DoubleJumpNewAirVelocity()
    {
        if (moveRightPressed)
        {
            currentHorizontalAirSpeed = horizontalAirSpeed;
        }
        else if (moveLeftPressed)
        {
            currentHorizontalAirSpeed = -horizontalAirSpeed;
        }
        else
        {
            currentHorizontalAirSpeed = 0;
        }
    }

    //Camera Follows player using LERP
    private void CameraFollow()
    {
        //// If the player is far enough to the left of the camera, move the camera with the player
        //if (gameObject.transform.position.x <= (Camera.main.transform.position).x - 1)
        //    Camera.main.transform.Translate(-horizontalSpeed * Time.deltaTime, 0, 0);

        //// If the player is far enough to the right of the camera, move the camera with the player
        //if (gameObject.transform.position.x >= (Camera.main.transform.position).x + 1)
        //    Camera.main.transform.Translate(horizontalSpeed * Time.deltaTime, 0, 0);

        //OLD REAL CAMERA
        // Keep the camera at the same y position of the player minus a value
        //Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y + Screen.height / 600, Camera.main.transform.position.z);

        //OLD CAMERAS FOR TESTING (disable left and right code to use)

        //Follows player exactly 
        //Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + Screen.height / 600, Camera.main.transform.position.z);

        // Completely static
        //Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);

        //NEW CAMERA
        //These camera behaviors revolve entirely around the player. They work best for platforming.
        //We will likely have different behaviors designed to keep enemies on screen. (Lock on mechanics?)

        float interpolation = cameraSpeed * Time.deltaTime;
        Vector3 cameraPosition = Camera.main.transform.position;

        //Normal Behavior
        if (normalCameraMode)
        {

            Camera.main.orthographicSize = 7;

            //Invert horizontal offset when facing opposite direction
            if (!facingRight && cameraHorizontalOffset > 0 || facingRight && cameraHorizontalOffset < 0)
            {
                cameraHorizontalOffset *= -1.0f;
            }

            //HORIZONTAL BEHAVIOR
            //Camera horizontally follows the player with an offset.
            //This results in the player being able to see further ahead than behind.

            
            cameraPosition.x = Mathf.Lerp(Camera.main.transform.position.x, transform.position.x + cameraHorizontalOffset, interpolation);

            //VERTICAL BEHAVIOR
            //Camera vertically follows the player if they are grounded.
            //The camera will not vertically move when the player jumps. Instead it will move when they land on ground at a different height.
            //The camera will also move if the player nears the vertical edges of the screen.

            //If the player just landed
            if (currentState == CharacterStates.Landing)
            {
                //Save the vertical position of the player/current ground as the camera's destination
                cameraVerticalDestination = transform.position.y + cameraVerticalOffset;
                cameraMoveCompleted = false;
                cameraFalling = false;
            }

            //If the player is near the top or bottom of the screen or a fall is occurring
            if (transform.position.y > cameraPosition.y + 4.0f || transform.position.y < cameraPosition.y - 3.0f || cameraFalling)
            {
                //Save the vertical position of the player as the camera's destination
                cameraVerticalDestination = transform.position.y - cameraVerticalOffset;
                cameraMoveCompleted = false;
                cameraFalling = true;
            }

            //If the camera hasn't finished moving, continue towards the destination
            if (!cameraMoveCompleted)
            {
                cameraPosition.y = Mathf.Lerp(Camera.main.transform.position.y, cameraVerticalDestination, interpolation);
            }

            //If the camera is very close to its destination, place it there and mark the movement as completed
            if (Mathf.Abs(cameraPosition.y - cameraVerticalDestination) <= 0.01 && !cameraMoveCompleted)
            {
                cameraPosition.y = cameraVerticalDestination;
                cameraMoveCompleted = true;
                cameraFalling = false;
            }
        }

        //Update the camera's position
        Camera.main.transform.position = cameraPosition;
    }

    private void UpdateUI()
    {
        switch(currentForm)
        {
            // If in base form, put the base form in the active form slot
            case Forms.Base:
                baseFormIcon.transform.position = activeFormIcon.transform.position;
                baseFormIcon.GetComponent<RectTransform>().sizeDelta = activeFormIcon.GetComponent<RectTransform>().sizeDelta;
                baseFormIcon.SetActive(true);
                break;

            // If in arms form, put the arms form in the active form slot
            case Forms.Arms:
                armFormIcon.transform.position = activeFormIcon.transform.position;
                armFormIcon.GetComponent<RectTransform>().sizeDelta = activeFormIcon.GetComponent<RectTransform>().sizeDelta;
                armFormIcon.SetActive(true);
                break;

            // If in fire form, put the fire form in the active form slot
            case Forms.Fire:
                fireFormIcon.transform.position = activeFormIcon.transform.position;
                fireFormIcon.GetComponent<RectTransform>().sizeDelta = activeFormIcon.GetComponent<RectTransform>().sizeDelta;
                fireFormIcon.SetActive(true);
                break;
        }

        // If the player has more than the base form
        if (availableForms.Count != 1 && currentForm != Forms.Base)
        {
            switch (nextUpForm)
            {
                // If the next up form is the base form, put the base form in the next up slot
                case Forms.Base:
                    baseFormIcon.transform.position = nextUpFormIcon.transform.position;
                    baseFormIcon.GetComponent<RectTransform>().sizeDelta = nextUpFormIcon.GetComponent<RectTransform>().sizeDelta;
                    baseFormIcon.SetActive(true);
                    break;

                // If the next up form is the arms form, put the arms form in the next up slot
                case Forms.Arms:
                    armFormIcon.transform.position = nextUpFormIcon.transform.position;
                    armFormIcon.GetComponent<RectTransform>().sizeDelta = nextUpFormIcon.GetComponent<RectTransform>().sizeDelta;
                    armFormIcon.SetActive(true);
                    break;

                // If the next up form is the fire form, put the fire form in the next up slot
                case Forms.Fire:
                    fireFormIcon.transform.position = nextUpFormIcon.transform.position;
                    fireFormIcon.GetComponent<RectTransform>().sizeDelta = nextUpFormIcon.GetComponent<RectTransform>().sizeDelta;
                    fireFormIcon.SetActive(true);
                    break;
            }
        }

        if (currentForm != Forms.Base && nextUpForm != Forms.Base)
        {
            //transformSound.Play();
            baseFormIcon.SetActive(false);
        }
        if (currentForm != Forms.Arms && nextUpForm != Forms.Arms)
        {
            //transformSound.Play();
            armFormIcon.SetActive(false);
        }
        if (currentForm != Forms.Fire && nextUpForm != Forms.Fire)
        {
            //transformSound.Play();
            fireFormIcon.SetActive(false);
        }

    }
}
