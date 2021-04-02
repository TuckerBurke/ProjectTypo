using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Box : MonoBehaviour
{
    [SerializeField] protected float radius;
    [SerializeField] protected int teamNumber;
    [SerializeField] protected BoxManager.BoxSets parentSet;
    public bool isActive;
    public GameObject parentBone;
    protected GameObject vSphere;
    protected bool isHitbox;

    public int TeamNumber
    {
        get { return teamNumber; }
        set { teamNumber = value; }
    }
    public BoxManager.BoxSets ParentSet
    {
        get { return parentSet; }
        set { parentSet = value; }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        isActive = true;

        if (isHitbox)
        {
            parentBone = gameObject.transform.parent.gameObject;
        }
        else
        {
            parentBone = gameObject;
        }

        //vSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //vSphere.transform.localScale = new Vector3(radius * 2, radius * 2, 1);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    abstract protected void UpdatePosition();

    abstract protected void Draw();
}
