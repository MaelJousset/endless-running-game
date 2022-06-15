using UnityEngine;
using UnityEngine.UI;

public class showPowerUp : MonoBehaviour
{
    public Text powerUpTxt;

    // Update is called once per frame
    void Update()
    {
        if (GM.powerUp.ToString() != "none")                            //Affiche le power up en cours d'utilisation (bouclier ou score*2)
        {powerUpTxt.text = "Power-Up :" + GM.powerUp.ToString();}       //Si pas de power up, ca n'affiche rien
        else { powerUpTxt.text = ""; }
    }
}
