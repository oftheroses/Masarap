using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour {

    /* for the level select screen:
    * each recipe/level is a button
    * that takes you to its spread
    */

    public SpreadManager sm;
    public int newSpreadInt;

    public void Change() {
        sm.currentSpread = newSpreadInt;
    }
}
