using UnityEngine;

public class moveCam : MonoBehaviour
{ 
    // Update is called once per frame
    void Update() => GetComponent<Rigidbody>().velocity = new Vector3(0, ball.vertVel, ball.zVelAdj); // bouge a la meme vitesse que la balle selon l'axe z
}
