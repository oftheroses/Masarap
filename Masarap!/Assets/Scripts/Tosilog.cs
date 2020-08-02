using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tosilog : MonoBehaviour {

    #region BASE
    public AudioManager AM;

    private Vector3 mousePos;
    public Rigidbody2D rb;
    private Vector2 direction;

    public int FoodRandomiser;

    public GameObject paws;
    public bool isPaws = false;

    public Player player;

    public AudioSource collectSound;
    public AudioSource crash;

    public AudioSource lead;

    public Image progress;
    public GameObject fin;
    #endregion

    #region FOOD
    /*
     * INGREDIENTS:
    6 cloves of garlic [int: ]
    ¾ cup brown sugar
    ¼ cup pineapple juice [int]
    2 tablespoons rice vinegar
    1½ tablespoons salt [int]
    ½ tablespoons black pepper [int: ]
    1 tablespoon soy sauce [int: ]
    1 tablespoon rice flour [int ]
    2 teaspoons achuete [int: 5]
    2 LB PORK
     */

    public GameObject Fish;
    private GameObject SpawnedFish;
    private float FishScale;
    public int CollectedFish;

    public GameObject Garlic;
    private GameObject SpawnedGarlic; // declaring so we can change its scale
    private float GarlicScale;
    public int CollectedGarlic;

    public GameObject Sugar;
    private GameObject SpawnedSugar;
    private float SugarScale;
    public int CollectedSugar;

    public GameObject Juice;
    private GameObject SpawnedJuice;
    private float JuiceScale;
    public int CollectedJuice;

    public GameObject Vinegar;
    private GameObject SpawnedVinegar;
    private float VinegarScale;
    public int CollectedVinegar;

    public GameObject Salt;
    private GameObject SpawnedSalt;
    private float SaltScale;
    public int CollectedSalt;

    public GameObject Pepper;
    private GameObject SpawnedPepper;
    private float PepperScale;
    public int CollectedPepper;

    public GameObject Soy;
    private GameObject SpawnedSoy;
    private float SoyScale;
    public int CollectedSoy;

    public GameObject Flour;
    private GameObject SpawnedFlour;
    private float FlourScale;
    public int CollectedFlour;

    public GameObject Annatto;
    private GameObject SpawnedAnnatto;
    private float AnnattoScale;
    public int CollectedAnnatto;
    #endregion


    // InvokeRepeating "void FoodSpawn",
    void Start() {
        //AM = FindObjectOfType<AudioManager>();
        // player = FindObjectOfType<Player>();
        //StartCoroutine(Spawning());
        //InvokeRepeating("FoodSpawn", 0, 1);
        lead.Play();
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

    // Collision sounds
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "food") {

            //needs to go up 100. in increments of 10. only 10 times
            /*if (CollectedTuyo < 11) {
                progress.rectTransform.position = new Vector3(progress.rectTransform.position.x, (progress.rectTransform.position.y + 10), progress.rectTransform.position.z);
            }
            if (CollectedTuyo == 10) {
                CancelInvoke("FoodSpawn");
                fin.SetActive(true);
                gameObject.SetActive(false);
            }*/

            Destroy(col.gameObject);
            collectSound.Play(0);
        }

        if (col.gameObject.tag == "wall") {
            crash.Play(0);
        }
    }


    // Regular "void" doesn't allow delays,
    void FoodSpawn() {
        /*
        FoodRandomiser = Random.Range(1, 3);

        // top -> bottom
        if (FoodRandomiser == 1) {
            SpawnedTuyo = Instantiate(Tuyo, new Vector3((Random.Range(-6.454f, 6.454f)), 6.5f, 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            TuyoScale = Random.Range(0.5f, 1f);
            SpawnedTuyo.transform.localScale = new Vector3(TuyoScale, TuyoScale, 1);
        }

        // bottom -> thrown up
        else if (FoodRandomiser == 2) {
            SpawnedTuyo = Instantiate(Tuyo, new Vector3(Random.Range(-6.454f, 6.454f), -6.5f, 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            TuyoScale = Random.Range(0.5f, 1f);
            SpawnedTuyo.transform.localScale = new Vector3(TuyoScale, TuyoScale, 1);
            Rigidbody2D TuyoRB = SpawnedTuyo.GetComponent<Rigidbody2D>();
            TuyoRB.AddForce(transform.up * 1000);
        }

        // left -> right
        else if (FoodRandomiser == 3) {
            SpawnedTuyo = Instantiate(Tuyo, new Vector3((Random.Range(-7.26f, 7.14f)), 4.46f, 0), Quaternion.Euler(0, 0, 0));
            TuyoScale = Random.Range(0.5f, 0.65f);
            SpawnedTuyo.transform.localScale = new Vector3(TuyoScale, TuyoScale, 1);
            Rigidbody2D TuyoRB = SpawnedTuyo.GetComponent<Rigidbody2D>();
            TuyoRB.AddForce(transform.up * 700);
            TuyoRB.AddForce(transform.right * 700);
        }

        // right -> left
        else if (FoodRandomiser == 4) {
            SpawnedTuyo = Instantiate(Tuyo, new Vector3((Random.Range(-7.26f, 7.14f)), 4.46f, 0), Quaternion.Euler(0, 0, 0));
            TuyoScale = Random.Range(0.5f, 0.65f);
            SpawnedTuyo.transform.localScale = new Vector3(TuyoScale, TuyoScale, 1);
            Rigidbody2D TuyoRB = SpawnedTuyo.GetComponent<Rigidbody2D>();
            TuyoRB.AddForce(transform.up * 700);
            TuyoRB.AddForce(transform.right * -700);
        }*/
    }

    // but IEnumerator does.
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

    public void DeleteFood() {
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("food");
        foreach (GameObject yum in gameObjectArray) {
            yum.SetActive(false);
        }
    }
}