using UnityEngine;
using System.Collections;

public class WallRotateSpike : MonoBehaviour {

    public Transform root;
    Vector3 myNormal;
    float lerpSpeed = 10;

	// Use this for initialization
	void Start () {
        if (root == null)
            root = transform;
        myNormal = root.up;
        //transform.rotation = Quaternion.FromToRotation(transform.up, transform.forward);
	}
	
	// Update is called once per frame
	void Update () {
        CheckRotation ();
	}

    void CheckRotation() {
        
    }
}
