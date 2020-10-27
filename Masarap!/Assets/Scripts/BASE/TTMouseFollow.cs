using UnityEngine;

public class TTMouseFollow : MonoBehaviour {

    public RectTransform TT;
    public Vector3 hey;

    void Update() {
        hey = Input.mousePosition;
        transform.position = Input.mousePosition;

        if (Input.mousePosition.y > 550) {
            TT.pivot = new Vector2(TT.pivot.x, 0);
        }

        else if (Input.mousePosition.y < 549) {
            TT.pivot = new Vector2(TT.pivot.x, 1);
        }

        if (Input.mousePosition.x > 481) {
            TT.pivot = new Vector2(1, TT.pivot.y);
        }
        else if (Input.mousePosition.x < 480) {
            TT.pivot = new Vector2(0, TT.pivot.y);
        }
    }
}