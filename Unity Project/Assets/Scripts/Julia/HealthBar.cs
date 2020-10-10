using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public Image bar, bar2, bar3;
    public float fill;
    
    public float vieTemp;

    public float vieMax = 20;
    public float max1 = 20, max2 = 40, max3 = 60;
    public UnityEngine.UI.Text displayLife;

    // Start is called before the first frame update
    void Start()
    {
        //vieTemp = max1;
        bar.fillAmount = (vieTemp/max1);
    }

    // Update is called once per frame
    public void Update()
    {
        displayLife.text = vieTemp.ToString() + "/" + vieMax.ToString();
        if (vieTemp >= max2)
        {
            bar3.fillAmount = ((vieTemp - max2) / max1);
            bar2.fillAmount = 1f;
            bar.fillAmount = 1f;
        }
        else if (vieTemp >= max1)
        {
            bar3.fillAmount = 0f;
            bar2.fillAmount = ((vieTemp - max1) / max1);
            bar.fillAmount = 1f;
        }
        else if (vieTemp <= max1)
        {
            bar3.fillAmount = 0f;
            bar2.fillAmount = 0f;
            bar.fillAmount = (vieTemp  / max1);
            print("hey");
            if (vieTemp <= 0)
            {
                ApplyDeath();
            }
        }
           
    }
    public void ApplyDamage(float damage)
    {
        vieTemp -= damage;
        if (vieTemp > vieMax)
        {
            vieTemp = vieMax;
        }
        
    }
        
    public void ApplyDeath()
    {
        //play death animation and death sound
        SceneManager.LoadScene("DeathScreen");

    }

    public void UpgradeLife(float bonusHealth)
    {
        vieMax += bonusHealth;
        vieTemp += bonusHealth;
        if (vieMax > max3)
        {
            vieMax = max3;
            vieTemp = max3;
        }
    }
    
}
