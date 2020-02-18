using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
     private float gravity;
     private float flip = 1;

     public float gravityAmount;
     public bool flipGravity = false;


     /// <summary>
     /// Attracts any gravity bodies towards the center of this attractor (a.k.a the planet).
     /// </summary>
     /// <param name="body"></param>
     public void Attract(Transform body)
     {
          Vector3 targetDirection = (body.position - transform.position).normalized;

          // If gravity is flipped, flip the player's up vector,
          // so they're always falling towards their feet.
          Vector3 bodyUp = body.up * flip; 
          body.rotation = Quaternion.FromToRotation(bodyUp, targetDirection) * body.rotation;

          body.GetComponent<Rigidbody>().AddForce(targetDirection * gravity);

          // When activated, flip the gravity.
          FlipGravity();
     }



     /// <summary>
     /// Used to flip the gravity and orientation of the player.
     /// </summary>
     private void FlipGravity()
     {
          if (flipGravity)
          {
               gravity = gravityAmount;
               flip = -(gravityAmount / gravityAmount);
          }
          else
          {
               gravity = -gravityAmount;
               flip = (gravityAmount / gravityAmount);
          }
     }
}
