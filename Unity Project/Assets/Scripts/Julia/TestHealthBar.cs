using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHealthBar : MonoBehaviour
{
    Controler controller;
    public HealthBar healthBar;
    public Compteur compteur;
    bool test, cheat, moreMoney;
    public float testDamage = 4;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        healthBar = GameObject.FindGameObjectWithTag("HUD").GetComponent<HealthBar>();
        compteur = GameObject.FindGameObjectWithTag("Compteur").GetComponent<Compteur>();
        controller = new Controler();
        controller.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        test = controller.Keyboard.LifeCheat.triggered;
        cheat = controller.Keyboard.MoneyCheat.triggered;
        moreMoney = controller.Keyboard.DungeonCheat.triggered;
        if (test)
        {
            healthBar.ApplyDamage(-5);
        }
        if (cheat)
        {
            compteur.GainBoulon(50);
        }
        if (moreMoney)
        {
            compteur.GainPiecettes(50);
        }
        
    }
}
