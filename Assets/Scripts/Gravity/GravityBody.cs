using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour
{

     GravityAttractor planet;
     Rigidbody body;

     private void Awake()
     {
          planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
          body = GetComponent<Rigidbody>();
          body.useGravity = false;
          body.constraints = RigidbodyConstraints.FreezeRotation;
     }

     private void FixedUpdate()
     {
          planet.Attract(transform, true);
     }
}
