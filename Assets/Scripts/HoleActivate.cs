using System.Collections;
using UnityEngine;

public class HoleActivate : MonoBehaviour
{
     //[HideInInspector]
     public GameObject disablePlanet;
     public GameObject currentPlanet;

     private int maxNumVertices;
     private int vertexID;

     private Transform parentRoot;

     public float forceScale;


     private void Awake()
     {
          parentRoot = transform.parent;
          
          // First, need to find the max number of vertices in the current mesh,
          maxNumVertices = SetMaxNumVertices();

          // Then it's okay to set a random vertexID.
          vertexID = RandomVertex(maxNumVertices);

          // After finding vertexID, okay to start using it for calculation.
          SetRotation(vertexID);
     }


     private void FixedUpdate()
     {
          SetPosition(vertexID);
     }


     /// <summary>
     /// Set the maximum number of vertices of the current planet's mesh.
     /// </summary>
     private int SetMaxNumVertices()
     {
          return currentPlanet.GetComponent<VertexObjects>().GetNumberOfVertices();
     }


     private int RandomVertex(int maxVert)
     {
          return Random.Range(0, maxVert);
     }


     private void SetPosition(int vertexID)
     {
          // Update the Hole's position in respect to the planet's vertex world point.
          transform.position = currentPlanet.GetComponent<VertexObjects>().VertexPosition(vertexID);
     }


     private void SetRotation(int normalID)
     {
          Vector3 normalVec = currentPlanet.GetComponent<VertexObjects>().VertexNormalDirection(normalID);
          Quaternion targetDir = Quaternion.FromToRotation(transform.up, normalVec);

          //Update the Hole's rotation in respect to the planet's vertex normal.
          transform.localRotation = targetDir * transform.localRotation;
     }


     private void OnTriggerEnter(Collider player)
     {
          // Only activate if it's the player that triggers this.
          if (player.tag != "Player") return;

          // Disable current planet's collider.
          disablePlanet.GetComponent<MeshCollider>().enabled = false;

          // Add a sharp downward force to the player.
          player.GetComponent<Rigidbody>().AddForce(-Vector3.up * forceScale, ForceMode.Impulse);

          // Change all planets.
          // This is how the player falls towards the next one.
          parentRoot.GetComponent<PlanetManager>().OutsideNextPlanet();

          // Update the maximum number of vertices from the planet's new mesh.
          maxNumVertices = SetMaxNumVertices();

          // Randomize the hole's position on the new planet.
          vertexID = RandomVertex(maxNumVertices);

          // Update Hole's new rotation
          SetRotation(vertexID);
     }



     public void PlanetSurfacePosition()
     {

     }



     public void RandomSurfacePosition()
     {

     }


}
