using UnityEngine;
using System.Collections;

public class GalleryWalker : MonoBehaviour {
    [SerializeField]
    private bool showGUI;
    [SerializeField]
    private Transform root;
    [SerializeField]
    private float walkSpeed = 5f;
    [SerializeField]
    private float damping = 1f;
    [SerializeField]
    private CharacterController control;

    private Vector3 _velocity = Vector3.zero;

    bool canRotate = true;
    Vector3 origPos;
    Vector3 origUp;


	// Use this for initialization
	void Start () {
        if (root == null) {
            root = transform;
        }

        if (control == null) {
            control = GetComponent<CharacterController> ();
        }

        Debug.Log (root.eulerAngles);
        Debug.Log (root.forward);
        Debug.Log (root.up);

        MoveToGround ();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown (KeyCode.P)) {
            MoveToGround ();
        }

        CheckRotation ();

        Vector3 direction = Vector3.zero;

        if (Input.GetMouseButton (0) || Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
            direction += root.forward;
        } 
        if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
            direction -= root.forward;
        }
        if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
            direction -= root.right;
        }
        if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
            direction += root.right;
        }

        if(Vector3.Distance (direction, Vector3.zero) > 0.01f) {
            float speed = Mathf.Lerp (_velocity.magnitude, walkSpeed, Time.deltaTime * damping);
            _velocity = direction.normalized * speed;
        } else {
            // TODO: Deccelerative lerp
            _velocity = Vector3.zero;
        }

        if (canRotate)
            control.Move (_velocity * Time.deltaTime);
	}

    void CheckRotation() {
        float rayDistance = 0.7f;
//      Debug.DrawLine (root.position, root.position - (root.up * rayDistance));
        RaycastHit hit;
        int layerMask = 1 << 9;

        Debug.DrawRay (root.position, root.up);
        Debug.DrawRay (root.position, root.forward);
        Debug.DrawRay (root.position, -root.up);
    
        if(Physics.Raycast (root.position, -root.up, out hit, rayDistance, layerMask)) {
            if (canRotate) {
                root.rotation = Quaternion.FromToRotation (root.up, hit.normal);
                canRotate = false;
            }

            /////////////////////////////////
            // Looks at hit normal
            //root.rotation = Quaternion.FromToRotation(Vector3.forward, Vector3.up) * Quaternion.LookRotation(hit.normal);
            /////////////////////////////////

//            Vector3 eulerDelta = new Vector3 
//                (Mathf.DeltaAngle (root.up.x, hit.normal.x), 
//                    Mathf.DeltaAngle (root.up.y, hit.normal.y), 
//                    Mathf.DeltaAngle (root.up.z, hit.normal.z));
//
//            Debug.DrawRay (root.position, root.up, Color.cyan);
//
//
//
//            if (canRotate) {
//                origPos = root.position;
//                origUp = root.up;
//  
//                root.Rotate (eulerDelta);
//                canRotate = false;
//            }
//
//            Debug.DrawRay (root.position, root.up, Color.red);
//
//
//            Debug.DrawRay (hit.point, hit.normal, Color.green);



//            Vector3 upDelta = hit.normal - root.up;
//            Debug.DrawRay (root.position, hit.normal, Color.green);
//            Debug.DrawRay (root.position, root.forward - upDelta, Color.red);
            //transform.rotation = Quaternion.LookRotation (root.forward + upDelta, hit.normal);

//            Vector3 hitToVehicle = hit.point - root.position;
//            Vector3 projectionVector = Vector3.Project(hitToVehicle, hit.normal);
//            Vector3 directionVector = hitToVehicle - projectionVector;
//            transform.rotation = Quaternion.LookRotation(directionVector,hit.normal);

//            transform.rotation = Quaternion.LookRotation (transform.forward, hit.normal);

//            Vector3 targetUp = hit.normal;
//            float speed = 10f;
//            float step = speed * Time.deltaTime;
//            Vector3 newUp = Vector3.RotateTowards(root.up, targetUp, step, 0.0F);
//            Debug.DrawRay(transform.position, newUp, Color.red);
//            transform.rotation = Quaternion.LookRotation(newUp);
            //transform.up = up;
        }
    }

    void MoveToGround() {
        RaycastHit hit;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        if(Physics.Raycast (root.position, -root.up, out hit, Mathf.Infinity, layerMask)) {
            Vector3 delta = root.position - hit.point;
            root.position = hit.point + delta.normalized;
        }
    }

    void OnGUI () {
        if (showGUI) {
            GUILayout.Label (string.Format ("Forward: {0}", root.forward));
            GUILayout.Label (string.Format ("Right: {0}", root.right));
            GUILayout.Label (string.Format ("Velocity: {0}", _velocity));
            GUILayout.Label (string.Format ("Speed: {0}", _velocity.magnitude));
        }
    }
}
