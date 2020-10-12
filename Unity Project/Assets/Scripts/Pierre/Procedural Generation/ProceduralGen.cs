using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGen : MonoBehaviour
{
    public List<GameObject> possibleRooms;
    public RoomBehavior room;
    public GameObject doorStep;
    public GenManager genManager;
    public int deltaX, deltaY;
    public bool canGenerate, generates = true;
    public Vector3 instantiateRange, doorStepRange;
    // Start is called before the first frame update
    void Start()
    {
        if (canGenerate)
        {
            room = GetComponentInParent<RoomBehavior>();
            genManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GenManager>();
            if (genManager.roomAmount <= genManager.maxRooms)
            {

                for (int i = 0; i < genManager.rooms.Count; i++)
                {
                   /* if (genManager.roomCoordinates[i] == room.roomX+deltaX && genManager.rooms[i].GetComponent<RoomBehavior>().roomY == room.roomY+deltaY)
                    {
                        canGenerate = false;
                    }*/
                }
                if (canGenerate)
                {
                    int rng = Random.Range(0, possibleRooms.Count);
                    Instantiate(possibleRooms[rng], transform.position + instantiateRange, Quaternion.identity, transform.parent.parent);
                    //genManager.rooms[genManager.rooms.Count].GetComponent<RoomBehavior>().roomX = room.roomX + deltaX;
                    //genManager.rooms[genManager.rooms.Count].GetComponent<RoomBehavior>().roomY = room.roomY + deltaY;
                    Instantiate(doorStep, transform.position + doorStepRange, Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
