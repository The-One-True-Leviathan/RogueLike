using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehavior : MonoBehaviour
{
    public int roomID;
    public Vector2 roomCoo;
    public GenManager genManager;
    public List<GameObject> spawners;
    bool isStartingRoom;
    // Start is called before the first frame update
    void Start()
    {
        genManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GenManager>();
        if (isStartingRoom)
        {
            genManager.roomCoordinates.Add(Vector3.zero);
        }
        genManager.rooms.Add(gameObject);
        roomID = genManager.roomAmount;
        genManager.roomAmount += 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
