using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : Box
{
    public bool invulnerable;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        UpdatePosition();
    }

    protected override void UpdatePosition()
    {
        // Get parent bone
        // Move there

        //vSphere.transform.position = transform.position;
    }

    protected override void Draw()
    {

    }
}
