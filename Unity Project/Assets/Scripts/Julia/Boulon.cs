using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boulon : MonoBehaviour
{
    public int nbreBoulon;
    public UnityEngine.UI.Text displayBoulon;

    // Start is called before the first frame update
    void Start()
    {
        //mettre l'animation iddle
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
