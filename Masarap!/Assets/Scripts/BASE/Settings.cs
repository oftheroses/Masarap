using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class Settings : MonoBehaviour {

    public Player player;

    public Toggle enableTooltip;

    public byte tooltipTransparency;

    public TextMeshProUGUI TooltipTextDelica;
    public TextMeshProUGUI TooltipTextDyslexic;
    public Slider TooltipTransSlider;

    public TextMeshProUGUI musicTextDelica;
    public TextMeshProUGUI musicTextDyslexic;
    public Slider musicSlider;

    public TextMeshProUGUI SFXTextDelica;
    public TextMeshProUGUI SFXTextDyslexic;
    public Slider SFXSlider;

    public TextMeshProUGUI Delica;

    public TextMeshProUGUI Dyslexic;

    public bool openDyslexic;

    public List<GameObject> dyslexicSpread;

    public List<GameObject> delicaSpread;

    void Awake() {
        player = FindObjectOfType<Player>();
        if (player.tooltipEnabled == true) {
            enableTooltip.isOn = true;
        }
        else if (player.tooltipEnabled == false) {
            enableTooltip.isOn = false;
        }

        TooltipTrans();
        Music();
        SFX();

        if (player.openDyslexic == true) {
            foreach (GameObject open in dyslexicSpread) {
                open.SetActive(true);
            }
            foreach (GameObject denne in delicaSpread) {
                denne.SetActive(false);
            }
        }
        else if (player.openDyslexic == false) {
            foreach (GameObject open in dyslexicSpread) {
                open.SetActive(false);
            }
            foreach (GameObject denne in delicaSpread) {
                denne.SetActive(true);
            }
        }
    }

    public void Tooltips() {
        if (enableTooltip.isOn) {
            player.tooltipEnabled = true;
        }
        else if (!enableTooltip.isOn) {
            player.tooltipEnabled = false;
        }
    }

    public void TooltipTrans() {
        // convert tooltip slider's FLOAT to a BYTE
        tooltipTransparency = Convert.ToByte(TooltipTransSlider.value);

        // tell player.tooltip to equal it
        player.tooltipBGTransparency = tooltipTransparency;
        

        TooltipTextDelica.text = "Tooltip transparency: " + TooltipTransSlider.value.ToString();
        TooltipTextDyslexic.text = "Tooltip transparency: " + TooltipTransSlider.value.ToString();
    }

    public void Music() {
        player.musicVolume = musicSlider.value;
        musicTextDelica.text = "BG music: " + musicSlider.value.ToString() + "%";
        musicTextDyslexic.text = "BG music: " + musicSlider.value.ToString() + "%";
    }

    public void SFX() {
        player.SFXVolume = SFXSlider.value;
        SFXTextDelica.text = "SFX: " + SFXSlider.value.ToString() + "%";
        SFXTextDyslexic.text = "SFX: " + SFXSlider.value.ToString() + "%";
    }

    public void OpenDyslexic() {
        // font is DenneDelica. Set it to OpenDyslexic
        if (player.openDyslexic == false) {
            player.openDyslexic = true;

            foreach (GameObject open in dyslexicSpread) {
                open.SetActive(true);
            }
            foreach (GameObject denne in delicaSpread) {
                denne.SetActive(false);
            }
            
            Dyslexic.fontStyle = FontStyles.Bold;

            Delica.fontStyle = FontStyles.Normal;
        }
    }

    public void DenneDelica() {
        // font is OpenDyslexic. Set it to DenneDelica
        if (player.openDyslexic == true) {
            player.openDyslexic = false;

            foreach (GameObject open in dyslexicSpread) {
                open.SetActive(false);
            }
            foreach (GameObject denne in delicaSpread) {
                denne.SetActive(true);
            }

            Delica.fontStyle = FontStyles.Bold;

            Dyslexic.fontStyle = FontStyles.Normal;
        }
    }
}
