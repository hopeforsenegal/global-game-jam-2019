using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleControler : MonoBehaviour
{

    public static event Action PickupEvent;

    public static event Action DropCorrectEvent;

    public static event Action DropWrongEvent;

    public static event Action RotateEvent;


    PuzzlePiece Pp;

    //spawned piece ref
    private GameObject Piece;

    //locked piece ref
    private GameObject Slot;

    //slot pos ref
    private Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        doMouseThing();
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
                    //set hit to an obj ref
                    Slot = hit.collider.gameObject;
                    Pp = Slot.GetComponent<PuzzlePiece>();
                    if (Pp.Spawn == true)
                    {
                        var invokeEvent = PickupEvent;
                        if (invokeEvent != null) {
                            invokeEvent();
                        }

                        if (Pp.slot == true)
                        {
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
        else if (Input.mouseScrollDelta.y > 0f)
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
                    if (hit.collider.gameObject.GetComponent<PuzzlePiece>().slot != true)
                    {
                        Debug.Log("Do the rotation functions");
                        GameObject hitPiece = hit.collider.gameObject;

                        hitPiece.transform.Rotate(0,0,90);

                    }
                }
            }
        }
        //rotate left
        else if (Input.mouseScrollDelta.y < 0f)
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
                    if (hit.collider.gameObject.GetComponent<PuzzlePiece>().slot != true)
                    {
                        Debug.Log("Do the rotation functions");
                        GameObject hitPiece = hit.collider.gameObject;

                        hitPiece.transform.Rotate(0, 0, -90);

                    }
                }
            }
        }
    }
}