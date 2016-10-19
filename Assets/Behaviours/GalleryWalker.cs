using UnityEngine;
using System.Collections;

public class GalleryWalker : MonoBehaviour {
    [SerializeField]
    private bool showGUI;
    [SerializeField]
    private Transform root;
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float gravity = 10;
    [SerializeField]
    private CharacterController control;

    private Vector3 _velocity;

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
        _velocity = Vector3.zero;

        if (Input.GetMouseButton (0) || Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
            _velocity = root.forward.normalized * speed;
            // From Ascension
            //_velocity.x = Mathf.Lerp (_velocity.x, normHorizontalMove * runSpeed, Time.deltaTime * smoothedMovementFactor);
        }

        if (!control.isGrounded) {
            _velocity.y = -gravity;
        }

        control.Move (_velocity * Time.deltaTime);
	}

    void OnGUI () {
        if (showGUI) {
            GUILayout.Label (string.Format ("Forward: {0}", root.forward));
            GUILayout.Label (string.Format ("Velocity: {0}", _velocity));
        }
    }
}
