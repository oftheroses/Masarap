using UnityEngine;
using UnityEngine.UI;

public class TooltipResizer : MonoBehaviour {

    /* works w/ taglish script,
     * for guides / help tip boxes
     * resizes them & changes color
     * 
     * i have no idea how this script works
     * in reality, but like. it does! so.
     */

    public Image background;
    public Taglish taglishScript;
    public bool isTagalog = false;
    public bool isEnglish = false;
    public bool isCebuano = false;
    public VerticalLayoutGroup VLG;

    public void Tagalog() {
        if (taglishScript.languageInt == 0) {
            isTagalog = true;
            isEnglish = false;
            isCebuano = false;

            background.color = new Color32(218, 241, 251, 255); // tagalog color
            VLG.padding.left = 25;
            VLG.padding.right = 25;
        }
    }

    public void English() {
            
          if (taglishScript.languageInt == 1) {
            isTagalog = false;
            isEnglish = true;
            isCebuano = false;

            background.color = new Color32(251, 244, 218, 255); // english
            VLG.padding.left = 24;
            VLG.padding.right = 24;
         }
    }


    public void Cebuano() {
        if (taglishScript.languageInt == 2) {
            isTagalog = false;
            isEnglish = false;
            isCebuano = true;

            background.color = new Color32(217, 222, 252, 255); // cebuano
            VLG.padding.left = 25;
            VLG.padding.right = 25;
        }
    }
}
