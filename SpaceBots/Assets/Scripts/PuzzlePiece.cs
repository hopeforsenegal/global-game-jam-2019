using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
	enum State
	{
		Free,
		ReadyToLock,
		Locked
	}

	public static event Action PickupEvent;

	public static event Action DropWrongEvent;

	public static event Action RotateEvent;


	public int sNumPieces;


	//use to track number of pieces
	public int id;

	//use for piece type
	public PieceEnumUtil.PieceEnum type;
	public PieceEnumUtil.DirectionEnum direction;

	//if you can spawn a piece
	public bool Spawner;

	//if obj is stationary ref for copy
	public bool slot;

	public bool hasBeenPickedUp;

	//max number of objects this can spawn
	[SerializeField]
	private int MaxPieces;

	private GameObject Parent;

    private Quaternion lockRot;

	private Vector3 lockPos;

    private GameObject lockObj;

	//if obj is in place
	private State locked;

	#region Monobehaviours

	protected void Start()
	{
		if (slot) {
			Spawner = true;
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

    //public void setMaxP (int maxPieces)
    //{
    //    MaxPieces += maxPieces;
    //}

	void doMouseThing()
	{
		if (Input.GetMouseButtonUp(0)) {
			Debug.LogFormat("[{0}/{1}:doMouseThing] Sending RayCast for up. state:{2}", name, id, locked);
			{
				if (locked == State.Free) {
					var invokeEvent = DropWrongEvent;
					if (invokeEvent != null) {
						invokeEvent();
					}

					Parent.GetComponent<PuzzlePiece>().AddPiece();
					Destroy(this.gameObject);
				} else if (locked == State.ReadyToLock) {
					locked = State.Locked;
					this.gameObject.transform.position = lockPos;
                    checkRot();
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

			if (type == other.gameObject.GetComponent<Anchors>()._lockType && !other.gameObject.GetComponent<Anchors>().correct)
            {
                lockObj = other.gameObject;
                // Make into position to show above the lock
                lockPos = lockObj.transform.position - new Vector3(0, 0, 1);
                locked = State.ReadyToLock;
            }
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Anchor") {
			if (locked != State.Locked) {
				Debug.LogFormat("[{0}/{1}:OnTriggerExit] unlocked", name, id);
				if (lockObj != null) {
					lockObj.GetComponent<Anchors>().setIncorrect();
				}
				lockObj = null;
				locked = State.Free;
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
            Spawner = true;
        } else {
			Debug.Log("Cant Add Piece");
		}
	}

    public void setState()
    {
        locked = State.ReadyToLock;
    }

	public void SubPiece()
	{
		if (sNumPieces <= MaxPieces && sNumPieces > 0) {
			id = sNumPieces;
			sNumPieces--;
			Debug.Log("Subtracting Piece");
			if (sNumPieces <= 0) {

				Spawner = false;
				Debug.Log("Cannot spawn any more pieces");
			}
		} else {
			Debug.Log("Cant Sub Piece");
		}
	}

    public void checkRot()
    {
        if (!slot)
        {
            if (locked == State.Locked)
            {
                //if cross
				if (type == PieceEnumUtil.PieceEnum.Cross) {
                    lockObj.GetComponent<Anchors>().setCorrect();
                }
                //if line
				else if (type == PieceEnumUtil.PieceEnum.Line) {
					var anc = lockObj.GetComponent<Anchors>();
					Debug.LogFormat("[{0}:checkRot] direction {1} anc.direction {2}", name, direction, anc.direction);

					if (anc.direction == direction || PieceEnumUtil.ToAcross(anc.direction) == direction)
					{
						Debug.LogFormat("[{0}:checkRot] Matching rotation {1}", name, this.gameObject.transform.rotation.z);
						anc.setCorrect();
					}
                    else
					{
						Debug.LogFormat("[{0}:checkRot] Wrong rotation {1}", name, this.gameObject.transform.rotation.z);
						anc.setIncorrect();
					}
				}
				else if (type == PieceEnumUtil.PieceEnum.L || type == PieceEnumUtil.PieceEnum.T)
				{
					var anc = lockObj.GetComponent<Anchors>();
					Debug.LogFormat("[{0}:checkRot] direction {1} anc.direction {2}", name, direction, anc.direction);

					if (anc.direction == direction) {
						Debug.LogFormat("[{0}:checkRot] Matching rotation {1}", name, this.gameObject.transform.rotation.z);
						anc.setCorrect();
					} else {
						Debug.LogFormat("[{0}:checkRot] Wrong rotation {1}", name, this.gameObject.transform.rotation.z);
						anc.setIncorrect();
					}
                }
            }
        }
    }
}
