using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piecette : MonoBehaviour
{
    public int nbrePiecettes;
    public UnityEngine.UI.Text displayPiecettes;
    // Start is called before the first frame update
    void Start()
    { 
        //animation iddle
    }

    // Update is called once per frame
    void Update()
    {
        displayPiecettes.text = "Piecettes:" + nbrePiecettes.ToString();
    }

    private void OnCollisionEnter(Collision collisionPiecettes)
    {
        //son récupération
        nbrePiecettes ++;
        Destroy(gameObject, 1);
    }
}
