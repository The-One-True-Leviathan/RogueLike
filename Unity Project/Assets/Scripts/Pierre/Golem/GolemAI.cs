using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAI : MonoBehaviour
{

    GameObject player;
    
    //Line of Sight
    public float maxSight; //How far can the enemy see ?
    public LayerMask blockLOS;
    public LayerMask isPlayer;
    Vector3 golemToPlayer;
    public float distanceToPlayer, angleToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            golemToPlayer = player.transform.position - transform.position;
            distanceToPlayer = golemToPlayer.magnitude;
        
            Sightcast();
        }
    }

    void Sightcast() 
    {
        bool hit = Physics.Raycast(transform.position, golemToPlayer, maxSight, isPlayer);
        if (hit)
        {
            print("Player in sight !");
        }


        Debug.DrawRay(transform.position, golemToPlayer * maxSight, Color.red);
    }
}
