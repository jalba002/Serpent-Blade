using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthManager : HealthManager
{
    private MaterialPropertyBlock propertyBlock;
    public Renderer NeonHealthBarRenderer;

    private void Awake()
    {
        propertyBlock = new MaterialPropertyBlock();
    }

    public void UpdateHealthBarMaterial()
    {
        propertyBlock.SetFloat("_CurrentHealth", currentHealth / MaxHealth);
        NeonHealthBarRenderer.SetPropertyBlock(propertyBlock);
    }

    public override void Die()
    {
        UpdateHealthBarMaterial();

    }

    public override void DamageFeedback()
    {
        UpdateHealthBarMaterial();
        Debug.Log(currentHealth);
    }
}
