using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePiece : MonoBehaviour {

    public Camera cam;
    public static GameObject selectedPiece;
    public static GameObject movedPiece;
    public GameObject canMove;
    public GameObject canAttack;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && !Knight.knightAtk)
        {
            Debug.Log("Cast " + Board.whiteTurn);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.transform.parent.CompareTag("White"))
            {
                //if (Piece.attacking)
                //{
                if (selectedPiece != null && selectedPiece.transform.parent.CompareTag("Black"))
                {
                    selectedPiece.GetComponent<Piece>().Attack(hit.transform.gameObject);
                }
                //}
                else if (Board.whiteTurn)
                {
                    if (selectedPiece != null)
                    {
                        DeselectPiece();
                    }
                    if (movedPiece == null || movedPiece == hit.transform.gameObject)
                    {
                        selectedPiece = hit.transform.gameObject;
                        ShowMoves();
                        selectedPiece.GetComponent<Piece>().CreateDescription();
                        if (selectedPiece.tag == "Knight" && !Piece.attacking)
                        {
                            Knight.knightAtk = true;
                            selectedPiece.GetComponent<Piece>().atk = true;
                        }
                    }
                }
                //Debug.Log(Board.full[(int) hit.transform.position.y, (int) hit.transform.position.x]);
            }
            else if (hit.transform.parent.CompareTag("Black"))
            {

                // if (Piece.attacking)
                //  {
                if (selectedPiece != null && selectedPiece.transform.parent.CompareTag("White"))
                {
                    Debug.Log("Attack Black");
                    selectedPiece.GetComponent<Piece>().Attack(hit.transform.gameObject);
                }
                // }
                else if (!Board.whiteTurn)
                {
                    if (selectedPiece != null)
                    {
                        DeselectPiece();
                    }
                    if (movedPiece == null || movedPiece == hit.transform.gameObject)
                    {
                        selectedPiece = hit.transform.gameObject;
                        ShowMoves();
                        selectedPiece.GetComponent<Piece>().CreateDescription();
                        if (selectedPiece.tag == "Knight" && !Piece.attacking)
                        {
                            Knight.knightAtk = true;
                            selectedPiece.GetComponent<Piece>().atk = true;
                        }
                    }
                }
                //Debug.Log(Board.full[(int)hit.transform.position.y, (int)hit.transform.position.x]);
            }
            else if (hit.transform.CompareTag("Empty")) {
                if (selectedPiece != null && !Piece.attacking) {
                    selectedPiece.GetComponent<Piece>().Move(hit.transform.position);
                }
                //Debug.Log(Board.full[(int)hit.transform.position.y, (int)hit.transform.position.x]);
            }
        }
    }

    public static void DeselectPiece() {
        GameObject[] g = GameObject.FindGameObjectsWithTag("MoveOption");
        for (int i = 0; i < g.Length; ++i)
        {
            Destroy(g[i]);
        }
        selectedPiece.GetComponent<Piece>().DestroyDescription();
        selectedPiece = null;
    }

    public void ShowMoves() {
        for (int i = 0; i < Board.BOARD_SIZE; ++i)
        {
            for (int j = 0; j < Board.BOARD_SIZE; ++j)
            {
                if (selectedPiece.GetComponent<Piece>().CanMove(selectedPiece.transform.position, new Vector3(j, i, 0)))
                {
                    if (Board.full[i, j])
                    {
                        Instantiate(canAttack, new Vector3(j, i, 0), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(canMove, new Vector3(j, i, 0), Quaternion.identity);
                    }
                }
            }
        }
    }

    public static void EndTurn() {
        Piece.attacking = false;
        movedPiece = null;
        if (selectedPiece != null)
        {
            selectedPiece.GetComponent<Piece>().initPos = selectedPiece.transform.position;
            selectedPiece.GetComponent<Piece>().DestroyDescription();
            GameObject[] g = GameObject.FindGameObjectsWithTag("MoveOption");
            for (int i = 0; i < g.Length; ++i)
            {
                Destroy(g[i]);
            }
        }
        selectedPiece = null;
        Board.whiteTurn = !Board.whiteTurn;
        if (Board.whiteTurn)
        {
            GameObject.FindGameObjectWithTag("WhiteTurnBG").GetComponent<SpriteRenderer>().enabled = true;
        }
        else {
            GameObject.FindGameObjectWithTag("WhiteTurnBG").GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
