using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour {

    /* for the level select screen:
    * each recipe/level is a button
    * that takes you to its spread
    * 
    * +
    * 
    * activates/changes appearance
    * of levels to indicate the player
    * has completed them successfully
    */

    public Player player;
    public int currentSpread;
    public int currentLevel;

    public List<Button> dishes;

    void Start() {
        currentLevel = player.level;
    }


    void Update() {
        
    }
}
