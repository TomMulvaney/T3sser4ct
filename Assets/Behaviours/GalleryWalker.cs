using UnityEngine;
using System.Collections;

public class GalleryWalker : MonoBehaviour {
    [SerializeField]
    private Transform root;

	// Use this for initialization
	void Start () {
        if (root == null) {
            root = transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
