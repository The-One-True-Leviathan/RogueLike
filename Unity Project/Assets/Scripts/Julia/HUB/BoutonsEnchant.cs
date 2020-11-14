using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoutonsEnchant : MonoBehaviour
{
    public Button btn;
    public EnchantShop enchantShop;
    public int numeroEnchant;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        enchantShop = GetComponentInParent<EnchantShop>();
        btn.onClick.AddListener(() => { EnchantNumber(); });
    }

    // Update is called once per frame
    
    public void EnchantNumber()
    {
        enchantShop.Enchant(numeroEnchant);
    }
}
