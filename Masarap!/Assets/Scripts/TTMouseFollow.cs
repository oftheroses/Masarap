using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTMouseFollow : MonoBehaviour {

    public RectTransform TT;

    void Update() {
        transform.position = Input.mousePosition;

        //mouse too high, moving TT below mouse
        if (Input.mousePosition.y >= 548 && TT.localPosition.y == 35) {
            TT.localPosition = new Vector3(TT.localPosition.x, -35, TT.localPosition.z);
        }

        // mouse isn't too high, move back to regular position
        else if (Input.mousePosition.y <= 547 && TT.localPosition.y == -35) {
            TT.localPosition = new Vector3(TT.localPosition.x, 35, TT.localPosition.z);
        }
    }
}
