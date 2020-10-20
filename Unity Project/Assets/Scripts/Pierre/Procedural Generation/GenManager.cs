using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityScript.Scripting.Pipeline;

public class GenManager : MonoBehaviour
{
    public enum RoomType { None, Room, CorridorRoom, Start, End, Bonus, Weird }
    public int roomAmount, maxRooms;
    public List<GameObject> rooms, corridorRooms, ends, starts, none, bonus, weird;
    List<int> allRoomsX = new List<int>();
    List<int> allRoomsY = new List<int>();
    public RoomType[,] roomArray;
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
    }

    public void MainCorridor()
    {
        bool vert;
        int speed;
        vert = UnityEngine.Random.value < 0.5f ? true : false;
        speed = UnityEngine.Random.value < 0.5f ? -1 : 1;
        corridorWalker = new Walker(startX, startY, speed, vert, 0, 0, chanceMainCorridorSnakes, minMainCorridorLength, maxMainCorridorLength, 0);
        corridorWalker.CorridorWalk(ref roomArray);

    }

    public void BranchOut()
    {
        for (int x = 0; x < roomArray.GetLength(0); x++)
        {
            for (int y = 0; y < roomArray.GetLength(1); y++)
            {
                if (roomArray[x,y] == RoomType.CorridorRoom || roomArray[x, y] == RoomType.Start)
                {
                    if (UnityEngine.Random.value < chanceToCreateNewCorridor)
                    {
                        bool vert;
                        int speed;
                        vert = UnityEngine.Random.value < 0.5f ? true : false;
                        speed = UnityEngine.Random.value < 0.5f ? -1 : 1;
                        Walker secondaryWalker = new Walker(x, y, speed, vert, chanceSecCorridorDestroy, chanceSecCorridorCreates, chanceSecCorridorSnakes, minSecCorridorLength, maxSecCorridorLength, 0.33f);
                        secondaryWalker.Walk(ref roomArray);
                    }
                }
            }
        }
    }

    public void Furnish()
    {
        int roomCount=0;
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
        while (currentBonuses < minimumBonuses)
        {
            for (int x = 0; x < roomArray.GetLength(0); x++)
            {
                for (int y = 0; y < roomArray.GetLength(1); y++)
                {
                    if (roomArray[x, y] == RoomType.CorridorRoom)
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
        for (int x = 0; x < roomArray.GetLength(0); x++)
        {
            for (int y = 0; y < roomArray.GetLength(1); y++)
            {
                if (roomArray[x, y] == RoomType.Room)
                {
                    if (UnityEngine.Random.value < chanceToAddBonus)
                    {
                        bool vert;
                        int speed;
                        vert = UnityEngine.Random.value < 0.5f ? true : false;
                        speed = UnityEngine.Random.value < 0.5f ? -1 : 1;
                        Walker secondaryWalker = new Walker(x, y, speed, vert, chanceSecCorridorDestroy, chanceSecCorridorCreates, chanceSecCorridorSnakes, minSecCorridorLength, maxSecCorridorLength, 0.33f);
                        secondaryWalker.Walk(ref roomArray);
                    }
                }
            }
        }
    }

    public void BuildDungeon()
    {
        for(int x = 0; x < roomArray.GetLength(0); x++)
        {
            for(int y = 0; y < roomArray.GetLength(1); y++)
            {
                CreateRoom(x, y);
            }
        }
    }

    public void CreateRoom(int x, int y)
    {
        int rng;
        switch (roomArray[x, y])
        {
            case RoomType.None:
                rng = UnityEngine.Random.Range(0, none.Count - 1);
                Instantiate(none[rng], new Vector3(x * resolutionX, 0, y * resolutionY), Quaternion.identity);
                break;
            case RoomType.Start:
                rng = UnityEngine.Random.Range(0, starts.Count - 1);
                Instantiate(starts[rng], new Vector3(x * resolutionX, 0, y * resolutionY), Quaternion.identity);
                break;
            case RoomType.CorridorRoom:
                rng = UnityEngine.Random.Range(0, corridorRooms.Count - 1);
                Instantiate(corridorRooms[rng], new Vector3(x * resolutionX, 0, y * resolutionY), Quaternion.identity);
                break;
            case RoomType.Room:
                rng = UnityEngine.Random.Range(0, rooms.Count - 1);
                Instantiate(rooms[rng], new Vector3(x * resolutionX, 0, y * resolutionY), Quaternion.identity);
                break;
            case RoomType.Bonus:
                rng = UnityEngine.Random.Range(0, bonus.Count - 1);
                Instantiate(bonus[rng], new Vector3(x * resolutionX, 0, y * resolutionY), Quaternion.identity);
                break;
            case RoomType.Weird:
                rng = UnityEngine.Random.Range(0, weird.Count - 1);
                Instantiate(weird[rng], new Vector3(x * resolutionX, 0, y * resolutionY), Quaternion.identity);
                break;
            case RoomType.End:
                rng = UnityEngine.Random.Range(0, ends.Count - 1);
                Instantiate(ends[rng], new Vector3(x * resolutionX, 0, y * resolutionY), Quaternion.identity);
                break;
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

        public void CorridorWalk(ref RoomType[,] roomArray)
        {
            int intendedSteps = UnityEngine.Random.Range(minSteps, maxSteps);
            for (int i = 0; i < intendedSteps; i++)
            {
                if (UnityEngine.Random.value < turnChance)
                {
                    Debug.LogError("Snaked !");
                    ChangeDir();
                    speed = UnityEngine.Random.value < 0.5f ? -1 : 1;
                }
                if (CorridorStep(ref roomArray))
                {
                    break;
                }
            }
            roomArray[posX, posY] = RoomType.End;
        }

        bool CorridorStep(ref RoomType[,] roomArray)
        {
            int directionChanged = 0;
            bool end = false;
            Debug.LogWarning("Current position = " + posX + "/" + posY);
            while (directionChanged < 4)
            {
                if(VerifyStep(ref roomArray, ref directionChanged))
                {
                    if (vertical)
                    {
                        posY += speed;
                        roomArray[posX, posY] = RoomType.CorridorRoom;
                    } else
                    {
                        posX += speed;
                        roomArray[posX, posY] = RoomType.CorridorRoom;
                    }
                    break;
                } else
                {
                    ChangeDir();
                    directionChanged++;
                    Debug.LogWarning("Changed Direction n° " + directionChanged);
                }
                
            }
            end = directionChanged >= 4 ? true : false;
            return end;
        }

        public void Walk(ref RoomType[,] roomArray)
        {
            int intendedSteps = UnityEngine.Random.Range(minSteps, maxSteps);
            for (int i = 0; i < intendedSteps; i++)
            {
                if (UnityEngine.Random.value < turnChance)
                {
                    Debug.LogError("Snaked !");
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
                    tertiaryWalker.Walk(ref roomArray);
                }
                if (Step(ref roomArray))
                {
                    break;
                }
            }
            if (UnityEngine.Random.value < (0.1f * (intendedSteps)))
            {
                roomArray[posX, posY] = RoomType.Bonus;
            }
        }

        bool Step(ref RoomType[,] roomArray)
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
                    }
                    else
                    {
                        posX += speed;
                        roomArray[posX, posY] = RoomType.Room;
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
