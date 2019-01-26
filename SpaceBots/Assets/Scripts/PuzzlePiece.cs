using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
	enum State
	{
		Free,
		Moving,
		ReadyToLock,
		Locked
	}

	public static event Action PickupEvent;

	public static event Action DropCorrectEvent;

	public static event Action DropWrongEvent;

	public static event Action RotateEvent;


	private static int sNumPieces;


	//use to track number of pieces
	public int id;

	//if you can spawn a piece
	public bool Spawn;

	//if obj is stationary ref for copy
	public bool slot;

	//max number of objects this can spawn
	[SerializeField]
	private int MaxPieces;

	private GameObject Parent;

	private Vector3 lockPos;

	//if obj is in place
	private State locked;

	#region Monobehaviours

	protected void Start()
	{
		if (slot) {
			Spawn = true;
			sNumPieces = MaxPieces;
		}
	}

	protected void Update()
	{
		if (!slot) {
			if (locked != State.Locked) {
				doMouseThing();
			}
		}
	}

	#endregion

	void doMouseThing()
	{
		if (Input.GetMouseButtonUp(0)) {
			Debug.LogFormat("[{0}/{1}:doMouseThing] Sending RayCast for up. state:{2}", name, id, locked);
			{
				if (locked == State.Free) {
					var invokeEvent = DropCorrectEvent;
					if (invokeEvent != null) {
						invokeEvent();
					}

					Parent.GetComponent<PuzzlePiece>().AddPiece();
					Destroy(this.gameObject);
				} else if (locked == State.ReadyToLock) {
					var invokeEvent = DropWrongEvent;
					if (invokeEvent != null) {
						invokeEvent();
					}

					locked = State.Locked;
					this.gameObject.transform.position = lockPos;
					Debug.LogFormat("[{0}/{1}:doMouseThing] is locked", name, id);
				}
			}
		} else if (Input.GetMouseButton(0)) {
			Debug.LogFormat("[{0}/{1}:doMouseThing] Sending RayCast for hold. state:{2}", name, id, locked);
			if (locked != State.Locked) {
				Debug.LogFormat("[{0}/{1}:doMouseThing] is moving", name, id);
				this.gameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
			}
		}
	}

	//use for drop location
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Anchor") {
			Debug.LogFormat("[{0}/{1}:OnTriggerEnter] locked", name, id);
			// Make into position to show above the lock
			lockPos = other.gameObject.transform.position - new Vector3(0, 0, 1);
			locked = State.ReadyToLock;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Anchor") {
			if (locked != State.Locked) {
				Debug.LogFormat("[{0}/{1}:OnTriggerExit] unlocked", name, id);
				locked = State.Moving;
			}
		}
	}

	//use for setting parent piece
	public void SetParent(GameObject Par)
	{
		Parent = Par;
	}

	public void AddPiece()
	{
		if (sNumPieces < MaxPieces) {
			Debug.Log("Adding Piece");
			sNumPieces++;
			Spawn = true;
		} else {
			Debug.Log("Cant Add Piece");
		}
	}

	public void SubPiece()
	{
		if (sNumPieces <= MaxPieces && sNumPieces > 0) {
			id = sNumPieces;
			sNumPieces--;
			Debug.Log("Subtracting Piece");
			if (sNumPieces <= 0) {

				Spawn = false;
				Debug.Log("Cannot spawn any more pieces");
			}
		} else {
			Debug.Log("Cant Sub Piece");
		}
	}
}
