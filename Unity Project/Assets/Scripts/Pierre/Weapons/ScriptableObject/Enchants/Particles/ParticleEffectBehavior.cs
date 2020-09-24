using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectBehavior : MonoBehaviour
{
    public ParticleSystem emitter;
    // Start is called before the first frame update
    void Start()
    {
        emitter.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, emitter.main.duration+1);
    }
}
