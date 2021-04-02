using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// DELETE LATER
using UnityEngine.UI;
// DELETE LATER

public class Base_Attacks : MonoBehaviour
{

    // DELETE LATER
    public GameObject attackText;
    // DELETE LATER

    private BoxManager boxManager;

    private float forwardTilt1TotalLength;
    private float jab1TotalLength;
    private float downTilt1TotalLength;
    private float upTilt1TotalLength;

    private float forwardAir1TotalLength;
    private float neutralAir1TotalLength;
    private float backAir1TotalLength;
    private float downAir1TotalLength;
    private float upAir1TotalLength;

    private float moveTimer;

    // Start is called before the first frame update
    void Start()
    {
        forwardTilt1TotalLength = 0.55f;
        jab1TotalLength = 0.35f;
        downTilt1TotalLength = 0.55f;
        upTilt1TotalLength = 0.55f;

        forwardAir1TotalLength = 0.55f;
        neutralAir1TotalLength = 0.55f;
        backAir1TotalLength = 0.55f;
        downAir1TotalLength = 0.55f;
        upAir1TotalLength = 0.55f;

        moveTimer = 0.0f;

        boxManager = GameObject.Find("GameManager").GetComponent<BoxManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // DELETE LATER
        //Vector3 pos = gameObject.transform.position;
        //pos.x += 1.5f;
        //pos.y += 1;
        //attackText.transform.position = Camera.main.WorldToScreenPoint(pos);
        //attackText.GetComponent<Text>().text = gameObject.GetComponent<Character>().currentAttackID.ToString();
        // DELETE LATER

        if (gameObject.GetComponent<Character>().isAttacking)
        {
            moveTimer += Time.deltaTime;
        }
        else
        {
            moveTimer = 0.0f;
        }
    }

    // Attack Method

    public bool ForwardTilt1(Animator animator)
    {
        // Timer count down
        // When done change state

        if(moveTimer <= forwardTilt1TotalLength)
        {
            // Play animation
            animator.Play("forwardTilt");

            if (moveTimer >= 0.25f)
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().forwardTiltHitboxSet, true);
            }
        }
        else
        {
            boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().forwardTiltHitboxSet, false);
            return true;
        }

        return false;
    }

    public bool Jab1(Animator animator)
    {
        if(moveTimer <= jab1TotalLength)
        {
            // Play Animation

            animator.Play("jab");

            if (moveTimer >= 0.05f && moveTimer <= 0.083333f)
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().jabHitboxSet, true);
            }
            else
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().jabHitboxSet, false);
            }

            // if(attackHurtBox is collding with enemy hitbox)
            //{
            //  play 2nd jab animation
            //  somehow check if 2nd hurtbox is hitting and play 3rd animation
            //}
        }
        else
        {
            return true;
        }

        return false;
    }

    public bool UpTilt1(Animator animator)
    {
        if (moveTimer <= upTilt1TotalLength)
        {
            // Play animation
            animator.Play("upTilt");

            if (moveTimer >= 0.2166667f && moveTimer <= 0.433333f)
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().upTiltHitboxSet, true);
            }
            else
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().upTiltHitboxSet, false);
            }
        }
        else
        {
            return true;
        }

        return false;
    }

    public bool DownTilt1(Animator animator)
    {
        if(moveTimer <= downTilt1TotalLength)
        {
            // Play animation
            animator.Play("downTilt");

            if (moveTimer >= 0.1f && moveTimer <= 0.15f)
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().downTiltHitboxSet, true);
            }
            else
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().downTiltHitboxSet, false);
            }
        }
        else
        {
            return true;
        }

        return false;
    }

    public bool NeutralAir1(Animator animator)
    {
        gameObject.GetComponent<Character>().allAttacksOff = false;
        if (moveTimer <= neutralAir1TotalLength)
        {
            // Play animation
            animator.Play("neutralAir");

            if (moveTimer >= 0.066666667f)
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().neutralAirHitboxSet, true);
            }
        }
        else
        {
            boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().neutralAirHitboxSet, false);
            return true;
        }

        return false;
    }

    public bool ForwardAir1(Animator animator)
    {
        if (moveTimer <= forwardAir1TotalLength)
        {
            // Play animation
            animator.Play("forwardAir");

            if (moveTimer >= 0.233333f && moveTimer <= 0.266667f)
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().forwardAirHitboxSet, true);
            }
            else
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().forwardAirHitboxSet, false);
            }
        }
        else
        {
            return true;
        }

        return false;
    }

    public bool BackAir1(Animator animator)
    {
        if (moveTimer <= backAir1TotalLength)
        {
            // Play animation
            animator.Play("backAir");

            if (moveTimer >= 0.25f && moveTimer <= 0.3f)
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().backAirHitboxSet, true);
            }
            else
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().backAirHitboxSet, false);
            }
        }
        else
        {
            return true;
        }

        return false;
    }

    public bool UpAir1(Animator animator)
    {
        if (moveTimer <= upAir1TotalLength)
        {
            // Play animation
            animator.Play("upAir");

            if (moveTimer >= 0.1666667f && moveTimer <= 0.416667f)
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().upAirHitboxSet, true);
            }
            else
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().upAirHitboxSet, false);
            }
        }
        else
        {
            return true;
        }

        return false;
    }

    public bool DownAir1(Animator animator)
    {
        if (moveTimer <= downAir1TotalLength)
        {
            // Play animation
            animator.Play("downAir");

            if (moveTimer >= 0.083333f && moveTimer <= 0.266667f)
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().downAirHitboxSet, true);
            }
            else
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().downAirHitboxSet, false);
            }
        }
        else
        {
            return true;
        }

        return false;
    }
}
