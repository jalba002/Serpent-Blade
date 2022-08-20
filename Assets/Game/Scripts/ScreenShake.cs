using Cinemachine;
using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void CameraShake(float duration, float intensity)
    {
        StartCoroutine(CameraShakeCoroutine(duration, intensity));
    }

    private IEnumerator CameraShakeCoroutine(float duration, float intensity)
    {
        CinemachineBasicMultiChannelPerlin multiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            multiChannelPerlin.m_AmplitudeGain = intensity;

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        multiChannelPerlin.m_AmplitudeGain = 0f;
    }
}
