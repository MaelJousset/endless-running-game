using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mur : MonoBehaviour
{
    // Update is called once per frame
    void Update() => GetComponent<Rigidbody>().velocity = new Vector3(ball.horizVel, ball.vertVel, ball.zVelAdj);

    void OnTriggerEnter(Collider other) => Destroy(other.gameObject);// it's here we do things when we have a collision with an object with "is trigger" active
}
