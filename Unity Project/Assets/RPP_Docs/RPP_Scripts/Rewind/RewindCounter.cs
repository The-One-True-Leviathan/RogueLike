using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindCounter : MonoBehaviour
{

    [SerializeField] Rewind rewindScript;
    [SerializeField] GameObject comboVide1, comboVide2, comboVide3, comboPlein1, comboPlein2, comboPlein3;
    public Animator rewindAnimator;


    private void Start()
    {
        rewindScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Rewind>();
    }


    // Update is called once per frame
    void Update()
    {
        if (rewindScript.rewindCounter == 0)
        {
            comboVide1.SetActive(true);
            comboVide2.SetActive(true);
            comboVide3.SetActive(true);

            comboPlein1.SetActive(false);
            comboPlein2.SetActive(false);
            comboPlein3.SetActive(false);

            rewindAnimator.SetBool("CanRewind", false);
        }

        else if (rewindScript.rewindCounter == 1)
        {
            comboVide1.SetActive(false);
            comboVide2.SetActive(true);
            comboVide3.SetActive(true);

            comboPlein1.SetActive(true);
            comboPlein2.SetActive(false);
            comboPlein3.SetActive(false);

            rewindAnimator.SetBool("CanRewind", false);
        }

        else if (rewindScript.rewindCounter == 2)
        {
            comboVide1.SetActive(false);
            comboVide2.SetActive(false);
            comboVide3.SetActive(true);

            comboPlein1.SetActive(true);
            comboPlein2.SetActive(true);
            comboPlein3.SetActive(false);

            rewindAnimator.SetBool("CanRewind", false);
        }

        else if (rewindScript.rewindCounter == 3)
        {
            comboVide1.SetActive(false);
            comboVide2.SetActive(false);
            comboVide3.SetActive(false);

            comboPlein1.SetActive(true);
            comboPlein2.SetActive(true);
            comboPlein3.SetActive(true);

            rewindAnimator.SetBool("CanRewind", true);
        }
    }
}
