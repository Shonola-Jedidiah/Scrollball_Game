using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPiece : MonoBehaviour
{
   
    public bool isColored = false;

    private void Start()
    {
       
    }
    public void ColorChange(Color color )
    {
        GetComponent<MeshRenderer>().material.color = color;
        isColored = true;

        GameManager.Singleton.checkCompleted();
    }
}
