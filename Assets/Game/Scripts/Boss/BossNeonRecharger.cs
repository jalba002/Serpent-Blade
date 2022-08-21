using System.Collections;
using UnityEngine;

public class BossNeonRecharger : MonoBehaviour
{
    public float RechargeDuration = 7f;
    public Renderer NeonHealthBarRenderer;
    private float currentCharge = 50f;

    public void DecreaseNeonCharge(float value)
    {
        currentCharge = Mathf.Clamp(currentCharge - value, 0, 50);
        UpdateNeonCharge(currentCharge/50f);
    }

    private void UpdateNeonCharge(float newValue)
    {
        NeonHealthBarRenderer.materials[4].SetFloat("_Energy", newValue);
    }

    public void Recharge()
    {
        StartCoroutine(RechargeCoroutine(RechargeDuration));
    }

    IEnumerator RechargeCoroutine(float duration)
    {
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;

            currentCharge = counter / duration;
            NeonHealthBarRenderer.materials[4].SetFloat("_Energy", currentCharge);

            yield return null;
        }
    }
}
