using UnityEngine;

public class Syncopation : MonoBehaviour {

    /* when player starts game, let "intro" play for one last time,
     * activate the 3-2-1 countdown, then start "song" at end of
     * countdown.
     * 
     * when "song" ends, play outro
     */

    public AudioSource intro;
    public AudioSource song;
    public AudioSource outro;

    // detect intro end, deactivate it, play song
    public void Intro2Song() {
        
    }

    // detect song end, deactivate it, play outro
    public void Song2Outro() {

    }
}
