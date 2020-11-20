using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    public float radius = 0.05f;
    public LayerMask blocksPlayer;
    public bool isCollision;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] collision = Physics.OverlapSphere(transform.position, radius, blocksPlayer);
        isCollision = false;
        foreach (Collider col in collision)
        {
            isCollision = true;
        }
    }
}
