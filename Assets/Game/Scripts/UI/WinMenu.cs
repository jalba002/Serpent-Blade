using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using FMODUnity;
//using FMODUnity;

public class WinMenu : MonoBehaviour
{
    private CanvasGroup canvas;
    public float Duration = 0.4f;

    //public StudioEventEmitter HoverSoundRef;
    //public StudioEventEmitter ClickSoundRef;
    public StudioEventEmitter BackgroundMusic;

    private readonly string twitterNameParameter = "Check this amazing game made by @andrew_raya @JordiAlbaDev @Sergisggs @GuillemLlovDev @Belmontes_ART for the #MiniJam113 ! Here the link: ";
    private readonly string twitterDescriptionParam = "";
    private readonly string twitterAdress = "https://twitter.com/intent/tweet";
    private readonly string miniGameJamLink = "https://andrew-raya.itch.io/cosmic-horror";

    private void Start()
    {
        canvas = GetComponent<CanvasGroup>();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadScene(string scene_name)
    {
        //BackgroundMusic.Stop();
        canvas.interactable = false;
        //LoadingScreen.FadeIn();
        StartCoroutine(LoadAfterFade(scene_name));
    }

    IEnumerator LoadAfterFade(string scene_name)
    {
        float counter = 0f;

        while (counter < Duration)
        {
            counter += Time.deltaTime;
            canvas.alpha = Mathf.Lerp(1, 0, counter / Duration);

            yield return null;
        }

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(scene_name);
    }

    public void PlayHoverSound()
    {
        //HoverSoundRef.Play();
    }

    public void PlayClickSound()
    {
        //ClickSoundRef.Play();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShareTwitter()
    {
        Application.OpenURL(twitterAdress + "?text=" + UnityWebRequest.EscapeURL(twitterNameParameter + "\n" + twitterDescriptionParam + "\n" + miniGameJamLink));
    }
}