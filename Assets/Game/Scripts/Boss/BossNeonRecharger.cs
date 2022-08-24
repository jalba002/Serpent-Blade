using System.Collections;
using UnityEngine;

public class BossNeonRecharger : MonoBehaviour
{
    public float RechargeDuration = 7f;
    public Renderer NeonHealthBarRenderer;
    private float currentCharge = 1;

    private Coroutine DechargeCoroutine;

    public void UpdateNeonCharge(float newValue)
    {
        DechargeCoroutine = StartCoroutine(DechargeCo(newValue));
    }

    public void Recharge()
    {
        StopCoroutine(DechargeCoroutine);
        StartCoroutine(RechargeCoroutine(RechargeDuration));
    }

    IEnumerator DechargeCo(float goal)
    {
        while (currentCharge > goal)
        {
            currentCharge -= Time.deltaTime / 5f;
            NeonHealthBarRenderer.materials[4].SetFloat("_Energy", currentCharge);
            yield return null;
        }

        currentCharge = goal;
    }

    IEnumerator RechargeCoroutine(float duration)
    {
        float counter = NeonHealthBarRenderer.materials[4].GetFloat("_Energy");

        yield return new WaitForSeconds(2f);

        while (counter < duration)
        {
            counter += Time.deltaTime;

            currentCharge = counter / duration;
            NeonHealthBarRenderer.materials[4].SetFloat("_Energy", currentCharge);

            yield return null;
        }

        currentCharge = 1;
        NeonHealthBarRenderer.materials[4].SetFloat("_Energy", currentCharge);
    }
}
