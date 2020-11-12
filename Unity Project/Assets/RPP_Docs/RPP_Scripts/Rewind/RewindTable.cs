using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindTable : MonoBehaviour
{
    [SerializeField] GameObject tableObj;
    [SerializeField] Animator tableAnim;
    [SerializeField] TableFlip tableScript;
    [SerializeField] ReverseParticles reverseParticles;

    private void Start()
    {
        reverseParticles.enabled = false;
    }

    public void TableRewind()
    {
        if (tableObj.activeSelf == false)
        {
            StartCoroutine(StartReverse());
        }
    }

    IEnumerator StartReverse()
    {
        tableAnim.SetInteger("FlipInt", 4);
        tableScript.enemyDamageTable.currentHP = tableScript.enemyDamageTable.maxHP;
        reverseParticles.enabled = true;
        yield return new WaitForSeconds(2f);
        tableObj.SetActive(true);
        reverseParticles.enabled = false;
    }
}
