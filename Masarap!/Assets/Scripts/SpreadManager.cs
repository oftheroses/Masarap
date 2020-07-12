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

    public AudioManager pageTurn;
    public int currentSpread;
    public GameObject spreadZero;
    public GameObject spreadOne;
    public GameObject spreadTwo;
    public GameObject spreadThree;
    public GameObject spreadFour;
    public GameObject spreadFive;
    public GameObject spreadSix;
    #endregion


    public void changeSpread() {

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
    }


    void Update() {
        // goes to settings
        if (Input.GetKeyDown("escape")) {
            currentSpread = 0;
            changeSpread();
        }

        // W, D, ^, >, PgUp - navigate to next page
        if (Input.GetKeyDown("w") || Input.GetKeyDown("d") || Input.GetKeyDown("up") || Input.GetKeyDown("right") || Input.GetKeyDown("page up")) {
            spreadIncrease();
            changeSpread();
        }

        // A, S, v, <, PgDn - navigate to previous page
        if (Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("down") || Input.GetKeyDown("left") || Input.GetKeyDown("page down")) {
            spreadDecrease();
            changeSpread();
        }
    }

    public void spreadIncrease() {
        // ONLY if it's less than 5 - never lets int go past 5
        if (currentSpread < 6) {
            currentSpread++;
            pageTurn.Play("Page Turn");
        }
        else if (currentSpread == 6) {
            pageTurn.Play("Hit 2");
        }
    }

    // ONLY if it's more than 0 - never lets int go below 0
    public void spreadDecrease() {
        if (currentSpread > 0) {
            currentSpread--;
            pageTurn.Play("Page Turn");
        }
        else if (currentSpread == 0) {
            pageTurn.Play("Hit 2");
        }
    }
}
