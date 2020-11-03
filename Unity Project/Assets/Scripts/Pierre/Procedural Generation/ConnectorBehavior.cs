using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProcGen
{
    public class ConnectorBehavior : MonoBehaviour
    {
        public enum ConnectorType { Up, Down, Left, Right};
        public ConnectorType connectorType;
        public RoomBehavior roomBehavior;
        public GameObject doorObject, wallObject;
        public float doorShiftX, doorShiftY, wallShiftX, wallShiftY;
        // Start is called before the first frame update
        public void CreateConnections()
        {
            roomBehavior = transform.parent.GetComponentInParent<RoomBehavior>();
            switch(connectorType)
            {
                case ConnectorType.Up:
                    if (roomBehavior.connectUp)
                    {
                        PlaceDoor();
                    } else
                    {
                        PlaceWall();
                    }
                    break;
                case ConnectorType.Down:
                    if (roomBehavior.connectDown)
                    {
                        PlaceDoor();
                    }
                    else
                    {
                        PlaceWall();
                    }
                    break;
                case ConnectorType.Left:
                    if (roomBehavior.connectLeft)
                    {
                        PlaceDoor();
                    }
                    else
                    {
                        PlaceWall();
                    }
                    break;
                case ConnectorType.Right:
                    if (roomBehavior.connectRight)
                    {
                        PlaceDoor();
                    }
                    else
                    {
                        PlaceWall();
                    }
                    break;
            }
        }

        void PlaceDoor()
        {
            Debug.LogError("Placed Door !");
            Vector3 doorPos = transform.position + new Vector3(doorShiftX, 0, doorShiftY);
            Instantiate(doorObject, doorPos, Quaternion.identity);
        }

        void PlaceWall()
        {
            Debug.LogError("Placed Wall !");
            Vector3 wallPos = transform.position + new Vector3(wallShiftX, 0, wallShiftY);
            Instantiate(wallObject, wallPos, Quaternion.identity);
        }
    }
}
