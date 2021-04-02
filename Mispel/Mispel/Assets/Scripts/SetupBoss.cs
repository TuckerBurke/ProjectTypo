using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupBoss : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private string bossScriptName;
    [SerializeField] private GameObject bossDoor;
    [SerializeField] private float startingY;
    [SerializeField] private float endingY;
    [SerializeField] private float speed;

    [SerializeField] private float cameraSize;
    [SerializeField] private Vector2 cameraLocation;

    private bool hasTriggered;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        bossDoor.transform.position = new Vector3(bossDoor.transform.position.x, startingY, bossDoor.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        // If the player has left the zone and entered the boss arena
        if(hasTriggered)
        {
            // If the boss door is closing
            if(bossDoor.transform.position.y != endingY)
            {
                // Disable the players control, change them to standing, and change the camera mode
                player.canBeControlled = false;
                player.ChangeToStanding();
                player.normalCameraMode = false;
            }
            else
            {
                // When the door closes, renable the players movement
                player.canBeControlled = true;
                boss.GetComponent<IBoss>().ActivateBoss();
                GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayDoorClose();
            }

            // If the boss door has not reached the end
            if(bossDoor.transform.position.y > endingY)
            {
                // Move the boss door towards where it should be
                bossDoor.transform.position = new Vector3(bossDoor.transform.position.x, bossDoor.transform.position.y - speed * Time.deltaTime, bossDoor.transform.position.z);
            }
            else
            {
                // Set the doors position once it reaches it's closed position
                bossDoor.transform.position = new Vector3(bossDoor.transform.position.x,endingY, bossDoor.transform.position.z);
            }

            // Move the camera to the center of the boss arena and zoom it out a bit
            Vector3 cameraPosition = Camera.main.transform.position;
            float interpolation = player.cameraSpeed/2.8f * Time.deltaTime;
            Camera.main.orthographicSize = cameraSize;
            cameraPosition.x = Mathf.Lerp(Camera.main.transform.position.x, cameraLocation.x, interpolation);
            cameraPosition.y = Mathf.Lerp(Camera.main.transform.position.y, cameraLocation.y, interpolation);
            Camera.main.transform.position = cameraPosition;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.root.name == "Player")
        {
            if(collision.transform.root.transform.position.x > GetComponent<BoxCollider2D>().bounds.center.x)
            {
                hasTriggered = true;
                player = collision.transform.root.GetComponent<Player>();
            }
        }
    }
}
