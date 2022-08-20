using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHealthManager : HealthManager
{
    public float InvulnerableTime = 1f;
    public ScreenShake mainCameraShake;

    public float CameraShakeDuration = 0.15f;
    public float CameraShakeIntensity = 5f;

    //public VolumeProfile PostProcessVolume;

    public override void Die()
    {
        
    }

    public override void DamageFeedback()
    {
        InvulnerableOverTime(InvulnerableTime);
        //DamageSoundRef.Play();
        mainCameraShake.CameraShake(CameraShakeDuration, CameraShakeIntensity);
    }
}
