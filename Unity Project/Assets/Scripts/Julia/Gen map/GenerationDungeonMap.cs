using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class GenerationDungeonMap : MonoBehaviour
{
    public List<NodeBehavior> nodeScripts;
    public int[] nodesToActivate;
    public int playerIsHere = 0;
    public Sprite playerHead;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    
        //génération de la carte
        nodeScripts[0].type = NodeBehavior.DungeonTypes.HUB;
        nodeScripts[0].hasAType = true;
        nodeScripts[12].type = NodeBehavior.DungeonTypes.BOSS;
        nodeScripts[12].hasAType = true;

        for (int i= 0; i < nodeScripts.Count; i++)
        {

            if (!nodeScripts[i].hasAType)
            {
                int index = Random.Range(0, 2);
                switch (index)
                {
                    case 0:
                        nodeScripts[i].type = NodeBehavior.DungeonTypes.BOULON;
                        break;

                    case 1:
                        nodeScripts[i].type = NodeBehavior.DungeonTypes.WEAPON;
                        break;

                    case 2:
                        nodeScripts[i].type = NodeBehavior.DungeonTypes.ENCHANT;
                        break;
                }
            }
        }

    }
    // Update is called once per frame

    public void MapUpdate()
    {
        for (int i = 0; i < nodeScripts.Count; i++)
        {
            nodeScripts[i].desactivatingNode();
            nodeScripts[i].UpdateNode();
        }
        nodesToActivate[1] = nodeScripts[playerIsHere].canConnect[1];
        nodesToActivate[2] = nodeScripts[playerIsHere].canConnect[2];
        nodeScripts[nodesToActivate[1]].activatingNode();
        nodeScripts[nodesToActivate[2]].activatingNode();
        nodeScripts[playerIsHere].GetComponent<Image>().sprite = playerHead;

    }
}
