using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TaglishTutorial : MonoBehaviour, IPointerClickHandler {

    /*  a copy of the taglish script, specially for the tutorial
     *  so we raycast the "welcome" button instead of the text itself
     *  + change button color on highlight/click
     *  + responds to space bar / D / right arrow
     *  + resizes & recolors the box according to the text
     *  
     * language int:
     * -1 none
     * 0 tagalog
     * 1 english
     * 2 cebuano
     * 
     * font int:
     * 0 calibri
     * 1 denne delica
     * 2 opendyslexic
     */
    #region BASIC
    public AudioManager AM;
    public Player player;
    public TextMeshProUGUI baseText;
    public TextMeshProUGUI tooltipText;
    public RectTransform tooltipBG;

    public TextMeshProUGUI tooltipThreeText;
    public RectTransform tooltipThreeBG;
    public TextMeshProUGUI tooltipFourText;

    public Image background;
    public VerticalLayoutGroup VLG;

    public int languageInt;
    public int fontInt;

    public TMP_FontAsset denne;
    public TMP_FontAsset calibri;
    public TMP_FontAsset openDyslexic;

    public string tagalog;
    public string english;
    public string cebuano;

    public Material calibriTan;

    public Material delicaTag;
    public Material delicaEng;
    public Material delicaCeb;

    public Material dyslexicTag;
    public Material dyslexicEng;
    public Material dyslexicCeb;
    public Material dyslexicTan;

    private Color32 tagColor;
    private Color32 engColor;
    private Color32 cebColor;

    public bool firstClick = false;
    public Animation firstAnim;
    public Animation secondAnim;
    public Animation breakfastBlur;

    public GameObject mouseOne;
    public GameObject mouseTwo;

    public Settings settingScript;
    #endregion

    // on awake, set text to TL / EN / CB
    public void Start() {
        TextUpdater();
    }

    public void Update() {
        // upwards
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.RightArrow)) {
            AM.Play("Click");

            // won't go above 2
            if (languageInt < 2) {

                // if we're going upward FROM tagalog TO english
                if (languageInt == 0) {
                    TagToEng();
                }
                // if we're going upward FROM english TO cebuano
                else if (languageInt == 1) {
                    EngToCeb();
                }

                languageInt++;
                TextUpdater();
            }
        }

        // downwards
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) {
            AM.Play("Click");

            // if 0, 1, 2
            if (languageInt > 0) {
                // if we're going downward TO tagalog FROM english
                if (languageInt == 1) {
                    EngtoTag();
                }
                // if we're going downward TO english FROM cebuano
                else if (languageInt == 2) {
                    CebToEng();
                }

                languageInt--;
                TextUpdater();
            }
        
        }
    }

    public void TextUpdater() {

        if (player.openDyslexic == false) {
            fontInt = 1;

            baseText.fontSize = 50;
            baseText.font = denne;
            VLG.padding.top = 15;
            VLG.padding.bottom = 15;

            tooltipText.fontSize = 40;
            tooltipText.fontStyle = FontStyles.Normal;
            tooltipText.font = calibri;
            tooltipText.fontSharedMaterial = calibriTan;
            tooltipBG.sizeDelta = new Vector2(541.68f, 128.83f);

            tooltipThreeText.fontSize = 35;
            tooltipThreeText.fontStyle = FontStyles.Normal;
            tooltipThreeText.font = calibri;
            tooltipThreeText.fontSharedMaterial = calibriTan;
            tooltipThreeBG.sizeDelta = new Vector2(890.2f, 168.5f);

        }

        else if (player.openDyslexic == true) {
            fontInt = 3;

            baseText.fontSize = 45;
            baseText.font = openDyslexic;
            VLG.padding.top = -9;
            VLG.padding.bottom = 11;

            tooltipText.fontSize = 30;
            tooltipText.fontStyle = FontStyles.Bold;
            tooltipText.font = openDyslexic;
            tooltipText.fontSharedMaterial = dyslexicTan;
            tooltipBG.sizeDelta = new Vector2(619.83f, 133.93f);

            tooltipThreeText.fontSize = 30;
            tooltipThreeText.fontStyle = FontStyles.Bold;
            tooltipThreeText.font = openDyslexic;
            tooltipThreeText.fontSharedMaterial = dyslexicTan;
            tooltipThreeBG.sizeDelta = new Vector2(900, 100);

        }

        // base text: tagalog
        if (languageInt == 0) {
            background.color = new Color32(218, 241, 251, 255); // tagalog color
            VLG.padding.left = 25;
            VLG.padding.right = 25;
            baseText.text = tagalog;

            if (fontInt == 1) {
                baseText.fontSharedMaterial = delicaTag;
            }
            else if (fontInt == 3) {
                baseText.fontSharedMaterial = dyslexicTag;
            }
        }

        // base text: english
        else if (languageInt == 1) {
            background.color = new Color32(251, 244, 218, 255); // english
            VLG.padding.left = 24;
            VLG.padding.right = 24;
            baseText.text = english;

            if (fontInt == 1) {
                baseText.fontSharedMaterial = delicaEng;
            }
            else if (fontInt == 3) {
                baseText.fontSharedMaterial = dyslexicEng;
            }
        }

        // base text: cebuano
        else if (languageInt == 2) {
            background.color = new Color32(217, 222, 252, 255); // cebuano
            VLG.padding.left = 25;
            VLG.padding.right = 25;
            baseText.text = cebuano;

            if (fontInt == 1) {
                baseText.fontSharedMaterial = delicaCeb;
            }
            else if (fontInt == 3) {
                baseText.fontSharedMaterial = dyslexicCeb;
            }
        }
    }

    // pings settings so we can change from/to dyslexic
    public void SettingsPing() {
        settingScript.OpenDyslexic();
    }

    public void TagToEng() {
        mouseOne.SetActive(false);
        firstAnim.Play("Tooltip 2 - Together Animation");
        secondAnim.Play("Tooltip 3 - Words Animation");
        breakfastBlur.Play("Tutorial Blur Animation");
    }
    public void EngtoTag() {
        mouseOne.SetActive(true);
        firstAnim.Play("Tooltip 2 - Together Animation REV");
        secondAnim.Play("Tooltip 3 - Words Animation REV");
        breakfastBlur.Play("Tutorial Blur Animation REV");
    }
    public void EngToCeb() {

    }
    public void CebToEng() {

    }


    // pointer click is in a separate function, because if
    // we called it in update, it would listen to clicks
    // screen-wide instead of in the box itself
    public void OnPointerClick(PointerEventData eventData) {
        AM.Play("Click");

        // LEFT CLICK - transl. upward
        if (eventData.button == PointerEventData.InputButton.Left) {

            // won't go above 2
            if (languageInt < 2) {

                // if we're going upward FROM tagalog TO english
                if (languageInt == 0) {
                    TagToEng();
                }
                // if we're going upward FROM english TO cebuano
                else if (languageInt == 1) {
                    EngToCeb();
                }

                languageInt++;
                TextUpdater();
            }
        }

        // RIGHT CLICK - transl. downward
        if (eventData.button == PointerEventData.InputButton.Right) {

            // if 0, 1, 2
            if (languageInt > 0) {
                // if we're going downward TO tagalog FROM english
                if (languageInt == 1) {
                    EngtoTag();
                }
                // if we're going downward TO english FROM cebuano
                else if (languageInt == 2) {
                    CebToEng();
                }

                languageInt--;
                TextUpdater();
            }
        }
    }
}