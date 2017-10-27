using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    public GameObject whiteSquare;
    public GameObject blackSquare;
    public GameObject WhitePawn;
    public GameObject WhiteRook;
    public GameObject WhiteKnight;
    public GameObject WhiteBishop;
    public GameObject WhiteQueen;
    public GameObject WhiteKing;
    public GameObject BlackPawn;
    public GameObject BlackRook;
    public GameObject BlackKnight;
    public GameObject BlackBishop;
    public GameObject BlackQueen;
    public GameObject BlackKing;

    public GameObject Sword;
    public GameObject Heart;

    public static bool whiteTurn = true;
    public float heartOffset = 0.3f;
    public float swordOffset = 0.25f;

    public const int BOARD_SIZE = 8;

    public static bool[,] full = new bool[BOARD_SIZE,BOARD_SIZE];

    // Use this for initialization
    void Start () {
        for (int i = 0; i < BOARD_SIZE; ++i) {
            for (int j = 0; j < BOARD_SIZE; ++j) {
                if (i < 2 || i > BOARD_SIZE - 3)
                {
                    full[i,j] = true;
                }
                else {
                    full[i,j] = false;
                }
            }
        }
        CreateBoard();
        whiteTurn = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreateBoard() {
        GameObject g;
        for (int i = 0; i < BOARD_SIZE; ++i) {
            for (int j = 0; j < BOARD_SIZE; ++j) {
                if (i % 2 == 0)
                {
                    if (j % 2 == 0)
                    {
                        g = Instantiate(whiteSquare, new Vector3(j, i, 1), Quaternion.identity);
                    }
                    else
                    {
                        g = Instantiate(blackSquare, new Vector3(j, i, 1), Quaternion.identity);
                    }
                }
                else {
                    if (j % 2 == 0)
                    {
                        g = Instantiate(blackSquare, new Vector3(j, i, 1), Quaternion.identity);
                    }
                    else
                    {
                        g = Instantiate(whiteSquare, new Vector3(j, i, 1), Quaternion.identity);
                    }
                }
                g.transform.parent = GameObject.FindGameObjectWithTag("GameBoard").transform;
            }
        }
        for (int i = 0; i < BOARD_SIZE; ++i) {
            g = Instantiate(WhitePawn, new Vector3(i, 1, 0), Quaternion.identity);
            Instantiate(Sword, new Vector3(i - 0.25f, 1 - swordOffset, -1), Quaternion.identity).transform.parent = g.transform;
            Instantiate(Heart, new Vector3(i + 0.25f, 1 - heartOffset, -1), Quaternion.identity).transform.parent = g.transform;
            g.transform.parent = GameObject.FindGameObjectWithTag("White").transform;
            if (i == 0 || i == BOARD_SIZE - 1)
            {
                g = Instantiate(WhiteRook, new Vector3(i, 0, 0), Quaternion.identity);
            }
            else if (i == 1 || i == BOARD_SIZE - 2)
            {
                g = Instantiate(WhiteKnight, new Vector3(i, 0, 0), Quaternion.identity);
            }
            else if (i == 2 || i == BOARD_SIZE - 3)
            {
                g = Instantiate(WhiteBishop, new Vector3(i, 0, 0), Quaternion.identity);
            }
            else if (i == 4)
            {
                g = Instantiate(WhiteQueen, new Vector3(i, 0, 0), Quaternion.identity);
            }
            else {
                g = Instantiate(WhiteKing, new Vector3(i, 0, 0), Quaternion.identity);
            }
            Instantiate(Sword, new Vector3(i - 0.25f, -swordOffset, -1), Quaternion.identity).transform.parent = g.transform;
            Instantiate(Heart, new Vector3(i + 0.25f, -heartOffset, -1), Quaternion.identity).transform.parent = g.transform;
            g.transform.parent = GameObject.FindGameObjectWithTag("White").transform;
        }

        for (int i = 0; i < BOARD_SIZE; ++i)
        {
            g = Instantiate(BlackPawn, new Vector3(i, BOARD_SIZE - 2, 0), Quaternion.identity);
            Instantiate(Sword, new Vector3(i - 0.25f, BOARD_SIZE - 2 - swordOffset, -1), Quaternion.identity).transform.parent = g.transform;
            Instantiate(Heart, new Vector3(i + 0.25f, BOARD_SIZE - 2 - heartOffset, -1), Quaternion.identity).transform.parent = g.transform;
            g.transform.parent = GameObject.FindGameObjectWithTag("Black").transform;
            if (i == 0 || i == BOARD_SIZE - 1)
            {
                g = Instantiate(BlackRook, new Vector3(i, BOARD_SIZE - 1, 0), Quaternion.identity);
            }
            else if (i == 1 || i == BOARD_SIZE - 2)
            {
                g = Instantiate(BlackKnight, new Vector3(i, BOARD_SIZE - 1, 0), Quaternion.identity);
            }
            else if (i == 2 || i == BOARD_SIZE - 3)
            {
                g = Instantiate(BlackBishop, new Vector3(i, BOARD_SIZE - 1, 0), Quaternion.identity);
            }
            else if (i == 4)
            {
                g = Instantiate(BlackQueen, new Vector3(i, BOARD_SIZE - 1, 0), Quaternion.identity);
            }
            else
            {
                g = Instantiate(BlackKing, new Vector3(i, BOARD_SIZE - 1, 0), Quaternion.identity);
            }
            Instantiate(Sword, new Vector3(i - 0.25f, BOARD_SIZE - 1 - swordOffset, -1), Quaternion.identity).transform.parent = g.transform;
            Instantiate(Heart, new Vector3(i + 0.25f, BOARD_SIZE - 1 - heartOffset, -1), Quaternion.identity).transform.parent = g.transform;
            g.transform.parent = GameObject.FindGameObjectWithTag("Black").transform;
        }
    }

    void OnGUI() {
        if (!Knight.knightAtk)
        {
            if (Piece.attacking)
            {
                if (GUI.Button(new Rect(10, 10, 150, 100), "End Turn"))
                {
                    MovePiece.EndTurn();
                }
            }
            else if (MovePiece.selectedPiece != null)
            {
                if (MovePiece.selectedPiece.tag == "Knight")
                {
                    if (GUI.Button(new Rect(10, 150, 150, 100), "Attack With " + MovePiece.selectedPiece.tag))
                    {
                    
                        MovePiece.selectedPiece.GetComponent<Knight>().atk = true;
                        Knight.knightAtk = true;
                    }
                    Piece.attacking = true;
                }
            }
        }
    }
}
