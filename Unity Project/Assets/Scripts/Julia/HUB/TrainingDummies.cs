using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingDummies : MonoBehaviour
{
    public EnemyDamage damage;
    public Text damageText;

    // Start is called before the first frame update
    void Start()
    {
        damage = GetComponent<EnemyDamage>();
        damageText = GetComponentInChildren<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if(damage.currentDamage != 0)
        {
            StartCoroutine(DummyIsHit());
        }
    }

    IEnumerator DummyIsHit()
    {
        damageText.text = damage.currentDamage.ToString();
        damage.currentHP = damage.currentHP + damage.currentDamage * 2;
        damage.currentDamage = 0;
        yield return new WaitForSeconds(0.5f);
        damageText.text = null;
        
    }

}
