using UnityEngine;
using UnityEngine.UI;

public class stats : MonoBehaviour
{
    public Text scoreTxt;

    void Update() => scoreTxt.text = "Score : " + GM.scoreTotal.ToString() + '\n' + "picked up cans : " + GM.canTotal.ToString() + '\n' + "Vies : " + ball.vieTotal.ToString();
}
// Ce programme permet l'affichage du score, du nombre de cannettes recoltes, et du nombre de vies restantes