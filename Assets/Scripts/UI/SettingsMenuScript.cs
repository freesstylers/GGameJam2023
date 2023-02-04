using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Audio; //No se hasta que punto esto va con FMOD, pero ahi está
using UnityEngine.UI;

public class SettingsMenuScript : MonoBehaviour
{
    //Texts
    public TMPro.TextMeshProUGUI fullScreen;
    public TMPro.TextMeshProUGUI resolution;
    public TMPro.TextMeshProUGUI musicVolume;
    public TMPro.TextMeshProUGUI sfxVolume;
    //public TMPro.TextMeshProUGUI inputMode;
    //public Text language;
    public TMPro.TextMeshProUGUI FPSLimit;
    public TMPro.TextMeshProUGUI VSync;

    private void Start()
    {
        StartText();
    }

    protected void StartText()
    {
        FPSLimit.text = GameManager.instance.optionsLoader.FPSLimits[GameManager.instance.optionsLoader.FPSLimitIterator].ToString();

        musicVolume.text = GameManager.instance.optionsLoader.currentMusicVolume.ToString();

        sfxVolume.text = GameManager.instance.optionsLoader.currentSFX_Volume.ToString();

        //updateLanguageText();
    }

    public void changeFullScreen()
    {
        GameManager.instance.optionsLoader.fullscreen = !GameManager.instance.optionsLoader.fullscreen;

        if (GameManager.instance.optionsLoader.fullscreen)
        {
            //var test = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("UI Text", "Yes");

            //if (test.IsDone)
            //{
            //    fullScreen.text = test.Result;
            //}
            //else
            //    test.Completed += (test1) => fullScreen.text = test.Result;
            fullScreen.text = "Yes";
        }
        else
        {
            //var test = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("UI Text", "No");

            //if (test.IsDone)
            //{
            //    fullScreen.text = test.Result;
            //}
            //else
            //    test.Completed += (test1) => fullScreen.text = test.Result;
            fullScreen.text = "No";
        }

        Screen.fullScreen = GameManager.instance.optionsLoader.fullscreen;
    }

    public void lessResolution()
    {
        if (GameManager.instance.optionsLoader.currentResolutionIndex > 0)
            GameManager.instance.optionsLoader.currentResolutionIndex -= 1;
        else
            GameManager.instance.optionsLoader.currentResolutionIndex = GameManager.instance.optionsLoader.resolutions.GetLength(0) - 1;

        SetResolution(GameManager.instance.optionsLoader.currentResolutionIndex);
        resolution.text = GameManager.instance.optionsLoader.resolutionsString[GameManager.instance.optionsLoader.currentResolutionIndex];
    }

