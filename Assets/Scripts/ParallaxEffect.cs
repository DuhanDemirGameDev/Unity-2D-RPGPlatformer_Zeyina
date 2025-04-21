using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    //Starting position for the parallax game object
    Vector2 startingPosition;

    //Starz Z value of the parallax game object
    float startingZ;

    Vector2 canMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    //If object is in front of target, user near clip plane. If behind target, use farClipPlane

    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    //The further the object from the player, the faster the ParallaxEffect object will move. Drag It's Z value closer to the target to make it sace slower.
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;


    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }


    // Update is called once per frame
    void Update()
    {
        //When the target moevs, move the parallax object the same distance times a multiplier
        Vector2 newPosition = startingPosition + canMoveSinceStart * parallaxFactor;

        //The X/Y position changes based on target travel speed times the parallax factor, but Z stays consistent
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);

    }
}

