using UnityEngine;
using System.Collections;

public interface IDirection {
    void Init (Transform root);
    Vector3 GetDirection();
}
