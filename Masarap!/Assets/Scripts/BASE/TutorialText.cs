using TMPro;
using UnityEngine;
using System.Collections;

public class TutorialText : MonoBehaviour {

    public Taglish self;

    public Animation tooltipThreeAnim;

    public GameObject leftClicker;
    public GameObject rightClicker;

    public GameObject firstRight;
    public GameObject firstLeft;

    public GameObject secondRight;
    public GameObject secondLeft;

    public TextMeshProUGUI tagalog;
    public TextMeshProUGUI english;
    public TextMeshProUGUI cebuano;

    public bool firstState;
    public bool secondState;
    public bool thirdState;
    public bool fourthState;

    public Animation page;
    public GameObject blur1;
    public GameObject blur2;
    public Animation tooltip5;

    public SpreadManager sm;

    void Awake() {
        Right();
        Ping();
    }

    private void Update() {
        if (page.enabled == true) {
            if (sm.currentSpread == 4) {
                if (Input.GetKeyDown("w") || Input.GetKeyDown("d") || Input.GetKeyDown("up") || Input.GetKeyDown("right") || Input.GetKeyDown("page up")) {
                    sm.spreadIncrease();
                    sm.changeSpread();
                }
            }
        }
    }

    public void Ping() {

        if (self.languageInt == 0) {
            tagalog.fontStyle = FontStyles.Bold;
            tagalog.fontStyle = FontStyles.Underline;

            english.fontStyle = FontStyles.Normal;
            cebuano.fontStyle = FontStyles.Normal;

            Right();

                rightClicker.SetActive(false);
                leftClicker.SetActive(true);

            thirdState = true;
        }

        else if (self.languageInt == 1) {
            english.fontStyle = FontStyles.Bold;
            english.fontStyle = FontStyles.Underline;

            tagalog.fontStyle = FontStyles.Normal;
            cebuano.fontStyle = FontStyles.Normal;

            secondState = true;

            if (firstState == true && thirdState == true) {
                fourthState = true;
                tooltipThreeAnim.Play("Tooltip 3 - Words Animation 2");

                page.enabled = true;
                blur1.SetActive(false);
                blur2.SetActive(false);


                tooltip5.Play("Tooltip 5 - Let's Animation");
            }
        }

        else if (self.languageInt == 2) {
            cebuano.fontStyle = FontStyles.Bold;
            cebuano.fontStyle = FontStyles.Underline;

            english.fontStyle = FontStyles.Normal;
            tagalog.fontStyle = FontStyles.Normal;

            Left();

                rightClicker.SetActive(true);
                leftClicker.SetActive(false);

            firstState = true;
        }
    }

    public void Left() {
        firstLeft.SetActive(true);
        secondLeft.SetActive(true);

        firstRight.SetActive(false);
        secondRight.SetActive(false);
    }

    public void Right() {
        firstRight.SetActive(true);
        secondRight.SetActive(true);

        secondLeft.SetActive(false);
        firstLeft.SetActive(false);
    }
}
