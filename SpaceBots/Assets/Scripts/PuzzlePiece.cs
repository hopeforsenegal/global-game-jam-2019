using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
	public static event Action PickupEvent;

	public static event Action DropCorrectEvent;

	public static event Action DropWrongEvent;

	public static event Action RotateEvent;
	
    //use to track number of pieces
    public int id;

    //if you can spawn a piece
    public bool Spawn;

    private GameObject Parent;

    private Vector3 lockPos;

    //if obj is in place
    private int locked;

    //if obj is stationary ref for copy
    public bool slot;

    //number of pieces left
    [SerializeField]
    private int numPieces;

    //max number of objects this can spawn
    [SerializeField]
    private int MaxPieces;

    private void Start()
    {
        if (slot)
        {
            Spawn = true;
            numPieces = MaxPieces;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!slot)
        {
            if (locked != 3)
            {
                doMouseThing();
            }
        }

    }

    void doMouseThing()
    {
        if (Input.GetMouseButtonUp(0))
		{
			Debug.Log("Sending RayCast");
            {
                if (locked == 0)
				{
					var invokeEvent = DropCorrectEvent;
					if (invokeEvent != null) {
						invokeEvent();
					}

                    Parent.GetComponent<PuzzlePiece>().AddPiece();
                    Destroy(this.gameObject);
                }
                else if (locked == 1)
				{
					var invokeEvent = DropWrongEvent;
					if (invokeEvent != null) {
						invokeEvent();
					}

                    locked = 3;
                    this.gameObject.transform.position = lockPos;
                }
            }
        }
        else if (Input.GetMouseButton(0))
		{
			Debug.Log("Sending RayCast");
            if (locked == 0 || locked == 1)
            {
                Debug.Log(id + " is Moving to mouse");
                this.gameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
            }
        }
    }

    //use for drop location
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Anchor")
        {
            locked = 1;
            lockPos = other.gameObject.transform.position - new Vector3(0,0,1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Anchor")
        {
            locked = 0;
            //lockPos = Parent.transform.position;
        }
    }

    //use for setting parent piece
    public void SetParent(GameObject Par)
    {
        Parent = Par;
    }

    public void AddPiece()
    {
        if(numPieces < MaxPieces)
        {
            Debug.Log("Adding Piece");
            numPieces++;
            Spawn = true;
        }
        else
        {
            Debug.Log("Cant Add Piece");
        }
    }

    public void SubPiece()
    {
        if (numPieces <= MaxPieces && numPieces > 0)
        {
            id = numPieces;
            numPieces--;
            Debug.Log("Subtracting Piece");
            if (numPieces <= 0)
            {

                Spawn = false;
                Debug.Log("Cannot spawn any more pieces");
            }
        }
        else
        {
            Debug.Log("Cant Sub Piece");
        }
    }
}
