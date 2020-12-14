using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boulon : MonoBehaviour
{
    GameObject compteurBoulon;
    Compteur Compteur;
    bool pickUp;

    // Start is called before the first frame update
    void Start()
    {
        //mettre l'animation iddle
        compteurBoulon = GameObject.FindGameObjectWithTag ("Compteur");
        Compteur = compteurBoulon.GetComponent<Compteur>();
    }


    void OnCollisionEnter(Collision collisionBoulon) //il faut que l'un des colliders soit avec un non-kinematic rigidbody
    {
        if(!pickUp)
        {
            Compteur.GainBoulon(1);
            pickUp = true;
            Destroy(gameObject, 1);
        }
       
    }

}
