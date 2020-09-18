using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torch : MonoBehaviour
{
    // Start is called before the first frame update

    Light torhc;
    float baseIntensity;
    public float range = 0.4f;
    public float ranRange = 0.15f;
    public float maxTime = 0.1f;
    float timeElapsed = 0f;
    void Start()
    {
        torhc = GetComponent<Light>();
        baseIntensity = torhc.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= maxTime)
        {
            torhc.intensity += UnityEngine.Random.Range(-ranRange, ranRange);
            if (torhc.intensity > baseIntensity+range)
            {
                torhc.intensity = baseIntensity + range;
            } else if (torhc.intensity < baseIntensity - range)
            {
                torhc.intensity = baseIntensity - range;
            }
            timeElapsed = 0;
        }
    }
}
