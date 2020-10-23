using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    #region basic
    private Player player;
    private AudioManager AM;
    public TextMeshProUGUI text;
    private bool dyslexicTooltip; // ping's player settings to set tooltip font calibri/opendyslexic
    public GameObject tooltip; // tooltip prefab
    private GameObject spawnedTT; // declaring spawned TT to move it's position
    private TextMeshProUGUI spawnedTText; // declaring spawnedTT text so it can be translated
    private Color tooltipColor;
    public byte red;
    public byte green;
    public byte blue;
    private byte tooltipAlpha;
    private VerticalLayoutGroup tooltipVLG; // terrible. pure shit coding going on here.
    private Image tooltipImg;
    [TextArea]
    public string tooltipText;

    public TMP_FontAsset calibri;
    public TMP_FontAsset dyslexic;

    public Material calibriMat;
    public Material dyslexicMat;
    #endregion

    public void Start() {
        AM = FindObjectOfType<AudioManager>();
        player = FindObjectOfType<Player>();

        Settings(); // Ping user's settings whether TT is enabled
    }

    public void Settings() {
        tooltipAlpha = player.tooltipBGTransparency;

        dyslexicTooltip = player.openDyslexic;

        tooltipColor = new Color32(red, green, blue, tooltipAlpha);
    }

    public void TTUpdater() {
        spawnedTText.text = tooltipText;
        tooltipImg.color = tooltipColor;

        tooltipVLG.padding.left = 14;
        tooltipVLG.padding.right = 14;

        if (dyslexicTooltip == false) {
            spawnedTText.fontSharedMaterial = calibriMat;
        }
        else if (dyslexicTooltip == true) {
            spawnedTText.fontSharedMaterial = dyslexicMat;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        Settings();

        spawnedTT = Instantiate(tooltip, new Vector3(0, 0, 0), Quaternion.identity);
        spawnedTT.transform.SetParent(this.transform, false); // making it the child of the base text

        spawnedTText = spawnedTT.GetComponentInChildren<TextMeshProUGUI>(); // spawnedTT is the BG, TMPro is in the child

        tooltipImg = spawnedTT.GetComponentInChildren<Image>();
        tooltipVLG = spawnedTT.GetComponentInChildren<VerticalLayoutGroup>();
        TTUpdater();
    }

    public void OnPointerExit(PointerEventData eventData) {
        tooltipImg = null;
        tooltipVLG = null;
        Destroy(spawnedTT);
    }
}