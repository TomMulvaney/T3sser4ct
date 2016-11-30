using UnityEngine;
using System.Collections;

public class WallWalker : MonoBehaviour {
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

    private IDirection director;
    private IMover mover;
    private IRotator rotator;

    private Vector3 walkVelocity = Vector3.zero;
    private Vector3 gravVelocity = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
        if (root == null) {
            root = transform;
        }

        director = gameObject.GetComponentInChildren<IDirection>();
        if (director == null) {
            Debug.LogError ("Cannot find IDirection");
        }

        mover = gameObject.GetComponentInChildren<IMover>();
        if (mover == null) {
            Debug.LogError ("Cannot find IMover");
        }

        rotator = gameObject.GetComponentInChildren<IRotator>();
        if (rotator == null) {
            Debug.LogError ("Cannot find IRotator");
        }

//        Component directionComponent = gameObject.GetComponentInChildren (typeof(IDirection));
//        if (directionComponent != null) {
//            director = directionComponent as IDirection;
//        } else {
//            Debug.LogError ("Cannot find IDirection");
//        }
//
//        Component moverComponent = gameObject.GetComponent (typeof(IMover));
//        if (moverComponent != null) {
//            mover = moverComponent as IMover;
//        } else {
//            Debug.LogError ("Cannot find IMover");
//        }
//
//        Component rotatorComponent = gameObject.GetComponent (typeof(IRotator));
//        if (rotatorComponent != null) {
//            rotator = rotatorComponent as IRotator;
//        } else {
//            Debug.LogWarning ("Cannot find IRotator");
//        }
	}
	
	// Update is called once per frame
	void Update () {
        // TODO: Delete dirty hack before gravity is implemented
        if (Input.GetKeyDown (KeyCode.P)) {
            MoveToGround ();
        }

        if (rotator != null) {
            rotator.TryRotate ();
        }

        Vector3 direction = director != null ? director.GetDirection () : Vector3.zero;

        if(Vector3.Distance (direction, Vector3.zero) > 0.01f) {
            float speed = Mathf.Lerp (walkVelocity.magnitude, walkSpeed, Time.deltaTime * damping);
            walkVelocity = direction.normalized * speed;
        } else {
            // TODO: Deccelerative lerp
            walkVelocity = Vector3.zero;
        }

        // TODO: Gravity
        // TODO: Don't hardcode rayDistance, layerMask and invertLayerMask
//        float rayDistance = 1.1f;
//        int layerMask = 1 << 8;
//        layerMask = ~layerMask;
//
//        Debug.DrawLine (root.position, root.position - (root.up * rayDistance));
//        RaycastHit hit;
//        if(Physics.Raycast (root.position, -root.up, out hit, rayDistance, layerMask)) {
//            gravVelocity = Vector3.zero;
//        } else {
//            gravVelocity += (-root.up * gravity);
//        }

        velocity = walkVelocity + gravVelocity;

        if (mover != null) {
            mover.Move (velocity);
        }
	}

    void OnGUI () {
        if (showGUI) {
            GUILayout.Label (string.Format ("Forward: {0}", root.forward));
            GUILayout.Label (string.Format ("Right: {0}", root.right));
            GUILayout.Label (string.Format ("Velocity: {0}", velocity));
            GUILayout.Label (string.Format ("Speed: {0}", velocity.magnitude));
            GUILayout.Label (string.Format ("WalkVelocity: {0}", walkVelocity));
            GUILayout.Label (string.Format ("WalkSpeed: {0}", walkVelocity.magnitude));
        }
    }

    // TODO: Delete dirty hack before gravity is implemented
    void MoveToGround() {
        RaycastHit hit;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        if(Physics.Raycast (root.position, -root.up, out hit, Mathf.Infinity, layerMask)) {
            Vector3 delta = root.position - hit.point;
            root.position = hit.point + delta.normalized;
        }
    }
}
