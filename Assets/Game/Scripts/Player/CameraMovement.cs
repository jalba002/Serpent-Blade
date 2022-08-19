using Cinemachine;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private CinemachineOrbitalTransposer virtualCameraOrbitalTransposer;
    public Transform center;
    public Transform player;

    private float currentCameraAngle = 0f;
    public float CameraLerpSpeed = 1f;

    void Awake()
    {
        virtualCameraOrbitalTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineOrbitalTransposer>();
    }

    void Update()
    {
        var playerPos = player.position;
        playerPos.y = 0;
        var centerPos = center.position;
        centerPos.y = 0;

        Vector3 direction = (playerPos - centerPos).normalized;
        float angle = Vector3.Angle(direction, center.forward);
        Vector3 cross = Vector3.Cross(direction, center.forward);

        if (cross.y > 0) 
        {
            angle = -angle;
        }

        currentCameraAngle = Mathf.LerpAngle(currentCameraAngle, angle, Time.deltaTime * CameraLerpSpeed);
        virtualCameraOrbitalTransposer.m_Heading.m_Bias = currentCameraAngle;
    }
}