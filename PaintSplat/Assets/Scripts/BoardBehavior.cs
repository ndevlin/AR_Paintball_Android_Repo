using UnityEngine;
using System.Collections.Generic;
using Photon;

using System;

public class BoardBehavior : Photon.MonoBehaviour {

    public GameObject SplatterPrefab;
    public GameObject imageTarget;

    public int numBalls = 0;

    private List<GameObject> splatters = new List<GameObject>();

    private void Update () {
	    if (Input.GetKeyDown(KeyCode.Space)) {
            imageTarget.SetActive(!imageTarget.activeSelf);
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
       
        var other = collision.collider.gameObject;
        Vector3 hit_position = other.transform.position;
        if (other.CompareTag("Ball"))
        {
            PhotonNetwork.Destroy(other);
            Quaternion rot =  Quaternion.AngleAxis(UnityEngine.Random.Range(0f, 360f), new Vector3(0, 0, 1)) ; //*transform.rotation;

            float addZ = Convert.ToSingle(numBalls) / 100000.0f;
            hit_position.z = imageTarget.transform.position.z - addZ;

            numBalls++;

            var splatter = Instantiate(SplatterPrefab, hit_position, rot) as GameObject;

            splatter.GetComponent<Renderer>().material.color = other.GetComponent<Renderer>().material.color;

            splatters.Add(splatter);
        }

    }
}
