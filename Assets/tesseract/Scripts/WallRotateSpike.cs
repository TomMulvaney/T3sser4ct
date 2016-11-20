using UnityEngine;
using System.Collections;

public class WallRotateSpike : MonoBehaviour {

    public Transform root;
    Vector3 myNormal;
    float lerpSpeed = 10;

	// Use this for initialization
	void Start () {
        if (root == null)
            root = transform;
        myNormal = root.up;
        //transform.rotation = Quaternion.FromToRotation(transform.up, transform.forward);
	}
	
	// Update is called once per frame
	void Update () {
        CheckRotation ();
	}

    void CheckRotation() {
        float rayDistance = 0.7f;
        //      Debug.DrawLine (root.position, root.position - (root.forward * rayDistance));
        RaycastHit hit;
        int layerMask = 1 << 9;

        Debug.DrawRay (root.position, root.up);
        Debug.DrawRay (root.position, root.up);
        Debug.DrawRay (root.position, -root.up);

        if(Physics.Raycast (root.position, -root.up, out hit, rayDistance, layerMask)) {
            myNormal = Vector3.Lerp(myNormal, hit.normal, lerpSpeed*Time.deltaTime);
            // find forward direction with new myNormal:
            var myForward = Vector3.Cross(transform.right, myNormal);
            // align character to the new myNormal while keeping the forward direction:
            var targetRot = Quaternion.LookRotation(myForward, myNormal);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, lerpSpeed*Time.deltaTime);
        }
    }
}
