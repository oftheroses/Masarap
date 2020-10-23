using UnityEngine;

public class TTMouseFollow : MonoBehaviour {

    public RectTransform TT;
    public Vector3 hey;

    void Update() {
        hey = Input.mousePosition;
        transform.position = Input.mousePosition;

        if (Input.mousePosition.y > 550 && TT.localPosition.y != -30) {
            TT.localPosition = new Vector3(TT.localPosition.x, -30, TT.localPosition.z);
        }

        else if (Input.mousePosition.y < 549 && TT.localPosition.y != 0) {
            TT.localPosition = new Vector3(TT.localPosition.x, 0, TT.localPosition.z);
        }
    }
}