using ProcGen;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProcGen
{

    public class RoomBehavior : MonoBehaviour
    {
        public int roomID, roomX, roomY, sizeDungeonX, sizeDungeonY, roomCount;
        public GenManager genManager;
        public bool connectUp, connectDown, connectLeft, connectRight;
        public List<RoomBehavior> connectList, refuseList;
        [Header("Chances to Connect")]
        float corridorToRoom = 0.33f, roomToRoom = 1;
        public bool isStartingRoom;
        public RoomType roomType;
        // Start is called before the first frame update
        void Awake()
        {
            genManager = GameObject.FindGameObjectWithTag("Genmanager").GetComponent<GenManager>();
            if (isStartingRoom)
            {
                //genManager.roomCoordinates.Add(Vector3.zero);
            }
            sizeDungeonX = genManager.sizeX;
            sizeDungeonY = genManager.sizeY;
        }

        public void Connect()
        {
            if (roomType != RoomType.None)
            {
                switch (roomType)
                {
                    case RoomType.Start:
                        StartConnect();
                        break;
                    case RoomType.CorridorRoom:
                        CorridorConnect();
                        break;
                    case RoomType.End:
                        break;
                    case RoomType.Room:
                        RoomConnect();
                        break;
                    case RoomType.Bonus:
                        break;
                }
            }
        }

        void StartConnect()
        {
            RoomBehavior nextRoom;
         //   Debug.LogError(genManager.allRoomsInDungeon.Count);
            for (int i = 0; i < genManager.allRoomsInDungeon.Count; i++)
            {
             //       Debug.LogError("tried connecting");
                if (genManager.allRoomsInDungeon[i].GetComponent<RoomBehavior>())
                {
                    nextRoom = genManager.allRoomsInDungeon[i].GetComponent<RoomBehavior>();
                    if (nextRoom.roomType != RoomType.None & nextRoom.roomType != RoomType.End)
                    {
                        if (nextRoom.roomX == roomX)
                        {
                            if (nextRoom.roomY == roomY - 1)
                            {
                                nextRoom.connectUp = true;
                                connectDown = true;
                            }
                            if (nextRoom.roomY == roomY + 1)
                            {
                                nextRoom.connectDown = true;
                                connectUp = true;
                            }
                        }
                        if (nextRoom.roomY == roomY)
                        {
                            if (nextRoom.roomX == roomX - 1)
                            {
                                nextRoom.connectRight = true;
                                connectLeft = true;
                            }
                            if (nextRoom.roomX == roomX + 1)
                            {
                                nextRoom.connectLeft = true;
                                connectRight = true;
                            }
                        }
                    }
                }
            }
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(transform.position.x, transform.position.y+0.4f, transform.position.z);
            }
        }

        void CorridorConnect()
        {
            RoomBehavior nextRoom;
            for (int i = 0; i < genManager.allRoomsInDungeon.Count; i++)
            {
                if (genManager.allRoomsInDungeon[i].GetComponent<RoomBehavior>())
                {
                    nextRoom = genManager.allRoomsInDungeon[i].GetComponent<RoomBehavior>();
                    if (nextRoom.roomType != RoomType.None)
                    {
                        if (!refuseList.Contains<RoomBehavior>(nextRoom) & !connectList.Contains<RoomBehavior>(nextRoom))
                        {
                            if (nextRoom.roomX == roomX)
                            {
                                if (nextRoom.roomY == roomY - 1)
                                {
                                    switch (nextRoom.roomType)
                                    {
                                        case RoomType.CorridorRoom:
                                            if (nextRoom.roomID == roomID + 1)
                                            {
                                                nextRoom.connectUp = true;
                                                connectDown = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            break;
                                        case RoomType.End:
                                            if (nextRoom.roomID == roomID + 1)
                                            {
                                                nextRoom.connectUp = true;
                                                connectDown = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            break;
                                        case RoomType.Room:
                                            if (UnityEngine.Random.value < corridorToRoom)
                                            {
                                                nextRoom.connectUp = true;
                                                connectDown = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            else
                                            {
                                                refuseList.Add(nextRoom);
                                                nextRoom.refuseList.Add(this);
                                            }
                                            break;
                                        case RoomType.Bonus:
                                            if (UnityEngine.Random.value < corridorToRoom)
                                            {
                                                nextRoom.connectUp = true;
                                                connectDown = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            else
                                            {
                                                refuseList.Add(nextRoom);
                                                nextRoom.refuseList.Add(this);
                                            }
                                            break;

                                    }
                                }
                                if (nextRoom.roomY == roomY + 1)
                                {
                                    switch (nextRoom.roomType)
                                    {
                                        case RoomType.CorridorRoom:
                                            if (nextRoom.roomID == roomID + 1)
                                            {
                                                nextRoom.connectDown = true;
                                                connectUp = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            break;
                                        case RoomType.End:
                                            if (nextRoom.roomID == roomID + 1)
                                            {
                                                nextRoom.connectDown = true;
                                                connectUp = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            break;
                                        case RoomType.Room:
                                            if (UnityEngine.Random.value < corridorToRoom)
                                            {
                                                nextRoom.connectDown = true;
                                                connectUp = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            else
                                            {
                                                refuseList.Add(nextRoom);
                                                nextRoom.refuseList.Add(this);
                                            }
                                            break;
                                        case RoomType.Bonus:
                                            if (UnityEngine.Random.value < corridorToRoom)
                                            {
                                                nextRoom.connectDown = true;
                                                connectUp = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            else
                                            {
                                                refuseList.Add(nextRoom);
                                                nextRoom.refuseList.Add(this);
                                            }
                                            break;

                                    }
                                }
                            }
                            if (nextRoom.roomY == roomY)
                            {
                                if (nextRoom.roomX == roomX - 1)
                                {
                                    switch (nextRoom.roomType)
                                    {
                                        case RoomType.CorridorRoom:
                                            if (nextRoom.roomID == roomID + 1)
                                            {
                                                nextRoom.connectRight = true;
                                                connectLeft = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            break;
                                        case RoomType.End:
                                            if (nextRoom.roomID == roomID + 1)
                                            {
                                                nextRoom.connectRight = true;
                                                connectLeft = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            break;
                                        case RoomType.Room:
                                            if (UnityEngine.Random.value < corridorToRoom)
                                            {
                                                nextRoom.connectRight = true;
                                                connectLeft = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            else
                                            {
                                                refuseList.Add(nextRoom);
                                                nextRoom.refuseList.Add(this);
                                            }
                                            break;
                                        case RoomType.Bonus:
                                            if (UnityEngine.Random.value < corridorToRoom)
                                            {
                                                nextRoom.connectRight = true;
                                                connectLeft = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            else
                                            {
                                                refuseList.Add(nextRoom);
                                                nextRoom.refuseList.Add(this);
                                            }
                                            break;

                                    }
                                }
                                if (nextRoom.roomX == roomX + 1)
                                {
                                    switch (nextRoom.roomType)
                                    {
                                        case RoomType.CorridorRoom:
                                            if (nextRoom.roomID == roomID + 1)
                                            {
                                                nextRoom.connectLeft = true;
                                                connectRight = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            break;
                                        case RoomType.End:
                                            if (nextRoom.roomID == roomID + 1)
                                            {
                                                nextRoom.connectLeft = true;
                                                connectRight = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            break;
                                        case RoomType.Room:
                                            if (UnityEngine.Random.value < corridorToRoom)
                                            {
                                                nextRoom.connectLeft = true;
                                                connectRight = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            else
                                            {
                                                refuseList.Add(nextRoom);
                                                nextRoom.refuseList.Add(this);
                                            }
                                            break;
                                        case RoomType.Bonus:
                                            if (UnityEngine.Random.value < corridorToRoom)
                                            {
                                                nextRoom.connectLeft = true;
                                                connectRight = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            else
                                            {
                                                refuseList.Add(nextRoom);
                                                nextRoom.refuseList.Add(this);
                                            }
                                            break;

                                    }
                                }
                            }
                        }

                    }
                }
            }
        }

        void RoomConnect()
        {
            RoomBehavior nextRoom;
            for (int i = 0; i < genManager.allRoomsInDungeon.Count; i++)
            {
                if (genManager.allRoomsInDungeon[i].GetComponent<RoomBehavior>())
                {
                    nextRoom = genManager.allRoomsInDungeon[i].GetComponent<RoomBehavior>();
                    if (nextRoom.roomType != RoomType.None & nextRoom.roomType != RoomType.End)
                    {
                        if (!refuseList.Contains<RoomBehavior>(nextRoom) & !connectList.Contains<RoomBehavior>(nextRoom))
                        {
                            if (nextRoom.roomX == roomX)
                            {
                                if (nextRoom.roomY == roomY - 1)
                                {
                                    switch (nextRoom.roomType)
                                    {
                                        case RoomType.CorridorRoom:
                                            if (UnityEngine.Random.value < corridorToRoom)
                                            {
                                                nextRoom.connectUp = true;
                                                connectDown = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            break;
                                        case RoomType.Room:
                                            if (nextRoom.roomID == roomID + 1 || nextRoom.roomID == roomID - 1)
                                            {
                                                nextRoom.connectUp = true;
                                                connectDown = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            else if (UnityEngine.Random.value < roomToRoom)
                                            {
                                                nextRoom.connectUp = true;
                                                connectDown = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            else
                                            {
                                                refuseList.Add(nextRoom);
                                                nextRoom.refuseList.Add(this);
                                            }
                                            break;
                                        case RoomType.Bonus:
                                            nextRoom.connectUp = true;
                                            connectDown = true;
                                            connectList.Add(nextRoom);
                                            nextRoom.connectList.Add(this);
                                            break;

                                    }
                                }
                                if (nextRoom.roomY == roomY + 1)
                                {
                                    switch (nextRoom.roomType)
                                    {
                                        case RoomType.CorridorRoom:
                                            if (UnityEngine.Random.value < corridorToRoom)
                                            {
                                                nextRoom.connectDown = true;
                                                connectUp = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            break;
                                        case RoomType.Room:
                                            if (nextRoom.roomID == roomID + 1 || nextRoom.roomID == roomID - 1)
                                            {
                                                nextRoom.connectDown = true;
                                                connectUp = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            else if (UnityEngine.Random.value < roomToRoom)
                                            {
                                                nextRoom.connectDown = true;
                                                connectUp = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            else
                                            {
                                                refuseList.Add(nextRoom);
                                                nextRoom.refuseList.Add(this);
                                            }
                                            break;
                                        case RoomType.Bonus:
                                            nextRoom.connectDown = true;
                                            connectUp = true;
                                            connectList.Add(nextRoom);
                                            nextRoom.connectList.Add(this);
                                            break;

                                    }
                                }
                            }
                            if (nextRoom.roomY == roomY)
                            {
                                if (nextRoom.roomX == roomX - 1)
                                {
                                    switch (nextRoom.roomType)
                                    {
                                        case RoomType.CorridorRoom:
                                            if (UnityEngine.Random.value < corridorToRoom)
                                            {
                                                nextRoom.connectRight = true;
                                                connectLeft = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            break;
                                        case RoomType.Room:
                                            if (nextRoom.roomID == roomID + 1 || nextRoom.roomID == roomID - 1)
                                            {
                                                nextRoom.connectRight = true;
                                                connectLeft = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            else if (UnityEngine.Random.value < roomToRoom)
                                            {
                                                nextRoom.connectRight = true;
                                                connectLeft = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            else
                                            {
                                                refuseList.Add(nextRoom);
                                                nextRoom.refuseList.Add(this);
                                            }
                                            break;
                                        case RoomType.Bonus:
                                            nextRoom.connectRight = true;
                                            connectLeft = true;
                                            connectList.Add(nextRoom);
                                            nextRoom.connectList.Add(this);
                                            break;

                                    }
                                }
                                if (nextRoom.roomX == roomX + 1)
                                {
                                    switch (nextRoom.roomType)
                                    {
                                        case RoomType.CorridorRoom:
                                            if (UnityEngine.Random.value < corridorToRoom)
                                            {
                                                nextRoom.connectLeft = true;
                                                connectRight = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            break;
                                        case RoomType.Room:
                                            if (nextRoom.roomID == roomID + 1 || nextRoom.roomID == roomID - 1)
                                            {
                                                nextRoom.connectLeft = true;
                                                connectRight = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            else if (UnityEngine.Random.value < roomToRoom)
                                            {
                                                nextRoom.connectLeft = true;
                                                connectRight = true;
                                                connectList.Add(nextRoom);
                                                nextRoom.connectList.Add(this);
                                            }
                                            else
                                            {
                                                refuseList.Add(nextRoom);
                                                nextRoom.refuseList.Add(this);
                                            }
                                            break;
                                        case RoomType.Bonus:
                                            nextRoom.connectLeft = true;
                                            connectRight = true;
                                            connectList.Add(nextRoom);
                                            nextRoom.connectList.Add(this);
                                            break;

                                    }
                                }
                            }
                        }

                    }
                }
            }

        }
    }
}
