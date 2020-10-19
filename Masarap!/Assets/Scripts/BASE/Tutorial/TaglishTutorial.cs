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
    public SpreadManager sm;
    public AudioSource rhythm;

    public Animation firstD;
    public Animation firstSpace;
    public Animation firstRight;
    public GameObject secondKeys;
    public TextMeshProUGUI baseText;
    public TextMeshProUGUI tooltipText;
    public RectTransform tooltipTextRECT;
    public RectTransform tooltipBG;

    public TextMeshProUGUI tooltipThreeText;
    public TextMeshProUGUI patreonText;
    public RectTransform tooltipThreeBG;
    public Image tooltipThreeIMG;

    public TextMeshProUGUI tooltipFourText;
    public TextMeshProUGUI tooltipFiveText;

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

    public Material calibriRed;
    public Material calibriTag;
    public Material calibriEng;
    public Material calibriCeb;
    public Material calibriTan;
    public Material calibriOrange;

    public Material delicaTag;
    public Material delicaEng;
    public Material delicaCeb;

    public Material dyslexicRed;
    public Material dyslexicTag;
    public Material dyslexicEng;
    public Material dyslexicCeb;
    public Material dyslexicTan;
    public Material dyslexicOrange;

    private Color32 tagColor;
    private Color32 engColor;
    private Color32 cebColor;

    public Animation firstAnim;
    public Animation secondAnim;
    public Animation thirdAnim;
    public Animation breakfastBlur;
    public Animation blur2;

    public GameObject mouseOne;
    public RectTransform mouseOneRect;
    public GameObject mouseTwo;

    public Image mouseTwoLeft;
    public Image mouseTwoRight;

    public GameObject mouseThree;

    public Settings settingScript;
    public Toggle dyslexicToggle;

    public Taglish denneBreakfast;
    public TextMeshProUGUI denneTMP;
    public GameObject denneBreakfastGlow;

    public Taglish dyslexicBreakfast;
    public TextMeshProUGUI dyslexicTMP;
    public GameObject dyslexicBreakfastGlow;

    public TextMeshProUGUI tagalogCalibri;
    public TextMeshProUGUI englishCalibri;
    public TextMeshProUGUI cebuanoCalibri;

    public Animation arrowAnim;
    public GameObject arrow;
    public Animation mouseTwoAnim;
    public Animation miniTwoAnim;
    public Image miniTwoLeft;
    public Image miniTwoRight;

    public Animation nextPage;

    public bool pingedTag = false;
    public bool pingedCeb = false;

    public bool disableRightClick = false;
    public bool disableLeftClick = false;

    public GameObject Tutorial;
    #endregion

    // on awake, set text to TL / EN / CB
    public void Start() {
        denneBreakfast.disableLeftClick = true;
        dyslexicBreakfast.disableLeftClick = true;
        TextUpdater();
        rhythm.volume = 0.5f;
    }

    public void Update() {

        if (languageInt == 0) {
            // upwards
            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.RightArrow)) {
                if (disableRightClick == false) {
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
            }

            // downwards
            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) {
                if (disableLeftClick == false) {
                    AM.Play("Click");

                    // if 0, 1, 2
                    if (languageInt > 0) {
                        // if we're going downward TO tagalog FROM english
                        if (languageInt == 1) {
                            EngtoTag();
                        }
                        // if we're going downward TO english FROM cebuano
                        else if (languageInt == 2) 
{
                            CebToEng();
                        }

                        languageInt--;
                        TextUpdater();
                    }
                }
            }
        
        }

        // if we switch off english & the glow is still existing, destroy them
        if (denneBreakfast.languageInt != 1 && denneBreakfastGlow != null) {
            Destroy(denneBreakfastGlow);
            Destroy(dyslexicBreakfastGlow);
        }

        // when it's on tagalog,
        if (denneBreakfast.languageInt == 0) {

            if (pingedTag == false) {
                pingedTag = true;
            }

            if (tagalogCalibri.fontStyle != FontStyles.Underline) {
                tagalogCalibri.fontStyle = FontStyles.Underline;
                tagalogCalibri.fontStyle = FontStyles.Bold;

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
            if (arrowAnim.isActiveAndEnabled == true && !arrowAnim.IsPlaying("TutArrowTagToEng")) {
                // stop whatever is playing
                arrowAnim.Stop();
                // the arrow blinks towards english
                arrowAnim.Play("TutArrowTagToEng");
            }

            // if animator is not already playing right
            if (mouseTwoAnim.isActiveAndEnabled == true && !mouseTwoAnim.IsPlaying("TutMouseRight")) {

                mouseTwoAnim.Stop();
                mouseTwoLeft.color = new Color32(253, 58, 76, 0);
                mouseTwoRight.color = new Color32(253, 58, 76, 0);
                mouseTwoAnim.Play("TutMouseRight");
                miniTwoAnim.Stop();
                miniTwoLeft.color = new Color32(253, 58, 76, 0);
                miniTwoRight.color = new Color32(253, 58, 76, 0);
                miniTwoAnim.Play("TutMouseRight");
            }
        }

        // when it's on english,
        else if (denneBreakfast.languageInt == 1) {
            if (englishCalibri.fontStyle != FontStyles.Underline) {
                tagalogCalibri.fontStyle = FontStyles.Normal;
                englishCalibri.fontStyle = FontStyles.Underline;
                englishCalibri.fontStyle = FontStyles.Bold;
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

            // if haven't gone to tag or ceb, default to eng -> ceb
            if (pingedTag == false && pingedCeb == false) {
                if (!arrowAnim.IsPlaying("TutArrowEngToCeb")) {
                    // stop whatever is playing
                    arrowAnim.Stop();
                    // the arrow blinks -> to cebuano & <- to tagalog
                    arrowAnim.Play("TutArrowEngToCeb");
                }

                // if animator is not already playing rightclick
                if (mouseTwo.activeInHierarchy == true && !mouseTwoAnim.IsPlaying("TutMouseRight")) {
                    // stop whatever is playing
                    mouseTwoAnim.Stop();
                    mouseTwoLeft.color = new Color32(253, 58, 76, 0);
                    mouseTwoRight.color = new Color32(253, 58, 76, 0);

                    miniTwoAnim.Stop();
                    miniTwoLeft.color = new Color32(253, 58, 76, 0);
                    miniTwoRight.color = new Color32(253, 58, 76, 0);

                    mouseTwoAnim.Play("TutMouseRight");

                    miniTwoAnim.Play("TutMouseRight");
                }
            }
            // has trans. to ceb, not tag yet. show only left arrow+mouse
            else if (pingedTag == false && pingedCeb == true) {
                disableLeftClick = true;
                if (!arrowAnim.IsPlaying("TutArrowEngToTag")) {
                    arrowAnim.Stop();

                    arrowAnim.Play("TutArrowEngToTag");
                }

                if (mouseTwo.activeInHierarchy == true && !mouseTwoAnim.IsPlaying("TutMouseLeft")) {
                    mouseTwoAnim.Stop();
                    mouseTwoLeft.color = new Color32(253, 58, 76, 0);
                    mouseTwoRight.color = new Color32(253, 58, 76, 0);
                    mouseTwoAnim.Play("TutMouseLeft");

                    miniTwoAnim.Stop();
                    miniTwoLeft.color = new Color32(253, 58, 76, 0);
                    miniTwoRight.color = new Color32(253, 58, 76, 0);
                    miniTwoAnim.Play("TutMouseLeft");
                }
            }

            // has trans. to ceb, coming back from eng
            else if (pingedTag == true && pingedCeb == true && denneBreakfast.disableAll == false) {
                denneBreakfast.disableAll = true;
                dyslexicBreakfast.disableAll = true;
                mouseTwo.SetActive(false);
                arrow.SetActive(false);
                denneBreakfast.disableAll = true;
                dyslexicBreakfast.disableAll = true;
                sm.disableRight = false;
                denneTMP.color = new Color32(255, 255, 255, 128);
                dyslexicTMP.color = new Color32(255, 255, 255, 128);

                mouseThree.SetActive(true);
                background.color = new Color32(251, 244, 218, 255);

                tooltipThreeIMG.color = new Color32(255, 240, 235, 128);
                tagalogCalibri.color = new Color32(255, 255, 255, 128);
                englishCalibri.color = new Color32(255, 255, 255, 128);
                cebuanoCalibri.color = new Color32(255, 255, 255, 128);
                patreonText.color = new Color32(255, 255, 255, 128);
            }
        }

        // when it's on cebuano,
        else if (denneBreakfast.languageInt == 2) {

            if (pingedCeb == false) {
                pingedCeb = true;
                denneBreakfast.disableLeftClick = false;
                dyslexicBreakfast.disableLeftClick = false;
                disableRightClick = false;
                disableLeftClick = false;
            }

            if (cebuanoCalibri.fontStyle != FontStyles.Underline) {
                tagalogCalibri.fontStyle = FontStyles.Normal;
                englishCalibri.fontStyle = FontStyles.Normal;
                cebuanoCalibri.fontStyle = FontStyles.Underline;
                cebuanoCalibri.fontStyle = FontStyles.Bold;
                if (dyslexicToggle.isOn) {
                    cebuanoCalibri.fontSize = 35;

                    tagalogCalibri.fontSize = 30;
                    tagalogCalibri.fontSize = 30;
                    englishCalibri.fontSize = 30;
                }
                else if (!dyslexicToggle.isOn) {
                    cebuanoCalibri.fontSize = 40;

                    tagalogCalibri.fontSize = 30;
                    englishCalibri.fontSize = 30;
                }
            }
            // if animator is not already playing ceb to eng
            if (arrowAnim.isActiveAndEnabled == true && !arrowAnim.IsPlaying("TutArrowCebToEng")) {
                // the arrow points <- TO ENGLISH
                arrowAnim.Stop();
                arrowAnim.Play("TutArrowCebToEng");
            }

            // if animator is not already playing left mouse
            if (mouseTwoAnim.isActiveAndEnabled == true && !mouseTwoAnim.IsPlaying("TutMouseLeft")) {


                mouseTwoAnim.Stop();
                mouseTwoLeft.color = new Color32(253, 58, 76, 0);
                mouseTwoRight.color = new Color32(253, 58, 76, 0);
                mouseTwoAnim.Play("TutMouseLeft");
                miniTwoAnim.Stop();
                miniTwoLeft.color = new Color32(253, 58, 76, 0);
                miniTwoRight.color = new Color32(253, 58, 76, 0);
                miniTwoAnim.Play("TutMouseLeft");
            }

            if (pingedTag == true && pingedCeb == true && mouseThree.activeInHierarchy == true) {
                mouseThree.SetActive(false);
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

            if (pingedCeb == false) {
                background.color = new Color32(251, 244, 218, 128); // english, half alpha
                baseText.color = new Color32(255, 255, 255, 170);
            }
            if (pingedCeb == true && pingedTag == true) {
                background.color = new Color32(251, 244, 218, 255); // english, full alpha
                baseText.color = new Color32(255, 255, 255, 255);
            }

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

            // let's make...
            tooltipFiveText.font = openDyslexic;
            tooltipFiveText.fontSize = 30;
            tooltipFiveText.fontSharedMaterial = dyslexicRed;

            mouseOneRect.transform.localPosition = new Vector3(417.7f, -60.6f, 0);
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

            // let's make...
            tooltipFiveText.font = calibri;
            tooltipFiveText.fontSize = 40;
            tooltipFiveText.fontSharedMaterial = calibriRed;

            mouseOneRect.transform.localPosition = new Vector3(344.1f, -60.6f, 0);
        }

        TextUpdater();
    }

    public void TagToEng() {
        mouseOne.SetActive(false);
        firstAnim.Play("Tooltip 2 - Together Animation");
        secondAnim.Play("Tooltip 3 - Words Animation");
        if (breakfastBlur.enabled == false) {
            breakfastBlur.enabled = true;
        }
        mouseTwoAnim.enabled = true;

        if (denneBreakfastGlow != null) {
            denneBreakfastGlow.SetActive(true);
            dyslexicBreakfastGlow.SetActive(true);
        }

        if (pingedCeb == false) {
            disableLeftClick = true;
            disableRightClick = true;
        }

        if (firstD.enabled == true) {
            firstD.enabled = false;
            firstSpace.enabled = false;
            firstRight.enabled = false;
        }
    }
    public void EngtoTag() {
        mouseOne.SetActive(true);
        firstAnim.Play("Tooltip 2 - Together Animation REV");
        secondAnim.Play("Tooltip 3 - Words Animation REV");

        blur2.enabled = true;

        if (denneBreakfastGlow != null) {
            denneBreakfastGlow.SetActive(false);
            dyslexicBreakfastGlow.SetActive(false);
        }
    }
    public void EngToCeb() {
        if (pingedTag == true && pingedCeb == true) {
            secondKeys.SetActive(true);
            secondAnim.Play("Tooltip 3 - Words Animation 2");
            thirdAnim.Play("Tooltip 5 - Let's Animation");
            mouseThree.SetActive(false);
            disableRightClick = true;
            blur2.enabled = true;
            nextPage.enabled = true;
        }
    }
    public void CebToEng() {
        if (pingedTag == true && pingedCeb == true) {
            secondAnim.Play("Tooltip 3 - Words Animation 2 REV");
        }
    }

    public void End() {
        denneBreakfast.disableAll = false;
        denneBreakfast.disableLeftClick = false;
        denneBreakfast.disableRightClick = false;

        dyslexicBreakfast.disableAll = false;
        dyslexicBreakfast.disableLeftClick = false;
        dyslexicBreakfast.disableRightClick = false;

        denneTMP.color = new Color32(255, 255, 255, 255);
        dyslexicTMP.color = new Color32(255, 255, 255, 255);
        
        if (Tutorial != null) {
            Destroy(Tutorial);
        }
    }


    public void OnPointerClick(PointerEventData eventData) {
        if (disableRightClick == false) {
            // RIGHT CLICK - transl. upward
            if (eventData.button == PointerEventData.InputButton.Right) {
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
        }

        if (disableLeftClick == false) {
            // LEFT CLICK - transl. downward
            if (eventData.button == PointerEventData.InputButton.Left) {
                AM.Play("Click");
                if (pingedCeb == true) {
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
    }
}