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
    public AudioSource outro;

    public Image progress;
    public GameObject fin;
    #endregion

    #region FOOD
    public GameObject Tuyo;
    private GameObject SpawnedTuyo; // declaring so we can change its scale
    private float TuyoScale;
    public int CollectedTuyo;
    #endregion

    void Awake() {
        FoodRandomiser = Random.Range(1, 4);
        //AM = FindObjectOfType<AudioManager>();
        //player = FindObjectOfType<Player>();

        lead.Play();

        StartCoroutine("Spawning");
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

        if (!lead.isPlaying && !outro.isPlaying) {
            outro.Play();
        }
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
        // -50 from each thing, i guess
        FoodSpawn();                              // 0:00:000 [1]
        FoodRandomiser = Random.Range(1, 4);
        yield return new WaitForSeconds(00.200f);

        FoodSpawn();                              // 0:00:250 [2]
        FoodRandomiser = Random.Range(1, 4);
        yield return new WaitForSeconds(00.200f);

        FoodSpawn();                              // 0:00:500 [3]
        FoodRandomiser = Random.Range(1, 4);
        yield return new WaitForSeconds(00.200f);

        FoodSpawn();                              // 0:00:750 [4]
        FoodRandomiser = Random.Range(1, 4);
        yield return new WaitForSeconds(00.250f);

        FoodSpawn();                              // 0:01:250 [5]
        FoodRandomiser = Random.Range(1, 4);
        yield return new WaitForSeconds(00.200f);

        FoodSpawn();                              // 0:01:500 [6]
        FoodRandomiser = Random.Range(1, 4);
        yield return new WaitForSeconds(00.200f);

        FoodSpawn();                              // 0:01:750 [7]
        FoodRandomiser = Random.Range(1, 4);
        yield return new WaitForSeconds(00.700f);

        FoodSpawn();                              // 0:02:500 [8]
        FoodRandomiser = Random.Range(1, 4);
        yield return new WaitForSeconds(00.200f);

        FoodSpawn();                              // 0:02:750 [9]
        FoodRandomiser = Random.Range(1, 4);
        yield return new WaitForSeconds(00.450f);

        FoodSpawn();                              // 0:03:250 [10]
        FoodRandomiser = Random.Range(1, 4);
        yield return new WaitForSeconds(00.200f);

        FoodSpawn();                              // 0:03:500 [11]
        FoodRandomiser = Random.Range(1, 4);
        yield return new WaitForSeconds(00.200f);

        FoodSpawn(); // 0:03:750 [12]
        FoodRandomiser = Random.Range(1, 4);
        yield return new WaitForSeconds(00.00f);
    }

    void FoodSpawn() {
        // UP -> DOWN [0 rotation]
        if (FoodRandomiser == 1) {
            SpawnedTuyo = Instantiate(Tuyo, new Vector3((Random.Range(-6.454f, 6.454f)), 6.028f, 0), Quaternion.Euler(0, 0, 0));
            //TuyoScale = Random.Range(0.5f, 1f);
            SpawnedTuyo.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }

        // DOWN -> UP [180 rotation]
        else if (FoodRandomiser == 2) {
            SpawnedTuyo = Instantiate(Tuyo, new Vector3(Random.Range(-6.454f, 6.454f), -6.018f, 0), Quaternion.Euler(0, 0, 180));
            //TuyoScale = Random.Range(0.5f, 1f);
            SpawnedTuyo.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            Rigidbody2D TuyoRB = SpawnedTuyo.GetComponent<Rigidbody2D>();
            TuyoRB.AddForce(transform.up * Random.Range(400,700));
        }

        // UL -> CENTER
        else if (FoodRandomiser == 3) {
            SpawnedTuyo = Instantiate(Tuyo, new Vector3(-8.339f, Random.Range(0.71f, 4.2f), 0), Quaternion.Euler(0, 0, 0));
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