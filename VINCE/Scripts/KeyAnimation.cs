using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAnimation : MonoBehaviour {
    private bool investigated;
	// Use this for initialization
	void Start () {
        investigated = false;
	}
	
	// Update is called once per frame
	void Update () {
        //When player press K to investigate 
        //"Input.GetKey(KeyCode.L)" is just a subtitution of activating purple lens
        if (Input.GetKey(KeyCode.K) && !investigated && Input.GetKey(KeyCode.L))
            investigated = true;
        
        
        //Move the key upward until it reach 3
        if (transform.position.y < 3&&investigated)
            transform.Translate(0, Time.deltaTime * 0.2f, 0, Space.World);
        //Rotate the key when its moving upward
        if (transform.position.y > 2.6)
            transform.Rotate(Time.deltaTime *30 , 5*0.2f, 10*0.2f, Space.World);
	}
}
