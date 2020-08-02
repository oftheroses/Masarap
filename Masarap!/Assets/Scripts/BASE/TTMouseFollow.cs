using UnityEngine;

public class TTMouseFollow : MonoBehaviour {

    public RectTransform TT;
    public Vector3 hey;

    void Update() {
        hey = Input.mousePosition;
        transform.position = Input.mousePosition;

        //mouse too high, moving TT below mouse
        if (Input.mousePosition.y > 550) {
            TT.localPosition = new Vector3(TT.localPosition.x, -30, TT.localPosition.z);
        }

        // mouse isn't too high, move back to regular position
        else if (Input.mousePosition.y < 549) {
            TT.localPosition = new Vector3(TT.localPosition.x, 0, TT.localPosition.z);
        }
    }
}
