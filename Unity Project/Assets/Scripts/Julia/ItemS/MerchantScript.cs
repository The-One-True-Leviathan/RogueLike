using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantScript : MonoBehaviour
{
    public Text textBox;
    public List<string> merchantQuotes, badMerchantQuotes;
    int lastQuote = -1, lastBadQuote = -1; 
    // Start is called before the first frame update
    void Start()
    {
        textBox = GameObject.Find("Merchant box").GetComponent<Text>();
        //merchantQuotes = new List<string>();
        merchantQuotes.Add("We aim to please !");
        merchantQuotes.Add("Are you looking for protection or damage dealing ?");
        merchantQuotes.Add("Thank yooou !");
        merchantQuotes.Add("Have you heard about the tanuki businessman ?");
        merchantQuotes.Add("Got some other rare things on sale, stranger !");
        merchantQuotes.Add("What're ya buyin ?");
        merchantQuotes.Add("Is that all, stranger ?");
        merchantQuotes.Add("You've met a terrible fate, haven't you ?");
        //badMerchantQuotes = new List<string>();
        badMerchantQuotes.Add("Not only will you need cash, but you'll need GUTS to buy my weapons !");
        badMerchantQuotes.Add("Look and buy. Nothing could be easier.");
        badMerchantQuotes.Add("We don't accept bells.");
        badMerchantQuotes.Add("Not enough money, stranger.");
        badMerchantQuotes.Add("The khajiit has ware if you have coin");
    }

    // Update is called once per frame
    public void BuyingDialogue()
    {
        int index = Random.Range(0, merchantQuotes.Count-1);
        if(lastQuote == index)
        {
            if(index == merchantQuotes.Count-1)
            {
                index -= 1;
            }
            else
            {
                index += 1;
            }
        }
        lastQuote = index;
        textBox.text = (merchantQuotes[index]);
    }

    public void NoMoneyDialogue()
    {
        int index = Random.Range(0, badMerchantQuotes.Count-1);
        if (lastBadQuote == index)
        {
            if (index == badMerchantQuotes.Count - 1)
            {
                index -= 1;
            }
            else
            {
                index += 1;
            }
        }
        lastBadQuote = index;
        textBox.text = (badMerchantQuotes[index]);
    }
}
