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
     }


     private void FixedUpdate()
     {
          SetPosition(vertexID);
          SetRotation();
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


     /// <summary>
     /// Update the Hole's position in respect to the planet's vertex world point.
     /// </summary>
     /// <param name="vertexID"></param>
     private void SetPosition(int vertexID)
     {
          
          transform.position = currentPlanet.GetComponent<VertexObjects>().VertexPosition(vertexID);
     }


     /// <summary>
     /// Update the Hole's rotation in respect to the the GravityAttractor in parentRoot.
     /// </summary>
     private void SetRotation()
     {
          parentRoot.GetComponent<GravityAttractor>().Attract(transform, false);
     }


     private void OnTriggerEnter(Collider player)
     {
          // Only activate if it's the player that triggers this.
          if (player.tag != "Player") return;

          // Disable current planet's collider.
          disablePlanet.GetComponent<MeshCollider>().enabled = false;

          // Add a sharp downward force to the player.
          player.GetComponent<Rigidbody>().AddForce(transform.rotation * -Vector3.up * forceScale, ForceMode.Impulse);

          // Change all planets.
          // This is how the player falls towards the next one.
          parentRoot.GetComponent<PlanetManager>().OutsideNextPlanet();

          // Update the maximum number of vertices from the planet's new mesh.
          maxNumVertices = SetMaxNumVertices();

          // Randomize the hole's position on the new planet.
          vertexID = RandomVertex(maxNumVertices);
     }


}
