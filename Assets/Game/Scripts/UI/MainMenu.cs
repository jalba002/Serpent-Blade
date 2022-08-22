﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using FMODUnity;
//using FMODUnity;

public enum MenuState
{
    Main,
    Options,
    Controls,
    Credits
}

public class MainMenu : MonoBehaviour
{
    private MenuState currentMenu = MenuState.Main;
    private MenuState previousMenu;

    private CanvasGroup canvas;
    public CanvasFadeIn LoadingScreen;
    public CanvasFadeIn OptionsCanvas;
    public CanvasFadeIn ControlsCanvas;
    public CanvasFadeIn CreditsCanvas;
    public float Duration = 0.4f;

    [SerializeField]
    private Button startGameButton;

    [SerializeField]
    private Button optionsButton;

    [SerializeField]
    private Button creditsButton;

    [SerializeField]
    private Button quitGameButton;

    [SerializeField]
    private Button shareButton;

    [SerializeField]
    private GameStateCanvasTable gameStateCanvasTable;

    [SerializeField]
    private GameStateButtonTable gameStateButtonTable;

    public Animator protagonistAnimator;

    //public StudioEventEmitter HoverSoundRef;
    //public StudioEventEmitter ClickSoundRef;
    public StudioEventEmitter BackgroundMusic;
    public StudioEventEmitter SlashRef;

    private readonly string twitterNameParameter = "Check this amazing game made by @andrew_raya @JordiAlbaDev @Sergisggs @GuillemLlovDev @Belmontes_ART for the #MiniJam113 ! Here the link: ";
    private readonly string twitterDescriptionParam = "";
    private readonly string twitterAdress = "https://twitter.com/intent/tweet";
    private readonly string miniGameJamLink = "https://andrew-raya.itch.io/serpent-blade";

    private void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        LoadingScreen.gameObject.SetActive(true);
        LoadingScreen.GetComponent<CanvasFadeIn>().FadeOut();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameStateCanvasTable.Add(MenuState.Main, GetComponent<CanvasFadeIn>());
        gameStateCanvasTable.Add(MenuState.Options, OptionsCanvas);
        gameStateCanvasTable.Add(MenuState.Controls, ControlsCanvas);
        gameStateCanvasTable.Add(MenuState.Credits, CreditsCanvas);

        foreach (var item in gameStateCanvasTable)
        {
            if (item.Key == MenuState.Main)
                item.Value.FadeIn();
            else
                item.Value.FadeOut();
        }
    }

    public void LoadScene(string scene_name)
    {
        BackgroundMusic.Stop();
        canvas.interactable = false;
        LoadingScreen.FadeIn();
        protagonistAnimator.SetTrigger("Start");
        SlashRef.Play();
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

    public void MainMenuFade()
    {
        SetCanvas(MenuState.Main);
    }

    public void ControlsMenu()
    {
        SetCanvas(MenuState.Controls);
    }

    public void SetCanvas(MenuState newMenu)
    {
        previousMenu = currentMenu;
        currentMenu = newMenu;

        gameStateCanvasTable[newMenu].FadeIn();
        gameStateCanvasTable[previousMenu].FadeOut();
    }

    public void OptionsMenu()
    {
        SetCanvas(MenuState.Options);
    }

    public void CreditsMenu()
    {
        SetCanvas(MenuState.Credits);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShareTwitter()
    {
        Application.OpenURL(twitterAdress + "?text=" + UnityWebRequest.EscapeURL(twitterNameParameter + "\n" + twitterDescriptionParam + "\n" + miniGameJamLink));
    }

    [Serializable]
    public class GameStateCanvasTable : UnitySerializedDictionary<MenuState, CanvasFadeIn> { }

    [Serializable]
    public class GameStateButtonTable : UnitySerializedDictionary<MenuState, ButtonsList> { }

    [Serializable]
    public class ButtonsList
    {
        public List<Button> buttonsList;
    }
}