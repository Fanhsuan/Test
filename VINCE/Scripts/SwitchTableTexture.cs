using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTableTexture : MonoBehaviour {
    public Texture originalTexture;
    public Texture newTexture;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        
        //When player press L, The texture changed.
        //"Input.GetKey(KeyCode.L)" is just a subtitution of activating purple lens
        if (Input.GetKey(KeyCode.L))
            gameObject.GetComponent<Renderer>().material.mainTexture = newTexture;
        //Otherwise stay normal texture
        else
            gameObject.GetComponent<Renderer>().material.mainTexture = originalTexture;
    }
}
