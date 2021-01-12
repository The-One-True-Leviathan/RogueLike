using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOrientationBehavior : MonoBehaviour
{
    public Player player;
    public Vector3 orientation;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isInHitSpan)
        {
            orientation = player.attackDirection;
        } else
        {
            orientation = player.attackDirection;
        }

        transform.rotation = Quaternion.LookRotation(orientation, Vector3.up);
    }
}
