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
	
	// Update is called once per frame
	void Update () {
        Vector3 direction = Vector3.zero;

        if (Input.GetMouseButton (0) || Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
            direction += root.forward;
            //_velocity.x = Mathf.Lerp (_velocity.x, normHorizontalMove * runSpeed, Time.deltaTime * smoothedMovementFactor); // From Ascension
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
        else {
            _velocity.x = 0;
            _velocity.z = 0;
        }

        if(Vector3.Distance (direction, Vector3.zero) > 0.01f) {
            float speed = Mathf.Lerp (_velocity.magnitude, walkSpeed, Time.deltaTime * damping);
            Debug.Log (direction);
            _velocity = direction.normalized * speed;
            //_velocity = Vector3.Lerp (direction * _velocity.magnitude, direction * walkSpeed, Time.deltaTime * damping);
        }

        if (!control.isGrounded) {
            _velocity.y = -gravity;
        } else {
            _velocity.y = 0;
        }

        control.Move (_velocity * Time.deltaTime);
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
