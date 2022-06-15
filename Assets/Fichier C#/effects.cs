using UnityEngine;


//Permet de faire tourner les objets a collecter sur eux-memes
//Sert juste pour le design du jeu mais c'est pas tres utile
public class effects : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "powerUp") { transform.Rotate(0, -0.3f, 0); } // Fait tourner l'objet powerUp sur lui-meme

        if (gameObject.tag == "Cylinderbleu") { transform.Rotate(-0.5f, 0, 0); } // Fait tourner l'objet cylindre bleu sur lui-meme

        if (gameObject.tag == "Cylinderred") { transform.Rotate(-0.5f, 0, 0); } // Fait tourner l'objet cylindre rouge sur lui-meme

        if (gameObject.tag == "Cylinderyellow") { transform.Rotate(-0.5f, 0, 0); } // Fait tourner l'objet cylindre jaune sur lui-meme

    }
}
