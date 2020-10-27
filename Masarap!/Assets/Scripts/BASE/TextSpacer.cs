using UnityEngine;

public class TextSpacer : MonoBehaviour {

    public Taglish taglish;
    public RectTransform rt;
    public bool tagalog = false;
    public Vector3 tagPos;
    public bool english = false;
    public Vector3 engPos;
    public bool cebuano = false;
    public Vector3 cebPos;

    void Update() {
        if (taglish.languageInt == 0 && tagalog == false) {
            tagalog = true;
            rt.anchoredPosition = tagPos;
            english = false;
            cebuano = false;
        }
        else if (taglish.languageInt == 1 && english == false) {
            english = true;
            rt.anchoredPosition = engPos;
            tagalog = false;
            cebuano = false;
        }
        else if (taglish.languageInt == 2 && cebuano == false) {
            cebuano = true;
            rt.anchoredPosition = cebPos;
            tagalog = false;
            english = false;
        }
    }
}
