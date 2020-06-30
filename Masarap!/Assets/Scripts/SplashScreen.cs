using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {

    #region BASE
    public SpriteRenderer cursorImg;

    public GameObject paws;

    private Vector3 mousePos;
    public Rigidbody2D rb;
    private Vector2 direction;

    private static int mouseSpeed;

    public AudioSource collectSound;
    public AudioSource crash;
    #endregion

    #region FOOD
    public GameObject Asukal;
    private GameObject SpawnedAsukal; // declaring so we can change its scale
    private float AsukalScale;

    public GameObject Kamatis;
    private GameObject SpawnedKamatis;
    private float KamatisScale;
    private GameObject SpawnedKamatis2;
    private float KamatisScale2;

    public GameObject Niyog;
    private GameObject SpawnedNiyog;
    private float NiyogScale;

    public GameObject Pichay;
    private GameObject SpawnedPichay;
    private float PichayScale;

    public GameObject Sitaw;
    private GameObject SpawnedSitaw;
    private float SitawScale;
    #endregion


    // InvokeRepeating "void FoodSpawn",
    void Start() {
        InvokeRepeating("FoodSpawn", 0, 7); // starting in 0 seconds, repeat it every 7 seconds
    }

    // Mouse follow
    void Update() {

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * /*Player.mouseSpeed*/ 100, direction.y * /*Player.mouseSpeed*/ 100);

        /*if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Space)) {

            if (paws.activeInHierarchy == false) { // if pause screen isn't *already active*, bring it up

                cursorImg.enabled = !cursorImg.enabled; // hide cursor image
                paws.SetActive(true);

                if (Time.timeScale == 1.0f)
                { // freeze time
                    Time.timeScale = 0.0f;
                }
            }
        }*/
    }

    // Collision sounds
    void OnCollisionEnter2D(Collision2D col) {

        if (col.gameObject.tag == "food") {
            Destroy(col.gameObject);
            collectSound.Play(0);
        }

        if (col.gameObject.tag == "wall") {
            crash.Play(0);
        }
    }


    /*public void UnFreeze() {
        if (Time.timeScale == 0.0f) {
            Time.timeScale = 1.0f;
        }
    }*/

    // Regular "void" doesn't allow delays,
    void FoodSpawn() {
        StartCoroutine(Spawning());
    }

    IEnumerator Spawning() {
        // Sugar - spawns from the top
        AsukalScale = Random.Range(0.5f, 1f);
        SpawnedAsukal = Instantiate(Asukal, new Vector3(Random.Range(-7.15f, 7.15f), (Random.Range(8.7f, 6.31f)), 0), Quaternion.Euler(0, 0, Random.Range(0, 180)));
        SpawnedAsukal.transform.localScale = new Vector3(AsukalScale, AsukalScale, 1);

        yield return new WaitForSeconds(1); // 1 second

        // Tomatoes - spawn from the sides, thrown inward
        KamatisScale = Random.Range(1.0f, 1.75f);
        SpawnedKamatis = Instantiate(Kamatis, new Vector3(Random.Range(-10.62f, -9f), (Random.Range(-4.4f, 0f)), 0), Quaternion.Euler(0, 0, Random.Range(0, 180)));
        SpawnedKamatis.transform.localScale = new Vector3(KamatisScale, KamatisScale, 1);
        Rigidbody2D KamatisRB = SpawnedKamatis.GetComponent<Rigidbody2D>();
        KamatisRB.AddForce(transform.up * 700);
        KamatisRB.AddForce(transform.right * 700);

        yield return new WaitForSeconds(1); // 2 seconds

        // Beans - spawns from the top
        SitawScale = Random.Range(0.55f, 0.75f);
        SpawnedSitaw = Instantiate(Sitaw, new Vector3(Random.Range(9.34f, 10.13f), (Random.Range(8.67f, 3.78f)), 0), Quaternion.Euler(0, 0, Random.Range(0, 180)));
        SpawnedSitaw.transform.localScale = new Vector3(SitawScale, SitawScale, 1);
        Rigidbody2D SitawRB = SpawnedSitaw.GetComponent<Rigidbody2D>();
        SitawRB.AddForce(transform.right * -500);

        yield return new WaitForSeconds(1); // 3 seconds

        // Coconut - spawns from the top
        NiyogScale = Random.Range(0.65f, 1f);
        SpawnedNiyog = Instantiate(Niyog, new Vector3(Random.Range(-7.15f, 7.15f), (Random.Range(8.7f, 6.31f)), 0), Quaternion.Euler(0, 0, Random.Range(0, 180)));
        SpawnedNiyog.transform.localScale = new Vector3(NiyogScale, NiyogScale, 1);

        yield return new WaitForSeconds(1); // 4 seconds

        // Cabbage - thrown up from the bottom
        PichayScale = Random.Range(0.7f, 1f);
        SpawnedPichay = Instantiate(Pichay, new Vector3(Random.Range(-7.46f, 0f), (Random.Range(-8.99f, -5.91f)), 0), Quaternion.Euler(0, 0, Random.Range(0, 180)));
        SpawnedPichay.transform.localScale = new Vector3(PichayScale, PichayScale, 1);
        Rigidbody2D PichayRB = SpawnedPichay.GetComponent<Rigidbody2D>();
        PichayRB.AddForce(transform.up * 2300);

        yield return new WaitForSeconds(1); // 5 seconds

        // Tomatoes - spawn from the sides, thrown inward
        KamatisScale2 = Random.Range(1.0f, 1.75f);
        SpawnedKamatis2 = Instantiate(Kamatis, new Vector3(Random.Range(9f, 10.45f), (Random.Range(0f, 4.75f)), 0), Quaternion.Euler(0, 0, Random.Range(0, 180)));
        SpawnedKamatis2.transform.localScale = new Vector3(KamatisScale2, KamatisScale2, 1);
        Rigidbody2D KamatisRB2 = SpawnedKamatis2.GetComponent<Rigidbody2D>();
        KamatisRB2.AddForce(transform.right * -700);

        yield return new WaitForSeconds(1); // 6 seconds total
    }
}
