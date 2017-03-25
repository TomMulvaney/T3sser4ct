using UnityEngine;
using System.Collections;

public class RotatorWall : MonoBehaviour, IRotator {
    [SerializeField]
    private Transform root;
    [SerializeField]
    private Rigidbody rb;
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

    Vector3 myNormal;
    

    void Start() {
        if (root == null) {
            root = transform;
        }

        if (rb == null) {
            rb = GetComponent <Rigidbody> ();
        }
    }
	
    public void TryRotate(Vector3 moveDir) {
        for (var i = 0; i < 4; ++i) {
            RaycastHit hit;
            var layerMask = lowLayerMask << highLayerMask;
            if (invertLayerMask) {
                layerMask = ~layerMask;
            }

            Vector3 rayDir = Vector3.zero;

            switch (i) {
            case 0:
                rayDir = transform.forward;
                break;
            case 1:
                rayDir = transform.right;
                break;
            case 2:
                rayDir = -transform.forward;
                break;
            case 3:
                rayDir = -transform.right;
                break;
            default:
                continue; // TODO: Clean this dirty hack which prevents extra rays
            }

            if (drawRay) {
                Debug.DrawRay (root.position, rayDir, Color.green);
            }

            bool hasHit = useLayerMask ? 
                Physics.Raycast (root.position, rayDir, out hit, rayDistance, layerMask) : 
                Physics.Raycast (root.position, rayDir, out hit, rayDistance);

            if (hasHit) {
                Debug.Log (Vector3.Dot (hit.normal, moveDir));
                // If dot product between hit normal and rigidbody velocity < 0.25 then rotate
                if (Mathf.Abs (Vector3.Dot (hit.normal, moveDir)) > 0.25f) {
                    myNormal = Vector3.Lerp (myNormal, hit.normal, lerpSpeed * Time.deltaTime);
                    // find forward direction with new myNormal:
                    var myForward = Vector3.Cross (root.right, myNormal);
                    // align character to the new myNormal while keeping the forward direction:
                    var targetRot = Quaternion.LookRotation (myForward, myNormal);

                    Debug.Log ("Set Rotation");
                    Debug.Log (root.rotation.eulerAngles);
                    Debug.Log (targetRot.eulerAngles);

                    // TODO: Nice rotation lerp
                    //root.rotation = Quaternion.Lerp (root.rotation, targetRot, lerpSpeed * Time.deltaTime);
                    root.rotation = Quaternion.Lerp (root.rotation, targetRot, 1);
                    Debug.Log (root.rotation.eulerAngles);
                }
            }
        }

        if (rb != null) {
            rb.angularVelocity = Vector3.zero;
        }
    }


}
