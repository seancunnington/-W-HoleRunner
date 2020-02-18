using UnityEngine;

[CreateAssetMenu (fileName = "Planets", menuName = "new Planet")]
public class Planets : ScriptableObject
{
     public string planetName;

     public Mesh mesh;

     [HideInInspector]
     public Material material;

}
