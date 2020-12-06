using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProcGen
{
    public enum RoomType { None, Room, CorridorRoom, Start, End, Bonus, Weird }
    public class GenManager : MonoBehaviour
    {
        public int roomAmount = 0, maxRooms;
        public List<GameObject> rooms, corridorRooms, ends, starts, none, bonus, weird, allRoomsInDungeon, unsureRooms, verifiedRooms;
        List<int> allRoomsX = new List<int>();
        List<int> allRoomsY = new List<int>();
        public RoomType[,] roomArray;
        public int[,] roomIDs;
        public int sizeX, sizeY, marginX, marginY, startX, startY;
        public float resolutionX, resolutionY;
        [Header("Main Corridor")]
        public int minMainCorridorLength;
        public int maxMainCorridorLength;
        public float chanceMainCorridorSnakes;
        Walker corridorWalker;
        [Header("Secondary Corridors")]
        public float chanceToCreateNewCorridor;
        public int minSecCorridorLength, maxSecCorridorLength;
        public float chanceSecCorridorSnakes, chanceSecCorridorDestroy, chanceSecCorridorCreates;
        [Header("Furnishing")]
        public float chanceToAddBonus;
        public int minimumBonuses;
        // Start is called before the first frame update
        void Start()
        {
            Initialize();
            DesignDungeon();

        }

        public void Initialize()
        {
            roomArray = new RoomType[sizeX, sizeY];
            roomIDs = new int[sizeX, sizeY];
            for (int x = 0; x < sizeX - 1; x++)
            {
                for (int y = 0; y < sizeY - 1; y++)
                {
                    roomArray[x, y] = RoomType.None;
                }
            }
        }

        public void DesignDungeon()
        {
            StartRoom();
            MainCorridor();
            BranchOut();
            Furnish();
            BuildDungeon();
            Connect();
        }

        public void StartRoom()
        {
            int maxX, minX, maxY, minY;
            maxX = sizeX - marginX;
            minX = marginX;
            maxY = sizeY - marginY;
            minY = marginY;

            startX = UnityEngine.Random.Range(minX, maxX);
            startY = UnityEngine.Random.Range(minY, maxY);

            roomArray[startX, startY] = RoomType.Start;
            roomIDs[startX, startY] = roomAmount;
            roomAmount++;
        }

        public void MainCorridor()
        {
            bool vert;
            int speed;
            vert = UnityEngine.Random.value < 0.5f ? true : false;
            speed = UnityEngine.Random.value < 0.5f ? -1 : 1;
            corridorWalker = new Walker(startX, startY, speed, vert, 0, 0, chanceMainCorridorSnakes, minMainCorridorLength, maxMainCorridorLength, 0);
            corridorWalker.CorridorWalk(ref roomArray, ref roomIDs, ref roomAmount);

        }

        public void BranchOut()
        {
            for (int x = 0; x < roomArray.GetLength(0); x++)
            {
                for (int y = 0; y < roomArray.GetLength(1); y++)
                {
                    if (roomArray[x, y] == RoomType.CorridorRoom || roomArray[x, y] == RoomType.Start)
                    {
                        if (UnityEngine.Random.value < chanceToCreateNewCorridor)
                        {
                            bool vert;
                            int speed;
                            vert = UnityEngine.Random.value < 0.5f ? true : false;
                            speed = UnityEngine.Random.value < 0.5f ? -1 : 1;
                            Walker secondaryWalker = new Walker(x, y, speed, vert, chanceSecCorridorDestroy, chanceSecCorridorCreates, chanceSecCorridorSnakes, minSecCorridorLength, maxSecCorridorLength, 0.33f);
                            secondaryWalker.Walk(ref roomArray, ref roomIDs, ref roomAmount);
                        }
                    }
                }
            }
        }

        public void Furnish()
        {
            int roomCount = 0;
            int currentBonuses = 0;
            for (int x = 0; x < roomArray.GetLength(0); x++)
            {
                for (int y = 0; y < roomArray.GetLength(1); y++)
                {
                    if (roomArray[x, y] == RoomType.Bonus)
                    {
                        currentBonuses += 1;
                    }
                }
            }
            for (int x = 0; x < roomArray.GetLength(0); x++)
            {
                for (int y = 0; y < roomArray.GetLength(1); y++)
                {
                    if (roomArray[x, y] == RoomType.Room)
                    {
                        roomCount += 1;
                    }
                }
            }
            if (roomCount >= minimumBonuses)
            {
                while (currentBonuses < minimumBonuses)
                {
                    for (int x = 0; x < roomArray.GetLength(0); x++)
                    {
                        for (int y = 0; y < roomArray.GetLength(1); y++)
                        {
                            if (roomArray[x, y] == RoomType.Room)
                            {
                                if (UnityEngine.Random.value < chanceToAddBonus)
                                {
                                    roomArray[x, y] = RoomType.Bonus;
                                    currentBonuses++;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void BuildDungeon()
        {
            for (int x = 0; x < roomArray.GetLength(0); x++)
            {
                for (int y = 0; y < roomArray.GetLength(1); y++)
                {
                    CreateRoom(x, y);
                }
            }
        }

        public void CreateRoom(int x, int y)
        {
            int rng;
            GameObject currentRoom;
            switch (roomArray[x, y])
            {
                case RoomType.None:/*
                    rng = UnityEngine.Random.Range(0, none.Count - 1);
                    currentRoom = Instantiate(none[rng], new Vector3(x * resolutionX, 0, y * resolutionY), Quaternion.identity);
                    currentRoom.GetComponent<RoomBehavior>().roomType = RoomType.None;
                    allRoomsInDungeon.Add(currentRoom);*/
                    break;
                case RoomType.Start:
                    rng = UnityEngine.Random.Range(0, starts.Count - 1);
                    currentRoom = Instantiate(starts[rng], new Vector3(x * resolutionX, 0, y * resolutionY), Quaternion.identity);
                    currentRoom.GetComponent<RoomBehavior>().roomType = RoomType.Start;
                    currentRoom.GetComponent<RoomBehavior>().roomID = roomIDs[x, y];
                    currentRoom.GetComponent<RoomBehavior>().roomX = x;
                    currentRoom.GetComponent<RoomBehavior>().roomY = y;
                    allRoomsInDungeon.Add(currentRoom);
                    break;
                case RoomType.CorridorRoom:
                    rng = UnityEngine.Random.Range(0, corridorRooms.Count - 1);
                    currentRoom = Instantiate(corridorRooms[rng], new Vector3(x * resolutionX, 0, y * resolutionY), Quaternion.identity);
                    currentRoom.GetComponent<RoomBehavior>().roomType = RoomType.CorridorRoom;
                    currentRoom.GetComponent<RoomBehavior>().roomID = roomIDs[x, y];
                    currentRoom.GetComponent<RoomBehavior>().roomX = x;
                    currentRoom.GetComponent<RoomBehavior>().roomY = y;
                    allRoomsInDungeon.Add(currentRoom);
                    break;
                case RoomType.Room:
                    rng = UnityEngine.Random.Range(0, rooms.Count - 1);
                    currentRoom = Instantiate(rooms[rng], new Vector3(x * resolutionX, 0, y * resolutionY), Quaternion.identity);
                    currentRoom.GetComponent<RoomBehavior>().roomType = RoomType.Room;
                    currentRoom.GetComponent<RoomBehavior>().roomID = roomIDs[x, y];
                    currentRoom.GetComponent<RoomBehavior>().roomX = x;
                    currentRoom.GetComponent<RoomBehavior>().roomY = y;
                    allRoomsInDungeon.Add(currentRoom);
                    break;
                case RoomType.Bonus:
                    rng = UnityEngine.Random.Range(0, bonus.Count - 1);
                    currentRoom = Instantiate(bonus[rng], new Vector3(x * resolutionX, 0, y * resolutionY), Quaternion.identity);
                    currentRoom.GetComponent<RoomBehavior>().roomType = RoomType.Bonus;
                    currentRoom.GetComponent<RoomBehavior>().roomID = roomIDs[x, y];
                    currentRoom.GetComponent<RoomBehavior>().roomX = x;
                    currentRoom.GetComponent<RoomBehavior>().roomY = y;
                    allRoomsInDungeon.Add(currentRoom);
                    break;
                case RoomType.Weird:
                    rng = UnityEngine.Random.Range(0, weird.Count - 1);
                    currentRoom = Instantiate(weird[rng], new Vector3(x * resolutionX, 0, y * resolutionY), Quaternion.identity);
                    currentRoom.GetComponent<RoomBehavior>().roomType = RoomType.Weird;
                    currentRoom.GetComponent<RoomBehavior>().roomID = roomIDs[x, y];
                    currentRoom.GetComponent<RoomBehavior>().roomX = x;
                    currentRoom.GetComponent<RoomBehavior>().roomY = y;
                    allRoomsInDungeon.Add(currentRoom);
                    break;
                case RoomType.End:
                    rng = UnityEngine.Random.Range(0, ends.Count - 1);
                    currentRoom = Instantiate(ends[rng], new Vector3(x * resolutionX, 0, y * resolutionY), Quaternion.identity);
                    currentRoom.GetComponent<RoomBehavior>().roomType = RoomType.End;
                    currentRoom.GetComponent<RoomBehavior>().roomID = roomIDs[x, y];
                    currentRoom.GetComponent<RoomBehavior>().roomX = x;
                    currentRoom.GetComponent<RoomBehavior>().roomY = y;
                    allRoomsInDungeon.Add(currentRoom);
                    break;
            }
        }

        private void Connect()
        {
            //determine virtual connections
            for (int i = 0; i < allRoomsInDungeon.Count; i++)
            {
                allRoomsInDungeon[i].GetComponent<RoomBehavior>().roomCount = allRoomsInDungeon.Count;
                allRoomsInDungeon[i].GetComponent<RoomBehavior>().Connect();
               // Debug.LogError("Connected");
            }

            //place doors and walls depending on virtual connections
            for (int i = 0; i < allRoomsInDungeon.Count; i++)
            {
                if (allRoomsInDungeon[i].GetComponent<RoomBehavior>().roomType != RoomType.None)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        allRoomsInDungeon[i].transform.GetChild(0).GetChild(j).GetComponent<ConnectorBehavior>().CreateConnections();
                    }
                }
            }
        }


    }

        struct Walker
        {
            public int posX, posY, speed, minSteps, maxSteps;
            public bool vertical;
            public float destroyChance, createChance, createChancefalloff, turnChance;
            public Walker(int X, int Y, int walkSpeed, bool isVertical, float destroy, float create, float turn, int minStepsToPerform, int maxStepsToPerform, float fallout)
            {
                posX = X;
                posY = Y;
                speed = walkSpeed;
                vertical = isVertical;
                destroyChance = destroy;
                createChance = create;
                turnChance = turn;
                minSteps = minStepsToPerform;
                maxSteps = maxStepsToPerform;
                createChancefalloff = fallout;
            }

            public void CorridorWalk(ref RoomType[,] roomArray, ref int[,] roomIDs, ref int roomAmount)
            {
                int intendedSteps = UnityEngine.Random.Range(minSteps, maxSteps);
                for (int i = 0; i < intendedSteps; i++)
                {
                    if (UnityEngine.Random.value < turnChance)
                    {
                        //Debug.LogError("Snaked !");
                        ChangeDir();
                        speed = UnityEngine.Random.value < 0.5f ? -1 : 1;
                    }
                    if (CorridorStep(ref roomArray, ref roomIDs, ref roomAmount))
                    {
                        break;
                    }
                }
                roomArray[posX, posY] = RoomType.End;
                roomIDs[posX, posY] = roomAmount-1;
                roomAmount++;
            }

            bool CorridorStep(ref RoomType[,] roomArray, ref int[,] roomIDs, ref int roomAmount)
            {
                int directionChanged = 0;
                bool end = false;
                Debug.LogWarning("Current position = " + posX + "/" + posY);
                while (directionChanged < 4)
                {
                    if (VerifyStep(ref roomArray, ref directionChanged))
                    {
                        if (vertical)
                        {
                            posY += speed;
                            roomArray[posX, posY] = RoomType.CorridorRoom;
                            roomIDs[posX, posY] = roomAmount;
                            roomAmount++;
                        }
                        else
                        {
                            posX += speed;
                            roomArray[posX, posY] = RoomType.CorridorRoom;
                            roomIDs[posX, posY] = roomAmount;
                            roomAmount++;
                        }
                        break;
                    }
                    else
                    {
                        ChangeDir();
                        directionChanged++;
                        Debug.LogWarning("Changed Direction n° " + directionChanged);
                    }

                }
                end = directionChanged >= 4 ? true : false;
                return end;
            }

            public void Walk(ref RoomType[,] roomArray, ref int[,] roomIDs, ref int roomAmount)
            {
                int intendedSteps = UnityEngine.Random.Range(minSteps, maxSteps);
                for (int i = 0; i < intendedSteps; i++)
                {
                    if (UnityEngine.Random.value < turnChance)
                    {
                        //Debug.LogError("Snaked !");
                        ChangeDir();
                        speed = UnityEngine.Random.value < 0.5f ? -1 : 1;
                    }
                    if (UnityEngine.Random.value < createChance)
                    {
                        bool vert;
                        int speed;
                        vert = UnityEngine.Random.value < 0.5f ? true : false;
                        speed = UnityEngine.Random.value < 0.5f ? -1 : 1;
                        Walker tertiaryWalker = new Walker(posX, posY, speed, vert, destroyChance, createChance * createChancefalloff, turnChance, minSteps, maxSteps, 0);
                        tertiaryWalker.Walk(ref roomArray, ref roomIDs, ref roomAmount);
                    }
                    if (Step(ref roomArray, ref roomIDs, ref roomAmount))
                    {
                        break;
                    }
                }
                if (UnityEngine.Random.value < (0.1f * (intendedSteps)))
                {
                    roomArray[posX, posY] = RoomType.Bonus;
                }
            }

            bool Step(ref RoomType[,] roomArray, ref int[,] roomIDs, ref int roomAmount)
            {
                int directionChanged = 0;
                bool end = false;
                Debug.LogWarning("Current position = " + posX + "/" + posY);
                while (directionChanged < 4)
                {
                    if (VerifyStep(ref roomArray, ref directionChanged))
                    {
                        if (vertical)
                        {
                            posY += speed;
                            roomArray[posX, posY] = RoomType.Room;
                            roomIDs[posX, posY] = roomAmount;
                            roomAmount++;
                        }
                        else
                        {
                            posX += speed;
                            roomArray[posX, posY] = RoomType.Room;
                            roomIDs[posX, posY] = roomAmount;
                            roomAmount++;
                        }
                        break;
                    }
                    else
                    {
                        ChangeDir();
                        directionChanged++;
                        Debug.LogWarning("Changed Direction n° " + directionChanged);
                    }

                }
                end = directionChanged >= 4 ? true : false;
                return end;
            }

            bool VerifyStep(ref RoomType[,] roomArray, ref int directionChanged)
            {
                //Verify Next Step and turn if necessary
                if (vertical)
                {
                    if (posY + speed < 0 || posY + speed > roomArray.GetLength(1) - 1)
                    {
                        return false;
                    }
                }
                else
                {
                    if (posX + speed < 0 || posX + speed > roomArray.GetLength(0) - 1)
                    {
                        return false;
                    }
                }
                if (vertical)
                {

                    RoomType nextRoom = roomArray[posX, posY + speed];
                    switch (nextRoom)
                    {
                        case RoomType.None:
                            return true;
                        case RoomType.Start:
                            return false;
                        case RoomType.CorridorRoom:
                            return false;
                        case RoomType.Room:
                            return false;
                    }
                }
                else
                {


                    RoomType nextRoom = roomArray[posX + speed, posY];
                    switch (nextRoom)
                    {
                        case RoomType.None:
                            return true;
                        case RoomType.Start:
                            return false;
                        case RoomType.CorridorRoom:
                            return false;
                        case RoomType.Room:
                            return false;
                    }

                }
                return false;
            }

            void ChangeDir()
            {
                if (vertical)
                {
                    if (speed == 1)
                    {
                        vertical = false;
                    }
                    else
                    {
                        vertical = false;
                        speed = -1;
                    }
                }
                else
                {
                    if (speed == 1)
                    {
                        vertical = true;
                        speed = -1;
                    }
                    else
                    {
                        vertical = true;
                        speed = 1;
                    }
                }


            }
        }

    }

