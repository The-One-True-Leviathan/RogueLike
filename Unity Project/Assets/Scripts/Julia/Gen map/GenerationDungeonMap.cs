using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class GenerationDungeonMap : MonoBehaviour
{
    public List<NodeBehavior> nodeBehaviors;
    public List<int> nodesToActivate;
    public int playerIsHere = 0;
    public Sprite playerHead;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        //génération de la carte
        nodeBehaviors[0].type = NodeBehavior.DungeonTypes.HUB;
        nodeBehaviors[0].hasAType = true;
        nodeBehaviors[12].type = NodeBehavior.DungeonTypes.BOSS;
        nodeBehaviors[12].hasAType = true;

        for (int i= 0; i < nodeBehaviors.Count; i++)
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

    }
    // Update is called once per frame

    public void MapUpdate()
    {
        nodesToActivate.Clear();
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

    }
}
