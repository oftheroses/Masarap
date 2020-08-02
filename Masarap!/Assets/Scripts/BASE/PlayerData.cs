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
    public byte tooltipBGTransparency;
    public float SFXVolume;
    public float musicVolume;
    public bool openDyslexic;

    public int langOne;
    public int langTwo;
    public int langThree;

    public PlayerData(Player player) {
        currentSpread = player.currentSpread;
        level = player.level;
        lives = player.lives;
        mouseSpeed = player.mouseSpeed;
        tooltipEnabled = player.tooltipEnabled;
        tooltipBGTransparency = player.tooltipBGTransparency;
        SFXVolume = player.SFXVolume;
        musicVolume = player.musicVolume;
        openDyslexic = player.openDyslexic;

        langOne = player.langOne;
        langTwo = player.langTwo;
        langThree = player.langThree;
    }
}