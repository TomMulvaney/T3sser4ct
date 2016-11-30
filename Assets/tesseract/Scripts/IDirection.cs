using UnityEngine;
using System.Collections;

public interface IDirection {
    void SetRoot (Transform root);
    Vector3 GetDirection();
}
