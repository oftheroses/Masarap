using UnityEngine;

public class LeftToRight : MonoBehaviour {

    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + 20, transform.position.y, 0), Time.deltaTime * 4);
    }
}
