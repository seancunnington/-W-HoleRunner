using UnityEngine;

public class PlanetManager : MonoBehaviour
{
     Transform outerP;
     Transform middleP;
     Transform innerP;

     public int maxScale;
     public float scaleSpeed = 0.5f;

     public PlanetLists rootList;
     private int maxPlanetSize;
     private int maxMaterialSize;


     private void Awake()
     {
          outerP = transform.Find("OuterPlanet");
          middleP = transform.Find("MiddlePlanet");
          innerP = transform.Find("InnerPlanet");

          maxPlanetSize = rootList.planetMeshes.Length;
          maxMaterialSize = rootList.materials.Length;
     }


     private void FixedUpdate()
     {
          ScaleDown(outerP);
          ScaleDown(middleP);
          ScaleDown(innerP);
     }


     /// <summary>
     /// Scales down the planet being referenced, if it hasn't already reached the mininum scaling amount (10).
     /// </summary>
     /// <param name="planet"></param>
     private void ScaleDown(Transform planet)
     {
          // If the planet has reached the minimum scaling (hardcoded to 10), stop scaling.
          if (planet.transform.localScale.x <= 10)
               return;

          // Else, scale down.
          float scaling = Time.fixedDeltaTime * scaleSpeed;
          planet.transform.localScale -= new Vector3(scaling, scaling, scaling);
     }



     /// <summary>
     /// Generate a new scale based on the scales of two planets.
     /// </summary>
     /// <returns></returns>
     private float GenerateScale(Transform p1, Transform p2)
     {
          return p1.localScale.x + p2.localScale.x;
     }


     public void OutsideNextPlanet()
     {
          // Set the INNER planet to the current MIDDLE planet.
          // (to preserve the planet falling from)
          SetPlanet(innerP, middleP);

          // Set the MIDDLE planet's mesh and scale to the current OUTER planet's.
          // (the current planet to fall towards)
          SetPlanet(middleP, outerP);

          // Generate a OUTER planet.
          // (the next planet to fall towards)
          GeneratePlanet(outerP, GenerateScale(innerP, middleP));

          // The MIDDLE planet's collider has been disabled via the Hole Object. 
          // After a brief delay, re-enable it here.
          Invoke("ReactivateCollider", 2f);
     }


     /// <summary>
     /// Assign a planet random attributes for: mesh, material, rotation and scale. 
     /// </summary>
     /// <param name="planet"></param>
     /// <param name="scale"></param>
     private void GeneratePlanet(Transform planet, float scale)
     {
          int randomNum = Random.Range(0, maxPlanetSize);
          Mesh tempMesh = rootList.planetMeshes[randomNum];      // Random Mesh

          randomNum = Random.Range(0, maxMaterialSize);
          Material tempMat = rootList.materials[randomNum];      // Random Material

          randomNum = Random.Range(0, 359);
          int rand2 = Random.Range(0, 359);
          int rand3 = Random.Range(0, 359);
          planet.localRotation = Quaternion.Euler(randomNum, rand2, rand3);   // Random Rotation

          // If the scale is larger than the maxScale, then set scale to maxScale.
          if (scale > maxScale)
               scale = maxScale;

          planet.GetComponent<MeshFilter>().mesh = tempMesh;
          planet.GetComponent<MeshCollider>().sharedMesh = tempMesh;
          planet.GetComponent<MeshRenderer>().material = tempMat;
          planet.localScale = new Vector3(scale, scale, scale);   // Reset the scale of the largest planet
     }


     /// <summary>
     /// // Set the FROM planet's mesh, material, collider, rotation and scale to the current TO planet's.
     /// </summary>
     /// <param name="planetFrom"></param>
     /// <param name="planetTo"></param>
     private void SetPlanet(Transform planetFrom, Transform planetTo)
     {
          planetFrom.GetComponent<MeshFilter>().mesh = planetTo.GetComponent<MeshFilter>().mesh;
          planetFrom.GetComponent<MeshRenderer>().material = planetTo.GetComponent<MeshRenderer>().material;
          planetFrom.GetComponent<MeshCollider>().sharedMesh = planetTo.GetComponent<MeshFilter>().mesh;
          planetFrom.localRotation = planetTo.localRotation;
          planetFrom.localScale = planetTo.localScale;
     }

     public void ReactivateCollider()
     {
          innerP.GetComponent<MeshCollider>().enabled = true;
     }
}
