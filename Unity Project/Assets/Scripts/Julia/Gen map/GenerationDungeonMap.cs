using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GenerationDungeonMap : MonoBehaviour
{
    public List<NodeBehavior> nodeBehaviors;
    public List<int> nodesToActivate;
    public int playerIsHere = 0;
    public Sprite playerHead;
    public GameObject tutoObject, regenObject;
    public Compteur compteur;
    // Start is called before the first frame update
    void Start()
    {
        compteur = GameObject.FindGameObjectWithTag("Compteur").GetComponent<Compteur>();

        //génération de la carte
        MapGeneration();
        

    }
    // Update is called once per frame

    public void MapUpdate()
    {
        nodesToActivate.Clear();
        tutoObject.SetActive(false);
        regenObject.SetActive(false);
        for (int i = 0; i < nodeBehaviors.Count; i++)
        {
            nodeBehaviors[i].desactivatingNode();
            nodeBehaviors[i].UpdateNode();
        }
        for (int i = 0; i < nodeBehaviors[playerIsHere].canConnect.Count; i++)
        {
            nodesToActivate.Add(nodeBehaviors[playerIsHere].canConnect[i]);
        }
        foreach (int node in nodesToActivate)
        {
            nodeBehaviors[node].activatingNode();
        }

        nodeBehaviors[playerIsHere].GetComponent<Image>().sprite = playerHead;
        nodeBehaviors[playerIsHere].GetComponent<Image>().SetNativeSize();
        nodeBehaviors[playerIsHere].GetComponent<RectTransform>().localScale = Vector3.one * 0.8f;

        if (playerIsHere == 0)
        {
            tutoObject.SetActive(true);
            regenObject.SetActive(true);
        }

    }

    public void MapGeneration()
    {
        nodeBehaviors[0].type = NodeBehavior.DungeonTypes.HUB;
        nodeBehaviors[0].hasAType = true;
        nodeBehaviors[12].type = NodeBehavior.DungeonTypes.BOSS;
        nodeBehaviors[12].hasAType = true;

        for (int i = 0; i < nodeBehaviors.Count; i++)
        {

            if (!nodeBehaviors[i].hasAType)
            {
                int index = Random.Range(0, 3);
                switch (index)
                {
                    case 0:
                        nodeBehaviors[i].type = NodeBehavior.DungeonTypes.BOULON;
                        break;

                    case 1:
                        nodeBehaviors[i].type = NodeBehavior.DungeonTypes.WEAPON;
                        break;

                    case 2:
                        nodeBehaviors[i].type = NodeBehavior.DungeonTypes.ENCHANT;
                        break;
                }
            }
        }

        MapUpdate();
    }

    public void ButtonRegen()
    {
        if (compteur.boulonsActuels >= 1)
        {
            compteur.GainBoulon(-1);
            MapGeneration();
        }
        
    }

    public void Tuto()
    {
        GameObject.Destroy(GameObject.Find("Game Components"));
        SceneManager.LoadScene("Tuto");
    }
}
