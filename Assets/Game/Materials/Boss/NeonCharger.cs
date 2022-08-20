using UnityEngine;

public class NeonCharger : MonoBehaviour
{
    MaterialPropertyBlock propertyBlock;
    Renderer neonRenderer;

    private void Awake()
    {
        propertyBlock = new MaterialPropertyBlock();
        neonRenderer = GetComponent<Renderer>();
    }

    public void SetAnimationDuration(float animationDuration)
    {
        propertyBlock.SetFloat("_AnimationDuration", animationDuration);
        neonRenderer.SetPropertyBlock(propertyBlock);
    }

    public void SetMaterialTimer(float waitTime)
    {
        propertyBlock.SetFloat("_AnimationTimer", Time.time + waitTime);
        neonRenderer.SetPropertyBlock(propertyBlock);
    }
}