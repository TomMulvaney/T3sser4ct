using UnityEngine;
using System.Collections;

public class MoverCharCtrl : MonoBehaviour {
    CharacterController control;
    Transform root;

    void Start() {
        control = transform.GetComponent <CharacterController> ();
    }

    public void Init(Transform newRoot) {
        root = newRoot;
    }

    public void Move(Vector3 velocity) {
        control.Move (velocity * Time.deltaTime);
    }
}
