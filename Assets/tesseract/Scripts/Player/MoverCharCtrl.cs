using UnityEngine;
using System.Collections;

public class MoverCharCtrl : MonoBehaviour, IMover {
    CharacterController control;

    void Start() {
        control = transform.GetComponent <CharacterController> ();
    }

    public void Move(Vector3 velocity) {
        control.Move (velocity * Time.deltaTime);
    }
}
