using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerHealthManager : HealthManager
{
    public float InvulnerableTime = 1f;
    public ScreenShake mainCameraShake;

    public float CameraShakeDuration = 0.15f;
    public float CameraShakeIntensity = 5f;

    public Slider HealthSlider;
    public Slider HealthSliderYellow;

    private Coroutine healthBarLerpCoroutine;
    private Coroutine healthBarYellowLerpCoroutine;
    //public VolumeProfile PostProcessVolume;

    public override void Start()
    {
        base.Start();

        HealthSlider.maxValue = MaxHealth;
        HealthSliderYellow.maxValue = MaxHealth;

        HealthSlider.value = MaxHealth;
        HealthSliderYellow.value = MaxHealth;
    }

    public override void Die()
    {
        
    }

    public override void DamageFeedback()
    {
        InvulnerableOverTime(InvulnerableTime);
        //DamageSoundRef.Play();
        mainCameraShake.CameraShake(CameraShakeDuration, CameraShakeIntensity);
        SmoothUpdateUI(currentHealth, -50f);
    }

    private void SmoothUpdateUI(float healthRemaining, float duration)
    {
        if (healthBarLerpCoroutine != null)
        {
            StopCoroutine(healthBarLerpCoroutine);
        }
        if (healthBarYellowLerpCoroutine != null)
        {
            StopCoroutine(healthBarYellowLerpCoroutine);
        }

        healthBarLerpCoroutine = StartCoroutine(LerpCoroutine(healthRemaining, duration, 0, HealthSlider));
        healthBarYellowLerpCoroutine = StartCoroutine(LerpCoroutine(healthRemaining, -25f, 1f, HealthSliderYellow));
    }

    IEnumerator LerpCoroutine(float end, float speed, float delay, Slider healthSlider)
    {
        float preChangeValue = healthSlider.value;

        yield return new WaitForSeconds(delay);

        while (preChangeValue > end)
        {
            preChangeValue += Time.deltaTime * speed;
            healthSlider.value = preChangeValue;

            yield return null;
        }

        healthSlider.value = end;
    }
}