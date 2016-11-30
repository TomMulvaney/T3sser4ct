using UnityEngine;
using System.Collections;

public class MoverRigidbodySimple : MonoBehaviour, IMover {
    Rigidbody rb;

    void Start() {
        rb = gameObject.GetComponent <Rigidbody> ();
    }

    public void Move(Vector3 velocity) {
        rb.velocity = velocity;
    }
}
