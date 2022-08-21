using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSoundPlayer : MonoBehaviour
{
    [SerializeField] StudioEventEmitter spawnAudioRef;
    [SerializeField] StudioEventEmitter screamAudioRef;
    [SerializeField] StudioEventEmitter dechargeAudioRef;
    [SerializeField] StudioEventEmitter chargeAudioRef;
    [SerializeField] StudioEventEmitter projectile1AudioRef;
    [SerializeField] StudioEventEmitter projectile2AudioRef;
    [SerializeField] StudioEventEmitter thunderAudioRef;
    [SerializeField] StudioEventEmitter electricityAudioRef;
}
