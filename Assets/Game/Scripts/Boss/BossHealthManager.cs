using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BossHealthManager : HealthManager
{
    private MaterialPropertyBlock propertyBlock;
    public Renderer NeonHealthBarRenderer;
    private Animator animator;
    public CanvasFadeIn LoadingScreen;
    public CanvasFadeIn WinScreen;
    public string NextSceneName = "";
    private BossSoundPlayer sounds;

    private void Awake()
    {
        propertyBlock = new MaterialPropertyBlock();
        animator = GetComponent<Animator>();
        sounds = GetComponent<BossSoundPlayer>();
    }

    public void UpdateHealthBarMaterial()
    {
        propertyBlock.SetFloat("_CurrentHealth", currentHealth / MaxHealth);
        NeonHealthBarRenderer.SetPropertyBlock(propertyBlock);
    }

    public override void Die()
    {
        UpdateHealthBarMaterial();
        animator.SetTrigger("Death");
        sounds.PlayDamageSound();

        StartCoroutine(NextSceneCoroutine());
    }

    public override void DamageFeedback()
    {
        UpdateHealthBarMaterial();
        sounds.PlayDamageSound();
    }

    IEnumerator NextSceneCoroutine()
    {
        yield return new WaitForSeconds(2f);
        if (NextSceneName != "GameEnd")
            LoadingScreen.FadeIn();
        yield return new WaitForSeconds(1f);
        if (NextSceneName != "GameEnd")
            SceneManager.LoadScene(NextSceneName);
        else
        {
            WinScreen.FadeIn();
            Time.timeScale = 0f;
            var playerInput = FindObjectOfType<PlayerInputsManager>();
            playerInput.DisableInputsWin();
            playerInput.GameOver = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}