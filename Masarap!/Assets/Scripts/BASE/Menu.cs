using UnityEngine.UI;
using UnityEngine;

public class Menu : MonoBehaviour {

    /* i'm in charge of all the level buttons
     * 
     */

    public LevelManager levelmanager;
    public Player player;

    #region breakfast
    public Button Champorado;
    public Button Tosilog;
    public Button Pandesal;
    public Button ginisangAmpalaya;
    public Button tokwatBaboy;
    public Button tortangTalong;
    public Button Sopas;
    public Button ginisangSardinas;
    #endregion

    #region lunch
    #endregion

    #region dinner
    #endregion

    #region snacks
    #endregion

    void Awake() {

        if (levelmanager.tosilog == true) {
            Pandesal.interactable = true;
        }

        if (levelmanager.pandesal == true) {
            ginisangAmpalaya.interactable = true;
        }
    }
}