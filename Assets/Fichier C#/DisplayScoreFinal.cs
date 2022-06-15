using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScoreFinal : MonoBehaviour
{
    public Text scoreFinalTxt;

    [SerializeField]
    private int bestScore;

    public static int scoreFinal;

    void Start()
    {
        if(PlayerPrefs.HasKey("bestScore"))                 // verifie qu'il existe une valeur lie a bestScore
        {
            bestScore = PlayerPrefs.GetInt("bestScore");    // recupere cette valeur
        }
        else                                
        {
            PlayerPrefs.SetInt("bestScore", 0);             // si bestScore n'existe pas, on le creer avec une valeur de 0
        }
    }

    void Update()
    {
        if (scoreFinal <= bestScore)                        // Si le joueur ne bat pas le record existant sur la machine
        {
            scoreFinalTxt.text = "score :" + scoreFinal.ToString() + '\n' + "record :" + bestScore.ToString();      // On affiche le score et le record
        }
        else
        {
            scoreFinalTxt.text = "New Record : " + scoreFinal.ToString();       // On affiche seulement le record
            PlayerPrefs.SetInt("bestScore", scoreFinal);                        // Et on l'enregistre
        }
    }
}
// Ce programme permet d'afficher le score et le record