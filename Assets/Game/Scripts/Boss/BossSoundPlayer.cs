using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSoundPlayer : MonoBehaviour
{
    [SerializeField] StudioEventEmitter audioRef;
    [SerializeField] StudioEventEmitter mouthAudioRef;

    public string screamRefName;
    public string screamRefGuid;

    public string screamLaserRefName;
    public string screamLaserRefGuid;

    [SerializeField] StudioEventEmittersList soundList;

    public void PlaySpawnSound()
    {
        //spawnAudioRef.Play();
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
        //spawnAudioRef.Play();
    }

    public void PlayChargeSound()
    {
        //spawnAudioRef.Play();
    }
}
