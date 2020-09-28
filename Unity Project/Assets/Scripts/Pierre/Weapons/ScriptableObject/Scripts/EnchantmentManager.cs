using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class EnchantmentManager : MonoBehaviour
{
    GameObject playerObject;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Player>();
    }

    public void DoEnchants(WeaponScriptableObject weapon, int specialType)
    {
        print("DoEnchant started");
        for (int i = 0; i < weapon.enchantments.Count; i++)
        {
            Enchantment enchant = weapon.enchantments[i];
            print("Tried DoEffects");
            DoEffects(enchant, specialType);
        }
    }

    public void DoEffects(Enchantment enchant, int specialType)
    {
        print("DoEffect started");
        int rng = Random.Range(1, 100);
        for (int i = 0; i < enchant.effects.Length; i++)
        {
            if (enchant.effects[i].type == specialType)
            {
                EnchantmentsEffect effect = enchant.effects[i];
                enchant.effects[i].nativeRNG = rng;
                print("Tried EffectAction");
                EffectAction(effect);
            }
        }
    }

    public void EffectAction(EnchantmentsEffect effect)
    {
        print("EffectAction started");
        if (effect.effectType != EffectEnchantmentType.None)
        {
            print("passed line 49");
            if (effect.isInEffect != true)
            {
                print("Tried Coroutine");
                StartCoroutine("EffectCoroutine", effect);
            }
        }
    }

    public IEnumerator EffectCoroutine(EnchantmentsEffect effect)
    {
        print("Coroutine Started");
        Debug.LogWarning("Spécial !");
        effect.isInEffect = true;
        effect.DoEffect();
        yield return new WaitForSeconds(effect.effectTicLength);
        effect.isInEffect = false;
    }


}
