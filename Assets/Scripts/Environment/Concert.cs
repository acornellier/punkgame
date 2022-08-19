using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Concert : MonoBehaviour
{
    [SerializeField] Light2D globalLight;
    [SerializeField] Light2D stageLight;

    float _initialGlobalLightIntensity;
    float _initialStageLightIntensity;

    void Start()
    {
        _initialGlobalLightIntensity = globalLight.intensity;
        _initialStageLightIntensity = stageLight.intensity;
        stageLight.intensity = 0;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.GetComponent<Player>();
        if (!player)
            return;

        player.Dance();
        StartCoroutine(FadeLights(globalLight, 0.3f));
        StartCoroutine(FadeLights(stageLight, _initialStageLightIntensity));
    }

    void OnTriggerExit2D(Collider2D col)
    {
        var player = col.GetComponent<Player>();
        if (!player)
            return;

        player.StopDance();
        StartCoroutine(FadeLights(globalLight, _initialGlobalLightIntensity));
        StartCoroutine(FadeLights(stageLight, 0f));
    }

    static IEnumerator FadeLights(Light2D light, float to)
    {
        var initialIntensity = light.intensity;

        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime;
            light.intensity = Mathf.Lerp(initialIntensity, to, t);
            yield return null;
        }
    }
}