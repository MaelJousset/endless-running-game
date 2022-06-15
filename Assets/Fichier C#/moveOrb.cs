using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveOrb : MonoBehaviour
{
    public KeyCode moveL;   // a 1 quand on appuie sur la touche Q
    public KeyCode moveR;   // a 1 quand on appuie sue la touche D

    public static int laneNum = 2;
    public static float zPosition;

    public int boost = 1;
    public int boost2 = 1;

    public AudioClip audioMvtRight;  // attributs representant le clip audio sur unity
    public AudioClip audioMvtLeft;
    public AudioClip audioLife;
    public AudioClip audioPowerUp;
    public AudioClip audioBloc;
    public AudioClip audioGameOver;
    public AudioSource audioPerso;

    void Awake() => audioPerso = GetComponent<AudioSource>(); // composant audio representant le son emis par l'objet

    // Permet de se deplacer selon l'axe x (droite/gauche)
    void moveX()
    {
        if ((Input.GetKeyDown(moveL)) && (laneNum > 1))
        {
            audioPerso.PlayOneShot(audioMvtLeft); // Jouer un son et modifier le volume
            Vector3 temp = new Vector3(1.0f, 0, 0);
            gameObject.transform.position -= temp;
            laneNum -= 1;
        }
        if ((Input.GetKeyDown(moveR)) && (laneNum < 3))
        {
            audioPerso.PlayOneShot(audioMvtRight,(float)0.3);
            Vector3 temp = new Vector3(1.0f, 0, 0);
            gameObject.transform.position += temp;
            laneNum += 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        zPosition = transform.position.z;
        GetComponent<Rigidbody>().velocity = new Vector3(ball.horizVel, ball.vertVel, ball.zVelAdj);
        moveX();
    }

    // Permet de gerer la collision entre la balle et les autres objets
    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "lethal")               //Collision avec un obstacle
        {
            audioPerso.PlayOneShot(audioBloc,(float)0.5);
            GM.scoreTotal -= 10;                            //Diminution du score
                                                
            if (ball.vieTotal > 1)
            {                                   
                Destroy(other.gameObject);                  // Si il reste des points de vie on detruit l'obstacle
                ball.vieTotal -= 1*boost2;                         //Reduction des points de vie 
            }
            else
            {
                GM.gameFinish = true;
                audioPerso.PlayOneShot(audioGameOver,1);    //Si les points de vie sont egales a zero on detruit la balle et on charge la scene GameEnd
                ball.zVelAdj = 0;                           //Et passe sa vitesse a zero
                Destroy(gameObject);
            }
        }
        if (other.gameObject.tag== "Cylinderbleu" || other.gameObject.tag == "Cylinderyellow" || other.gameObject.tag == "Cylinderred")
        {
            Destroy(other.gameObject);                      // Si la balle touche une cannette, la cannette est detruit
            GM.scoreTotal += 2*boost;                       // Le score est incremente de 2
            GM.canTotal += 1;                               // Le nombre de cannettes ramassees est incremente de 1
        }
        if (other.gameObject.tag == "powerUp")
        {
            Destroy(other.gameObject);                      // Si la balle touche un powerUp, le powerUp est detruit
            audioPerso.PlayOneShot(audioPowerUp, (float)0.5);
            GM.scoreTotal += 2;                             // Le score est incremente de 2
            int number = Random.Range(0, 2);
            if (number == 0)
            {
                boost = 2;
                GM.powerUp = "score x2";
            }
            if (number == 1)
            {
                GM.powerUp = "bouclier";
                boost2 = 0;
            }
            yield return new WaitForSeconds(10f);
            GM.powerUp = "none";
            boost = 1;
            boost2 = 1;
        }
        if (other.gameObject.tag == "Life")                 //Si la balle touche une vie, la vie est detruit
        {
            Destroy(other.gameObject);
            audioPerso.PlayOneShot(audioLife,(float)0.3);
            GM.scoreTotal += 2*boost;                       // Le nombre de vie augmente si il est inferieur a 4
            if (ball.vieTotal == 4) {ball.vieTotal = 4; }   // Le score est incremente de 2
            else {ball.vieTotal += 1;}
        }
    }
}
