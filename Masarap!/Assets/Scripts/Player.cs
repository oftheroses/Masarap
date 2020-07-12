using UnityEngine;

public class Player : MonoBehaviour {

    public static Player instance;

    public int currentSpread;
    public int level;
    public int lives;
    public int mouseSpeed;
    public bool tooltipEnabled;
    public float musicVolume;
    public float SFXVolume;
    public bool openDyslexic;

    void Awake() {

        if (instance == null)
            instance = this;
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
    

    public void SavePlayer() {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer () {
        PlayerData data = SaveSystem.LoadPlayer();

        currentSpread = data.currentSpread;
        level = data.level;
        lives = data.lives;
        mouseSpeed = data.mouseSpeed;
        tooltipEnabled = data.tooltipEnabled;
        SFXVolume = data.SFXVolume;
        musicVolume = data.musicVolume;
        openDyslexic = data.openDyslexic;
    }
}
