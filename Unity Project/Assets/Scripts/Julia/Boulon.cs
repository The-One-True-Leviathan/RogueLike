using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boulon : MonoBehaviour
{
    public int nbreBoulon;
    public GameObject compteurBoulon;
    public UnityEngine.UI.Text displayBoulon;

    // Start is called before the first frame update
    void Start()
    {
        //mettre l'animation iddle
        compteurBoulon = GameObject.FindGameObjectWithTag ("Compteur Boulon");
        displayBoulon = compteurBoulon.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        displayBoulon.text = "Boulons:" + nbreBoulon.ToString();
    }

    void OnCollisionEnter(Collision collisionBoulon) //il faut que l'un des colliders soit avec un non-kinematic rigidbody
    {
        //son récupération
        nbreBoulon++;
        Destroy(gameObject, 1);
    }

}
