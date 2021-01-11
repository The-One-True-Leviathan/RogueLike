using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NodeBehavior : MonoBehaviour
{
    public enum DungeonTypes { BOULON, WEAPON, ENCHANT, HUB, BOSS };
    public DungeonTypes type;
    public bool hasAType;
    public int number, deapth;
    public List<int> canConnect;
    public Button button;
    public List<Sprite> sprites;
    public Image image;
    public GenerationDungeonMap generation;
    public List<string> nameScene;
    public float scaleMultiplier;
    public GameObject mapObject;
    // Start is called before the first frame update
    void Start()
    {
        generation = GetComponentInParent<GenerationDungeonMap>();
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        mapObject = GameObject.FindGameObjectWithTag("Map");
        //gestion d'où se trouve le joueur
    }

    // Update is called once per frame
    public void UpdateNode()
    {
        switch(type)
        {
            case DungeonTypes.BOULON:
                image.sprite = sprites[0];
                break;

            case DungeonTypes.WEAPON:
                image.sprite = sprites[1];
                break;

            case DungeonTypes.ENCHANT:
                image.sprite = sprites[2];
                break;

            case DungeonTypes.HUB:
                image.sprite = sprites[3];
                break;

            case DungeonTypes.BOSS:
                image.sprite = sprites[4];
                break;
        }
        button.GetComponent<Image>().SetNativeSize();
    }

    public void nodeSelected()
    {
        generation.playerIsHere = number;
        Time.timeScale = 1f;
        /*switch (type)
        {
            case DungeonTypes.BOULON:
                SceneManager.LoadScene(nameScene[0]);
                break;

            case DungeonTypes.WEAPON:
                SceneManager.LoadScene(nameScene[1]);
                break;

            case DungeonTypes.ENCHANT:
                SceneManager.LoadScene(nameScene[2]);
                break;

            case DungeonTypes.HUB:
                SceneManager.LoadScene("HUB");
                break;

            case DungeonTypes.BOSS:
                SceneManager.LoadScene("Boss");
                break;
        }*/

        mapObject.GetComponent<RectTransform>().localScale = Vector3.zero;
        SceneManager.LoadScene(nameScene[0]);
    }

    public void activatingNode()
    {
        Debug.LogWarning("Node activated");
        button.enabled = true;
        button.GetComponent<RectTransform>().localScale = Vector3.one * scaleMultiplier;
    }

    public void desactivatingNode()
    {
        button = GetComponent<Button>();
        button.enabled = false;
        button.GetComponent<RectTransform>().localScale = Vector3.one * 0.8f;
    }
}
