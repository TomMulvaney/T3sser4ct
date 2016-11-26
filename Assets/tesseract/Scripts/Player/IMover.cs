using UnityEngine;
using System.Collections;

public interface IMover {
    void Init (Transform root);
    void Move(Vector3 vel);
}
