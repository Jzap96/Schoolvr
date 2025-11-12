using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invisible : MonoBehaviour
{

    LayerMask invisible_layer_mask;

    // Start is called before the first frame update
    void Start()
    {
        invisible_layer_mask = LayerMask.NameToLayer("Invisible_layer");
        invisible_layer_mask = ~invisible_layer_mask;
        Camera.main.cullingMask = 1 << invisible_layer_mask;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
