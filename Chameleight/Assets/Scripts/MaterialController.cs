using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    public  List<Material> mats = new List<Material>();
    private System.Random random = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        int val = random.Next(0,mats.Count);
        Debug.Log(val);
        Renderer rend = GetComponent<Renderer>();
        rend.material = mats.ElementAt(val);
    }
}
