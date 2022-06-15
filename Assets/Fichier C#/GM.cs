using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    //variable qui sont affichees a l'ecran
    public static int canTotal = 0;
    public static int scoreTotal = 0;
    
    public static float timeTotal = 0;
    
    public static string powerUp = "none";

    //permet de savoir si la partie est fini ou non
    public static bool gameFinish = false;

    private int randNum;
    private float waitToLoad = 0;
    private float zScenePos = 0;    //Permet de connaitre la position de la balle, pour pouvoir instantier les objets a une certaine distance de la balle
    private float randNumObst;
    private int lignecan = 0;
    private int lignenb = 0;
    private int palier = 100;   //Variable qui permet d'augmenter la vitesse du jeu tous les i*palier

    private bool entite1 = false;
    private bool entite2 = false;
    private bool entite3 = false;

    // Les variables de type Transform donne la position (x,y,z), la taille (x,y,z) et la rotation (x,y,z) des objets
    public Transform obstacleObj;   //Lie a l'objet obstacle
    public Transform PowerObj;      //Lie a l'objet Power (objet qui fait *2 pour le score, ou qui donne un bouclier)
    public Transform LifeObj;       //Lie a l'objet Life (objet qui rend une vie)
    public Transform basicBloc;     //Lie a l'objet Bloc (le sol et les barrières)
    public Transform Bleu_canObj;   //Lie a l'objet Cannette Bleu
    public Transform Red_canObj;    //Lie a l'objet Cannette Rouge
    public Transform Yellow_canObj; //Lie a l'objet Cannette Jaune

    //permet d'instancier une ligne de nb objet obj
    void ligne_instance(int nb, Transform obj)
    {
        float recurrence = Random.Range(0, 100);

        //variables permettant d'eviter d'avoir un autre objet sur cette ligne
        int lcan = lignecan;    //variable pour savoir le nombre d'instance restant sur la ligne
        int lnb = lignenb;      //variable pour savoir la colonne correspondante à la ligne d'instance
        if (recurrence < 10 & lcan == 0)
        {
            int randomT = Random.Range(-1, 2);
            lignecan = nb + 2;
            lignenb = randomT;

            int i = 0;
            while (i <= nb)
            {
                Instantiate(obj, new Vector3(randomT, 1, zScenePos + 6 * i), obj.rotation);
                i++;
            }
        }
    }

    //fonction qui instancie un objet sur l'une des lignes
    void instance_obj(float randNum,
                      float limite,
                      Transform obj)
    {
        int lcan = lignecan;
        int lnb = lignenb;

        if (randNum < limite)
        {
            int randNumCoin = Random.Range(-1, 1);
            if (randNumCoin == -1 & (lcan == 0 | lnb != -1))    //verification qu'il s'agit bien de cette ligne && verification qu'il n'y pas déjà un ligne d'instance sur cette ligne
            {
                bool e1 = entite1;
                if (e1 == false)                                //verifier qu'il n'y pas deja un obstacle
                {
                    Instantiate(obj, new Vector3(-1, 1, zScenePos), obj.rotation);
                    entite1 = true;
                }
            }
            else if (randNumCoin == 0 & (lcan == 0 | lnb != 0))
            {
                bool e2 = entite2;
                if (e2 == false)
                {
                    Instantiate(obj, new Vector3(0, 1, zScenePos), obj.rotation);
                    entite2 = true;
                }
            }
            else if ((lcan == 0 | lnb != 1))
            {
                bool e3 = entite3;
                if (e3 == false)
                {
                    Instantiate(obj, new Vector3(1, 1, zScenePos), obj.rotation);
                    entite3 = true;
                }
            }
        }
    }

    // Cette fonction permet d'instancier les blocs de sol sur 120 mètres
    void initiale_instantiation()
    {
        for (int i = 0; i < 0b110; i++)
        {
            Instantiate(basicBloc, new Vector3(0, 0, i * 6), basicBloc.rotation);
            zScenePos = i * 6;
        }
    }


    //fonction permettant de generer des instances avec des frequences aleatoires
    void random_instantiation()
    {

        //frequence pour les obstacles (augmente au fil du temps)
        int freq = 60;
        if (timeTotal >= 15)
        {
            if (timeTotal >= 35)
            {
                if (timeTotal >= 50)
                {
                    if (timeTotal >= 65)
                    {
                        freq = 100;
                    }
                    else
                    {
                        freq = 95;
                    }
                }
                else
                {
                    freq = 90;
                }
            }
            else
            {
                freq = 80;
            }
        }
        else
        {
            freq = 70;
        }
        if (zScenePos >= 120 + moveOrb.zPosition)
        {
            return;
        }
        randNum = Random.Range(1, 100);
        int randNum2 = Random.Range(5, 10);

        int can_number = Random.Range(0, 3);
        //liste des 3 cannetes differentes
        List<Transform> can = new List<Transform>();
        // Ajouter des elements a la liste 
        can.Add(Bleu_canObj);
        can.Add(Yellow_canObj);
        can.Add(Red_canObj);

        //instanciation des differents objets
        ligne_instance(randNum2, can[can_number]);
        instance_obj(randNum, 20, can[can_number]);
        instance_obj(randNum, freq, obstacleObj);
        instance_obj(randNum, 10, PowerObj);
        instance_obj(randNum, 10, LifeObj);

        //instanciation d'un bloc
        Instantiate(basicBloc, new Vector3(0, 0, zScenePos), basicBloc.rotation);
        zScenePos += 6;
        scoreTotal += (int)(zScenePos / 60);

        //initialisation des variables verifiant si un objet existant à telle ligne
        entite1 = false;
        entite2 = false;
        entite3 = false;

        int lcan = lignecan;
        if (lcan > 0) {lignecan -= 1;}
    }

    //Permet de changer de scene quand le joueur n'a plus de vie. Passe a la scène GameEnd
    void changeScene()
    {
        if (gameFinish == true) 
        {
            waitToLoad += Time.deltaTime;
            DisplayScoreFinal.scoreFinal = scoreTotal;
        }
        if (waitToLoad > 0.1)
        {
            SceneManager.LoadScene("GameEnd");
            waitToLoad = 0;
        }
    }

    //Permet de faire accelerer le jeu tous les i*paliers (palier = 100m)
    void speed_up()
    {
        if(palier < 800)
        {
            if (zScenePos > palier)
            {
                ball.zVelAdj += 2;
                palier += 100;
            }
        }
        else if(palier < 3000)
        {
            if (zScenePos > palier)
            {
                ball.zVelAdj += 1.2f;
                palier += 100;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        initiale_instantiation();
        moveOrb.laneNum = 2;        // Permet de ne pas avoir de decalage au redemarrage d'une partie
        // Remise au valeur initiale quand un commence la partie
        ball.zVelAdj = 16;
        ball.vieTotal = 4;
        GM.canTotal = 0;
        GM.scoreTotal = 0;
        GM.timeTotal = 0;
        GM.powerUp = "none";
    }

    // Update is called once per frame
    void Update()
    {
        random_instantiation();
        timeTotal += Time.deltaTime;    //Permet de savoir depuis combien de temps on joue (pour augmenter la difficulte au niveau de l'apparation des obstacles)
        speed_up();
        changeScene();
    }
}
