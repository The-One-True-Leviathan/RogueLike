using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshake : MonoBehaviour
{
    GameObject shookCamera;

    private void Start()
    {
        shookCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public void Shake(float duration, float magnitude, float pause)
    {
        StartCoroutine(ShakeCoroutine(duration, magnitude, pause));
    }

    IEnumerator ShakeCoroutine(float duration, float magnitude, float pause)
    {
        Vector3 originalPos = shookCamera.transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            /*x = Random.Range(0, 1) > 0.5f ? x * -1 : x * 1;
            y = Random.Range(0, 1) > 0.5f ? y * -1 : y * 1;*/

            shookCamera.transform.localPosition += new Vector3(x,y,0);

            yield return new WaitForSeconds(pause);
            shookCamera.transform.localPosition = new Vector3(0,5,-5);
            elapsed += Time.deltaTime;
        }
    }

}
