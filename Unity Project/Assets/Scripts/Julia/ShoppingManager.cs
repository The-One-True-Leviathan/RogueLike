using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingManager : MonoBehaviour
{
    // Start is called before the first frame update
    //ce script va conserver les données des choses achetées dans les boutiques

    public bool BarrelsWereBought = false;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    
    public void BarrelUpdate()
    {
        BarrelsWereBought = true;
    }
}
