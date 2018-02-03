using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewind : MonoBehaviour {

    public bool isRewinding = false;

    public float recordTime = 5f;

    LinkedList<Vector3> positions;
    LinkedList<Quaternion> rotations;

	// Use this for initialization
	void Start () 
	{
        positions = new LinkedList<Vector3>();
        rotations = new LinkedList<Quaternion>();
	}

	// Update is called once per frame
	void Update () 
	{
        if (Input.GetKeyDown(KeyCode.R))
            StartRewind();
        if (Input.GetKeyUp(KeyCode.R))
            StopRewind();
	}

    void FixedUpdate()
    {
        if(isRewinding)
            RewindTime();
        else
            Record();
    }

    void Record()
    {
        if (positions.Count + rotations.Count > Mathf.Round(recordTime / Time.fixedDeltaTime) / 2)
        {
            positions.RemoveLast();
            rotations.RemoveLast();
        }

        positions.AddFirst(transform.position);
        rotations.AddFirst(transform.rotation);
    }

    void RewindTime()
    {
        if (positions.Count > 0)
        {
            transform.position = positions.First.Value;
            transform.rotation = rotations.First.Value;
            positions.RemoveFirst();
            rotations.RemoveFirst();
        }
        else
            StopRewind();

    }

	void StartRewind()
	{
        isRewinding = true;
	}

	void StopRewind()
	{
        isRewinding = false;
	}
	
}