using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Ground" && collision.gameObject.tag != "Platform" && collision.gameObject.tag != "Broken Wall Bits")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<PolygonCollider2D>());
        }
    }
}
