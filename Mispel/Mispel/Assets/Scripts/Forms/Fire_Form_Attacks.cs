using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Form_Attacks : MonoBehaviour
{
    private BoxManager boxManager;

    private float upSpecialTotalLength;
    private float sideSpecialTotalLength;
    private float neutralSpecialTotalLength;
    private float downSpecialTotalLength;

    private float moveTimer;

    private GameObject fireCircle;

    // Start is called before the first frame update
    void Start()
    {
        upSpecialTotalLength = 0.55f;
        sideSpecialTotalLength = 0.55f;
        neutralSpecialTotalLength = 1f;
        downSpecialTotalLength = 1f;

        moveTimer = 0.0f;
        boxManager = GameObject.Find("GameManager").GetComponent<BoxManager>();

        fireCircle = Instantiate(GameObject.Find("FireCircle"));
        
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

        if(gameObject.GetComponent<Character>().CurrentAttackID != AttackIDs.AttackID.FireUpSpecial)
        {
            fireCircle.SetActive(false);
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
            animator.Play("FIREupB");

            if (moveTimer >= 0.15f)
            {
                fireCircle.transform.position = animator.transform.root.position + new Vector3(0,1f,0);
                fireCircle.SetActive(true);
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().fireUpBHitboxSet, true);
            }
        }
        else
        {
            boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().fireUpBHitboxSet, false);
            return true;
        }

        return false;
    }

    public bool SideSpecial(Animator animator)
    {
        if (moveTimer <= sideSpecialTotalLength)
        {
            // Play Animation
            animator.Play("FIREsideB");

            if (moveTimer >= 0.15f)
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().fireSideBHitboxSet, true);
            }
        }
        else
        {
            boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().fireSideBHitboxSet, true);
            return true;
        }

        return false;
    }

    public bool NeutralSpecial(Animator animator)
    {
        if (moveTimer <= neutralSpecialTotalLength)
        {
            // Play animation
            animator.Play("FIREdownB");

            if (moveTimer >= 0.33333f && moveTimer <= 0.366667f)
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().fireNeutralBHitboxSet, true);
            }
            else
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().fireNeutralBHitboxSet, false);
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
            animator.Play("FIREdownB");

            if (moveTimer >= 0.33333f && moveTimer <= 0.366667f)
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().fireNeutralBHitboxSet, true);
            }
            else
            {
                boxManager.ChangeSetActiveStatus(gameObject.GetComponent<Character>().fireNeutralBHitboxSet, false);
            }
        }
        else
        {
            return true;
        }

        return false;
    }
}
