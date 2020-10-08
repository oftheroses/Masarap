using System.Collections;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour {

    private AudioManager am;

    private Player player;
    public TextMeshProUGUI text;

    public TMP_FontAsset denne;
    public Material delica;

    public TMP_FontAsset open;
    public Material dyslexic;

    public GameObject recipe;

    public GameObject prog;

    public float s1;
    public float s2;
    public float s3;

    void Awake() {
        am = FindObjectOfType<AudioManager>();
        player = FindObjectOfType<Player>();

        if (player.openDyslexic == true) {
            text.font = open;
            text.fontSharedMaterial = dyslexic;
        }
        else if (player.openDyslexic == false) {
            text.font = denne;
            text.fontSharedMaterial = delica;
        }

        StartCoroutine(Tick());
    }
    
    IEnumerator Tick() {
        //am.Play("Hit 1");
        text.text = "3";
        yield return new WaitForSeconds(s1);
        //am.Play("Hit 1");
        text.text = "2";
        yield return new WaitForSeconds(s2);
        //am.Play("Hit 1");
        text.text = "1";
        yield return new WaitForSeconds(s3);
        //am.Play("Hit 2");

        gameObject.SetActive(false);
        //recipe.SetActive(true);
    }
}
