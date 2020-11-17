using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public List<GameObject> possibleEnnemies;
    // Start is called before the first frame update
    void Awake()
    {
        int index = Random.Range(0, possibleEnnemies.Count - 1);
        Instantiate(possibleEnnemies[index], transform.position, Quaternion.identity, transform);
    }

}
