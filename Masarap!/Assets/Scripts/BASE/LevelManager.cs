using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    /* this script is full of sins
     * 
     * it's on buttons
     * 
     * & it's ran on a level
     * 
     * futureshira i'm so sorry
     * i just didn't want to have
     * TWO scripts with every single
     * level name
     * :(
     */

    int sceneID;

    #region almusal
    public bool champorado;
    public bool tosilog;
    public bool pandesal;
    #endregion

    #region tanghalian
    public bool lumpia;
    #endregion

    #region hapunan
    public bool pancit;
    #endregion

    #region meryenda
    public bool halohalo;
    #endregion

    public void CheckScene() {
        sceneID = SceneManager.GetActiveScene().buildIndex;
    }

    #region LEVELSELECT
    public void Splash() {
        SceneManager.LoadScene(0);
    }

    #region breakfast;
    public void Champorado() {
        CheckScene();

        if (sceneID != 1) {
            SceneManager.LoadScene(1);
        }

        else if (sceneID == 1 && champorado == false) {
                champorado = true;
        }
    }

    public void Tosilog() {
        CheckScene();

        if (sceneID != 2) {
            SceneManager.LoadScene(1);
        }

        else if (sceneID == 2 && tosilog == false) {
            tosilog = true;
        }
    }

    public void Pandesal()
    {
        SceneManager.LoadScene(7);
    }

    public void GinisangAmpalaya()
    {

    }

    public void TokwatBaboy()
    {

    }

    public void TortangTalong()
    {

    }

    public void Sopas()
    {

    }

    public void GinisangSardinas()
    {

    }
    #endregion
    #endregion
}
