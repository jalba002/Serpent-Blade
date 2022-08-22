using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSoundPlayer : MonoBehaviour
{
    [SerializeField] StudioEventEmittersList soundList;

    public void PlaySpawnSound()
    {
        soundList.spawnAudioRef.Play();
    }

    public void PlayScreamSound()
    {
        soundList.screamAudioRef.Play();
    }

    public void PlayLaserScreamSound()
    {
        soundList.screamLaserAudioRef.Play();
    }

    public void PlayLaserScreamSound2()
    {
        soundList.screamLaser2AudioRef.Play();
    }

    public void PlayScreamThunderSound()
    {
        soundList.screamThunderAudioRef.Play();
    }

    public void PlayDechargeSound()
    {
        soundList.dechargeAudioRef.Play();
    }

    public void PlayChargeSound()
    {
        soundList.chargeAudioRef.Play();
    }

    public void PlayHeadSlamSound()
    {
        soundList.headSlamAudioRef.Play();
    }

    public void PlayDeathScreamSound()
    {
        soundList.deathScreamAudioRef.Play();
    }

    public void PlayDamageSound()
    {
        soundList.damageAudioRef.Play();
    }
}
