using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piecette : MonoBehaviour
{
    GameObject compteurPieces;
    Compteur Compteur;
    bool pickUp;
    public int valueMoney; 

    // Start is called before the first frame update
    void Start()
    {
        //animation iddle
        compteurPieces = GameObject.FindGameObjectWithTag("Compteur");
        Compteur = compteurPieces.GetComponent<Compteur>();
    }


    private void OnCollisionEnter(Collision collisionPiecettes)
    {
        if(!pickUp)
        {
            Compteur.GainPiecettes(valueMoney);
            pickUp = true;
            Destroy(gameObject);
        }
        
    }
}
