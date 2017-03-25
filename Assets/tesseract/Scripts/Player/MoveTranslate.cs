using UnityEngine;
using System.Collections;

public class MoveTranslate : MonoBehaviour, IMover {

    public void Move(Vector3 velocity) {
        transform.Translate (velocity * Time.deltaTime, Space.World);
    }
}
