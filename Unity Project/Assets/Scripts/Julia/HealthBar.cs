using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public Image bar;
    public float fill;
    
    public float vieTemp;

    public Image bar2;

    public Image bar3;

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
        if (vieTemp > 40 && vieTemp <= vieMax)
        {
            bar3.fillAmount = ((vieTemp - 40) / 20);
            bar2.fillAmount = 1f;
            bar.fillAmount = 1f;
        }
        else if (vieTemp > 20 && vieTemp <= vieMax)
        {
            bar3.fillAmount = 0f;
            bar2.fillAmount = ((vieTemp - 20) / 20);
            bar.fillAmount = 1f;
        }
        else if (vieTemp < 20)
        {
            bar3.fillAmount = 0f;
            bar2.fillAmount = 0f;
            bar.fillAmount = (vieTemp / 20);
            if (vieTemp <= 0)
            {
                ApplyDeath();
            }
        }
           
    }
    public void ApplyDamage(float damage)
    {
        if ((vieTemp-damage) <= vieMax)
        {
            vieTemp -= damage;
        }
        
    }
        
    public void ApplyDeath()
    {
        //play death animation and death sound
        SceneManager.LoadScene("DeathScreen");

    }

    public void UpgradeLife(int bonusHealth)
    {
        vieMax += bonusHealth;
    }
    
}
