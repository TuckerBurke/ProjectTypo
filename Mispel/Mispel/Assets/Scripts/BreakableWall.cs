using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] private GameObject brokenVersion;

    [SerializeField] private List<AttackIDs.AttackID> validMoves;

    private bool hasBeenBroken;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.root.name == "Player")
        {
            if(validMoves.Contains(collision.transform.root.gameObject.GetComponent<Player>().CurrentAttackID))
            {
                if (hasBeenBroken == false)
                {
                    Break();
                    hasBeenBroken = true;
                }
            }
        }
    }
    private void Break()
    {
        brokenVersion = Instantiate(brokenVersion);
        brokenVersion.transform.localScale = transform.localScale;
        brokenVersion.transform.position = transform.position;
        brokenVersion.transform.rotation = transform.rotation;

        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayWallCrumble();

        Destroy(gameObject);
    }
}
