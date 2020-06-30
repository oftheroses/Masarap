using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    public int level = 0;
    public int lives = 3;
    public float mouseSpeed = 100f;

    public void SavePlayer() {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer () {
        PlayerData data = SaveSystem.LoadPlayer();

        level = data.level;
        lives = data.lives;
        mouseSpeed = data.mouseSpeed;
    }
}
