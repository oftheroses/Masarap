using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls2 : MonoBehaviour {

    /* walls 2: electric bugaloo
     * if i'm in contact with anything for more than 3 seconds
     * delete it
     */

     IEnumerator OnTriggerExit2D(Collider2D collision) {

        if (collision.gameObject.tag == "food") {
            yield return new WaitForSeconds(5);
            Destroy(collision.gameObject);
        }
     }
}