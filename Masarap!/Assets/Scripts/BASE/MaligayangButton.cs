using UnityEngine;

public class MaligayangButton : MonoBehaviour {

    /* Require the player to click through
     * to Cebuano welcome message from tutorial
     * before continueing
     */
    
    public TooltipResizer tooltipResizer;
    public Animation tooltipTwoAnim;
    public Animation tooltipThreeAnim;
    public GameObject mouseOne;
    public GameObject mouseTwo;

    public GameObject breakfastText;
    public TutorialText breakfastScript;

    public GameObject regBreakfastText;

    public bool firstState = false;
    public bool secondState = false;
    public bool thirdState = false;

    public Animation blurAnim;

    public GameObject blocker;

    void Update() {

        
        if (tooltipResizer.isTagalog == true) {

            #region mice
            if (mouseOne.activeInHierarchy == false) {
                mouseOne.SetActive(true);
            }
            if (mouseTwo.activeInHierarchy == true) {
                mouseTwo.SetActive(false);
            }
            #endregion

        }
        else if (tooltipResizer.isEnglish == true) {

            #region mice
            // only show mouse 2 under breakfast text the first time coming back from tagalog
            if (mouseOne.activeInHierarchy == true) {
                mouseOne.SetActive(false);
            }

            if (mouseTwo.activeInHierarchy == false && breakfastScript.fourthState == false) {
                mouseTwo.SetActive(true);
            }
            #endregion

            // Going to second state for the first time. Don't bring back first tooltip
            if (firstState == false) {
                firstState = true;
                tooltipTwoAnim.Play("Tooltip 2 - Together Animation");
                tooltipThreeAnim.Play("Tooltip 3 - Words Animation");
                blurAnim.enabled = true;

                regBreakfastText.SetActive(false);
                breakfastText.SetActive(true);
                blocker.SetActive(true);
            }

            if (breakfastScript.fourthState == true) {
                blocker.SetActive(false);
                mouseTwo.SetActive(false);
            }
        }


        else if (tooltipResizer.isCebuano == true) {

            #region mice
            if (mouseOne.activeInHierarchy == true) {
                mouseOne.SetActive(false);
            }
            if (mouseTwo.activeInHierarchy == true) {
                mouseTwo.SetActive(false);
            }
            #endregion
        }
    }
}