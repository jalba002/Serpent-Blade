using System.Collections;
using UnityEngine;

public class CanvasFadeIn : MonoBehaviour
{
    public float Duration = 0.4f;
    public float Delay = 0f;
    private CanvasGroup canvGroup;

    public void Awake()
    {
        canvGroup = GetComponent<CanvasGroup>();
    }

    public void FadeIn()
    {
        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, 1));
    }

    public void FadeOut()
    {
        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, 0));
    }

    public IEnumerator DoFade(CanvasGroup canvGroup, float start, float end)
    {
        float counter = 0f;

        yield return new WaitForSeconds(Delay);

        if(end == 1)
        {
            canvGroup.interactable = true;
            canvGroup.blocksRaycasts = true;
            yield return new WaitForSeconds(0.7f);
        }
        else
        {
            canvGroup.interactable = false;
            canvGroup.blocksRaycasts = false;
        }

        yield return new WaitForSeconds(0.2f);

        while (counter < Duration)
        {
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / Duration);

            yield return null;
        }
    }
}
