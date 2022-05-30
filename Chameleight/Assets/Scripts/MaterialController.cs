using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    public  List<Material> mats = new List<Material>();
    private System.Random random = new System.Random();

    //Set random material.
    void Start()
    {
        //Select a random material from the list of materials.
        int val = random.Next(0,mats.Count);
        Renderer rend = GetComponent<Renderer>();
        //Set the random material to the object.
        rend.material = mats.ElementAt(val);
    }
}
