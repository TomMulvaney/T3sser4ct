using UnityEngine;
using System.Collections;

public interface IRotator {
    void SetRoot(Transform root);
    void TryRotate ();
}
