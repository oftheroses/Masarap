using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Malee.List;

public class SpreadManager : MonoBehaviour {
    #region BASICS

    public GameObject tutorial1;
    public GameObject tutorial2;

    public Player player;
    public AudioManager pageTurn;
    public int currentSpread;

    public bool disableAll = false;
    public bool disableRight = false;
    public bool disableLeft = false;

    // the background page & img assets. denne/dyslexic text is handled in settings
    [Reorderable(paginate = true, pageSize = 20)]
    public GameObjectList spreads;
    [System.Serializable]
    public class GameObjectList : ReorderableArray<GameObject> {
    }
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

        // deactivate all pages
        foreach (GameObject spread in spreads) {
            spread.SetActive(false);
        }

        spreads[currentSpread].SetActive(true);
    }


    void Update() {
        if (disableAll == false) {
            // goes to settings
            if (Input.GetKeyUp("escape")) {
                currentSpread = 0;
                changeSpread();
            }

            if (disableRight == false) {
                // KB: W, D, UpArrow, RightArrow, PgUp / PS4: DPad right, DPad up
                // navigate to NEXT page
                if (Input.GetKeyUp("w") || Input.GetKeyUp("d") || Input.GetKeyUp("up") || Input.GetKeyUp("right") || Input.GetKeyUp("page up")) {
                    spreadIncrease();
                    changeSpread();
                }
            }

            if (disableLeft == false) {
                // KB: A, S, DownArrow, LeftArrow, PgDn / PS4: DPad left, DPad down
                // navigate to PREVIOUS page
                if (Input.GetKeyUp("a") || Input.GetKeyUp("s") || Input.GetKeyUp("down") || Input.GetKeyUp("left") || Input.GetKeyUp("page down")) {
                    spreadDecrease();
                    changeSpread();
                }
            }
        }
    }

    public void spreadIncrease() {
        if (disableAll == false && disableRight == false) {

            if (currentSpread < 4) {
                currentSpread++;

                changeSpread();
                pageTurn.Play("Page Turn");
            }
            else if (currentSpread == 4) {
                pageTurn.Play("Hit 2");
            }
        }
    }

    public void spreadDecrease() {
        if (disableLeft == false && disableAll == false) {
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
}
