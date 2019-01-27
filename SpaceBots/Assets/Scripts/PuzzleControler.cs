using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleControler : MonoBehaviour
{
    public static event Action PickupEvent;

    public static event Action DropWrongEvent;

    public static event Action RotateEvent;

    PuzzlePiece Pp;

    public int score;

    public int finalScore;

    //spawned piece ref
    private GameObject Piece;

    //locked piece ref
    private GameObject Slot;

    private GameObject[] Anchors;

    private GameObject[] Slots;

    //slot pos ref
    private Vector3 lastPos;

    private void Start()
    {
        if (Anchors == null)
            Anchors = GameObject.FindGameObjectsWithTag("Anchor");

        //if (Slots == null)
        //{
        //    Slots = GameObject.FindGameObjectsWithTag("Piece");
        //}
        //
        //for(int i = 0; i<Anchors.Length-1; i++)
        //{
        //   int newType = Anchors[i].GetComponent<Anchors>()._lockType;
        //    for(int j = 0; j<Slots.Length-1; j++)
        //    {
        //        if(Slots[j].GetComponent<PuzzlePiece>().type == newType)
        //        {
        //            Slots[j].GetComponent<PuzzlePiece>().setMaxP(1);
        //        }
        //    }
        //}

        finalScore = Anchors.Length;
        Debug.Log("Number of anchors is " + Anchors.Length);
        Debug.Log("final score is " + finalScore);
    }

    // Update is called once per frame
    void Update()
    {
        doMouseThing();
        Debug.Log(score);
        checkScore();
    }

    void checkScore()
    {
        if (score == finalScore)
        {
            //proceed to next scene
            Debug.Log("PUZZLE COMPLETE");
            GameController.Instance.LoadNextScene();
        }
        else
        {
            Debug.Log("Number of pieces correct is " + score + "/" + finalScore);
        }
    }

    void doMouseThing()
    {
        //do raycast
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray CheckRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log("Sending RayCast");
            if (Physics.Raycast(CheckRay, out hit, 100f))
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.tag == "Piece")
                {
                    var invokeEvent = PickupEvent;
                    if (invokeEvent != null)
                    {
                        invokeEvent();
                    }

                    hit.collider.gameObject.GetComponent<PuzzlePiece>().hasBeenPickedUp = true;

                    if (hit.collider.gameObject.GetComponent<PuzzlePiece>().slot == false)
                    {
                        hit.collider.gameObject.GetComponent<PuzzlePiece>().setState();
                    }
                    else if (hit.collider.gameObject.GetComponent<PuzzlePiece>().Spawn == true) {
						//set hit to an obj ref
						Slot = hit.collider.gameObject;
						Pp = Slot.GetComponent<PuzzlePiece>();
						if (Pp.Spawn == true && Pp.slot == true) {
							//subtract piece
							Pp.SubPiece();
							Piece = Instantiate(Slot);
							Piece.GetComponent<PuzzlePiece>().slot = false;
							Pp = Piece.GetComponent<PuzzlePiece>();
						}

						//set last position to the hit location
						lastPos = Slot.GetComponent<Transform>().transform.position;
						Pp.SetParent(Slot);
					}
                }
            }
        }
        //do lock
        else if (Input.GetMouseButtonUp(0))
        {
            Piece = null;
        }
        //rotate right
		else if (Input.mouseScrollDelta.y > 0f|| Input.GetKeyUp(KeyCode.RightArrow))
        {
            Debug.Log("Casting rotation ray");
            RaycastHit hit;
            Ray CheckRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log("Sending RayCast");
            if (Physics.Raycast(CheckRay, out hit, 100f))
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.tag == "Piece")
				{
					var puz = hit.collider.gameObject.GetComponent<PuzzlePiece>();
					if (puz.slot != true)
                    {
                        Debug.Log("Do the rotation functions");
                        GameObject hitPiece = hit.collider.gameObject;

                        var invokeEvent = RotateEvent;
                        if (invokeEvent != null)
                        {
                            invokeEvent();
                        }

						puz.direction = PieceEnumUtil.ToRight(puz.direction);
                        hitPiece.transform.Rotate(0, 0, 90);
						puz.checkRot();
                    }
                }
            }
        }
        //rotate left
		else if (Input.mouseScrollDelta.y < 0f || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            Debug.Log("Casting rotation ray");
            RaycastHit hit;
            Ray CheckRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log("Sending RayCast");
            if (Physics.Raycast(CheckRay, out hit, 100f))
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.tag == "Piece")
                {
					var puz = hit.collider.gameObject.GetComponent<PuzzlePiece>();
					if (puz.slot != true)
                    {
                        Debug.Log("Do the rotation functions");
                        GameObject hitPiece = hit.collider.gameObject;

                        var invokeEvent = RotateEvent;
                        if (invokeEvent != null)
                        {
                            invokeEvent();
                        }

						puz.direction = PieceEnumUtil.ToLeft(puz.direction);
                        hitPiece.transform.Rotate(0, 0, -90);
						puz.checkRot();
                    }
                }
            }
        }
        
    }
}

