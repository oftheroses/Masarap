using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour {

    public Player player;
    public int currentLevel;

    public List<Button> dishes;

    void Start() {
        currentLevel = player.level;
    }


    void Update() {
        
    }
}
