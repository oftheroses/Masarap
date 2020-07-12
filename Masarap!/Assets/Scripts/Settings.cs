using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Settings : MonoBehaviour {

    public Player player;
    public SpreadManager sm;

    public Toggle enableTooltip;

    public TextMeshProUGUI musicText;
    public Slider musicSlider;

    public TextMeshProUGUI SFXText;
    public Slider SFXSlider;

    public TextMeshProUGUI Delica;
    public Material DelicaOrange;
    public Material DelicaTan;

    public TextMeshProUGUI Dyslexic;
    public Material DyslexicOrange;
    public Material DyslexicTan;

    public bool openDyslexic;

    public List<GameObject> dyslexicSpread;

    public List<GameObject> delicaSpread;

    void Awake() {
        if (player.tooltipEnabled == true) {
            enableTooltip.isOn = true;
        }
        else if (player.tooltipEnabled == false) {
            enableTooltip.isOn = false;
        }

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

    #region levels
    public void SpreadOne() {
        sm.currentSpread = 1;
    }

    public void SpreadTwo() {
        sm.currentSpread = 2;
    }

    public void SpreadThree() {
        sm.currentSpread = 3;
    }

    public void SpreadFour() {
        sm.currentSpread = 4;
    }

    public void SpreadFive() {
        sm.currentSpread = 5;
    }

    public void SpreadSix() {
        sm.currentSpread = 6;
    }

    public void SpreadSeven() {
        sm.currentSpread = 7;
    }
    #endregion

    public void Tooltips() {
        if (enableTooltip.isOn) {
            player.tooltipEnabled = true;
        }
        else if (!enableTooltip.isOn) {
            player.tooltipEnabled = false;
        }
    }

    public void Music() {
        player.musicVolume = musicSlider.value;
        musicText.text = "Music Volume: " + musicSlider.value.ToString() + "%";
    }

    public void SFX() {
        player.SFXVolume = SFXSlider.value;
        SFXText.text = "SFX Volume: " + SFXSlider.value.ToString() + "%";
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

            Dyslexic.fontSharedMaterial = DyslexicOrange;
            Dyslexic.fontStyle = FontStyles.Underline;

            Delica.fontSharedMaterial = DelicaTan;
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

            Delica.fontSharedMaterial = DelicaOrange;
            Delica.fontStyle = FontStyles.Underline;

            Dyslexic.fontSharedMaterial = DyslexicTan;
            Dyslexic.fontStyle = FontStyles.Normal;
        }
    }
}
