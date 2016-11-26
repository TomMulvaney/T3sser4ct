using UnityEngine;
using System.Collections;

public interface IRotator {
    void Init(Transform root);
    void TryRotate ();
}
