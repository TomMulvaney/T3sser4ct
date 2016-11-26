using UnityEngine;
using System.Collections;

public class GalleryWalkerGrav : MonoBehaviour {
    [SerializeField]
    private bool showGUI;
    [SerializeField]
    private Transform root;
    [SerializeField]
    private float walkSpeed = 5f;
    [SerializeField]
    private float damping = 1f;
    [SerializeField]
    private float gravity = 10f;
    [SerializeField]
    private CharacterController control;

    private Vector3 _velocity = Vector3.zero;


    // Use this for initialization
    void Start () {
        if (root == null) {
            root = transform;
        }

        if (control == null) {
            control = GetComponent<CharacterController> ();
        }
    }

    // TODO: Combine separate grav and walk velocities

    // Update is called once per frame
    void Update () {
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
            //StopHorizontal ();
            _velocity.x = 0;
            _velocity.z = 0;
        }

        float rayDistance = 1.1f;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        Debug.DrawLine (root.position, root.position - (root.up * rayDistance));
        RaycastHit hit;
        if(Physics.Raycast (root.position, -root.up, out hit, rayDistance, layerMask)) {
            _velocity.y = 0;
        } else {
            _velocity.y = -gravity;
        }

        // TODO: Combine separate grav and walk velocities

        control.Move (_velocity * Time.deltaTime);
    }

    void StopHorizontal() {

    }

    bool IsUpX() {
        return Mathf.Approximately (Mathf.Abs (Vector3.Dot (root.up, Vector3.left)), 1f);
    }

    bool IsUpY() {
        return Mathf.Approximately (Mathf.Abs (Vector3.Dot (root.up, Vector3.up)), 1f);
    }

    bool IsUpZ() {
        return Mathf.Approximately (Mathf.Abs (Vector3.Dot (root.up, Vector3.forward)), 1f);
    }

    void OnGUI () {
        if (showGUI) {
            GUILayout.Label (string.Format ("Forward: {0}", root.forward));
            GUILayout.Label (string.Format ("Right: {0}", root.right));
            GUILayout.Label (string.Format ("Velocity: {0}", _velocity));
            GUILayout.Label (string.Format ("Speed: {0}", _velocity.magnitude));
            GUILayout.Label (string.Format ("IsUpX: {0}", IsUpX ()));
            GUILayout.Label (string.Format ("IsUpY: {0}", IsUpY ()));
            GUILayout.Label (string.Format ("IsUpZ: {0}", IsUpZ ()));
        }
    }
}
