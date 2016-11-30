using UnityEngine;
using System.Collections;

public class RotatorSlope : MonoBehaviour, IRotator {
    [SerializeField]
    private float lerpSpeed = 10;
    [SerializeField]
    private float rayDistance = 0.7f;
    [SerializeField]
    private bool useLayerMask = false;
    [SerializeField]
    private int lowLayerMask = 1;
    [SerializeField]
    private int highLayerMask = 9;
    [SerializeField]
    private bool invertLayerMask = false;
    [SerializeField]
    private bool drawRay = true;

    Transform root;
    Vector3 myNormal;

    void Start() {
        if (root == null) {
            root = transform;
        }
    }

    public void SetRoot(Transform newRoot) {
        root = newRoot;
    }

    public void TryRotate () {
        RaycastHit hit;
        int layerMask = lowLayerMask << highLayerMask;
        if (invertLayerMask) {
            layerMask = ~layerMask;
        }

        if (drawRay) {
            Debug.DrawRay (root.position, root.up);
            Debug.DrawRay (root.position, root.up);
            Debug.DrawRay (root.position, -root.up);
        }

        bool hasHit = useLayerMask ? 
            Physics.Raycast (root.position, -root.up, out hit, rayDistance, layerMask) : 
            Physics.Raycast (root.position, -root.up, out hit, rayDistance);

        if(hasHit) {
            myNormal = Vector3.Lerp(myNormal, hit.normal, lerpSpeed*Time.deltaTime);
            // find forward direction with new myNormal:
            var myForward = Vector3.Cross(root.right, myNormal);
            // align character to the new myNormal while keeping the forward direction:
            var targetRot = Quaternion.LookRotation(myForward, myNormal);
            root.rotation = Quaternion.Lerp(root.rotation, targetRot, lerpSpeed*Time.deltaTime);
        }
    }
}
