using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm_Form_Attacks : MonoBehaviour
{
    private BoxManager boxManager;

    private float upSpecialTotalLength;
    private float sideSpecialTotalLength;
    private float neutralSpecialTotalLength;
    private float downSpecialTotalLength;

    private float moveTimer;

    // Start is called before the first frame update
    void Start()
    {
        upSpecialTotalLength = 0.75f;
        sideSpecialTotalLength = 0.55f;
        neutralSpecialTotalLength = 1.75f;
        downSpecialTotalLength = 0.55f;

        moveTimer = 0.0f;
        boxManager = GameObject.Find("GameManager").GetComponent<BoxManager>();
    }

    // Update is called once per frame
    void Update()
    {
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

    public bool UpSpecial(Animator animator)
    {
        // Timer count down
        // When done change state

        if (moveTimer <= upSpecialTotalLength)
        {
            // Play animation
            animator.Play("ARMSupB");

            if (moveTimer >= 0.1f)
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().armsUpBHitboxSet, true);
            }
        }
        else
        {
            boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().armsUpBHitboxSet, false);
            return true;
        }

        return false;
    }

    public bool SideSpecial(Animator animator)
    {
        if (moveTimer <= sideSpecialTotalLength)
        {
            // Play Animation
            animator.Play("ARMSsideB");

            if (moveTimer >= 0.3166667f)
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().armsSideBHitboxSet, true);
            }
        }
        else
        {
            boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().armsSideBHitboxSet, false);
            return true;
        }

        return false;
    }

    public bool NeutralSpecial(Animator animator)
    {
        if (moveTimer <= neutralSpecialTotalLength)
        {
            // Play animation
            animator.Play("ARMSneutralB");

            if (moveTimer >= 1.083333f && moveTimer <= 1.11666667f)
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().armsNeutralBHitboxSet, true);
            }
            else
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().armsNeutralBHitboxSet, false);
            }
        }
        else
        {
            return true;
        }

        return false;
    }

    public bool DownSpecial(Animator animator)
    {
        if (moveTimer <= downSpecialTotalLength)
        {
            // Play animation
            animator.Play("ARMSdownB");

            if (moveTimer >= 1.083333f && moveTimer <= 1.11666667f)
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().armsNeutralBHitboxSet, true);
            }
            else
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().armsNeutralBHitboxSet, false);
            }
        }
        else
        {
            return true;
        }

        return false;
    }
}
