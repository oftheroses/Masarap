using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {

    public int currentSpread;
    public int level;
    public int lives;
    public int mouseSpeed;
    public bool tooltipEnabled;
    public float SFXVolume;
    public float musicVolume;
    public bool openDyslexic;

    public PlayerData(Player player) {
        currentSpread = player.currentSpread;
        level = player.level;
        lives = player.lives;
        mouseSpeed = player.mouseSpeed;
        tooltipEnabled = player.tooltipEnabled;
        SFXVolume = player.SFXVolume;
        musicVolume = player.musicVolume;
        openDyslexic = player.openDyslexic;
    }
}