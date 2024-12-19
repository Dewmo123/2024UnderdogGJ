using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
public class SlowTimeCam : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virCamera;
    [SerializeField] float duration;
    [SerializeField] float addOrthographicSize = 3;
    [SerializeField] float defaultOrthographicSize = 5;
    private Coroutine coroutine;
    void OnEnable()
    {
        virCamera.m_Lens.OrthographicSize = defaultOrthographicSize;
    }
    public void Enable()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(SetVolumeWeight(defaultOrthographicSize + addOrthographicSize));
    }
    public void Disable()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(SetVolumeWeight(defaultOrthographicSize));
    }

    private IEnumerator SetVolumeWeight(float value)
    {
        float time = 0;
        float startValue = virCamera.m_Lens.OrthographicSize;
        while (time < duration)
        {
            time += Time.deltaTime;
            Debug.Log(time / duration);
            virCamera.m_Lens.OrthographicSize = Mathf.Lerp(startValue, value, time / duration);
            yield return null;
        }
    }
}
