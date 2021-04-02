using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    public enum BoxSets
    {
        Null,
        PlayerHurtbox,

        PlayerNeutralAirHitbox,
        PlayerForwardAirHitbox,
        PlayerUpAirHitbox,
        PlayerBackAirHitbox,
        PlayerDownAirHitbox,

        PlayerJabHitbox,
        PlayerForwardTiltHitbox,
        PlayerUpTiltHitbox,
        PlayerDownTiltHitbox,

        PlayerARMSNeutralBHitbox,
        PlayerARMSUpBHitbox,
        PlayerARMSSideBHitbox,

        PlayerFIRENeutralBHitbox,
        PlayerFIREUpBHitbox,
        PlayerFIRESideBHitbox,

        Enemy1Hurtbox,
        Enemy2Hurtbox,
        Enemy3Hurtbox,
        Enemy4Hurtbox,
        Enemy5Hurtbox,
        Enemy6Hurtbox,
        Enemy7Hurtbox,
        Enemy8Hurtbox,

        FireBossHurtbox,

        Enemy1ForwardTiltHitbox,
        Enemy2ForwardTiltHitbox,
        Enemy3ForwardTiltHitbox,
        Enemy4ForwardTiltHitbox,
        Enemy5ForwardTiltHitbox,
        Enemy6ForwardTiltHitbox,
        Enemy7ForwardTiltHitbox,
        Enemy8ForwardTiltHitbox,

        FireBossNeutralBHitbox,
    }

    public List<GameObject> playerHurtboxSet;
    public bool playerHurtboxSetIsActive;

    public List<GameObject> enemy1HurtboxSet;
    public List<GameObject> enemy2HurtboxSet;
    public List<GameObject> enemy3HurtboxSet;
    public List<GameObject> enemy4HurtboxSet;
    public List<GameObject> enemy5HurtboxSet;
    public List<GameObject> enemy6HurtboxSet;
    public List<GameObject> enemy7HurtboxSet;
    public List<GameObject> enemy8HurtboxSet;
    public bool enemy1HurtboxSetIsActive;
    public bool enemy2HurtboxSetIsActive;
    public bool enemy3HurtboxSetIsActive;
    public bool enemy4HurtboxSetIsActive;
    public bool enemy5HurtboxSetIsActive;
    public bool enemy6HurtboxSetIsActive;
    public bool enemy7HurtboxSetIsActive;
    public bool enemy8HurtboxSetIsActive;

    public List<GameObject> fireBossHurtboxSet;
    public bool fireBossHurtboxSetIsActive;

    public List<GameObject> playerNeutralAirHitboxSet;
    public List<GameObject> playerForwardAirHitboxSet;
    public List<GameObject> playerUpAirHitboxSet;
    public List<GameObject> playerBackAirHitboxSet;
    public List<GameObject> playerDownAirHitboxSet;
    public bool playerNeutralAirHitboxSetIsActive;
    public bool playerForwardAirHitboxSetIsActive;
    public bool playerUpAirAirHitboxSetIsActive;
    public bool playerBackAirHitboxSetIsActive;
    public bool playerDownAirHitboxSetIsActive;


    public List<GameObject> playerJabHitboxSet;
    public List<GameObject> playerForwardTiltHitboxSet;
    public List<GameObject> playerUpTiltHitboxSet;
    public List<GameObject> playerDownTiltHitboxSet;
    public bool playerJabHitboxSetIsActive;
    public bool playerForwardTiltHitboxSetIsActive;
    public bool playerUpTiltHitboxSetIsActive;
    public bool playerDownTiltHitboxSetIsActive;

    public List<GameObject> playerARMSNeutralBHitboxSet;
    public List<GameObject> playerARMSSideBHitboxSet;
    public List<GameObject> playerARMSUpBHitboxSet;
    public bool playerARMSNeutralBHitboxSetIsActive;
    public bool playerARMSSideBHitboxSetIsActive;
    public bool playerARMSUpBHitboxSetIsActive;


    public List<GameObject> playerFIRENeutralBHitboxSet;
    public List<GameObject> playerFIRESideBHitboxSet;
    public List<GameObject> playerFIREUpBHitboxSet;
    public bool playerFIRENeutralBHitboxSetIsActive;
    public bool playerFIRESideBHitboxSetIsActive;
    public bool playerFIREUpBHitboxSetIsActive;

    public List<GameObject> enemy1ForwardTiltHitboxSet;
    public List<GameObject> enemy2ForwardTiltHitboxSet;
    public List<GameObject> enemy3ForwardTiltHitboxSet;
    public List<GameObject> enemy4ForwardTiltHitboxSet;
    public List<GameObject> enemy5ForwardTiltHitboxSet;
    public List<GameObject> enemy6ForwardTiltHitboxSet;
    public List<GameObject> enemy7ForwardTiltHitboxSet;
    public List<GameObject> enemy8ForwardTiltHitboxSet;
    public bool enemy1ForwardTiltHitboxSetIsActive;
    public bool enemy2ForwardTiltHitboxSetIsActive;
    public bool enemy3ForwardTiltHitboxSetIsActive;
    public bool enemy4ForwardTiltHitboxSetIsActive;
    public bool enemy5ForwardTiltHitboxSetIsActive;
    public bool enemy6ForwardTiltHitboxSetIsActive;
    public bool enemy7ForwardTiltHitboxSetIsActive;
    public bool enemy8ForwardTiltHitboxSetIsActive;

    public List<GameObject> fireBossNeutralBHitboxSet;
    public bool fireBossNeutralBHitboxSetIsActive;
    // Etc.

    public GameObject player;

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject enemy5;
    public GameObject enemy6;
    public GameObject enemy7;
    public GameObject enemy8;

    public GameObject fireBoss;

    // Start is called before the first frame update
    void Start()
    {
        GetChildrenWithScript(player.transform, "Hurtbox", playerHurtboxSet, BoxSets.PlayerHurtbox);

        GetChildrenWithScript(enemy1.transform, "Hurtbox", enemy1HurtboxSet, BoxSets.Enemy1Hurtbox);
        GetChildrenWithScript(enemy2.transform, "Hurtbox", enemy2HurtboxSet, BoxSets.Enemy2Hurtbox);
        GetChildrenWithScript(enemy3.transform, "Hurtbox", enemy3HurtboxSet, BoxSets.Enemy3Hurtbox);
        GetChildrenWithScript(enemy4.transform, "Hurtbox", enemy4HurtboxSet, BoxSets.Enemy4Hurtbox);
        GetChildrenWithScript(enemy5.transform, "Hurtbox", enemy5HurtboxSet, BoxSets.Enemy5Hurtbox);
        GetChildrenWithScript(enemy6.transform, "Hurtbox", enemy6HurtboxSet, BoxSets.Enemy6Hurtbox);
        GetChildrenWithScript(enemy7.transform, "Hurtbox", enemy7HurtboxSet, BoxSets.Enemy7Hurtbox);
        GetChildrenWithScript(enemy8.transform, "Hurtbox", enemy8HurtboxSet, BoxSets.Enemy8Hurtbox);

        SetTeamNumberOfSet(enemy1HurtboxSet, 1);
        SetTeamNumberOfSet(enemy2HurtboxSet, 1);
        SetTeamNumberOfSet(enemy3HurtboxSet, 1);
        SetTeamNumberOfSet(enemy4HurtboxSet, 1);
        SetTeamNumberOfSet(enemy5HurtboxSet, 1);
        SetTeamNumberOfSet(enemy6HurtboxSet, 1);
        SetTeamNumberOfSet(enemy7HurtboxSet, 1);
        SetTeamNumberOfSet(enemy8HurtboxSet, 1);

        ChangeSetActiveStatus(enemy1HurtboxSet, true);
        ChangeSetActiveStatus(enemy2HurtboxSet, true);
        ChangeSetActiveStatus(enemy3HurtboxSet, true);
        ChangeSetActiveStatus(enemy4HurtboxSet, true);
        ChangeSetActiveStatus(enemy5HurtboxSet, true);
        ChangeSetActiveStatus(enemy6HurtboxSet, true);
        ChangeSetActiveStatus(enemy7HurtboxSet, true);
        ChangeSetActiveStatus(enemy8HurtboxSet, true);

        GetChildrenWithScript(fireBoss.transform, "Hurtbox", fireBossHurtboxSet, BoxSets.FireBossHurtbox);
        SetTeamNumberOfSet(fireBossHurtboxSet, 1);
        ChangeSetActiveStatus(fireBossHurtboxSet, true);

        CompileHitboxSet(player.transform, playerNeutralAirHitboxSet, "NeutralAirHitbox", BoxSets.PlayerNeutralAirHitbox);
        CompileHitboxSet(player.transform, playerForwardAirHitboxSet, "ForwardAirHitbox", BoxSets.PlayerForwardAirHitbox);
        CompileHitboxSet(player.transform, playerUpAirHitboxSet, "UpAirHitbox", BoxSets.PlayerUpAirHitbox);
        CompileHitboxSet(player.transform, playerBackAirHitboxSet, "BackAirHitbox", BoxSets.PlayerBackAirHitbox);
        CompileHitboxSet(player.transform, playerDownAirHitboxSet, "DownAirHitbox", BoxSets.PlayerDownAirHitbox);
        ChangeSetActiveStatus(playerNeutralAirHitboxSet, false);
        ChangeSetActiveStatus(playerForwardAirHitboxSet, false);
        ChangeSetActiveStatus(playerUpAirHitboxSet, false);
        ChangeSetActiveStatus(playerBackAirHitboxSet, false);
        ChangeSetActiveStatus(playerDownAirHitboxSet, false);

        CompileHitboxSet(player.transform, playerJabHitboxSet, "JabHitbox", BoxSets.PlayerJabHitbox);
        CompileHitboxSet(player.transform, playerForwardTiltHitboxSet, "ForwardTiltHitbox", BoxSets.PlayerForwardTiltHitbox);
        CompileHitboxSet(player.transform, playerUpTiltHitboxSet, "UpTiltHitbox", BoxSets.PlayerUpTiltHitbox);
        CompileHitboxSet(player.transform, playerDownTiltHitboxSet, "DownTiltHitbox", BoxSets.PlayerDownTiltHitbox);
        ChangeSetActiveStatus(playerJabHitboxSet, false);
        ChangeSetActiveStatus(playerForwardTiltHitboxSet, false);
        ChangeSetActiveStatus(playerUpTiltHitboxSet, false);
        ChangeSetActiveStatus(playerDownTiltHitboxSet, false);

        CompileHitboxSet(player.transform, playerARMSNeutralBHitboxSet, "ARMSNeutralBHitbox", BoxSets.PlayerARMSNeutralBHitbox);
        CompileHitboxSet(player.transform, playerARMSSideBHitboxSet, "ARMSSideBHitbox", BoxSets.PlayerARMSSideBHitbox);
        CompileHitboxSet(player.transform, playerARMSUpBHitboxSet, "ARMSUpBHitbox", BoxSets.PlayerARMSUpBHitbox);
        ChangeSetActiveStatus(playerARMSNeutralBHitboxSet, false);
        ChangeSetActiveStatus(playerARMSSideBHitboxSet, false);
        ChangeSetActiveStatus(playerARMSUpBHitboxSet, false);

        CompileHitboxSet(player.transform, playerFIRENeutralBHitboxSet, "FIRENeutralBHitbox", BoxSets.PlayerFIRENeutralBHitbox);
        CompileHitboxSet(player.transform, playerFIRESideBHitboxSet, "FIRESideBHitbox", BoxSets.PlayerFIRESideBHitbox);
        CompileHitboxSet(player.transform, playerFIREUpBHitboxSet, "FIREUpBHitbox", BoxSets.PlayerFIREUpBHitbox);
        ChangeSetActiveStatus(playerFIRENeutralBHitboxSet, false);
        ChangeSetActiveStatus(playerFIRESideBHitboxSet, false);
        ChangeSetActiveStatus(playerFIREUpBHitboxSet, false);


        CompileHitboxSet(enemy1.transform, enemy1ForwardTiltHitboxSet, "FtiltHitbox", BoxSets.Enemy1ForwardTiltHitbox);
        CompileHitboxSet(enemy2.transform, enemy2ForwardTiltHitboxSet, "FtiltHitbox", BoxSets.Enemy2ForwardTiltHitbox);
        CompileHitboxSet(enemy3.transform, enemy3ForwardTiltHitboxSet, "FtiltHitbox", BoxSets.Enemy3ForwardTiltHitbox);
        CompileHitboxSet(enemy4.transform, enemy4ForwardTiltHitboxSet, "FtiltHitbox", BoxSets.Enemy4ForwardTiltHitbox);
        CompileHitboxSet(enemy5.transform, enemy5ForwardTiltHitboxSet, "FtiltHitbox", BoxSets.Enemy5ForwardTiltHitbox);
        CompileHitboxSet(enemy6.transform, enemy6ForwardTiltHitboxSet, "FtiltHitbox", BoxSets.Enemy6ForwardTiltHitbox);
        CompileHitboxSet(enemy7.transform, enemy7ForwardTiltHitboxSet, "FtiltHitbox", BoxSets.Enemy7ForwardTiltHitbox);
        CompileHitboxSet(enemy8.transform, enemy8ForwardTiltHitboxSet, "FtiltHitbox", BoxSets.Enemy8ForwardTiltHitbox);
        ChangeSetActiveStatus(enemy1ForwardTiltHitboxSet, false);
        ChangeSetActiveStatus(enemy2ForwardTiltHitboxSet, false);
        ChangeSetActiveStatus(enemy3ForwardTiltHitboxSet, false);
        ChangeSetActiveStatus(enemy4ForwardTiltHitboxSet, false);
        ChangeSetActiveStatus(enemy5ForwardTiltHitboxSet, false);
        ChangeSetActiveStatus(enemy6ForwardTiltHitboxSet, false);
        ChangeSetActiveStatus(enemy7ForwardTiltHitboxSet, false);
        ChangeSetActiveStatus(enemy8ForwardTiltHitboxSet, false);

        SetTeamNumberOfSet(enemy1ForwardTiltHitboxSet, 1);
        SetTeamNumberOfSet(enemy2ForwardTiltHitboxSet, 1);
        SetTeamNumberOfSet(enemy3ForwardTiltHitboxSet, 1);
        SetTeamNumberOfSet(enemy4ForwardTiltHitboxSet, 1);
        SetTeamNumberOfSet(enemy5ForwardTiltHitboxSet, 1);
        SetTeamNumberOfSet(enemy6ForwardTiltHitboxSet, 1);
        SetTeamNumberOfSet(enemy7ForwardTiltHitboxSet, 1);
        SetTeamNumberOfSet(enemy8ForwardTiltHitboxSet, 1);

        CompileHitboxSet(fireBoss.transform, fireBossNeutralBHitboxSet, "FIRENeutralBHitbox", BoxSets.FireBossNeutralBHitbox);
        ChangeSetActiveStatus(fireBossNeutralBHitboxSet, false);


        SetTeamNumberOfSet(fireBossNeutralBHitboxSet, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GetChildrenWithScript(Transform parent, string type, List<GameObject> list, BoxSets setName)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.GetComponent(type) != null)
            {
                child.gameObject.GetComponent<Box>().ParentSet = setName;
                list.Add(child.gameObject);
            }
            GetChildrenWithScript(child, type, list, setName);
        }
    }

    private void CompileHitboxSet(Transform parent, List<GameObject> list, string identifier, BoxSets setName)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.name.Contains(identifier))//child.gameObject.name.Substring(0,child.gameObject.name.IndexOf(delimenator)) == identifier
            {
                child.gameObject.GetComponent<Hitbox>().ParentSet = setName;
                list.Add(child.gameObject);
            }
            CompileHitboxSet(child, list, identifier, setName);
        }
    }

    public void ChangeSetActiveStatus(List<GameObject> list, bool activeStatus)
    {
        if (list.Count != 0 && list[0].GetComponent<Box>().isActive == !activeStatus)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].GetComponent<Box>().isActive = activeStatus;
            }
        }
    }

    public void SetTeamNumberOfSet(List<GameObject> list, int teamNumber)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].GetComponent<Box>().TeamNumber = teamNumber;
        }
    }

    // DELETE IF ABOVE METHOD WORKS WELL
    //public void MakeSetActive(List<GameObject> list)
    //{
    //    if(list.Count != 0 && list[0].GetComponent<Box>().isActive == false)
    //    {
    //        for (int i = 0; i < list.Count; i++)
    //        {
    //            list[i].GetComponent<Box>().isActive = true;
    //        }
    //    }
    //}
    //
    //public void MakeSetInActive(List<GameObject> list)
    //{
    //    if (list.Count != 0 && list[0].GetComponent<Box>().isActive == true)
    //    {
    //        for (int i = 0; i < list.Count; i++)
    //        {
    //            list[i].GetComponent<Box>().isActive = false;
    //        }
    //    }
    //}

}
