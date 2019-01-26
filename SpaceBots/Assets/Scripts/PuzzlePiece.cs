using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public int id;

    private GameObject Parent;

    private Vector3 lockPos;

    //if obj is in place
    private bool locked;

    //if obj is stationary ref for copy
    public bool slot;

    //number of pieces left
    private int numPieces;

    //max number of objects this can spawn
    [SerializeField]
    private int MaxPieces;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!slot)
        {
            //lerp to mouse pos
            doMouseThing();
        }

    }

    void doMouseThing()
    {
        if (Input.GetMouseButtonUp(0))
        {
            {
                if (!locked)
                {
                    //lerp to origin here
                    Destroy(this.gameObject);
                }
                else
                {
                    this.gameObject.transform.position = lockPos;
                }
            }
        }
    }

    //use for drop location
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Anchor")
        {
            locked = true;
            other.gameObject.transform.position = lockPos;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Anchor")
        {
            locked = false;
            lockPos = Parent.transform.position;
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
        }
        else
        {
            Debug.Log("Cant Add Piece");
        }
    }

    public void SubPiece()
    {
        if(numPieces<= MaxPieces && numPieces > 0)
        {
            numPieces--;
            Debug.Log("Subtracting Piece");
        }
        else
        {
            Debug.Log("Cant Sub Piece");
        }
    }
}
