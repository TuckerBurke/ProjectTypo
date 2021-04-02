using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBoss : Character, IBoss
{

    [SerializeField] private GameObject bossHealthBar;
    [SerializeField] private GameObject bossArmorBar;

    private bool isEnabled;
    private bool hasBeenKilled;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        bossHealthBar.SetActive(false);
        fireFormAttacks = GetComponent<Fire_Form_Attacks>();

        availableForms.Add(Forms.Fire);

        BoxManager boxManager = GameObject.Find("GameManager").GetComponent<BoxManager>();

        fireNeutralBHitboxSet = boxManager.fireBossNeutralBHitboxSet;
        //fireSideBHitboxSet = boxManager.playerFIRESideBHitboxSet;
        //fireUpBHitboxSet = boxManager.playerFIREUpBHitboxSet;
    }

    protected override void Initialize()
    {
        base.Initialize();

        armor = 0.0f;
        maxArmor = 0.0f;
        timeTillArmorRegen = 3.0f;
        health = 300;
        maxHealth = 300;

        //horizontalSpeed = movementSpeed;
        currentState = CharacterStates.Falling;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (isEnabled)
        {

            // Set the health bar fill amount to how much health the player has.
            bossHealthBar.transform.Find("Bar").GetComponent<Image>().fillAmount = health * (1.0f / maxHealth);
            bossArmorBar.transform.Find("Bar").GetComponent<Image>().fillAmount = armor * (1.0f / maxArmor);

        }

        switch(currentState)
        {
            case CharacterStates.Death:

                if(hasBeenKilled == false)
                {
                    hasBeenKilled = true;
                    if(GameObject.Find("Player").GetComponent<Player>().availableForms[0] == Forms.Base)
                    {
                        GameObject.Find("Player").GetComponent<Player>().availableForms.Clear();
                    }
                    GameObject.Find("Player").GetComponent<Player>().availableForms.Add(Forms.Fire);
                    GameObject.Find("NPC Man").GetComponent<AINPC>().bossDefeated = true;
                    GameObject.Find("Form Swap Tutorial Zone").transform.position = new Vector3(154.6f, -17.93f, -1f);
                    GameObject.Find("Player").GetComponent<Player>().normalCameraMode = true;
                    GameObject.Find("Fire Boss Door").SetActive(false);
                    GameObject.Find("Boss Room Trigger").SetActive(false);
                    availableForms.Clear();
                    availableForms.Add(Forms.Base);
                    GameObject.Find("BossHealthBar").SetActive(false);
                    GameObject.Find("BossArmorBar").SetActive(false);
                    GameObject.Find("Player").GetComponent<Player>().CycleForms();
                }
                break;
        }

    }

    public void ActivateBoss()
    {
        isEnabled = true;
        bossHealthBar.SetActive(true);
        bossArmorBar.SetActive(true);
    }
}
