using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Maybe have different lists for different areas of the map?
    [SerializeField] private List<GameObject> firstAreaEnemies;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnEnemies(List<GameObject> enemiesToRespawn)
    {
        for(int i = 0; i < enemiesToRespawn.Count; i ++)
        {
            enemiesToRespawn[i].GetComponent<BasicEnemy>().shouldRespawn = true;
        }
    }
}
