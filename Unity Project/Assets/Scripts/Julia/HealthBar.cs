using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image bar;
    public int fill;
    public int vieTemp;
    public UnityEngine.UI.Text displayLife;

    // Start is called before the first frame update
    void Start()
    {
        fill = 20;
        vieTemp = fill;
        bar.fillAmount = fill;
    }

    // Update is called once per frame
    public void Update()
    {
        displayLife.text = vieTemp.ToString() + "/" + fill.ToString();
    }
    public void ApplyDamage(int damage)
    {
        vieTemp -= damage;
        bar.fillAmount = vieTemp;
    }
        
    
}
