﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipTransTester : MonoBehaviour {

    /* sample/test tooltip for settings page
     */

    #region basic
    public GameObject tooltip;
    private GameObject spawnedTT;
    private TextMeshProUGUI spawnedTText;
    public TMP_FontAsset denne;
    public Material denneRed;
    private Image tooltipImg;
    #endregion

    public Player player;

    public void Down() {
            spawnedTT = Instantiate(tooltip, new Vector3(0, 0, 0), Quaternion.identity);
            spawnedTT.transform.localScale = new Vector3(0.75f, 0.75f, 1);

            tooltipImg = spawnedTT.GetComponentInChildren<Image>();

            spawnedTT.transform.SetParent(this.transform, false);

            spawnedTText = spawnedTT.GetComponentInChildren<TextMeshProUGUI>();
            spawnedTText.text = "<3";
            spawnedTText.font = denne;
            spawnedTText.fontSharedMaterial = denneRed;
    }
        
    public void Up() {
            tooltipImg = null;
            Destroy(spawnedTT);
    }

    public void Hm() {
        tooltipImg.color = new Color32(255, 255, 255, player.tooltipBGTransparency);
    }
}
