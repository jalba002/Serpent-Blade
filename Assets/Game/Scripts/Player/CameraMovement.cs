using Cinemachine;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private CinemachineOrbitalTransposer virtualCameraOrbitalTransposer;
    public Transform center;
    public Transform player;

    private float currentCameraAngle = 0f;
    public float CameraHorizontalLerpSpeed = 1f;

    public float MaxZoom = -17f;
    public float MinZoom = -24f;

    private Vector3 followOffset;
    public float CameraVerticalLerpSpeed = 1f;

    void Awake()
    {
        virtualCameraOrbitalTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineOrbitalTransposer>();
    }

    private void Start()
    {
        followOffset = virtualCameraOrbitalTransposer.m_FollowOffset;
    }

    void Update()
    {
        var playerPos = player.position;
        playerPos.y = 0;
        var centerPos = center.position;
        centerPos.y = 0;

        Vector3 distance = playerPos - centerPos;
        Vector3 direction = distance.normalized;
        float angle = Vector3.Angle(direction, center.forward);
        Vector3 cross = Vector3.Cross(direction, center.forward);

        if (cross.y > 0) 
        {
            angle = -angle;
        }

        currentCameraAngle = Mathf.LerpAngle(currentCameraAngle, angle, Time.deltaTime * CameraHorizontalLerpSpeed);
        virtualCameraOrbitalTransposer.m_Heading.m_Bias = currentCameraAngle;

        var targetOffset = Mathf.Lerp(MinZoom, MaxZoom, (distance.magnitude - 10.8f) / 6.2f);
        followOffset.z = Mathf.Lerp(followOffset.z, targetOffset, Time.deltaTime * CameraVerticalLerpSpeed);
        virtualCameraOrbitalTransposer.m_FollowOffset = followOffset;
    }
}