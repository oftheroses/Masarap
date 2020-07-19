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
    public bool Updated = true;

    void Update() {
         if (taglishScript.languageInt == 0 && Updated == true) {
            Debug.Log("language int 0");
            isTagalog = true;
            isEnglish = false;
            isCebuano = false;

            background.color = new Color32(218, 241, 251, 255); // tagalog color
            VLG.padding.left = (VLG.padding.left) -1;
            VLG.padding.right = (VLG.padding.right) -1;
            Updated = false;
         }
            
         else if (taglishScript.languageInt == 1 && Updated == false) {
            Debug.Log("language int 1");
            isTagalog = false;
            isEnglish = true;
            isCebuano = false;

            background.color = new Color32(251, 244, 218, 255); // english
            VLG.padding.left = (VLG.padding.right) +1;
            VLG.padding.right = (VLG.padding.left) +1;
            Updated = true;
         }

         else if (taglishScript.languageInt == 2 && Updated == true) {
            Debug.Log("language int 2");
            isTagalog = false;
            isEnglish = false;
            isCebuano = true;

            background.color = new Color32(217, 222, 252, 255); // cebuano
            VLG.padding.left = (VLG.padding.right) -1;
            VLG.padding.right = (VLG.padding.left) -1;
            Updated = false;
         }
    }
}
