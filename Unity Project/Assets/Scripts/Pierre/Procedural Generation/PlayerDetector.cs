using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{

    public List<GameObject> doors;
    public bool hasClosed;
    public bool bossOverride;
    public GameObject monsters;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!bossOverride)
        {
            int children = transform.parent.parent.parent.childCount;
            for (int i = 0; i < children; i++)
            {
                if (transform.parent.parent.parent.GetChild(i).tag == "Door")
                {
                    doors.Add(transform.parent.parent.parent.GetChild(i).gameObject);
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasClosed)
        {
            if (other.tag == "Player")
            {
                hasClosed = true;
                if (monsters != null)
                {
                    monsters.SetActive(true);
                }
                foreach (GameObject door in doors)
                {
                    door.GetComponent<DoorManager>().Close();
                }
            }
        }
    }
}
