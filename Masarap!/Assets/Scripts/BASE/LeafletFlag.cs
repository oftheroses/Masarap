using UnityEngine.UI;
using UnityEngine;

public class LeafletFlag : MonoBehaviour {
    /* Flags leaflets/bookmarks/etc.
     * whether clicked for first time
     */

    public Image img;
    public bool initiallyClicked = false;
    public Animation anim;

    void Start() {
        if (initiallyClicked == false) {
            img.color = new Color32(216, 0, 21, 255);
        }
        else if (initiallyClicked == true) {
            img.color = new Color32(253, 164, 172, 255);
        }
    }

    // Update is called once per frame
    public void Click() {
        if (initiallyClicked == false) {
            initiallyClicked = true;
            img.color = new Color32(253, 164, 172, 255);
        }
    }

    public void EnterHover() {
        anim.Play("Leaflet Open Animation");
    }

    public void ExitHover() {
        anim.Play("Leaflet Close Animation");
    }
}
