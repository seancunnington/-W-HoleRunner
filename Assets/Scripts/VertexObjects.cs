using System.Collections;
using UnityEngine;

public class VertexObjects : MonoBehaviour
{
     private Vector3[] verticeObjects;
     private int numberOfVertices = 0;


     public Vector3 VertexPosition(int vertexPosition)
     {
          Vector3 vertPoint = GetComponent<MeshFilter>().mesh.vertices[vertexPosition];
          Vector3 worldPoint = transform.TransformPoint(vertPoint);

          return worldPoint;
     }

     public Vector3 VertexNormalDirection(int normalPosition)
     {
          Vector3 normalPoint = GetComponent<MeshFilter>().mesh.normals[normalPosition];

          return normalPoint;

     }


     public int GetNumberOfVertices()
     {
          return transform.GetComponent<MeshFilter>().mesh.vertices.Length;
     }


     private void NewVertexArray()
     {
          verticeObjects = new Vector3[numberOfVertices];
     }
}
