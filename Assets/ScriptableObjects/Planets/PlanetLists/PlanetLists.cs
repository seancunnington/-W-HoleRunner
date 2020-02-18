using UnityEngine;

[CreateAssetMenu(fileName = "PlanetLists", menuName = "new Planet List")]
public class PlanetLists : ScriptableObject
{
     public string listName;

     public Planets[] planets;
     public Material[] materials;

}