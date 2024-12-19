using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Rendering;

public class SlowTimeVolume : MonoBehaviour
{
    [SerializeField] Volume volume;
    [SerializeField] float duration;
    private Coroutine coroutine;
    public void Enable()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(SetVolumeWeight(1));
    }
    public void Disable()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(SetVolumeWeight(0));
    }

    private IEnumerator SetVolumeWeight(float value)
    {
        float time = 0;
        float startValue = volume.weight;
        while (time < duration)
        {
            time += Time.deltaTime;
            volume.weight = Mathf.Lerp(startValue, value, time / duration);
            yield return null;
        }
    }
}
