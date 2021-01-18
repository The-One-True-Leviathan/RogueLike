using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnder : MonoBehaviour
{
    public InteractibleBehavior interactible;

    private void Update()
    {
        if (interactible.interacted)
        {
            SceneManager.LoadScene("VictoryScene");
        }
    }
}
