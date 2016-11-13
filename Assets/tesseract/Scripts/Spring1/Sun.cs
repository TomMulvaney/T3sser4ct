using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {
    public float targetTime;
    public Vector3 targetPos;

    float startTime;
    Vector3 startPos;
    float timeElapsed = 0f;
	
    void Start () {
        startTime = Time.time;
        startPos = transform.position;
    }

	// Update is called once per frame
	void Update () {
        timeElapsed += Time.deltaTime;

        // Lerp between startPos and targetPos based on time
        transform.position = Vector3.Lerp (startPos, targetPos, timeElapsed / targetTime);
	}
}
