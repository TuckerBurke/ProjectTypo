using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPointSetter : MonoBehaviour
{
    private BoxCollider2D triggerZone;

    // Start is called before the first frame update
    void Start()
    {
        triggerZone = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.transform.root.name == "Player")
        {
            Player player = collider.transform.root.gameObject.GetComponent<Player>();
            player.respawnPoint = new Vector3(triggerZone.bounds.min.x,triggerZone.bounds.min.y + player.gameObject.GetComponent<BoxCollider2D>().size.y/2, player.transform.position.z);
        }
    }
}
