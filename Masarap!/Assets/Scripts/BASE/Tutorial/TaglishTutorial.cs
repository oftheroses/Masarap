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
    public RectTransform tooltipTextRECT;
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

    public Material calibriTag;
    public Material calibriEng;
    public Material calibriCeb;
    public Material calibriTan;
    public Material calibriOrange;

    public Material delicaTag;
    public Material delicaEng;
    public Material delicaCeb;

    public Material dyslexicTag;
    public Material dyslexicEng;
    public Material dyslexicCeb;
    public Material dyslexicTan;
    public Material dyslexicOrange;

    private Color32 tagColor;
    private Color32 engColor;
    private Color32 cebColor;

    public bool firstClick = false;
    public Animation firstAnim;
    public Animation secondAnim;
    public Animation thirdAnim;
    public Animation breakfastBlur;

    public GameObject mouseOne;
    public GameObject mouseTwo;

    public Settings settingScript;
    public Toggle dyslexicToggle;

    public Taglish denneBreakfast;
    public GameObject denneBreakfastGlow;
    public Taglish dyslexicBreakfast;
    public GameObject dyslexicBreakfastGlow;

    public TextMeshProUGUI tagalogCalibri;
    public TextMeshProUGUI englishCalibri;
    public TextMeshProUGUI cebuanoCalibri;

    public Animation arrowAnim;

    public Animation mouseTwoAnim;
    public Animation miniTwoAnim;

    public bool pingedTag = false;
    public bool pingedCeb = false;
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

        // if we switch off english & the glow is still existing, destroy them
        if ((denneBreakfast.languageInt != 1 || dyslexicBreakfast.languageInt != 1) && denneBreakfastGlow != null) {
            Destroy(denneBreakfastGlow);
            Destroy(dyslexicBreakfastGlow);
            mouseTwo.SetActive(false);
        }

        // when it's on tagalog,
        if (denneBreakfast.languageInt == 0) {

            if (pingedTag == false) {
                pingedTag = true;
            }

            if (tagalogCalibri.fontStyle != FontStyles.Underline) {
                tagalogCalibri.fontStyle = FontStyles.Underline;

                if (dyslexicToggle.isOn) {
                    tagalogCalibri.fontSize = 35;

                    englishCalibri.fontSize = 30;
                    cebuanoCalibri.fontSize = 30;
                }
                else if (!dyslexicToggle.isOn) {
                    tagalogCalibri.fontSize = 40;

                    englishCalibri.fontSize = 30;
                    cebuanoCalibri.fontSize = 30;
                }

                englishCalibri.fontStyle = FontStyles.Normal;
                cebuanoCalibri.fontStyle = FontStyles.Normal;
            }

            // if animator is not already playing tag->eng
            if (!arrowAnim.IsPlaying("TutArrowTagToEng")) {
                // stop whatever is playing
                arrowAnim.Stop();
                // the arrow blinks towards english
                arrowAnim.Play("TutArrowTagToEng");
            }

            // if animator is not already playing left
            if (!mouseTwoAnim.IsPlaying("TutMouseLeft")) {
                // stop whatever is playing
                mouseTwoAnim.Stop();

                miniTwoAnim.Stop();

                mouseTwoAnim.Play("TutMouseLeft");

                miniTwoAnim.Play("TutMouseLeft");
            }
        }

        // when it's on english,
        else if (denneBreakfast.languageInt == 1) {
            if (englishCalibri.fontStyle != FontStyles.Underline) {
                tagalogCalibri.fontStyle = FontStyles.Normal;
                englishCalibri.fontStyle = FontStyles.Underline;
                cebuanoCalibri.fontStyle = FontStyles.Normal;

                if (dyslexicToggle.isOn) {
                    englishCalibri.fontSize = 35;

                    tagalogCalibri.fontSize = 30;
                    cebuanoCalibri.fontSize = 30;
                }
                else if (!dyslexicToggle.isOn) {
                    englishCalibri.fontSize = 40;

                    tagalogCalibri.fontSize = 30;
                    cebuanoCalibri.fontSize = 30;
                }
            }
            // if animator is not already playing the looped arrow
            if (!arrowAnim.IsPlaying("TutArrowLoop")) {
                // stop whatever is playing
                arrowAnim.Stop();
                // the arrow blinks -> to cebuano & <- to tagalog
                arrowAnim.Play("TutArrowLoop");
            }

            // if animator is not already playing loop
            if (!mouseTwoAnim.IsPlaying("TutMouseLoop")) {
                // stop whatever is playing
                mouseTwoAnim.Stop();

                miniTwoAnim.Stop();

                mouseTwoAnim.Play("TutMouseLoop");

                miniTwoAnim.Play("TutMouseLoop");
            }
        }

        // when it's on cebuano,
        else if (denneBreakfast.languageInt == 2) {

            if (pingedCeb == false) {
                pingedCeb = true;
            }

            if (cebuanoCalibri.fontStyle != FontStyles.Underline) {
                tagalogCalibri.fontStyle = FontStyles.Normal;
                englishCalibri.fontStyle = FontStyles.Normal;
                cebuanoCalibri.fontStyle = FontStyles.Underline;

                if (dyslexicToggle.isOn) {
                    cebuanoCalibri.fontSize = 35;

                    tagalogCalibri.fontSize = 30;
                    englishCalibri.fontSize = 30;
                }
                else if (!dyslexicToggle.isOn) {
                    cebuanoCalibri.fontSize = 40;

                    tagalogCalibri.fontSize = 30;
                    englishCalibri.fontSize = 30;
                }
            }
            // if animator is not already playing eng<-ceb
            if (!arrowAnim.IsPlaying("TutArrowCebToEng")) {
                // stop whatever is playing
                arrowAnim.Stop();
                // the arrow points <- TO ENGLISH
                arrowAnim.Play("TutArrowCebToEng");
            }

            // if animator is not already playing right
            if (!mouseTwoAnim.IsPlaying("TutMouseRight")) {
                // stop whatever is playing
                mouseTwoAnim.Stop();

                miniTwoAnim.Stop();

                mouseTwoAnim.Play("TutMouseRight");

                miniTwoAnim.Play("TutMouseRight");
            }
        }
    }

    public void TextUpdater() {

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
        if (dyslexicToggle.isOn) {
            player.openDyslexic = false;
            settingScript.OpenDyslexic();

            fontInt = 3;

            baseText.fontSize = 45;
            baseText.font = openDyslexic;
            VLG.padding.top = -9;
            VLG.padding.bottom = 9;

            // Today we will be learning Filipino Language & Cuisine
            tooltipText.fontSize = 30;
            tooltipText.fontStyle = FontStyles.Bold;
            tooltipText.font = openDyslexic;
            tooltipText.fontSharedMaterial = dyslexicTan;
            tooltipText.lineSpacing = -25;
            tooltipTextRECT.localPosition = new Vector3(0, 6.7f, 0);
            tooltipBG.sizeDelta = new Vector2(619.83f, 133.93f);

            // Click highlighted words to translate!
            tooltipThreeText.fontSize = 30;
            tooltipThreeText.font = openDyslexic;
            tooltipThreeText.fontSharedMaterial = dyslexicTan;
            tooltipThreeText.lineSpacing = -25;
            tooltipThreeBG.sizeDelta = new Vector2(945, 168.5f);

            // as I reach more Patreon goals
            tooltipFourText.fontSize = 25;
            tooltipFourText.font = openDyslexic;
            tooltipFourText.fontSharedMaterial = dyslexicOrange;

            // tagalog -> english -> cebuano texts
            tagalogCalibri.font = openDyslexic;
            tagalogCalibri.fontSharedMaterial = dyslexicTag;

            englishCalibri.font = openDyslexic;
            englishCalibri.fontSharedMaterial = dyslexicEng;

            cebuanoCalibri.font = openDyslexic;
            cebuanoCalibri.fontSharedMaterial = dyslexicCeb;
        }

        else if (!dyslexicToggle.isOn) {
            player.openDyslexic = true;
            settingScript.DenneDelica();

            fontInt = 1;

            baseText.fontSize = 50;
            baseText.font = denne;
            VLG.padding.top = 15;
            VLG.padding.bottom = 15;

            // Today we will be learning Filipino Language & Cuisine
            tooltipText.fontSize = 40;
            tooltipText.fontStyle = FontStyles.Normal;
            tooltipText.font = calibri;
            tooltipText.fontSharedMaterial = calibriTan;
            tooltipText.lineSpacing = 0;
            tooltipTextRECT.localPosition = new Vector3(0, 0, 0);
            tooltipBG.sizeDelta = new Vector2(541.68f, 128.83f);

            // Click highlighted words to translate!
            tooltipThreeText.fontSize = 35;
            tooltipThreeText.fontStyle = FontStyles.Normal;
            tooltipThreeText.font = calibri;
            tooltipThreeText.fontSharedMaterial = calibriTan;
            tooltipThreeText.lineSpacing = 0;
            tooltipThreeBG.sizeDelta = new Vector2(786.38f, 168.5f);

            // as I reach more Patreon goals
            tooltipFourText.fontSize = 31;
            tooltipFourText.font = calibri;
            tooltipFourText.fontSharedMaterial = calibriOrange;

            // tagalog -> english -> cebuano texts
            tagalogCalibri.font = calibri;
            tagalogCalibri.fontSharedMaterial = calibriTag;

            englishCalibri.font = calibri;
            englishCalibri.fontSharedMaterial = calibriEng;

            cebuanoCalibri.font = calibri;
            cebuanoCalibri.fontSharedMaterial = calibriCeb;
        }

        TextUpdater();
    }

    public void TagToEng() {
        mouseOne.SetActive(false);
        firstAnim.Play("Tooltip 2 - Together Animation");
        secondAnim.Play("Tooltip 3 - Words Animation");
        breakfastBlur.Play("Tutorial Blur Animation");
  

        denneBreakfastGlow.SetActive(true);
        dyslexicBreakfastGlow.SetActive(true);
    }
    public void EngtoTag() {
        mouseOne.SetActive(true);
        firstAnim.Play("Tooltip 2 - Together Animation REV");
        secondAnim.Play("Tooltip 3 - Words Animation REV");
        breakfastBlur.Play("Tutorial Blur Animation REV");
        denneBreakfastGlow.SetActive(false);
        dyslexicBreakfastGlow.SetActive(false);
    }
    public void EngToCeb() {
        // if breakfastDenne.languageInt reached 0 & 2
        if (pingedTag == true && pingedCeb == true) {
            secondAnim.Play("Tooltip 3 - Words Animation 2");
            thirdAnim.Play("Tooltip 5 - Let's Animation");
        }
    }
    public void CebToEng() {
        if (pingedTag == true && pingedCeb == true) {
            secondAnim.Play("Tooltip 3 - Words Animation 2 REV");
        }
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