using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
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
     * (3) - RIGHTCLICK: translates TL -> EN -> CB
     * 
     * (4) - LEFTCLICK: translates TL <- EN <- CB
     *  
     * language int:
     * 0 tagalog
     * 1 english
     * 2 cebuano
     * 
     * font int:
     * 0 custom
     * 1 dyslexic
     * 
     * i commit some serious sins in this script, beware
     */

    private Player player;
    private AudioManager AM;
    public TextMeshProUGUI baseText;

    public List<Taglish> counterparts;

    public int languageInt;
    public int fontInt;

    private bool tooltipActive = false;
    private bool dyslexicTooltip; // ping's player settings to set tooltip font calibri/opendyslexic
    public GameObject tooltip; // tooltip prefab
    private GameObject spawnedTT; // declaring spawned TT to move it's position
    private TextMeshProUGUI spawnedTText; // declaring spawnedTT text so it can be translated
    private byte tooltipAlpha;
    private VerticalLayoutGroup tooltipVLG; // terrible. pure shit coding going on here.
    private bool isTooltip;
    [TextArea]
    public string tooltipTag;
    [TextArea]
    public string tooltipEng;
    [TextArea]
    public string tooltipCeb;
    // tooltip fonts
    public TMP_FontAsset calibri;
    public TMP_FontAsset openDyslexic;

    public string tagalog;
    public string english;
    public string cebuano;

    public Material calibriTag;
    public Material calibriEng;
    public Material calibriCeb;

    public Material fontTag;
    public Material fontEng;
    public Material fontCeb;

    public Material dyslexicTag;
    public Material dyslexicEng;
    public Material dyslexicCeb;

    private Image tooltipImg;
    private Color32 tagColor;
    private Color32 engColor;
    private Color32 cebColor;

    public bool disableRightClick = false;
    public bool disableLeftClick = false;
    public bool disableAll = false;
    #endregion

    public void Awake() {
        AM = FindObjectOfType<AudioManager>();
        player = FindObjectOfType<Player>();
    }

    public void Start() {
        TextUpdater();
        Settings(); // Ping user's settings whether TT is enabled
    }

    public void TextUpdater() {
        if (languageInt == 0) {
            disableLeftClick = true;
            if (!string.IsNullOrEmpty(english) || !string.IsNullOrEmpty(cebuano) && disableRightClick == true) {
                disableRightClick = false;
            }
            else if (string.IsNullOrEmpty(english) && string.IsNullOrEmpty(cebuano)) {
                disableRightClick = true;
            }

            baseText.text = tagalog;

            if (fontInt == 0) {
                baseText.fontSharedMaterial = fontTag;
            }
            else if (fontInt == 1) {
                baseText.fontSharedMaterial = dyslexicTag;
            }
        }

        else if (languageInt == 1) {
            if (!string.IsNullOrEmpty(tagalog) && disableLeftClick == true) {
                disableLeftClick = false;
            }
            else if (string.IsNullOrEmpty(tagalog)) {
                disableLeftClick = true;
            }

            if (string.IsNullOrEmpty(cebuano)) {
                disableRightClick = true;
            }
            else if (!string.IsNullOrEmpty(cebuano) && disableRightClick == true) {
                disableRightClick = false;
            }

            baseText.text = english;

            if (fontInt == 0) {
                baseText.fontSharedMaterial = fontEng;
            }
            else if (fontInt == 1) {
                baseText.fontSharedMaterial = dyslexicEng;
            }
        }

        else if (languageInt == 2) {
            disableRightClick = true;
            if (!string.IsNullOrEmpty(tagalog) || !string.IsNullOrEmpty(english) && disableLeftClick == true) {
                disableLeftClick = false;
            }
            else if (string.IsNullOrEmpty(tagalog) && string.IsNullOrEmpty(english)) {
                disableLeftClick = true;
            }

            baseText.text = cebuano;

            if (fontInt == 0) {
                baseText.fontSharedMaterial = fontCeb;
            }
            else if (fontInt == 2) {
                baseText.fontSharedMaterial = dyslexicCeb;
            }
        }
        
    }

    public void TTUpdater() {
        if (languageInt == 0) {
            if (!string.IsNullOrEmpty(tooltipTag)) {
                isTooltip = true;
                spawnedTText.text = tooltipTag;
                tooltipImg.color = tagColor;

                tooltipVLG.padding.left = 14;
                tooltipVLG.padding.right = 14;

                if (dyslexicTooltip == false) {
                    spawnedTText.fontSharedMaterial = calibriTag;
                }
                else if (dyslexicTooltip == true) {
                    spawnedTText.fontSharedMaterial = dyslexicTag;
                }
            }
            else if (string.IsNullOrEmpty(tooltipTag)) {
                isTooltip = false;
            }
        }

        else if (languageInt == 1) {
            if (!string.IsNullOrEmpty(tooltipEng)) {
                isTooltip = true;
                spawnedTText.text = tooltipEng;
                tooltipImg.color = engColor;

                tooltipVLG.padding.left = 15;
                tooltipVLG.padding.right = 15;

                if (dyslexicTooltip == false) {
                    spawnedTText.fontSharedMaterial = calibriEng;
                }
                else if (dyslexicTooltip == true) {
                    spawnedTText.fontSharedMaterial = dyslexicEng;
                }
            }
            else if (string.IsNullOrEmpty(tooltipEng))
            {
                isTooltip = false;
            }
        }

        else if (languageInt == 2) {
            if (!string.IsNullOrEmpty(tooltipCeb)) {
                isTooltip = true;
                spawnedTText.text = tooltipCeb;
                tooltipImg.color = cebColor;

                tooltipVLG.padding.left = 14;
                tooltipVLG.padding.right = 14;

                if (dyslexicTooltip == false) {
                    spawnedTText.fontSharedMaterial = calibriCeb;
                }
                else if (dyslexicTooltip == true) {
                    spawnedTText.fontSharedMaterial = dyslexicCeb;
                }
            }
            else if (!string.IsNullOrEmpty(tooltipCeb)) {
                isTooltip = false;
            }
        }

        if (isTooltip == true) {
            if (dyslexicTooltip == true) {
                spawnedTText.font = openDyslexic;
            }
            else if (dyslexicTooltip == false) {
                spawnedTText.font = calibri;
            }
        }
    }

    public void Settings() {
        tooltipAlpha = player.tooltipBGTransparency;

        dyslexicTooltip = player.openDyslexic;

        tagColor = new Color32(218, 241, 251, tooltipAlpha);
        engColor = new Color32(251, 244, 218, tooltipAlpha);
        cebColor = new Color32(217, 222, 252, tooltipAlpha);
    }

    public void tooltipMaster() {

        if (string.IsNullOrEmpty(tooltipTag) && string.IsNullOrEmpty(tooltipEng) && string.IsNullOrEmpty(tooltipCeb))
        {
            spawnedTT = null;
            spawnedTText = null;
            tooltipImg = null;
            tooltipVLG = null;
        }

        else if (languageInt == 0) {
            if (!string.IsNullOrEmpty(tooltipTag)) {
                Settings();

                spawnedTT = Instantiate(tooltip, new Vector3(0, 0, 0), Quaternion.identity);
                spawnedTT.transform.SetParent(this.transform, false);

                spawnedTText = spawnedTT.GetComponentInChildren<TextMeshProUGUI>();

                tooltipImg = spawnedTT.GetComponentInChildren<Image>();
                tooltipVLG = spawnedTT.GetComponentInChildren<VerticalLayoutGroup>();
                TTUpdater();
                tooltipActive = true;
            }
            else if (string.IsNullOrEmpty(tooltipTag)) {
                spawnedTT = null;
                spawnedTText = null;
                tooltipImg = null;
                tooltipVLG = null;
            }
        }
        else if (languageInt == 1) {
            if (!string.IsNullOrEmpty(tooltipEng)) {
                Settings();

                spawnedTT = Instantiate(tooltip, new Vector3(0, 0, 0), Quaternion.identity);
                spawnedTT.transform.SetParent(this.transform, false);

                spawnedTText = spawnedTT.GetComponentInChildren<TextMeshProUGUI>();

                tooltipImg = spawnedTT.GetComponentInChildren<Image>();
                tooltipVLG = spawnedTT.GetComponentInChildren<VerticalLayoutGroup>();
                TTUpdater();
                tooltipActive = true;
            }
            else if (string.IsNullOrEmpty(tooltipEng)) {
                spawnedTT = null;
                spawnedTText = null;
                tooltipImg = null;
                tooltipVLG = null;
            }
        }
        else if (languageInt == 2) {
            if (!string.IsNullOrEmpty(tooltipCeb)) {
                Settings();

                spawnedTT = Instantiate(tooltip, new Vector3(0, 0, 0), Quaternion.identity);
                spawnedTT.transform.SetParent(this.transform, false);

                spawnedTText = spawnedTT.GetComponentInChildren<TextMeshProUGUI>();

                tooltipImg = spawnedTT.GetComponentInChildren<Image>();
                tooltipVLG = spawnedTT.GetComponentInChildren<VerticalLayoutGroup>();
                TTUpdater();
                tooltipActive = true;
            }
            else if (string.IsNullOrEmpty(tooltipCeb)) {
                spawnedTT = null;
                spawnedTText = null;
                tooltipImg = null;
                tooltipVLG = null;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        AM.Play("Hover");

        tooltipMaster();
    }
    
    public void OnPointerExit(PointerEventData eventData) {
        if (tooltipActive == true) {
            tooltipImg = null;
            tooltipVLG = null;
            Destroy(spawnedTT);
            tooltipActive = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (disableAll == false) {
            if (tooltipActive == true) {
                tooltipImg = null;
                tooltipVLG = null;
                Destroy(spawnedTT);
                tooltipActive = false;
            }

            if (eventData.button == PointerEventData.InputButton.Left) {
                if (disableLeftClick == false) {
                    AM.Play("Click");

                    if (languageInt == 2 && string.IsNullOrEmpty(english)) {
                        languageInt = 0;
                        TextUpdater();
                        for (int i = 0; i < counterparts.Count; i++) {
                            counterparts[i].languageInt = 0;
                            counterparts[i].TextUpdater();
                        }
                        TextUpdater();
                        tooltipMaster();
                    }
                    else if (languageInt > 0) {
                        languageInt--;

                        for (int i = 0; i < counterparts.Count; i++) {
                            counterparts[i].languageInt--;
                            counterparts[i].TextUpdater();
                        }
                        TextUpdater();
                        tooltipMaster();
                    }
                }
            }

            if (eventData.button == PointerEventData.InputButton.Right) {

                if (disableRightClick == false) {
                    AM.Play("Click");
                    if (languageInt == 0 && string.IsNullOrEmpty(english)) {
                        languageInt = 2;
                        TextUpdater();
                        for (int i = 0; i < counterparts.Count; i++) {
                            counterparts[i].languageInt = 2;
                            counterparts[i].TextUpdater();
                        }
                        TextUpdater();
                        tooltipMaster();
                    }
                    else if (languageInt < 2) {
                        languageInt++;
                        for (int i = 0; i < counterparts.Count; i++) {
                            counterparts[i].languageInt++;
                            counterparts[i].TextUpdater();
                        }

                        TextUpdater();
                        tooltipMaster();
                    }
                }
            }
        }
    }
}