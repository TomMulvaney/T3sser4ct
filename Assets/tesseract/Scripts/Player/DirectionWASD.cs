using UnityEngine;
using System.Collections;

public class DirectionWASD : MonoBehaviour, IDirection {
    Transform root;

    public void Start() {
        if (root == null) {
            root = transform;
        }
    }

    public void SetRoot (Transform newRoot) {
        root = newRoot;
	}
	
    public Vector3 GetDirection () {
        Vector3 direction = Vector3.zero;

        if (Input.GetMouseButton (0) || Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
            direction += root.forward;
        } 
        if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
            direction -= root.forward;
        }
        if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
            direction -= root.right;
        }
        if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
            direction += root.right;
        }

        return direction;
	}
}
