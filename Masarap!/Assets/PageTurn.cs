using UnityEngine;
using UnityEngine.UI;

public class PageTurn : MonoBehaviour {

    // for level intros

    public AudioManager am;
    public Image self;
    public Sprite bg1;
    public Sprite bg2;
    public Sprite bg3;
    public GameObject p1;
    public GameObject p2;
    public GameObject back1;
    public GameObject back2;

    void Awake() {
        am = FindObjectOfType<AudioManager>();
        PageOne();
    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow)) {
            if (p1.activeInHierarchy == true) {
                PageTwo();
            }
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.DownArrow)) {
            if (p2.activeInHierarchy == true) {
                PageOne();
            }
        }
    }

    public void PageOne() {
        self.sprite = bg1;
        am.Play("Page Turn");
        p1.SetActive(true);
        p2.SetActive(false);

        back1.SetActive(true);
        back2.SetActive(false);
    }
    public void PageTwo() {
        self.sprite = bg2;
        am.Play("Page Turn");
        p1.SetActive(false);
        p2.SetActive(true);

        back1.SetActive(false);
        back2.SetActive(true);
    }

    public void Close() {
        self.sprite = bg3;
        back2.SetActive(false);
        self.rectTransform.localRotation = Quaternion.Euler(0, 180, 0);
    }
    public void Open() {
        self.sprite = bg2;
        back2.SetActive(true);
        self.rectTransform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}