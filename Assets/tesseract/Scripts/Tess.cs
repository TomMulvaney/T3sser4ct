using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tess : MonoBehaviour 
{
    const int CUBE_COUNT = 8;

	// Use this for initialization
	void Start () {
        TessCube[] cubes = GetComponentsInChildren <TessCube> ();
        if (cubes.Length != CUBE_COUNT) {
            // TODO: Raise exception
        }

        foreach (TessCube cube in cubes) {
            cube.AssignMissingCubes (cubes);
        }
	}
}
