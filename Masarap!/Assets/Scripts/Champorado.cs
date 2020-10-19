using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class Champorado : MonoBehaviour {

    #region BASE
    public Rigidbody2D BGrb;
    public bool stuckTop = false;
    public bool stuckBottom = false;
    public bool stuckRight = false;
    public bool stuckLeft = false;

    public Player player;
    public AudioManager AM;

    public GameObject paws;
    public bool isPaws = false;

    private Vector3 mousePos;
    public Rigidbody2D rb;
    private Vector2 direction;

    public int FoodRandomiser;


    public AudioSource collectSound;
    public AudioSource crash;

    public AudioSource lead;

    public Image progress;
    public GameObject fin;
    #endregion

    #region FOOD
    public GameObject Tuyo;
    private GameObject SpawnedTuyo; // declaring so we can change its scale
    private float TuyoScale;
    public int CollectedTuyo;
    #endregion

    // InvokeRepeating "void FoodSpawn",
    void Awake() {
        //AM = FindObjectOfType<AudioManager>();
        //player = FindObjectOfType<Player>();

        lead.Play();

        if (player.level == 0) {

        }

        else if (player.level > 1) {
            
        }
    }

    void Update() {

        // Mouse follow
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * player.mouseSpeed, direction.y * player.mouseSpeed);

        // paws
        if (Input.GetKeyDown(KeyCode.Escape)) {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            lead.Pause();
            Time.timeScale = 0;
        }

        // maybe add interactive buttons to drum?
    }

    public void Unpaws() {
            rb.constraints = RigidbodyConstraints2D.None;
            lead.Play();
            Time.timeScale = 1;
            paws.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "food") {
            CollectedTuyo++;

            // start: -81, end 0
            if (CollectedTuyo < 19) {
                progress.rectTransform.position = new Vector3(progress.rectTransform.position.x, (progress.rectTransform.position.y + 4.5f), progress.rectTransform.position.z);
            }

            if (CollectedTuyo == 18) {
                CancelInvoke("Hey");
                StopAllCoroutines();
                fin.SetActive(true);
                //gameObject.SetActive(false);
            }

            Destroy(col.gameObject);
            collectSound.Play(0);
        }

        if (col.gameObject.tag == "wall") {
            crash.Play(0);
        }
    }

    void Play() {
        StartCoroutine("Spawning");
    }

    IEnumerator Spawning() {
        // song is 32.065

        FoodSpawn();

        yield return new WaitForSeconds(0.050f); //00.300 [2]

        FoodSpawn();

        yield return new WaitForSeconds(0.350f); //0.900 [4]

        FoodSpawn();

        yield return new WaitForSeconds(0.800f); //1.835 [6]

        FoodSpawn();
    }

    void FoodSpawn() {
        // UP -> DOWN
        if (FoodRandomiser == 1) {
            SpawnedTuyo = Instantiate(Tuyo, new Vector3((Random.Range(-6.454f, 6.454f)), 6.5f, 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            //TuyoScale = Random.Range(0.5f, 1f);
            SpawnedTuyo.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }

        // DOWN -> UP
        else if (FoodRandomiser == 2) {
            SpawnedTuyo = Instantiate(Tuyo, new Vector3(Random.Range(-6.454f, 6.454f), -6.5f, 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            //TuyoScale = Random.Range(0.5f, 1f);
            SpawnedTuyo.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            Rigidbody2D TuyoRB = SpawnedTuyo.GetComponent<Rigidbody2D>();
            TuyoRB.AddForce(transform.up * Random.Range(400,700));
        }

        // UL -> CENTER
        else if (FoodRandomiser == 3) {
            SpawnedTuyo = Instantiate(Tuyo, new Vector3(-8.748f, Random.Range(0.71f, 4.2f), 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            //TuyoScale = Random.Range(0.5f, 0.65f);
            SpawnedTuyo.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            Rigidbody2D TuyoRB = SpawnedTuyo.GetComponent<Rigidbody2D>();
            TuyoRB.AddForce(transform.right * Random.Range(300,700));
        }

        // BL -> CENTER
        else if (FoodRandomiser == 4) {
            SpawnedTuyo = Instantiate(Tuyo, new Vector3(-8.748f, Random.Range(-4.355f, -0.71f), 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            //TuyoScale = Random.Range(0.5f, 0.65f);
            SpawnedTuyo.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            Rigidbody2D TuyoRB = SpawnedTuyo.GetComponent<Rigidbody2D>();
            TuyoRB.AddForce(transform.up * Random.Range(400, 600));
            TuyoRB.AddForce(transform.right * Random.Range(400,500));
        }

        // UR -> CENTER
        else if (FoodRandomiser == 5) {
            SpawnedTuyo = Instantiate(Tuyo, new Vector3(8.76f, Random.Range(0.74f, 4.26f), 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            //TuyoScale = Random.Range(0.5f, 0.65f);
            SpawnedTuyo.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            Rigidbody2D TuyoRB = SpawnedTuyo.GetComponent<Rigidbody2D>();
            TuyoRB.AddForce(transform.right * Random.Range(-300, -500));
        }

        // BR -> CENTER
        else if (FoodRandomiser == 6) {
            SpawnedTuyo = Instantiate(Tuyo, new Vector3(8.8f, Random.Range(-4.35f, 0.73f), 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            //TuyoScale = Random.Range(0.5f, 0.65f);
            SpawnedTuyo.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            Rigidbody2D TuyoRB = SpawnedTuyo.GetComponent<Rigidbody2D>();
            TuyoRB.AddForce(transform.up * Random.Range(400, 500));
            TuyoRB.AddForce(transform.right * Random.Range(-400, -500));
        }
    }

    public void DeleteFood() {
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("food");
        foreach (GameObject yum in gameObjectArray) {
            yum.SetActive(false);
        }
    }
}