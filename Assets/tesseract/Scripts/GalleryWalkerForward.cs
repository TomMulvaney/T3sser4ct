using UnityEngine;
using System.Collections;

public class GalleryWalkerForward : MonoBehaviour {
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
        Debug.Log (root.up);
        Debug.Log (root.forward);

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
            direction += root.up;
        } 
        if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
            direction -= root.up;
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
        //      Debug.DrawLine (root.position, root.position - (root.forward * rayDistance));
        RaycastHit hit;
        int layerMask = 1 << 9;

        Debug.DrawRay (root.position, root.forward);
        Debug.DrawRay (root.position, root.forward);
        Debug.DrawRay (root.position, -root.forward);

        if(Physics.Raycast (root.position, -root.forward, out hit, rayDistance, layerMask)) {
            if (canRotate) {
                root.rotation = Quaternion.FromToRotation (root.forward, hit.normal);
                canRotate = false;
            }
        }
    }

    void MoveToGround() {
        RaycastHit hit;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        if(Physics.Raycast (root.position, -root.forward, out hit, Mathf.Infinity, layerMask)) {
            Vector3 delta = root.position - hit.point;
            root.position = hit.point + delta.normalized;
        }
    }

    void OnGUI () {
        if (showGUI) {
            GUILayout.Label (string.Format ("Forward: {0}", root.up));
            GUILayout.Label (string.Format ("Right: {0}", root.right));
            GUILayout.Label (string.Format ("Velocity: {0}", _velocity));
            GUILayout.Label (string.Format ("Speed: {0}", _velocity.magnitude));
        }
    }
}
