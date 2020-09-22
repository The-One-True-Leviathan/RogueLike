using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image bar;
    public float fill;
    public float vieTemp;
    public int vieMax = 20;
    public UnityEngine.UI.Text displayLife;

    // Start is called before the first frame update
    void Start()
    {
        fill = 1f;
        vieTemp = 20;
        bar.fillAmount = (vieTemp/20);
    }

    // Update is called once per frame
    public void Update()
    {
        displayLife.text = vieTemp.ToString() + "/" + vieMax.ToString();
        bar.fillAmount = (vieTemp/20);
    }
    public void ApplyDamage(float damage)
    {
        vieTemp -= damage;
    }
        
    
}
