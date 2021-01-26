using TMPro;
using System;
using Malee.List;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections.Generic;

public class Settings : MonoBehaviour {

    #region basic
    public Player player;

    public byte tooltipTransparency;

    public TextMeshProUGUI TooltipTextDelica;
    public TextMeshProUGUI TooltipTextDyslexic;
    public Slider TooltipTransSlider;

    public AudioMixer mixer;

    public TextMeshProUGUI musicTextDelica;
    public TextMeshProUGUI musicTextDyslexic;
    public Slider musicSlider;

    public TextMeshProUGUI SFXTextDelica;
    public TextMeshProUGUI SFXTextDyslexic;
    public Slider SFXSlider;

    public TextMeshProUGUI Delica;

    public TextMeshProUGUI Dyslexic;

    public bool openDyslexic;

    // page/img assets are handled in spreadmanager
    [Reorderable(paginate = true, pageSize = 20)]
    public GameObjectList dyslexicText;
    [System.Serializable]
    public class GameObjectList : ReorderableArray<GameObject> {
    }

    [Reorderable(paginate = true, pageSize = 20)]
    public GameObjectList delicaText;
    #endregion

    void Awake() {
        player = FindObjectOfType<Player>();

        TooltipTrans();
        MusicVar();
        SFXVar();

        if (player.openDyslexic == true) {
            openDyslexic = true;
            foreach (GameObject open in dyslexicText) {
                open.SetActive(true);
            }
            foreach (GameObject denne in delicaText) {
                denne.SetActive(false);
            }
        }
        else if (player.openDyslexic == false) {
            openDyslexic = false;
            foreach (GameObject open in dyslexicText) {
                open.SetActive(false);
            }
            foreach (GameObject denne in delicaText) {
                denne.SetActive(true);
            }
        }
    }

    public void TooltipTrans() {
        // convert tooltip slider's FLOAT to a BYTE
        tooltipTransparency = Convert.ToByte(TooltipTransSlider.value);

        // tell player.tooltip to equal it
        player.tooltipBGTransparency = tooltipTransparency;
        

        TooltipTextDelica.text = "Tooltip transparency: " + (TooltipTransSlider.value / 255).ToString("0%");
        TooltipTextDyslexic.text = "Tooltip transparency: " + (TooltipTransSlider.value / 100).ToString("0%");
    }

    public void Music(float bgmVal) {
        mixer.SetFloat("BGM", Mathf.Log10(bgmVal) * 20); // change the audio mixer with slider
    }

    public void MusicVar() {
        player.musicVolume = musicSlider.value; // change player with slider
        musicTextDelica.text = "BG music: " + musicSlider.value.ToString("0%"); // change text with slider
        musicTextDyslexic.text = "BG music: " + musicSlider.value.ToString("0%"); // change text with slider.
    }

    public void SFX(float sfxVal) {
        mixer.SetFloat("SFX", Mathf.Log10(sfxVal) * 20);
    }

    public void SFXVar() {
        player.SFXVolume = SFXSlider.value; // change player with slider
        SFXTextDelica.text = "SFX: " + SFXSlider.value.ToString("0%");
        SFXTextDyslexic.text = "SFX: " + SFXSlider.value.ToString("0%");
    }

    public void OpenDyslexic() {
        if (player.openDyslexic == false) {
            player.openDyslexic = true;

            foreach (GameObject open in dyslexicText) {
                open.SetActive(true);
            }
            foreach (GameObject denne in delicaText) {
                denne.SetActive(false);
            }
            
            Dyslexic.fontStyle = FontStyles.Bold;

            Delica.fontStyle = FontStyles.Normal;
        }
    }

    public void DenneDelica() {
        if (player.openDyslexic == true) {
            player.openDyslexic = false;

            foreach (GameObject open in dyslexicText) {
                open.SetActive(false);
            }
            foreach (GameObject denne in delicaText) {
                denne.SetActive(true);
            }

            Delica.fontStyle = FontStyles.Bold;

            Dyslexic.fontStyle = FontStyles.Normal;
        }
    }
}