    public void moreResolution()
    {
        if (GameManager.instance.optionsLoader.currentResolutionIndex < (GameManager.instance.optionsLoader.resolutions.GetLength(0) - 1))
            GameManager.instance.optionsLoader.currentResolutionIndex += 1;
        else
            GameManager.instance.optionsLoader.currentResolutionIndex = 0;

        SetResolution(GameManager.instance.optionsLoader.currentResolutionIndex);
        resolution.text = GameManager.instance.optionsLoader.resolutionsString[GameManager.instance.optionsLoader.currentResolutionIndex];
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = GameManager.instance.optionsLoader.resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void changeVSync()
    {
        if (GameManager.instance.optionsLoader.vSyncOn) //Pasa a false
        {
            QualitySettings.vSyncCount = 0; //Dont Sync
            GameManager.instance.optionsLoader.vSyncOn = false;

            //var test = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("UI Text", "No");

            //if (test.IsDone)
            //{
            //    VSync.text = test.Result;
            //}

            VSync.text = "No";

            //Enable de FPSLimit
        }
        else //Pasa a true
        {
            QualitySettings.vSyncCount = 1; //Sync a lo que vaya el monitor
            GameManager.instance.optionsLoader.vSyncOn = true;

            //var test = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("UI Text", "Yes");

            //if (test.IsDone)
            //{
            //    VSync.text = test.Result;
            //}

            VSync.text = "Yes";
            //Disable de FPSLimit
        }
    }

    public void moreFPSLimit()
    {
        if (GameManager.instance.optionsLoader.FPSLimitIterator < GameManager.instance.optionsLoader.FPSLimits.GetLength(0) - 1)
        {
            GameManager.instance.optionsLoader.FPSLimitIterator += 1;
        }
        else
        {
            GameManager.instance.optionsLoader.FPSLimitIterator = 0;
        }

        Application.targetFrameRate = GameManager.instance.optionsLoader.FPSLimits[GameManager.instance.optionsLoader.FPSLimitIterator];

        updateFPSLimitText();
    }

    public void lessFPSLimit()
    {
        if (GameManager.instance.optionsLoader.FPSLimitIterator > 0)
        {
            GameManager.instance.optionsLoader.FPSLimitIterator -= 1;
        }
        else
        {
            GameManager.instance.optionsLoader.FPSLimitIterator = GameManager.instance.optionsLoader.FPSLimits.GetLength(0) - 1;
        }

        Application.targetFrameRate = GameManager.instance.optionsLoader.FPSLimits[GameManager.instance.optionsLoader.FPSLimitIterator];

        updateFPSLimitText();
    }

    void updateFPSLimitText()
    {
        FPSLimit.text = (GameManager.instance.optionsLoader.FPSLimits[GameManager.instance.optionsLoader.FPSLimitIterator]).ToString();
    }

    public void moreSFX_Volume()
    {
        if (GameManager.instance.optionsLoader.currentSFX_Volume <= 95)
        {
            GameManager.instance.optionsLoader.currentSFX_Volume += 5;
            //options.sfx.setVolume(options.currentSFX_Volume / 100.0f);

            sfxVolume.text = GameManager.instance.optionsLoader.currentSFX_Volume.ToString(); //UpdateText
        }
        else
        {

        }
    }

    public void lessSFX_Volume()
    {
        if (GameManager.instance.optionsLoader.currentSFX_Volume >= 5)
        {
            GameManager.instance.optionsLoader.currentSFX_Volume -= 5;
            //options.sfx.setVolume(options.currentSFX_Volume / 100.0f);

            sfxVolume.text = GameManager.instance.optionsLoader.currentSFX_Volume.ToString(); //UpdateText
        }
        else
        {

        }
    }

    public void moreMusicVolume()
    {
        if (GameManager.instance.optionsLoader.currentMusicVolume <= 95)
        {
            GameManager.instance.optionsLoader.currentMusicVolume += 5;
            //options.music.setVolume(options.currentMusicVolume / 100.0f);

            musicVolume.text = GameManager.instance.optionsLoader.currentMusicVolume.ToString(); //UpdateText
        }
        else
        {

        }
    }

    public void lessMusicVolume()
    {
        if (GameManager.instance.optionsLoader.currentMusicVolume >= 5)
        {
            GameManager.instance.optionsLoader.currentMusicVolume -= 5;
            //options.music.setVolume(options.currentMusicVolume / 100.0f);

            musicVolume.text = GameManager.instance.optionsLoader.currentMusicVolume.ToString(); //UpdateText
        }
        else
        {

        }
    }

    //public void lessLanguage()
    //{
    //    if (options.languageIterator > 0)
    //    {
    //        options.languageIterator -= 1;
    //    }
    //    else
    //    {
    //        options.languageIterator = options.languageMax;
    //    }

    //    LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[options.languageIterator];

    //    updateLanguageText();
    //}

    //public void moreLanguage()
    //{
    //    if (options.languageIterator < options.languageMax)
    //    {
    //        options.languageIterator += 1;
    //    }
    //    else
    //    {
    //        options.languageIterator = 0;
    //    }

    //    LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[options.languageIterator];

    //    updateLanguageText();
    //}

    ////Las 1/2 primeras veces que se actualiza el texto va regular, luego bien??
    ////No se si es por el aSync o otra cosa, asi que habrá que testear

    ////Hay veces que falla por el aSync 100% (a veces, si se pone FullScreen a "Si" y se cambia de idioma, se queda en "Si" en vez de "Yes"
    //void updateLanguageText()
    //{
    //    if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])//Ingles
    //    {
    //        var test = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("UI Text", "English");

    //        if (test.IsDone)
    //        {
    //            language.text = test.Result;
    //        }
    //        else
    //            test.Completed += (t) => language.text = test.Result;
    //    }
    //    else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1]) //Español
    //    {
    //        var test = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("UI Text", "Castellano");

    //        if (test.IsDone)
    //        {
    //            language.text = test.Result;
    //        }
    //        else
    //            test.Completed += (t) => language.text = test.Result;
    //    }

    //    if (options.fullscreen)
    //    {
    //        var test = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("UI Text", "Yes");

    //        if (test.IsDone)
    //        {
    //            fullScreen.text = test.Result;
    //        }
    //        else
    //            test.Completed += (t) => fullScreen.text = test.Result;
    //    }
    //    else
    //    {
    //        var test = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("UI Text", "No");

    //        if (test.IsDone)
    //        {
    //            fullScreen.text = test.Result;
    //        }
    //        else
    //            test.Completed += (t) => fullScreen.text = test.Result;
    //    }

    //    resolution.text = options.resolutionsString[options.currentResolutionIndex];

    //    if (options.vSyncOn)
    //    {
    //        var test = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("UI Text", "Yes");

    //        if (test.IsDone)
    //        {
    //            VSync.text = test.Result;
    //        }
    //        else
    //            test.Completed += (t) => VSync.text = test.Result;
    //    }
    //    else
    //    {
    //        var test = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("UI Text", "No");

    //        if (test.IsDone)
    //        {
    //            VSync.text = test.Result;
    //        }
    //        else
    //            test.Completed += (t) => VSync.text = test.Result;
    //    }
    //}

    public void SaveSettings()
    {
        GameManager.instance.optionsLoader.SaveSettings();
    }
}