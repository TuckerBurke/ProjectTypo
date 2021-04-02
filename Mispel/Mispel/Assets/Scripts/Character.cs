using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Character : MonoBehaviour
{
    protected enum CharacterStates
    {
        Null,
        Standing,
        Squatting,
        Jumping,
        Running,
        Crouching,
        Dashing,
        Landing,
        Falling,
        DoubleJumping,
        GroundAttacking,
        AirAttacking,
        Parrying,
        Blocking,
        GettingHit,
        LyingDown,
        Teching,
        Death
    }

    public enum Forms
    {
        Base,
        Arms,
        Fire,
        Cell
    }

    protected BoxCollider2D groundColliderBox;
    ContactFilter2D filter;

    [SerializeField] private GameObject transformImage;

    //Sprite Sets
    [SerializeField] protected Sprite[] TiltSprites;
    [SerializeField] protected Sprite[] AirSprites;
    [SerializeField] protected Sprite[] SpecSprites;
    [SerializeField] protected Sprite[] GroundSprites;
    [SerializeField] protected Sprite[] JumpSprites;
    [SerializeField] protected Sprite[] RunSprites;

    //Object with visible sprites
    [SerializeField] protected GameObject characterSprite;
    private SpriteRenderer currentSprite;

    protected GameObject gameManager;
    private Base_Attacks baseAttacks;

    protected Arm_Form_Attacks armFormAttacks;
    protected Fire_Form_Attacks fireFormAttacks;

    protected GameObject model;

    // Change to protected once the attack text is removed                                                     !!!!!!!!
    [SerializeField] protected AttackIDs.AttackID currentAttackID;
    private AttackIDs.AttackID previousAttackID;
    // Change to protected once the attack text is removed                                                     !!!!!!!!
    [SerializeField] protected CharacterStates currentState;
    protected CharacterStates lastState;
    protected CharacterStates stateBeforeAttacking;
    private const float gravity = -9.81f;

    protected Vector3 jumpVelocity;

    protected float horizontalSpeed;
    protected float horizontalAirSpeed;
    protected float currentHorizontalAirSpeed;
    protected float runPotentialAirSpeed;

    protected float horizontalAirSpeedUpAmount;
    protected float horizontalAirSpeedDownAmount;

    private float jumpStrength;

    protected float jumpVelocitySlowRate;
    protected float defaultJumpVelocitySlowRate;
    protected float shortHopVelocitySlowRate;
    protected float fastFallVelocitySlowRate;

    protected float terminalVelocity;
    protected float defaultTerminalVelocity;
    protected float fastFallBaseVelocity;
    protected float fastFallTerminalVelocity;

    private bool hasJumped;
    protected bool hasDoubleJumped;

    protected float jumpSquatTimer;
    private float jumpSquatLength;
    protected bool jumpTypeDecided;
    protected bool shortHopping;
    protected bool fastFalling;

    private float groundHelpAmount;
    protected float platformHelpAmount;

    public bool facingRight;
    protected bool jumpForward;

    protected bool fallForward;

    public bool isAttacking;

    protected float stamina;
    protected float maxStamina;
    private float staminaRegenSpeed;
    private bool initiatedAttack;
    private float staminaRechargeTimer;

    protected float health;
    protected float maxHealth;

    private float leftRightCollisionOffset;

    protected bool fallThroughPlatform;

    // Ground Landing Lag
    private float landingLag;

    // Aerial Landing Lag
    private float neutralAir1LandingLag;
    private float forwardAir1LandingLag;
    private float backAir1LandingLag;
    private float upAir1LandingLag;
    private float downAir1LandingLag;

    // Arm Form Landing Lag
    private float armsUpSpecialLandingLag;
    private float armsSideSpecialLandingLag;
    private float armsNeutralSpecialLandingLag;
    private float armsDownSpecialLandingLag;

    // Fire Form Landing Lag
    private float fireUpSpecialLandingLag;
    private float fireSideSpecialLandingLag;
    private float fireNeutralSpecialLandingLag;
    private float fireDownSpecialLandingLag;

    // Tilts
    private float jab1StaminaCost;
    private float forwardTilt1StaminaCost;
    private float upTilt1StaminaCost;
    private float downTilt1StaminaCost;

    // Aerials
    private float neutralAir1StaminaCost;
    private float forwardAir1StaminaCost;
    private float backAir1StaminaCost;
    private float upAir1StaminaCost;
    private float downAir1StaminaCost;

    // Arm Form Specials
    private float armsUpSpecialStaminaCost;
    private float armsSideSpecialStaminaCost;
    private float armsNeutralSpecialStaminaCost;
    private float armsDownSpecialStaminaCost;

    // Fire Form Specials
    private float fireUpSpecialStaminaCost;
    private float fireSideSpecialStaminaCost;
    private float fireNeutralSpecialStaminaCost;
    private float fireDownSpecialStaminaCost;


    private float landingLagTimer;
    protected CharacterStates previousCharacterState;

    [SerializeField] private Vector3 knockbackForce;
    private float knockbackForceSlowRate;
    private float hitStunLength;
    private float hitStunTimer;

    public Vector3 respawnPoint;
    protected bool respawnPointIsSet;

    private Vector3 pastPosition;

    protected Forms currentForm;
    protected Forms nextUpForm;

    private float formSwapFlashTimer;
    protected bool swapForm;
    private float formSwapFlashDuration;
    private bool halfWayThroughFlash;
    public List<Forms> availableForms;

    protected float armor;
    protected float maxArmor;
    protected float timeTillArmorRegen;
    private float armorRegenTimer;
 
    public Animator animator;

    public List<GameObject> neutralAirHitboxSet;
    public List<GameObject> forwardAirHitboxSet;
    public List<GameObject> upAirHitboxSet;
    public List<GameObject> backAirHitboxSet;
    public List<GameObject> downAirHitboxSet;

    public List<GameObject> jabHitboxSet;
    public List<GameObject> forwardTiltHitboxSet;
    public List<GameObject> upTiltHitboxSet;
    public List<GameObject> downTiltHitboxSet;

    public List<GameObject> armsNeutralBHitboxSet;
    public List<GameObject> armsSideBHitboxSet;
    public List<GameObject> armsUpBHitboxSet;

    public List<GameObject> fireNeutralBHitboxSet;
    public List<GameObject> fireSideBHitboxSet;
    public List<GameObject> fireUpBHitboxSet;

    public bool allAttacksOff;

    public AttackIDs.AttackID CurrentAttackID
    {
        get { return currentAttackID; }
    }

    public BoxCollider2D GroundColliderBox
    {
        get { return groundColliderBox; }
    }

    protected virtual void Start()
    {
        gameManager = GameObject.Find("GameManager");
        baseAttacks = GetComponent<Base_Attacks>();

        groundColliderBox = GetComponent<BoxCollider2D>();
        filter = new ContactFilter2D();

        animator = GetComponentInChildren<Animator>();

        currentState = CharacterStates.Falling;
        //Set initial respawn point
        respawnPoint = transform.position;

        availableForms = new List<Forms>();
        //availableForms.Add(Forms.Base);

        model = transform.GetChild(0).GetChild(0).gameObject;//Find("Model").gameObject;

        Initialize();

    }

    //Runs once on startup and every time the player dies / respawns
    protected virtual void Initialize()
    {
        respawnPointIsSet = false;
        
        currentSprite = characterSprite.GetComponent<SpriteRenderer>();

        horizontalSpeed = 7.5f;
        horizontalAirSpeed = runPotentialAirSpeed = horizontalSpeed;

        horizontalAirSpeedUpAmount = 0.2f;
        horizontalAirSpeedDownAmount = 0.99f;

        jumpStrength = 15f;

        jumpVelocitySlowRate = -25.0f; //How high fullhops go
        defaultJumpVelocitySlowRate = jumpVelocitySlowRate;
        shortHopVelocitySlowRate = -40.0f; //How high shorthops go
        fastFallVelocitySlowRate = -60.0f;

        jumpSquatLength = 0.08f; //Jumpsquat is 4 frames @ 60fps
        shortHopping = false;

        terminalVelocity = -10.0f;
        defaultTerminalVelocity = terminalVelocity;
        fastFallBaseVelocity = -5.0f;
        fastFallTerminalVelocity = -20.0f;

        groundHelpAmount = 0.25f;
        platformHelpAmount = 0.15f;

        jumpForward = true;
        facingRight = true;

        stamina = 100;
        maxStamina = 100;
        staminaRegenSpeed = 100;

        health = 100;
        maxHealth = 100;
        armor = 0.0f;
        maxArmor = armor;
        timeTillArmorRegen = 3.0f;

        leftRightCollisionOffset = 0.05f;

        fallThroughPlatform = false;

        landingLag = 0.05f; // 3 frames @ 60fps
        neutralAir1LandingLag = 0.25f; // 15 frames @ 60fps
        forwardAir1LandingLag = 0.25f; // 15 frames @ 60fps
        backAir1LandingLag = 0.25f; // 15 frames @ 60fps
        upAir1LandingLag = 0.25f; // 15 frames @ 60fps
        downAir1LandingLag = 0.25f; // 15 frames @ 60fps

        // Arm Form Landing Lag
        armsUpSpecialLandingLag = 0.25f;
        armsSideSpecialLandingLag = 0.25f;
        armsNeutralSpecialLandingLag = 0.25f;
        armsDownSpecialLandingLag = 0.25f;

        // Fire Form Landing Lag
        fireUpSpecialLandingLag = 0.25f;
        fireSideSpecialLandingLag = 0.25f;
        fireNeutralSpecialLandingLag = 0.25f;
        fireDownSpecialLandingLag = 0.25f;

        // Tilts
        jab1StaminaCost = 5.0f;
        forwardTilt1StaminaCost = 5.0f;
        upTilt1StaminaCost = 5.0f;
        downTilt1StaminaCost = 5.0f;
        // Aerials
        neutralAir1StaminaCost = 5.0f;
        forwardAir1StaminaCost = 5.0f;
        backAir1StaminaCost = 5.0f;
        upAir1StaminaCost = 5.0f;
        downAir1StaminaCost = 5.0f;

        // Arm Form Specials
        armsUpSpecialStaminaCost = 5.0f;
        armsSideSpecialStaminaCost = 5.0f;
        armsNeutralSpecialStaminaCost = 5.0f;
        armsDownSpecialStaminaCost = 5.0f;

        // Fire Form Specials
        fireUpSpecialStaminaCost = 5.0f;
        fireSideSpecialStaminaCost = 5.0f;
        fireNeutralSpecialStaminaCost = 5.0f;
        fireDownSpecialStaminaCost = 5.0f;

        landingLagTimer = 0.0f;

        knockbackForce = Vector3.zero;
        knockbackForceSlowRate = 0.99f;

        formSwapFlashTimer = 0.0f;
        formSwapFlashDuration = 0.083333f; // 5 frames @ 60fps
    }

    protected virtual void Update()
    {
        switch (currentState)
        {
            #region Standing
            case CharacterStates.Standing:

                animator.Play("idle");

                currentSprite.sprite = GroundSprites[0];

                // Respawnpoint is now set to be the first time to land on the ground
                if(respawnPointIsSet == false)
                {
                    
                    respawnPointIsSet = true;
                }

                //animator.SetBool("Run", false);
                //animator.SetBool("JumpForward", false);
                //animator.SetBool("JumpBackward", false);
                //animator.SetBool("FallForward", false);
                //animator.SetBool("FallBackward", false);

                currentHorizontalAirSpeed = 0;

                hasJumped = false;
                hasDoubleJumped = false;
                terminalVelocity = defaultTerminalVelocity;
                jumpVelocitySlowRate = defaultJumpVelocitySlowRate;

                //HandlePlatformCollisions();

                CheckForEdge();

                break;

            #endregion

            #region Squatting
            //JumpSquat always precedes jump
            //The state lasts for a preset amount of time before jump begins
            case CharacterStates.Squatting:

                animator.Play("squat");

                currentSprite.sprite = JumpSprites[0];

                //If jump type is still unknown, default to fullHop
                if (!jumpTypeDecided)
                    shortHopping = false;

                jumpSquatTimer += Time.deltaTime;

                if (jumpSquatTimer >= jumpSquatLength)
                {
                    currentState = CharacterStates.Jumping;
                    jumpSquatTimer = 0;
                }

                break;

            #endregion

            #region Jumping
            case CharacterStates.Jumping:

                animator.Play("jump");

                currentSprite.sprite = JumpSprites[1];

                gameObject.transform.Translate(currentHorizontalAirSpeed * Time.deltaTime, 0, 0);

                //animator.SetBool("Run", false);
                //if(jumpForward == true)
                //{
                //    animator.SetBool("JumpForward", true);
                //    animator.SetBool("JumpBackward", false);
                //}
                //else
                //{
                //    animator.SetBool("JumpForward", false);
                //    animator.SetBool("JumpBackward", true);
                //}

                terminalVelocity = defaultTerminalVelocity;
                jumpVelocitySlowRate = defaultJumpVelocitySlowRate;

                // If the character has not jumped yet, apply the initial jump force
                if (hasJumped == false)
                    Jump();

                // If the jumpVelocty is close to 0
                if (jumpVelocity.y <= 0)
                {
                    // zero out the jump velocity
                    jumpVelocity.y = 0;

                    // If the character has reached the top of their jump, change the state to the falling state
                    currentState = CharacterStates.Falling;
                }

                //Change velocity slowing for full jump vs. short hop
                if (shortHopping)
                {
                    jumpVelocitySlowRate = shortHopVelocitySlowRate;
                }
                else
                {
                    jumpVelocitySlowRate = defaultJumpVelocitySlowRate;
                }

                ApplyGravity();

                HandleGroundCollisions();

                break;

            #endregion

            #region Running
            case CharacterStates.Running:

                animator.Play("run");

                //This is not animated because I don't feel like it
                currentSprite.sprite = RunSprites[0];

                //animator.SetBool("Run", true);
                //animator.SetBool("JumpForward", false);
                //animator.SetBool("JumpBackward", false);
                //animator.SetBool("FallForward", false);
                //animator.SetBool("FallBackward", false);

                CheckForEdge();

                hasDoubleJumped = false;
                hasJumped = false;

                HandleGroundCollisions();

                break;

            #endregion

            #region Crouching
            case CharacterStates.Crouching:

                animator.Play("crouch");

                currentSprite.sprite = GroundSprites[1];

                hasJumped = false;
                hasDoubleJumped = false;
                terminalVelocity = defaultTerminalVelocity;
                jumpVelocitySlowRate = defaultJumpVelocitySlowRate;

                //gameObject.transform.GetChild(0).transform.localScale = new Vector3(1, 0.5f, 1);

                break;

            #endregion

            #region Dashing
            case CharacterStates.Dashing:

                break;

            #endregion

            #region Landing
            case CharacterStates.Landing:

                animator.Play("land");

		        currentSprite.sprite = GroundSprites[1];
                currentSprite.flipX = false;

                // Store the attack ID before it is set to null
                if (currentAttackID != AttackIDs.AttackID.Null)
                {
                    previousAttackID = currentAttackID;
                }

                // Reset the current attack ID
                currentAttackID = AttackIDs.AttackID.Null;

                if (currentHorizontalAirSpeed != 0)
                {
                    gameObject.transform.Translate(currentHorizontalAirSpeed * Time.deltaTime, 0, 0);
                }

                // If the character landed during an aerial attack
                if (previousCharacterState == CharacterStates.AirAttacking)
                {
                    if((previousAttackID == AttackIDs.AttackID.NeutralAir1 && landingLagTimer >= neutralAir1LandingLag) ||
                        (previousAttackID == AttackIDs.AttackID.ForwardAir1 && landingLagTimer >= forwardAir1LandingLag) ||
                        (previousAttackID == AttackIDs.AttackID.BackAir1 && landingLagTimer >= backAir1LandingLag) ||
                        (previousAttackID == AttackIDs.AttackID.UpAir1 && landingLagTimer >= upAir1LandingLag) ||
                        (previousAttackID == AttackIDs.AttackID.DownAir1 && landingLagTimer >= downAir1LandingLag) ||
                        (previousAttackID == AttackIDs.AttackID.ArmsUpSpecial && landingLagTimer >= armsUpSpecialLandingLag) ||
                        (previousAttackID == AttackIDs.AttackID.ArmsSideSpecial && landingLagTimer >= armsSideSpecialLandingLag) ||
                        (previousAttackID == AttackIDs.AttackID.ArmsNeutralSpecial && landingLagTimer >= armsNeutralSpecialLandingLag) ||
                        (previousAttackID == AttackIDs.AttackID.ArmsDownSpecial && landingLagTimer >= armsDownSpecialLandingLag) ||
                        (previousAttackID == AttackIDs.AttackID.FireUpSpecial && landingLagTimer >= fireUpSpecialLandingLag) ||
                        (previousAttackID == AttackIDs.AttackID.FireSideSpecial && landingLagTimer >= fireSideSpecialLandingLag) ||
                        (previousAttackID == AttackIDs.AttackID.FireNeutralSpecial && landingLagTimer >= fireNeutralSpecialLandingLag) ||
                        (previousAttackID == AttackIDs.AttackID.FireDownSpecial && landingLagTimer >= fireDownSpecialLandingLag)
                        )
                    {
                        currentState = CharacterStates.Standing;
                        currentHorizontalAirSpeed = 0;
                    }
                }
                else
                {
                    // Otherwise, apply normal landing lag
                    if (landingLagTimer >= landingLag)
                    {
                        currentState = CharacterStates.Standing;
                        currentHorizontalAirSpeed = 0;
                    }
                }

                // If the character slides off a surface
                CheckForEdge();

                // Count up the landing lag timer
                landingLagTimer += Time.deltaTime;

                // If the character is leaving the landing state
                if(currentState == CharacterStates.Standing || currentState == CharacterStates.Falling)
                {
                    // Reset the timer
                    landingLagTimer = 0.0f;
                    previousAttackID = AttackIDs.AttackID.Null;
                }


                break;

            #endregion

            #region Falling
            case CharacterStates.Falling:

                animator.Play("fall");

                currentSprite.sprite = JumpSprites[2];

                gameObject.transform.Translate(currentHorizontalAirSpeed * Time.deltaTime, 0, 0);

                //animator.SetBool("Run", false);
                //
                //if (fallForward == true)
                //{
                //    animator.SetBool("FallForward", true);
                //    animator.SetBool("FallBackward", false);
                //}
                //else
                //{
                //    animator.SetBool("FallForward", false);
                //    animator.SetBool("FallBackward", true);
                //}

                //FastFall functionality
                if (fastFalling)
                {
                    //Fastfalling allows for a greater max fall speed
                    terminalVelocity = fastFallTerminalVelocity;
                    jumpVelocitySlowRate = fastFallVelocitySlowRate;
                    //Fastfalling immediately increases fall speed to a certain value
                    if (jumpVelocity.y > fastFallBaseVelocity)
                        jumpVelocity.y = fastFallBaseVelocity;
                }

                if (jumpVelocity.y >= 0.0f)
                    jumpVelocity.y = 0.0f;
                ApplyGravity();
                HandleGroundCollisions();
                HandlePlatformCollisions();
                break;

            #endregion

            #region Double Jumping
            case CharacterStates.DoubleJumping:

                animator.Play("doubleJump");

                currentSprite.sprite = JumpSprites[1];

                gameObject.transform.Translate(currentHorizontalAirSpeed * Time.deltaTime, 0, 0);

                //animator.SetBool("Run", false);
                //if (jumpForward == true)
                //{
                //    animator.SetBool("JumpForward", true);
                //    animator.SetBool("JumpBackward", false);
                //}
                //else
                //{
                //    animator.SetBool("JumpForward", false);
                //    animator.SetBool("JumpBackward", true);
                //}

                terminalVelocity = defaultTerminalVelocity;
                jumpVelocitySlowRate = defaultJumpVelocitySlowRate;

                // If the character has not double jumped yet then apply the initial jump force
                if (hasDoubleJumped == false)
                    DoubleJump();

                // If the jumpVelocty is less than 0
                if (jumpVelocity.y <= 0)
                {
                    // zero out the jump velocity
                    jumpVelocity.y = 0;

                    // If the character has reached the top of their jump, change the state to the falling state
                    currentState = CharacterStates.Falling;
                }

                ApplyGravity();

                HandleGroundCollisions();

                break;

            #endregion

            #region Ground Attacking
            case CharacterStates.GroundAttacking:

                isAttacking = true;

                switch(currentAttackID)
                {
                    case AttackIDs.AttackID.UpTilt1:

                        currentSprite.sprite = TiltSprites[2];

                        if (initiatedAttack == false)
                        {
                            stamina -= upTilt1StaminaCost;
                            staminaRechargeTimer = 0.0f;
                        }

                        // Perform the attack until it is done (returns true when move has finished)
                        if (baseAttacks.UpTilt1(animator) == true)
                        {
                            // Change the current state to standing and reset the attack ID
                            currentState = CharacterStates.Standing;
                            currentAttackID = AttackIDs.AttackID.Null;
                        }

                        break;

                    case AttackIDs.AttackID.ForwardTilt1:

                        currentSprite.sprite = TiltSprites[1];

                        if (initiatedAttack == false)
                        {
                            stamina -= forwardTilt1StaminaCost;
                            staminaRechargeTimer = 0.0f;
                            jumpVelocity.y = 0;
                        }

                        if(facingRight)
                        {
                            gameObject.transform.Translate(horizontalSpeed * Time.deltaTime, 0, 0);
                        }
                        else
                        {
                            gameObject.transform.Translate(-horizontalSpeed * Time.deltaTime, 0, 0);
                        }

                        HandleGroundCollisions();

                        //if(CheckIfOnGround() == false && CheckIfOnPlatform() == false)
                        //    ApplyGravity();
                        CheckForEdge();

                        // Perform the attack until it is done (returns true when move has finished)
                        if (baseAttacks.ForwardTilt1(animator) == true)
                        {
                            // Change the current state to standing and reset the attack ID
                            currentState = CharacterStates.Standing;
                            currentAttackID = AttackIDs.AttackID.Null;
                        }

                        break;

                    case AttackIDs.AttackID.Jab1:

                        currentSprite.sprite = TiltSprites[0];

                        if (initiatedAttack == false)
                        {
                            stamina -= jab1StaminaCost;
                            staminaRechargeTimer = 0.0f;
                        }

                        // Perform the attack until it is done (returns true when move has finished)
                        if (baseAttacks.Jab1(animator) == true)
                        {
                            // Change the current state to standing and reset the attack ID
                            currentState = CharacterStates.Standing;
                            currentAttackID = AttackIDs.AttackID.Null;
                        }

                        break;

                    case AttackIDs.AttackID.DownTilt1:

                        currentSprite.sprite = TiltSprites[3];

                        if (initiatedAttack == false)
                        {
                            stamina -= downTilt1StaminaCost;
                            staminaRechargeTimer = 0.0f;
                        }

                        // Perform the attack until it is done (returns true when move has finished)
                        if (baseAttacks.DownTilt1(animator) == true)
                        {
                            // Change the current state to standing and reset the attack ID
                            currentState = CharacterStates.Standing;
                            currentAttackID = AttackIDs.AttackID.Null;
                        }

                        break;

                    // Arms Form Specials

                    case AttackIDs.AttackID.ArmsUpSpecial:

                        if(initiatedAttack == false)
                        {
                            stamina -= armsUpSpecialStaminaCost;
                            staminaRechargeTimer = 0.0f;
                            jumpVelocity.y = 5.0f;
                            currentHorizontalAirSpeed = 3.0f;
                        }


                        if (facingRight)
                        {
                            gameObject.transform.Translate(currentHorizontalAirSpeed * Time.deltaTime, jumpVelocity.y * Time.deltaTime, 0);
                        }
                        else
                        {
                            gameObject.transform.Translate(-currentHorizontalAirSpeed * Time.deltaTime, jumpVelocity.y * Time.deltaTime, 0);
                        }
                        //ApplyGravity();
                        HandleGroundCollisions();

                        // Perform the attack until it is done (returns true when move has finished)
                        if (armFormAttacks.UpSpecial(animator) == true)
                        {
                            // Change the current state to standing and reset the attack ID
                            currentState = CharacterStates.Standing;
                            currentAttackID = AttackIDs.AttackID.Null;
                        }

                        break;

                    case AttackIDs.AttackID.ArmsSideSpecial:

                        if (initiatedAttack == false)
                        {
                            stamina -= armsSideSpecialStaminaCost;
                            staminaRechargeTimer = 0.0f;
                        }

                        if (facingRight)
                        {
                            gameObject.transform.Translate(horizontalSpeed * 1.5f * Time.deltaTime, 0, 0);
                        }
                        else
                        {
                            gameObject.transform.Translate(-horizontalSpeed * 1.5f * Time.deltaTime, 0, 0);
                        }
                        ApplyGravity();
                        HandleGroundCollisions();

                        // Perform the attack until it is done (returns true when move has finished)
                        if (armFormAttacks.SideSpecial(animator) == true)
                        {
                            // Change the current state to standing and reset the attack ID
                            currentState = CharacterStates.Standing;
                            currentAttackID = AttackIDs.AttackID.Null;
                        }

                        break;

                    case AttackIDs.AttackID.ArmsNeutralSpecial:

                        if (initiatedAttack == false)
                        {
                            stamina -= armsNeutralSpecialStaminaCost;
                            staminaRechargeTimer = 0.0f;
                        }

                        // Perform the attack until it is done (returns true when move has finished)
                        if (armFormAttacks.NeutralSpecial(animator) == true)
                        {
                            // Change the current state to standing and reset the attack ID
                            currentState = CharacterStates.Standing;
                            currentAttackID = AttackIDs.AttackID.Null;
                        }

                        break;

                    case AttackIDs.AttackID.ArmsDownSpecial:

                        if (initiatedAttack == false)
                        {
                            stamina -= armsDownSpecialStaminaCost;
                            staminaRechargeTimer = 0.0f;
                        }

                        // Perform the attack until it is done (returns true when move has finished)
                        if (armFormAttacks.NeutralSpecial(animator) == true)
                        {
                            // Change the current state to standing and reset the attack ID
                            currentState = CharacterStates.Standing;
                            currentAttackID = AttackIDs.AttackID.Null;
                        }

                        break;

                    // Fire Form Specials

                    case AttackIDs.AttackID.FireUpSpecial:

                        if (initiatedAttack == false)
                        {
                            stamina -= fireUpSpecialStaminaCost;
                            staminaRechargeTimer = 0.0f;
                            jumpVelocity.y = 5.0f;
                        }

                        if (facingRight)
                        {
                            gameObject.transform.Translate(0, jumpVelocity.y * Time.deltaTime, 0);
                        }
                        else
                        {
                            gameObject.transform.Translate(0, jumpVelocity.y * Time.deltaTime, 0);
                        }

                        // Perform the attack until it is done (returns true when move has finished)
                        if (fireFormAttacks.UpSpecial(animator) == true)
                        {
                            // Change the current state to standing and reset the attack ID
                            currentState = CharacterStates.Standing;
                            currentAttackID = AttackIDs.AttackID.Null;
                        }

                        break;

                    case AttackIDs.AttackID.FireSideSpecial:

                        if (initiatedAttack == false)
                        {
                            stamina -= fireSideSpecialStaminaCost;
                            staminaRechargeTimer = 0.0f;
                        }

                        // Perform the attack until it is done (returns true when move has finished)
                        if (fireFormAttacks.SideSpecial(animator) == true)
                        {
                            // Change the current state to standing and reset the attack ID
                            currentState = CharacterStates.Standing;
                            currentAttackID = AttackIDs.AttackID.Null;
                        }

                        break;

                    case AttackIDs.AttackID.FireNeutralSpecial:

                        if (initiatedAttack == false)
                        {
                            stamina -= fireNeutralSpecialStaminaCost;
                            staminaRechargeTimer = 0.0f;
                        }

                        // Perform the attack until it is done (returns true when move has finished)
                        if (fireFormAttacks.NeutralSpecial(animator) == true)
                        {
                            // Change the current state to standing and reset the attack ID
                            currentState = CharacterStates.Standing;
                            currentAttackID = AttackIDs.AttackID.Null;
                        }

                        break;

                    case AttackIDs.AttackID.FireDownSpecial:

                        if (initiatedAttack == false)
                        {
                            stamina -= fireDownSpecialStaminaCost;
                            staminaRechargeTimer = 0.0f;
                        }

                        // Perform the attack until it is done (returns true when move has finished)
                        if (fireFormAttacks.NeutralSpecial(animator) == true)
                        {
                            // Change the current state to standing and reset the attack ID
                            currentState = CharacterStates.Standing;
                            currentAttackID = AttackIDs.AttackID.Null;
                        }

                        break;

                }

                if (initiatedAttack == false)
                    initiatedAttack = true;

                break;

            #endregion

            #region Air Attacking
            case CharacterStates.AirAttacking:

                gameObject.transform.Translate(currentHorizontalAirSpeed * Time.deltaTime, 0, 0);

                // If the character should be falling, switch the code so it does falling code
                if (jumpVelocity.y < 0.0f)
                {
                    stateBeforeAttacking = CharacterStates.Null;
                }

                // If the character started air attacking while jumping
                if (stateBeforeAttacking == CharacterStates.Jumping || stateBeforeAttacking == CharacterStates.DoubleJumping)
                {
                    // Continue to jump while doing the attack
                    ApplyGravity();
                    HandleGroundCollisions();
                }
                // Otherwise
                else
                {
                    // Fall
                    if (jumpVelocity.y >= 0.0f)
                        jumpVelocity.y = 0.0f;
                    ApplyGravity();
                    HandleGroundCollisions();
                    HandlePlatformCollisions();
                }

                isAttacking = true;

                switch (currentAttackID)
                {

                    case AttackIDs.AttackID.NeutralAir1:

                        currentSprite.sprite = AirSprites[0];

                        if (initiatedAttack == false)
                        {
                            stamina -= neutralAir1StaminaCost;
                            staminaRechargeTimer = 0.0f;
                        }

                        // Perform the attack until it is done (returns true when move has finished)
                        if (baseAttacks.NeutralAir1(animator) == true)
                        {
                            // If still jumping when the attack is complete, continue jumping
                            if (stateBeforeAttacking == CharacterStates.Jumping)
                            {
                                currentState = CharacterStates.Jumping;
                            }
                            else if(stateBeforeAttacking == CharacterStates.DoubleJumping)
                            {
                                currentState = CharacterStates.DoubleJumping;
                            }
                            else
                            {
                                // Change the current state to falling and reset the attack ID
                                currentState = CharacterStates.Falling;
                            }

                            currentAttackID = AttackIDs.AttackID.Null;
                        }

                        break;

                    case AttackIDs.AttackID.ForwardAir1:

                        currentSprite.sprite = AirSprites[1];

                        if (initiatedAttack == false)
                        {
                            stamina -= forwardAir1StaminaCost;
                            staminaRechargeTimer = 0.0f;
                        }

                        // Perform the attack until it is done (returns true when move has finished)
                        if (baseAttacks.ForwardAir1(animator) == true)
                        {
                            if (stateBeforeAttacking == CharacterStates.Jumping)
                            {
                                currentState = CharacterStates.Jumping;
                            }
                            else if (stateBeforeAttacking == CharacterStates.DoubleJumping)
                            {
                                currentState = CharacterStates.DoubleJumping;
                            }
                            else
                            {
                                // Change the current state to falling and reset the attack ID
                                currentState = CharacterStates.Falling;
                            }

                            currentAttackID = AttackIDs.AttackID.Null;
                        }

                        break;

                    case AttackIDs.AttackID.BackAir1:

                        currentSprite.sprite = AirSprites[3];
                        currentSprite.flipX = true;

                        if (initiatedAttack == false)
                        {
                            stamina -= backAir1StaminaCost;
                            staminaRechargeTimer = 0.0f;
                        }

                        // Perform the attack until it is done (returns true when move has finished)
                        if (baseAttacks.BackAir1(animator) == true)
                        {
                            if (stateBeforeAttacking == CharacterStates.Jumping)
                            {
                                currentState = CharacterStates.Jumping;
                            }
                            else if (stateBeforeAttacking == CharacterStates.DoubleJumping)
                            {
                                currentState = CharacterStates.DoubleJumping;
                            }
                            else
                            {
                                // Change the current state to falling and reset the attack ID
                                currentState = CharacterStates.Falling;
                            }

                            currentAttackID = AttackIDs.AttackID.Null;
                            currentSprite.flipX = false;
                        }

                        break;

                    case AttackIDs.AttackID.UpAir1:

                        currentSprite.sprite = AirSprites[2];

                        if (initiatedAttack == false)
                        {
                            stamina -= upAir1StaminaCost;
                            staminaRechargeTimer = 0.0f;
                        }

                        // Perform the attack until it is done (returns true when move has finished)
                        if (baseAttacks.UpAir1(animator) == true)
                        {
                            if (stateBeforeAttacking == CharacterStates.Jumping)
                            {
                                currentState = CharacterStates.Jumping;
                            }
                            else if (stateBeforeAttacking == CharacterStates.DoubleJumping)
                            {
                                currentState = CharacterStates.DoubleJumping;
                            }
                            else
                            {
                                // Change the current state to falling and reset the attack ID
                                currentState = CharacterStates.Falling;
                            }

                            currentAttackID = AttackIDs.AttackID.Null;
                        }

                        break;

                    case AttackIDs.AttackID.DownAir1:

                        currentSprite.sprite = AirSprites[4];

                        if (initiatedAttack == false)
                        {
                            stamina -= downAir1StaminaCost;
                            staminaRechargeTimer = 0.0f;
                        }

                        // Perform the attack until it is done (returns true when move has finished)
                        if (baseAttacks.DownAir1(animator) == true)
                        {
                            if (stateBeforeAttacking == CharacterStates.Jumping)
                            {
                                currentState = CharacterStates.Jumping;
                            }
                            else if (stateBeforeAttacking == CharacterStates.DoubleJumping)
                            {
                                currentState = CharacterStates.DoubleJumping;
                            }
                            else
                            {
                                // Change the current state to falling and reset the attack ID
                                currentState = CharacterStates.Falling;
                            }

                            currentAttackID = AttackIDs.AttackID.Null;
                        }

                        break;

                    // Arms Form Specials

                    case AttackIDs.AttackID.ArmsUpSpecial:

                        if (initiatedAttack == false)
                        {
                            stamina -= armsUpSpecialStaminaCost;
                            staminaRechargeTimer = 0.0f;
                            jumpVelocity.y = 5.0f;
                            currentHorizontalAirSpeed = 3.0f;
                        }


                        if (facingRight)
                        {
                            gameObject.transform.Translate(currentHorizontalAirSpeed * Time.deltaTime, jumpVelocity.y * Time.deltaTime, 0);
                        }
                        else
                        {
                            gameObject.transform.Translate(-currentHorizontalAirSpeed * Time.deltaTime, jumpVelocity.y * Time.deltaTime, 0);
                        }
                        //ApplyGravity();
                        HandleGroundCollisions();

                        // Perform the attack until it is done (returns true when move has finished)
                        if (armFormAttacks.UpSpecial(animator) == true)
                        {
                            if (stateBeforeAttacking == CharacterStates.Jumping)
                            {
                                currentState = CharacterStates.Jumping;
                            }
                            else if (stateBeforeAttacking == CharacterStates.DoubleJumping)
                            {
                                currentState = CharacterStates.DoubleJumping;
                            }
                            else
                            {
                                // Change the current state to falling and reset the attack ID
                                currentState = CharacterStates.Falling;
                            }

                            currentAttackID = AttackIDs.AttackID.Null;
                        }

                        break;

                    case AttackIDs.AttackID.ArmsSideSpecial:

                        if (initiatedAttack == false)
                        {
                            stamina -= armsSideSpecialStaminaCost;
                            staminaRechargeTimer = 0.0f;
                        }

                        if (facingRight)
                        {
                            gameObject.transform.Translate(horizontalSpeed * 1.5f * Time.deltaTime, 0, 0);
                        }
                        else
                        {
                            gameObject.transform.Translate(-horizontalSpeed * 1.5f * Time.deltaTime, 0, 0);
                        }
                        //ApplyGravity();
                        HandleGroundCollisions();

                        // Perform the attack until it is done (returns true when move has finished)
                        if (armFormAttacks.SideSpecial(animator) == true)
                        {
                            if (stateBeforeAttacking == CharacterStates.Jumping)
                            {
                                currentState = CharacterStates.Jumping;
                            }
                            else if (stateBeforeAttacking == CharacterStates.DoubleJumping)
                            {
                                currentState = CharacterStates.DoubleJumping;
                            }
                            else
                            {
                                // Change the current state to falling and reset the attack ID
                                currentState = CharacterStates.Falling;
                            }

                            currentAttackID = AttackIDs.AttackID.Null;
                        }

                        break;

                    case AttackIDs.AttackID.ArmsNeutralSpecial:

                        if (initiatedAttack == false)
                        {
                            stamina -= armsNeutralSpecialStaminaCost;
                            staminaRechargeTimer = 0.0f;
                        }

                        // Perform the attack until it is done (returns true when move has finished)
                        if (armFormAttacks.NeutralSpecial(animator) == true)
                        {
                            if (stateBeforeAttacking == CharacterStates.Jumping)
                            {
                                currentState = CharacterStates.Jumping;
                            }
                            else if (stateBeforeAttacking == CharacterStates.DoubleJumping)
                            {
                                currentState = CharacterStates.DoubleJumping;
                            }
                            else
                            {
                                // Change the current state to falling and reset the attack ID
                                currentState = CharacterStates.Falling;
                            }

                            currentAttackID = AttackIDs.AttackID.Null;
                        }

                        break;

                    case AttackIDs.AttackID.ArmsDownSpecial:

                        if (initiatedAttack == false)
                        {
                            stamina -= armsDownSpecialStaminaCost;
                            staminaRechargeTimer = 0.0f;
                        }

                        // Perform the attack until it is done (returns true when move has finished)
                        if (armFormAttacks.NeutralSpecial(animator) == true)
                        {
                            if (stateBeforeAttacking == CharacterStates.Jumping)
                            {
                                currentState = CharacterStates.Jumping;
                            }
                            else if (stateBeforeAttacking == CharacterStates.DoubleJumping)
                            {
                                currentState = CharacterStates.DoubleJumping;
                            }
                            else
                            {
                                // Change the current state to falling and reset the attack ID
                                currentState = CharacterStates.Falling;
                            }

                            currentAttackID = AttackIDs.AttackID.Null;
                        }

                        break;

                    // Fire Form Specials

                    case AttackIDs.AttackID.FireUpSpecial:

                        if (initiatedAttack == false)
                        {
                            stamina -= fireUpSpecialStaminaCost;
                            staminaRechargeTimer = 0.0f;
                            jumpVelocity.y = 5.0f;
                        }

                        if (facingRight)
                        {
                            gameObject.transform.Translate(0, jumpVelocity.y * Time.deltaTime, 0);
                        }
                        else
                        {
                            gameObject.transform.Translate(0, jumpVelocity.y * Time.deltaTime, 0);
                        }

                        // Perform the attack until it is done (returns true when move has finished)
                        if (fireFormAttacks.UpSpecial(animator) == true)
                        {
                            if (stateBeforeAttacking == CharacterStates.Jumping)
                            {
                                currentState = CharacterStates.Jumping;
                            }
                            else if (stateBeforeAttacking == CharacterStates.DoubleJumping)
                            {
                                currentState = CharacterStates.DoubleJumping;
                            }
                            else
                            {
                                // Change the current state to falling and reset the attack ID
                                currentState = CharacterStates.Falling;
                            }

                            currentAttackID = AttackIDs.AttackID.Null;
                        }

                        break;

                    case AttackIDs.AttackID.FireSideSpecial:

                        if (initiatedAttack == false)
                        {
                            stamina -= fireSideSpecialStaminaCost;
                            staminaRechargeTimer = 0.0f;
                        }

                        // Perform the attack until it is done (returns true when move has finished)
                        if (fireFormAttacks.SideSpecial(animator) == true)
                        {
                            if (stateBeforeAttacking == CharacterStates.Jumping)
                            {
                                currentState = CharacterStates.Jumping;
                            }
                            else if (stateBeforeAttacking == CharacterStates.DoubleJumping)
                            {
                                currentState = CharacterStates.DoubleJumping;
                            }
                            else
                            {
                                // Change the current state to falling and reset the attack ID
                                currentState = CharacterStates.Falling;
                            }

                            currentAttackID = AttackIDs.AttackID.Null;
                        }

                        break;

                    case AttackIDs.AttackID.FireNeutralSpecial:

                        if (initiatedAttack == false)
                        {
                            stamina -= fireNeutralSpecialStaminaCost;
                            staminaRechargeTimer = 0.0f;
                        }

                        // Perform the attack until it is done (returns true when move has finished)
                        if (fireFormAttacks.NeutralSpecial(animator) == true)
                        {
                            if (stateBeforeAttacking == CharacterStates.Jumping)
                            {
                                currentState = CharacterStates.Jumping;
                            }
                            else if (stateBeforeAttacking == CharacterStates.DoubleJumping)
                            {
                                currentState = CharacterStates.DoubleJumping;
                            }
                            else
                            {
                                // Change the current state to falling and reset the attack ID
                                currentState = CharacterStates.Falling;
                            }

                            currentAttackID = AttackIDs.AttackID.Null;
                        }

                        break;

                    case AttackIDs.AttackID.FireDownSpecial:

                        if (initiatedAttack == false)
                        {
                            stamina -= fireDownSpecialStaminaCost;
                            staminaRechargeTimer = 0.0f;
                        }

                        // Perform the attack until it is done (returns true when move has finished)
                        if (fireFormAttacks.NeutralSpecial(animator) == true)
                        {
                            if (stateBeforeAttacking == CharacterStates.Jumping)
                            {
                                currentState = CharacterStates.Jumping;
                            }
                            else if (stateBeforeAttacking == CharacterStates.DoubleJumping)
                            {
                                currentState = CharacterStates.DoubleJumping;
                            }
                            else
                            {
                                // Change the current state to falling and reset the attack ID
                                currentState = CharacterStates.Falling;
                            }

                            currentAttackID = AttackIDs.AttackID.Null;
                        }

                        break;
                }

                if (initiatedAttack == false)
                    initiatedAttack = true;

                break;

            #endregion

            #region Parrying
            case CharacterStates.Parrying:

                break;

            #endregion

            #region Blocking
            case CharacterStates.Blocking:

                animator.Play("block");

                hasJumped = false;
                hasDoubleJumped = false;
                terminalVelocity = defaultTerminalVelocity;
                jumpVelocitySlowRate = defaultJumpVelocitySlowRate;

		        currentSprite.sprite = GroundSprites[2];

                break;

            #endregion

            #region Getting Hit
            case CharacterStates.GettingHit:

                animator.Play("getHit");

                // Store inital knockback from TakeDamage method
                // Prevent input (mainly in Player)
                // Move over time (not teleport)
                if(hitStunTimer >= hitStunLength)
                {
                    if (CheckIfOnGround())
                    {
                        currentState = CharacterStates.Landing;
                    }
                    else
                    {
                        currentState = CharacterStates.Falling;
                    }
                    hitStunTimer = 0;
                    knockbackForce = Vector3.zero;
                }

                //if (jumpVelocity.y >= 0)
                //    jumpVelocity.y = 0;
                currentHorizontalAirSpeed = 0;

                ApplyGravity();
                HandleGroundCollisions();
                HandlePlatformCollisions();

                hitStunTimer += Time.deltaTime;

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

                animator.Play("death");

                ApplyGravity();
                HandleGroundCollisions();
                HandlePlatformCollisions();

                break;

                #endregion
        }

        if (currentState != CharacterStates.GroundAttacking && currentState != CharacterStates.AirAttacking)
        {
            isAttacking = false;
        }

        currentHorizontalAirSpeed *= horizontalAirSpeedDownAmount;
        if (currentHorizontalAirSpeed >= -0.001 && currentHorizontalAirSpeed <= 0.001)
            currentHorizontalAirSpeed = 0.0f;

        if(currentState != CharacterStates.GroundAttacking && currentState != CharacterStates.AirAttacking)
        {
            initiatedAttack = false;
        }

        if(currentState != CharacterStates.Falling)
        {
            fastFalling = false;
        }

        staminaRechargeTimer += Time.deltaTime;

        // If stamina hasn't been used for over 2.5 seconds
        if(staminaRechargeTimer >= 2.5f)
        {
            // If stamina is not full
            if(stamina < maxStamina)
            {
                // Then start to refill the stamina bar
                stamina += staminaRegenSpeed * Time.deltaTime;//0.05f;
            }
        }

        if (currentState != CharacterStates.Landing)
        {
            previousCharacterState = currentState;
        }


        // Apply the knockback force to the character
        transform.position += knockbackForce;
        // Slow down the knockback force
        // Maybe make exponential
        knockbackForce *= knockbackForceSlowRate * (1- Time.deltaTime);

        if (knockbackForce.x >= -0.01 && knockbackForce.x <= 0.01)
        {
            knockbackForce.x = 0.0f;
        }
        if(knockbackForce.y >= -0.01 && knockbackForce.y <= 0.01)
        {
            knockbackForce.y = 0.0f;
        }

        if(health <= 0)
        {
            currentState = CharacterStates.Death;
            health = 0;
        }

        pastPosition = transform.position;

        // Set the current form to the first thing in the list
        currentForm = availableForms[0];
        // If 
        //if(currentForm != Forms.Base)
            nextUpForm = availableForms[availableForms.Count - 1];

        switch(currentForm)
        {
            case Forms.Base:
                model.GetComponent<SkeletonMecanim>().skeleton.SetSkin("base");
                break;
            case Forms.Arms:
                model.GetComponent<SkeletonMecanim>().skeleton.SetSkin("arms");
                break;
            case Forms.Fire:
                model.GetComponent<SkeletonMecanim>().skeleton.SetSkin("fire");
                break;
            case Forms.Cell:
                model.GetComponent<SkeletonMecanim>().skeleton.SetSkin("cell");
                break;
        }

        if(armor <= 0)
        {
            armor = 0;
            armorRegenTimer += Time.deltaTime;

            if(armorRegenTimer >= timeTillArmorRegen)
            {
                armorRegenTimer = 0;
                armor = maxArmor;
            }
        }

        if(currentState != CharacterStates.AirAttacking && currentState != CharacterStates.GroundAttacking)
        {
            if (allAttacksOff == false)
            {
                BoxManager boxManager = GameObject.Find("GameManager").GetComponent<BoxManager>();
                boxManager.ChangeSetActiveStatus(neutralAirHitboxSet, false);
                boxManager.ChangeSetActiveStatus(forwardAirHitboxSet, false);
                boxManager.ChangeSetActiveStatus(upAirHitboxSet, false);
                boxManager.ChangeSetActiveStatus(backAirHitboxSet, false);
                boxManager.ChangeSetActiveStatus(downAirHitboxSet, false);

                boxManager.ChangeSetActiveStatus(jabHitboxSet, false);
                boxManager.ChangeSetActiveStatus(forwardTiltHitboxSet, false);
                boxManager.ChangeSetActiveStatus(upTiltHitboxSet, false);
                boxManager.ChangeSetActiveStatus(downTiltHitboxSet, false);

                boxManager.ChangeSetActiveStatus(armsNeutralBHitboxSet, false);
                boxManager.ChangeSetActiveStatus(armsSideBHitboxSet, false);
                boxManager.ChangeSetActiveStatus(armsUpBHitboxSet, false);

                boxManager.ChangeSetActiveStatus(fireNeutralBHitboxSet, false);
                boxManager.ChangeSetActiveStatus(fireSideBHitboxSet, false);
                boxManager.ChangeSetActiveStatus(fireUpBHitboxSet, false);

                allAttacksOff = true;
            }
        }
    }

    protected void HandleGroundCollisions()
    {
        Vector3 sumMovementVector = Vector3.zero;

        Vector3 movementVector = transform.position - pastPosition;
        bool comingFromLeft = (movementVector.x <= 0);
        bool comingFromRight = (movementVector.x >= 0);
        bool comingFromUp = (movementVector.y <= 0);
        bool comingFromDown = (movementVector.y >= 0);

        if(comingFromLeft)
        {
            sumMovementVector += RightCollision("Ground");
        }
        if(comingFromRight)
        {
            sumMovementVector += LeftCollision("Ground");
        }
        if(comingFromUp)
        {
            sumMovementVector += TopCollision("Ground");
        }
        if(comingFromDown)
        {
            sumMovementVector += BottomCollision("Ground");
        }

        //sumMovementVector += TopCollision("Ground");
        //sumMovementVector += BottomCollision("Ground");
        //sumMovementVector += LeftCollision("Ground");
        //sumMovementVector += RightCollision("Ground");

        gameObject.transform.Translate(sumMovementVector);
    }

    /// <summary>
    /// Used to check for colliding with the top of a box.
    /// The tag system is there so theoretically, you can use this to move out of another character
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    private Vector3 TopCollision(string tag)
    {
        Vector3 movementVector = Vector3.zero;

        // Get a list of all colliders that are colliding with the ground collider box
        List<Collider2D> collisions = new List<Collider2D>();
        groundColliderBox.OverlapCollider(filter.NoFilter(), collisions);

        for (int i = 0; i < collisions.Count; i++)
        {
            // If the collider being looked at matches the type of colliders being looked for
            if (collisions[i].tag == tag)
            {
                // If colliding with the top of the box collider
                if (groundColliderBox.bounds.min.y < collisions[i].bounds.max.y + groundHelpAmount)
                {
                    // If in the top half of the box collider
                    if (groundColliderBox.bounds.max.y > collisions[i].bounds.center.y)
                    {
                        // If horizontally within the box collider
                        if (groundColliderBox.bounds.min.x < collisions[i].bounds.max.x - groundHelpAmount && groundColliderBox.bounds.max.x > collisions[i].bounds.min.x + groundHelpAmount)
                        {
                            // Sets the movement vector's x value to the top side of the box
                            movementVector.y = collisions[i].bounds.max.y;
                            
                            // Calculates how much is needed to move to the top side of the box
                            movementVector.y -= groundColliderBox.bounds.min.y;
                            
                            if (movementVector.y > 1)
                            {
                                movementVector.y = 0;
                            }

                            //movementVector.y += -(transform.position - pastPosition).y;

                            if (tag == "Ground")
                                if (currentState == CharacterStates.Falling || currentState == CharacterStates.AirAttacking)
                                {
                                    if (jumpVelocity.y < 0.0f)
                                        currentState = CharacterStates.Landing;
                                }
                        }
                    }
                }
            }
        }
        return movementVector;
    }

    /// <summary>
    /// Used to check for colliding with the bottom of a box.
    /// The tag system is there so theoretically, you can use this to move out of another character
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    private Vector3 BottomCollision(string tag)
    {
        Vector3 movementVector = Vector3.zero;

        // Get a list of all colliders that are colliding with the ground collider box
        List<Collider2D> collisions = new List<Collider2D>();
        groundColliderBox.OverlapCollider(filter.NoFilter(), collisions);

        for (int i = 0; i < collisions.Count; i++)
        {
            // If the collider being looked at matches the type of colliders being looked for
            if (collisions[i].tag == tag)
            {
                // If colliding with the bottom of the box collider
                if (groundColliderBox.bounds.max.y > collisions[i].bounds.min.y - groundHelpAmount)
                {
                    // If in the bottom half of the box collider
                    if (groundColliderBox.bounds.max.y < collisions[i].bounds.center.y)
                    {
                        // If horizontally within the box collider
                        if (groundColliderBox.bounds.min.x < collisions[i].bounds.max.x - groundHelpAmount && groundColliderBox.bounds.max.x > collisions[i].bounds.min.x + groundHelpAmount)
                        {
                            // Sets the movement vector's x value to the bottom side of the box
                            movementVector.y = collisions[i].bounds.min.y;

                            // Calculates how much is needed to move to the bottom side of the box
                            movementVector.y -= groundColliderBox.bounds.max.y;

                            if (movementVector.y < -1)
                            {
                                movementVector.y = 0;
                            }

                            if (tag == "Ground")
                            {
                                if (currentAttackID != AttackIDs.AttackID.ArmsUpSpecial)
                                {
                                    if (currentState == CharacterStates.AirAttacking)
                                    {
                                        stateBeforeAttacking = CharacterStates.Null;
                                    }
                                    else
                                    {
                                        currentState = CharacterStates.Falling;
                                    }
                                }
                                terminalVelocity = defaultTerminalVelocity;
                                jumpVelocitySlowRate = defaultJumpVelocitySlowRate;
                            }

                        }
                    }
                }
            }
        }

        return movementVector;
    }

    /// <summary>
    /// Used to check for colliding with the left of a box.
    /// The tag system is there so theoretically, you can use this to move out of another character
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    private Vector3 LeftCollision(string tag)
    {
        Vector3 movementVector = Vector3.zero;

        // Get a list of all colliders that are colliding with the ground collider box
        List<Collider2D> collisions = new List<Collider2D>();
        groundColliderBox.OverlapCollider(filter.NoFilter(), collisions);

        for (int i = 0; i < collisions.Count; i++)
        {
            // If the collider being looked at matches the type of colliders being looked for
            if (collisions[i].tag == tag)
            {
                // If colliding with the left of the box collider
                if (groundColliderBox.bounds.max.x > collisions[i].bounds.min.x - groundHelpAmount)
                {
                    // If in the left half of the box collider
                    if (groundColliderBox.bounds.center.x < collisions[i].bounds.center.x)
                    {
                        // If vertically within the box collider
                        if (groundColliderBox.bounds.min.y < collisions[i].bounds.max.y - groundHelpAmount && groundColliderBox.bounds.max.y > collisions[i].bounds.min.y + groundHelpAmount)
                        {
                            // Sets the movement vector's x value to the left side of the box
                            movementVector.x = collisions[i].bounds.min.x - leftRightCollisionOffset;

                            // Calculates how much is needed to move to the left side of the box
                            movementVector.x -= groundColliderBox.bounds.max.x;

                            if(movementVector.x < -1)
                            {
                                movementVector.x = 0;
                            }

                            currentHorizontalAirSpeed = 0;
                        }
                    }
                }
            }
        }

        return movementVector;
    }

    /// <summary>
    /// Used to check for colliding with the right of a box.
    /// The tag system is there so theoretically, you can use this to move out of another character
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    private Vector3 RightCollision(string tag)
    {
        Vector3 movementVector = Vector3.zero;

        // Get a list of all colliders that are colliding with the ground collider box
        List<Collider2D> collisions = new List<Collider2D>();
        groundColliderBox.OverlapCollider(filter.NoFilter(), collisions);

        for (int i = 0; i < collisions.Count; i++)
        {
            // If the collider being looked at matches the type of colliders being looked for
            if (collisions[i].tag == tag)
            {
                // If colliding with the right of the box collider
                if (groundColliderBox.bounds.min.x < collisions[i].bounds.max.x + groundHelpAmount)
                {
                    // If in the right half of the box collider
                    if (groundColliderBox.bounds.center.x > collisions[i].bounds.center.x)
                    {
                        // If vertically within the box collider
                        if (groundColliderBox.bounds.min.y < collisions[i].bounds.max.y - groundHelpAmount && groundColliderBox.bounds.max.y > collisions[i].bounds.min.y + groundHelpAmount)
                        {
                            // Sets the movement vector's x value to the right side of the box
                            movementVector.x = collisions[i].bounds.max.x + leftRightCollisionOffset;

                            // Calculates how much is needed to move to the right side of the box
                            movementVector.x -= groundColliderBox.bounds.min.x;

                            if (movementVector.x > 1)
                            {
                                movementVector.x = 0;
                            }

                            currentHorizontalAirSpeed = 0;
                        }
                    }
                }
            }
        }

        return movementVector;
    }

    protected bool CheckIfOnGround()
    {
        // Get a list of all colliders that are colliding with the ground collider box
        List<Collider2D> collisions = new List<Collider2D>();
        groundColliderBox.OverlapCollider(filter.NoFilter(), collisions);

        for (int i = 0; i < collisions.Count; i++)
        {
            // If the collider being looked at matches the type of colliders being looked for
            if (collisions[i].tag == "Ground")
            {
                // If colliding with the top of the box collider
                if (groundColliderBox.bounds.min.y + groundHelpAmount >= collisions[i].bounds.max.y)
                {
                    return true;
                }
            }
        }

        return false;
    }

    protected void HandlePlatformCollisions()
    {

        // When fast falling, the player falls too fast and can go through the platform
        // This is a problem for after the demo, but this is the note to remember it

        Vector3 sumMovementVector = Vector3.zero;

        // If supposed to collide with platforms
        if (fallThroughPlatform == false)
        {
            sumMovementVector += PlatformCollision();
        }
        else
        {
            // If no longer colliding with the platform, set the variable back to false
            //if (PlatformCollision() == Vector3.zero)
            //    fallThroughPlatform = false;
        }

        gameObject.transform.Translate(sumMovementVector);
    }

    private Vector3 PlatformCollision()
    {
        Vector3 movementVector = Vector3.zero;

        // Get a list of all colliders that are colliding with the ground collider box
        List<Collider2D> collisions = new List<Collider2D>();
        groundColliderBox.OverlapCollider(filter.NoFilter(), collisions);

        for (int i = 0; i < collisions.Count; i++)
        {
            // If the collider being looked at matches the type of colliders being looked for
            if (collisions[i].tag == "Platform")
            {
                // If colliding with the top of the box collider
                if (groundColliderBox.bounds.min.y + platformHelpAmount >= collisions[i].bounds.max.y)
                {
                    movementVector.y = collisions[i].bounds.max.y;

                    movementVector.y -= groundColliderBox.bounds.min.y;

                    if (currentState == CharacterStates.Falling || currentState == CharacterStates.AirAttacking)
                    {
                        if (jumpVelocity.y < 0.0f && !fallThroughPlatform)
                            currentState = CharacterStates.Landing;
                    }
                }
            }
        }

        return movementVector;

    }

    protected bool CheckIfOnPlatform()
    {
        // Get a list of all colliders that are colliding with the ground collider box
        List<Collider2D> collisions = new List<Collider2D>();
        groundColliderBox.OverlapCollider(filter.NoFilter(), collisions);

        for (int i = 0; i < collisions.Count; i++)
        {
            // If the collider being looked at matches the type of colliders being looked for
            if (collisions[i].tag == "Platform")
            {
                // If colliding with the top of the box collider
                if (groundColliderBox.bounds.min.y + platformHelpAmount >= collisions[i].bounds.max.y)
                {
                    return true;
                }
            }
        }

        return false;
    }

    protected void CheckForEdge()
    {
        // Get a list of all colliders that are colliding with the ground collider box
        List<Collider2D> collisions = new List<Collider2D>();
        groundColliderBox.OverlapCollider(filter.NoFilter(), collisions);

        // Loop through the list of collisions and check if any of them are ground or platforms
        bool onGround = false;
        for (int i = 0; i < collisions.Count; i++)
        {
            // The collision is the ground or a platform (only if the character is supposed to collide with them) and if the collision is below the players feet
            if ((collisions[i].tag == "Ground" || (collisions[i].tag == "Platform" && fallThroughPlatform == false)) && collisions[i].bounds.min.y <= transform.position.y)
                onGround = true;
        }

        // If no ground or platforms were found then change the state to falling
        if (onGround == false)
        {
            currentState = CharacterStates.Falling;
            jumpVelocity.y = 0;
        }
    }

    protected void ApplyGravity()
    {
        // Apply gravity, like jumping but down
        // Decrease the jumpvelocity by the slow rate every frame

        jumpVelocity.y += jumpVelocitySlowRate * Time.deltaTime;

        // Cap the falling speed
        if (jumpVelocity.y <= terminalVelocity)
            jumpVelocity.y = terminalVelocity;

        // Move the characters transform by the jumpVelocity * delta time
        gameObject.transform.Translate(jumpVelocity * Time.deltaTime);
    }

    protected void RunLeft()
    {
        facingRight = false;
        currentHorizontalAirSpeed = -runPotentialAirSpeed;
        transform.GetChild(0).transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        // Translate the character to the left at horizontal speed
        gameObject.transform.Translate(-horizontalSpeed * Time.deltaTime, 0, 0);
    }

    protected void RunRight()
    {
        facingRight = true;
        currentHorizontalAirSpeed = runPotentialAirSpeed;
        transform.GetChild(0).transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        // Translate the character to the right at horizontal speed
        gameObject.transform.Translate(horizontalSpeed * Time.deltaTime, 0, 0);
    }

    protected void AerialDriftLeft()
    {
        //facingRight = false;
        // Translate the character to the left at horizontal speed
        //gameObject.transform.Translate(-horizontalAirSpeed * Time.deltaTime, 0, 0);
        if(currentHorizontalAirSpeed > -horizontalAirSpeed)
        {
            currentHorizontalAirSpeed -= horizontalAirSpeedUpAmount;
        }
    }

    protected void AerialDriftRight()
    {
        //facingRight = true;
        // Translate the character to the right at horizontal speed
        //gameObject.transform.Translate(horizontalAirSpeed * Time.deltaTime, 0, 0);
        if (currentHorizontalAirSpeed < horizontalAirSpeed)
        {
            currentHorizontalAirSpeed += horizontalAirSpeedUpAmount;
        }
    }

    protected void Jump()
    {
        // Set the jump velocity to the jump strength
        jumpVelocity.y = jumpStrength;

        hasJumped = true;
    }

    protected void DoubleJump()
    {
        // Set the jump velocity to the jump strength
        jumpVelocity.y = jumpStrength;

        hasDoubleJumped = true;
    }

    public void TakeDamage(Hitbox hitbox)
    {
        armor -= hitbox.Damage / 100.0f;

        if(armor <= 0)
        {
            jumpVelocity.y = 0;
            currentHorizontalAirSpeed = 0;
            
            currentState = CharacterStates.GettingHit;
            // Set the knockbackForce to its inital value
            knockbackForce = hitbox.Knockback / 10;
            // Maybe add it for combos?

            knockbackForce.y *= 1.75f;
            hitStunLength = hitbox.HitStunLength;
        }

        // Scale the damage down to between 0 and 1
        health -= hitbox.Damage;// / 100.0f;

        // Apply Juice
    }

    protected void SwapForm()
    {
        // If SwapForm was just called
        if (formSwapFlashTimer == 0)
        {
            // Reset the scale of the flash
            transformImage.transform.localScale = Vector3.one;
            // Set it to active
            transformImage.SetActive(true);

            halfWayThroughFlash = false;

        }
        // While the animation should be playing
        else if(formSwapFlashTimer <= formSwapFlashDuration)
        {
            // Scale the image up
            transformImage.transform.localScale += new Vector3(100,100,0) * Time.deltaTime;
        }
        // Once the animation is done
        else
        {
            // Reset all the variables
            transformImage.transform.localScale = Vector3.one;
            transformImage.SetActive(false);
            swapForm = false;
            formSwapFlashTimer = 0.0f;
            // Eject from the method so timer isn't increased one last time
            return;
        }

        // If the halfway mark hasn't been crossed but the timer is at or past half way
        if(halfWayThroughFlash == false && formSwapFlashTimer >= formSwapFlashDuration/2f)
        {
            // Mark that the animation is halfway through
            halfWayThroughFlash = true;
            // Cycle the forms
            CycleForms();
        }

        // Increment the timer and keep the flash centered around the player
        formSwapFlashTimer += Time.deltaTime;
        transformImage.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }

    public void CycleForms()
    {
        // Take the Form at the end of the list and put it at the beginning
        availableForms.Insert(0,availableForms[availableForms.Count-1]);
        // Remove the Form from the end of the list
        availableForms.RemoveAt(availableForms.Count - 1);
    }

    public void ChangeToStanding()
    {
        currentState = CharacterStates.Standing;
    }

}
