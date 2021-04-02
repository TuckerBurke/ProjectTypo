using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : Box
{
    public List<List<Box>> setsHit;
    [SerializeField] private float damage;
    [SerializeField] private Vector3 knockback; // Direction and magnitude (Chuck Vector)
    [SerializeField] private BoxManager.BoxSets collidedSet;
    [SerializeField] private float hitStunLength;

    public Vector3 Knockback
    {
        get { return knockback; }
    }
    public float Damage
    {
        get { return damage; }
    }
    public float HitStunLength
    {
        get { return hitStunLength; }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        isHitbox = true;

        base.Start();
        collidedSet = BoxManager.BoxSets.Null;
    }

    // Update is called once per frame
    protected override void Update()
    {
        UpdatePosition();

        if(isActive == false)
        {
            collidedSet = BoxManager.BoxSets.Null;
        }

        if(transform.root.name.Contains("Basic Enemy"))
        {
            if (transform.root.GetChild(0).GetComponent<Character>().facingRight)
            {
                knockback.x = Mathf.Abs(knockback.x);
            }
            else
            {
                knockback.x = -Mathf.Abs(knockback.x);
            }
        }
        else
        {
            if (transform.root.GetComponent<Character>().facingRight)
            {
                knockback.x = Mathf.Abs(knockback.x);
            }
            else
            {
                knockback.x = -Mathf.Abs(knockback.x);
            }
        }

    }

    protected override void UpdatePosition()
    {
        // Get parent bone
        // Move there
        // Offset some amount

        //transform.position = parentBone.transform.position;

        //vSphere.transform.position = transform.position;

        Draw();
    }

    protected override void Draw()
    {
        //if (isActive)
        //    vSphere.SetActive(true);
        //else
        //    vSphere.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive)
        {
            Hurtbox hurtbox = collision.gameObject.GetComponent<Hurtbox>();

            if (hurtbox != null)
            {
                if (hurtbox.TeamNumber != teamNumber)
                {
                    if (collidedSet != hurtbox.ParentSet)
                    {
                        if (hurtbox.transform.root.name.Contains("Basic Enemy"))
                        {
                            hurtbox.transform.root.GetChild(0).GetComponent<Character>().TakeDamage(this);
                        }
                        else
                        {
                            
                            hurtbox.transform.root.GetComponent<Character>().TakeDamage(this);
                        }
                        collidedSet = hurtbox.ParentSet;

                        // Knock the camera a bit in the direction of the knockback for JUICE
                        Camera.main.transform.Translate(0.1f * Vector3.Normalize(knockback));
                    }
                }
            }
        }
        
    }
}
