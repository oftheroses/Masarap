using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class Taglish : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    #region BASIC
    /* TAGLISH - translates from TL -> EN -> CB
     * (1) - POINTER ENTER: instantiate tooltip box, cast as "spawnedTT"
     *       which follows mouse
     *       
     * (2) - POINTER EXIT: destroy "spawnedTT"
     * 
     * (3) - LEFTCLICK: translates TL -> EN -> CB
     * 
     * (4) - RIGHTCLICK: translates TL <- EN <- CB
     *  
     * language int:
     * 0 tagalog
     * 1 english
     * 2 cebuano
     * 
     * font int:
     * 0 calibri
     * 1 denne delica
     * 2 opendyslexic
     */

    public Player player;
    public AudioManager AM;
    public TextMeshProUGUI baseText;

    public int languageInt;
    public int fontInt;

    public bool tooltipEnabled;
    public bool tooltipActive = false;

    public TMP_FontAsset calibri;
    public TMP_FontAsset openDyslexic;
    public bool dyslexicTooltip; // ping's player settings to set tooltip font calibri/opendyslexic

    public string tagalog;
    public string english;
    public string cebuano;

    public Material calibriTag;
    public Material calibriEng;
    public Material calibriCeb;

    public Material delicaTag;
    public Material delicaEng;
    public Material delicaCeb;

    public Material dyslexicTag;
    public Material dyslexicEng;
    public Material dyslexicCeb;

    public GameObject tooltip; // tooltip prefab
    private GameObject spawnedTT; // declaring spawned TT to move it's position
    private TextMeshProUGUI spawnedTText; // declaring spawnedTT text so it can be translated
    #endregion

    // on awake, set text to TL / EN / CB
    void Awake() {
        TextUpdater();
        Settings(); // Ping user's settings whether TT is enabled
    }

    void TextUpdater() {

        // base text: tagalog & TT text: english
        if (languageInt == 0) {
            baseText.text = tagalog;

            if (fontInt == 0) {
                baseText.fontSharedMaterial = calibriTag;
            }
            else if (fontInt == 1) {
                baseText.fontSharedMaterial = delicaTag;
            }
            else if (fontInt == 3) {
                baseText.fontSharedMaterial = dyslexicTag;
            }
        }

        // base text: english & TT text: cebuano
        else if (languageInt == 1) {
            baseText.text = english;

            if (fontInt == 0) {
                baseText.fontSharedMaterial = calibriEng;
            }
            else if (fontInt == 1) {
                baseText.fontSharedMaterial = delicaEng;
            }
            else if (fontInt == 3) {
                baseText.fontSharedMaterial = dyslexicEng;
            }
        }

        // base text: cebuano & TT text: tagalog
        else if (languageInt == 2) {
            baseText.text = cebuano;

            if (fontInt == 0) {
                baseText.fontSharedMaterial = calibriCeb;
            }
            else if (fontInt == 1) {
                baseText.fontSharedMaterial = delicaCeb;
            }
            else if (fontInt == 3) {
                baseText.fontSharedMaterial = dyslexicCeb;
            }
        }
    }

    // same as TextUpdater, but for tooltip
    public void TTUpdater() {
        if (dyslexicTooltip == true) {
            spawnedTText.font = openDyslexic;
        }
        else if (dyslexicTooltip == false) {
            spawnedTText.font = calibri;
        }

        // base text: tagalog & TT text: english
        if (languageInt == 0) {
            spawnedTText.text = english;

            if (dyslexicTooltip == false) {
                spawnedTText.fontSharedMaterial = calibriEng;
            }
            else if (dyslexicTooltip == true) {
                spawnedTText.fontSharedMaterial = dyslexicEng;
            }
        }

        // base text: english & TT text: cebuano
        else if (languageInt == 1) {
                 spawnedTText.text = cebuano;

             if (dyslexicTooltip == false) {
                spawnedTText.fontSharedMaterial = calibriCeb;
             }
             else if (dyslexicTooltip == true) {
                spawnedTText.fontSharedMaterial = dyslexicCeb;
             }
        }

        // base text: cebuano & TT text: tagalog
        else if (languageInt == 2) {
                spawnedTText.text = tagalog;

                if (dyslexicTooltip == false) {
                spawnedTText.fontSharedMaterial = calibriTag;
                }
                else if (dyslexicTooltip == true) {
                spawnedTText.fontSharedMaterial = dyslexicTag;
                }
        }
    }

    public void Settings() {
        dyslexicTooltip = player.openDyslexic;

        if (player.tooltipEnabled == true) {
            tooltipEnabled = true;
        }

        if (player.tooltipEnabled == false) {
            tooltipEnabled = false;
        }
    }

    // POINTER ENTER - spawn tooltip
    public void OnPointerEnter(PointerEventData eventData) {
        Settings();

        AM.Play("Hover");

        if (tooltipEnabled == true) {
            spawnedTT = Instantiate(tooltip, new Vector3(0, 0, 0), Quaternion.identity);
            spawnedTT.transform.SetParent(this.transform, false); // making it the child of the base text

            spawnedTText = spawnedTT.GetComponentInChildren<TextMeshProUGUI>(); // spawnedTT is the BG, TMPro is in the child

            TTUpdater();

            tooltipActive = true;
        }
    }

    
    public void OnPointerExit(PointerEventData eventData) {

        if (tooltipActive == true) {
            Destroy(spawnedTT);
            tooltipActive = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData) {

        AM.Play("Click");
        // RIGHT CLICK - transl. downward
        if (eventData.button == PointerEventData.InputButton.Right) {
            // ONLY IF 0, 1, 2
            if (languageInt > 0) {
                languageInt--;
                TextUpdater();
                TTUpdater();
            }
        }

        // LEFT CLICK - transl. upward
        if (eventData.button == PointerEventData.InputButton.Left) {
            // WON'T GO below 0 or above 2
            if (languageInt < 2) {
                languageInt++;
                TextUpdater();
                TTUpdater();
            }
        }
    }
}
