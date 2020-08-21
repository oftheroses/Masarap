using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SplashScreen : MonoBehaviour {

    /* HEADSUP!!
     * this script is laced w sins
     * so if you're smarter than me
     * please don't rub my if statements
     * in my face :v(
     */

    #region BASE
    public AudioManager AM;

    public Button playButton;

    public GameObject walls;
    public bool touchingWall;

    private Vector3 mousePos;
    public Rigidbody2D rb;
    private Vector2 direction;
    private int speed;

    public Player playerScript;

    public AudioSource collectSound;
    public AudioSource crash;

    public int spawnInt;

    public AudioSource bgm;
    #endregion

    #region FOOD
    public GameObject Asukal;
    private GameObject SpawnedAsukal; // declaring so we can change its scale
    private float AsukalScale;

    public GameObject Kamatis;
    private GameObject SpawnedKamatis;
    private float KamatisScale;

    public GameObject Sitaw;
    private GameObject SpawnedSitawLEFT;
    private GameObject SpawnedSitawRIGHT;
    private float SitawScale;

    public GameObject Paminta;
    private GameObject SpawnedPaminta;
    private float PamintaScale;

    public GameObject Karot;
    private GameObject SpawnedKarot;
    private float KarotScale;

    public GameObject Pina;
    private GameObject SpawnedPinaLEFT;
    private GameObject SpawnedPinaRIGHT;
    private float PinaScale;
    #endregion

    // InvokeRepeating "void FoodSpawn",
    void Start() {
        speed = playerScript.mouseSpeed;
        Cursor.visible = false;
        InvokeRepeating("FoodSpawn", 0, 0.65f); // starting in 0 seconds, repeat it every 7 seconds

    }
    
    void Update() {

        #region mouse follow/play
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, direction.y * speed);

        #endregion

        // if anything pushes the pan out of place
        if (transform.rotation.z != 0) {

            // if it's between -15 and +15, move back
            if (transform.rotation.z > -15 && transform.rotation.z < 15) {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, (Quaternion.Euler(0, 0, 0)), Time.deltaTime * 35);
            }
            
            // if the pan falls past -15 or over +15 WHILE touching wall, freeze rotation
            if (transform.eulerAngles.z < -15 || transform.eulerAngles.z > 15) {
                if (touchingWall == true) {
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                }
            }
        }

        // unfreeze rotation when we exit collision with wall
        if (rb.freezeRotation == true && touchingWall == false) {
            rb.freezeRotation = false;
        }
        
    }

    // if wok is touching the wall & is rotated greater than 20, freeze it
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "food") {
            Destroy(col.gameObject);
            collectSound.Play(0);
        }

        if (col.gameObject.tag == "wall") {
            touchingWall = true;
            crash.Play(0);
        }
    }

    // detect if we stop touching wall
    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "wall") {
            touchingWall = false;
        }
    }

    void FoodSpawn() {
        spawnInt = Random.Range(0, 9);

        // Sugar - TOP
        if (spawnInt == 0) {
            AsukalScale = Random.Range(0.55f, 1);
            SpawnedAsukal = Instantiate(Asukal, new Vector3(Random.Range(-7.15f, 7.15f), 5.936f, 0), Quaternion.Euler(0, 0, Random.Range(0, 180)));
            SpawnedAsukal.transform.localScale = new Vector3(AsukalScale, AsukalScale, 1);
            Rigidbody2D AsukalRB = SpawnedAsukal.GetComponent<Rigidbody2D>();

            if (SpawnedAsukal.transform.position.x < 0) {
                AsukalRB.AddTorque(-100);
            }
            else if (SpawnedAsukal.transform.position.x > 0) {
                AsukalRB.AddTorque(100);
            }
        }

        // Pepper - TOP
        else if (spawnInt == 1) {
            PamintaScale = Random.Range(0.75f, 1);
            SpawnedPaminta = Instantiate(Paminta, new Vector3(Random.Range(-6.66f, 6.66f), 6.332f, 0), Quaternion.Euler(0, 0, Random.Range(0, 180)));
            SpawnedPaminta.transform.localScale = new Vector3(PamintaScale, PamintaScale, 1);
            Rigidbody2D PamintaRB = SpawnedPaminta.GetComponent<Rigidbody2D>();

            if (SpawnedPaminta.transform.position.x < 0) {
                PamintaRB.AddTorque(-40);
            }
            else if (SpawnedPaminta.transform.position.x > 0) {
                PamintaRB.AddTorque(40);
            }            
        }

        // Pepper - BOTTOM
        if (spawnInt == 2) {
            PamintaScale = Random.Range(0.75f, 1);
            SpawnedPaminta = Instantiate(Paminta, new Vector3(Random.Range(-6.66f, 6.66f), -6.332f, 0), Quaternion.Euler(0, 0, Random.Range(0, 180)));
            SpawnedPaminta.transform.localScale = new Vector3(PamintaScale, PamintaScale, 1);
            Rigidbody2D PamintaRB = SpawnedPaminta.GetComponent<Rigidbody2D>();

            PamintaRB.gravityScale = -0.55f;
            PamintaRB.AddForce(transform.up * 300);

            if (SpawnedPaminta.transform.position.x < 0) {
                PamintaRB.AddTorque(-40);
            }
            else if (SpawnedPaminta.transform.position.x > 0) {
                PamintaRB.AddTorque(-40);
            }
        }

        //  Bean - LEFT TO RIGHT
        else if (spawnInt == 3) {
            SitawScale = Random.Range(0.5f, 0.75f);
            SpawnedSitawLEFT = Instantiate(Sitaw, new Vector3(-9.545f, Random.Range(-3.542f, 3.542f), 0), Quaternion.Euler(0, 0, Random.Range(0, 180)));
            SpawnedSitawLEFT.transform.localScale = new Vector3(SitawScale, SitawScale, 1);
            Rigidbody2D SitawRB = SpawnedSitawLEFT.GetComponent<Rigidbody2D>();

            SitawRB.AddTorque(25);

            SpawnedSitawLEFT.AddComponent<LeftToRight>();
        }

        // Bean - RIGHT TO LEFT
        else if (spawnInt == 4) {
            SitawScale = Random.Range(0.5f, 0.75f);
            SpawnedSitawRIGHT = Instantiate(Sitaw, new Vector3(9.545f, Random.Range(-3.542f, 3.542f), 0), Quaternion.Euler(0, 0, Random.Range(0, 180)));
            SpawnedSitawRIGHT.transform.localScale = new Vector3(SitawScale, SitawScale, 1);
            Rigidbody2D SitawRB = SpawnedSitawRIGHT.GetComponent<Rigidbody2D>();

            SitawRB.AddTorque(-25);

            SpawnedSitawRIGHT.AddComponent<RightToLeft>();
        }

        // Carrot - TOP
        else if (spawnInt == 5) {
            KarotScale = Random.Range(0.75f, 1);
            SpawnedKarot = Instantiate(Karot, new Vector3(Random.Range(-6.242f, 6.424f), 6.724f, 0), Quaternion.Euler(0, 0, Random.Range(0, 180)));
            SpawnedKarot.transform.localScale = new Vector3(KarotScale, KarotScale, 1);
            Rigidbody2D KarotRB = SpawnedKarot.GetComponent<Rigidbody2D>();
            KarotRB.AddTorque(15);
        }

        // Tomato - BOTTOM
        else if (spawnInt == 6) {
            KamatisScale = Random.Range(0.75f, 1);
            SpawnedKamatis = Instantiate(Kamatis, new Vector3(Random.Range(-7, 7), -5.91f, 0), Quaternion.Euler(0, 0, Random.Range(0, 180)));
            SpawnedKamatis.transform.localScale = new Vector3(KamatisScale, KamatisScale, 1);
            Rigidbody2D KamatisRB = SpawnedKamatis.GetComponent<Rigidbody2D>();

            KamatisRB.AddForce(transform.up * 200);

            if (SpawnedKamatis.transform.position.x < 0) {
                KamatisRB.AddTorque(-75);
            }
            else if (SpawnedKamatis.transform.position.x > 0) {
                KamatisRB.AddTorque(-75);
            }
        }

        // Pineapple - LEFT TO RIGHT
        else if (spawnInt == 7) {
            PinaScale = Random.Range(0.55f, 1f);
            SpawnedPinaLEFT = Instantiate(Pina, new Vector3(-9.242f, Random.Range(-3.677f, 3.677f), 0), Quaternion.Euler(0, 0, Random.Range(0, 180)));
            SpawnedPinaLEFT.transform.localScale = new Vector3(PinaScale, PinaScale, 1);
            Rigidbody2D PinaRB = SpawnedPinaLEFT.GetComponent<Rigidbody2D>();

            PinaRB.AddTorque(25);

            SpawnedPinaLEFT.AddComponent<LeftToRight>();
        }

        // Pineapple - RIGHT TO LEFT
        else if (spawnInt == 8) {
            PinaScale = Random.Range(0.55f, 1f);
            SpawnedPinaRIGHT = Instantiate(Pina, new Vector3(9.242f, Random.Range(-3.677f, 3.677f), 0), Quaternion.Euler(0, 0, Random.Range(0, 180)));
            SpawnedPinaRIGHT.transform.localScale = new Vector3(PinaScale, PinaScale, 1);
            Rigidbody2D PinaRB = SpawnedPinaRIGHT.GetComponent<Rigidbody2D>();

            PinaRB.AddTorque(25);

            SpawnedPinaRIGHT.AddComponent<RightToLeft>();
        }
    }

    public void DeleteFood() {
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("food");
        foreach (GameObject yum in gameObjectArray) {
            yum.SetActive(false);
        }
    }

    public void Play() {
        bgm.volume = 0.1f;
        CancelInvoke("FoodSpawn");
        DeleteFood();
    }

    public void EnterButton() {
        Cursor.visible = true;
    }

    public void ExitButton() {
        Cursor.visible = false;
    }
}
