using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadManager : MonoBehaviour {
    #region BASICS
    /* spreads:
     * 0: 0-1
     * 1: 2-3
     * 2: 4-5
     * 3: 6-7
     * 4: 8-9
     * 5: 10-11
     * 6: 12-13
     */

    public GameObject tutorial1;
    public GameObject tutorial2;

    public Player player;
    public AudioManager pageTurn;
    public int currentSpread;

    public bool disableAll = false;
    public bool disableRight = false;
    public bool disableLeft = false;

    public GameObject spreadZero;
    public GameObject spreadOne;
    public GameObject spreadTwo;
    public GameObject spreadThree;
    public GameObject spreadFour;
    public GameObject spreadFive;
    public GameObject spreadSix;
    public GameObject spreadSeven;
    public GameObject spreadEight;
    public GameObject spreadNine;
    #endregion

    void Awake() {
        if (player.level == 0) {
            tutorial1.SetActive(true);
            tutorial2.SetActive(true);
            player.currentSpread = 4;
            disableLeft = true;
            disableRight = true;
        }
        else if (player.level >= 1) {
            if (tutorial1.activeInHierarchy == true || tutorial2.activeInHierarchy == true) {
                tutorial1.SetActive(false);
                tutorial2.SetActive(false);
            }
            currentSpread = player.currentSpread;
        }

        changeSpread();
    }

    public void changeSpread() {
        player.currentSpread = currentSpread;

        //deactivate all spreads
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("spread");
        foreach (GameObject spreads in gameObjectArray) {
            spreads.SetActive(false);
        }

        // deactivate 0-1, activate 2-3
        if (currentSpread == 0) {
            spreadZero.SetActive(true);
        }

        // deactivate 2-3, activate 4-5
        else if (currentSpread == 1) {
            spreadOne.SetActive(true);
        }

        // deactivate 4-5, activate 6-7
        else if (currentSpread == 2) {
            spreadTwo.SetActive(true);
        }

        else if (currentSpread == 3) {
            spreadThree.SetActive(true);
        }
        
        else if (currentSpread == 4) {
            spreadFour.SetActive(true);
        }

        else if (currentSpread == 5) {
            spreadFive.SetActive(true);
        }

        else if (currentSpread == 6) {
            spreadSix.SetActive(true);
        }

        else if (currentSpread == 7) {
            spreadSeven.SetActive(true);
        }

        else if (currentSpread == 8) {
            spreadEight.SetActive(true);
        }

        else if (currentSpread == 9) {
            spreadNine.SetActive(true);
        }

        /*else if (currentSpread == 10) {
            spreadTen.SetActive(true);
        }

        else if (currentSpread == 11) {
            spreadEleven.SetActive(true);
        }

          else if (currentSpread == 12) {
            spreadTwelve.SetActive(true);
        }*/
    }


    void Update() {
        if (disableAll == false) {
            // goes to settings
            if (Input.GetKeyUp("escape")) {
                currentSpread = 0;
                changeSpread();
            }

            if (disableRight == false) {
                // W, D, ^, >, PgUp - navigate to next page
                if (Input.GetKeyUp("w") || Input.GetKeyUp("d") || Input.GetKeyUp("up") || Input.GetKeyUp("right") || Input.GetKeyUp("page up")) {
                    spreadIncrease();
                    changeSpread();
                }
            }

            if (disableLeft == false) {
                // A, S, v, <, PgDn - navigate to previous page
                if (Input.GetKeyUp("a") || Input.GetKeyUp("s") || Input.GetKeyUp("down") || Input.GetKeyUp("left") || Input.GetKeyUp("page down")) {
                    spreadDecrease();
                    changeSpread();
                }
            }
        }
    }

    public void spreadIncrease() {
        // ONLY if it's less than 12 - never lets int go past 12
        if (currentSpread < 9) {
            currentSpread++;

            changeSpread();
            pageTurn.Play("Page Turn");
        }
        else if (currentSpread == 9) {
            pageTurn.Play("Hit 2");
        }
    }

    // ONLY if it's more than 0 - never lets int go below 0
    public void spreadDecrease() {
        if (currentSpread > 0) {
            currentSpread--;

            changeSpread();
            pageTurn.Play("Page Turn");
        }
        else if (currentSpread == 0) {
            pageTurn.Play("Hit 2");
        }
    }
}
