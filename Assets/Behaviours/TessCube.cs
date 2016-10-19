using UnityEngine;
using System.Collections;

public class TessCube : MonoBehaviour {
    public TessCube posX;
    public TessCube negX;
    public TessCube posY;
    public TessCube negY;
    public TessCube posZ;
    public TessCube negZ;

    public void AssignMissingCubes(TessCube[] cubes) {
        // For each cube in cubes, find the name and assign if the variable with matching name is null. Raise exception if match is self
    }
}
