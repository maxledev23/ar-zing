using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAnimation : MonoBehaviour
{
     // GameObjects
    private GameObject LightsObject, LightObject;

    // The coroutine
    private IEnumerator coroutine;

    // Time to wait between lights
    public float waitTime;

    // Start is called before the first frame update
    void Start() {
        // Find the GameObject containing all the Lights
        LightsObject = gameObject;

        // Turning off all the lights
        for (int i = 0; i < LightsObject.transform.childCount; i++) {
            LightObject = LightsObject.transform.GetChild(i).gameObject;
            ChangeLightIntensity(LightObject, 0);
        }
        coroutine = AnimationCoroutine(waitTime);
        StartCoroutine(coroutine);
    }

    // Coroutine to loop trought the lights
    IEnumerator AnimationCoroutine(float waitTime) {
        int i = 0;
        while (true)
        {
            i = (i + 1) % LightsObject.transform.childCount;
            LightObject = LightsObject.transform.GetChild(i).gameObject;
            ChangeLightIntensity(LightObject, 1);

            yield return new WaitForSeconds(waitTime);
            ChangeLightIntensity(LightObject, 0);
        }
    }

    // Read the name of the method
    private void ChangeLightIntensity(GameObject LightObject, int intensity) {
        if (LightObject.GetComponent<Light>() != null) {
            LightObject.GetComponent<Light>().intensity = intensity;
        }
    }
}