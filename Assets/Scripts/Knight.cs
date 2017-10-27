using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Knight : Piece {

    public static bool knightAtk = false;
    public bool hasMoved = false;
    public bool canMove = true;
    public Camera cam;

    // Use this for initialization
    public override void Start()
    {
        attack = 2;
        hp = 3;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MovePiece>().cam;
        base.Start();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0) && atk)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.transform.parent.CompareTag("White") || hit.transform.parent.CompareTag("Black"))
            {
                Attack(hit.transform.gameObject);
                if (hp <= 0)
                {
                    knightAtk = false;
                    atk = false;
                    MovePiece.EndTurn();
                    Debug.Log(knightAtk + " " + atk + " " + Board.whiteTurn);
                    Kill();
                }
            }
            else if (hit.transform.CompareTag("Empty"))
            {
                //MovePiece.EndTurn();
                int currX = (int)gameObject.transform.position.x;
                int currY = (int)gameObject.transform.position.y;
                if ((Math.Abs(currX - hit.transform.position.x) == 2 && Math.Abs(currY - hit.transform.position.y) == 1) ||
                    (Math.Abs(currX - hit.transform.position.x) == 1 && Math.Abs(currY - hit.transform.position.y) == 2))
                {
                    MoveTo(hit.transform.position);
                    hasMoved = false;
                    atk = false;
                    knightAtk = false;
                    MovePiece.EndTurn();
                }
                else {
                    knightAtk = false;
                    atk = false;
                    MovePiece.DeselectPiece();
                }
            }
            else {
                atk = false;
                knightAtk = false;
                attacking = false;
            }
        }
    }

    public override void Move(Vector3 target)
    {
        if (CanMove(gameObject.transform.position, target))
        {
            base.Move(target);
            MovePiece.EndTurn();
        }
    }

    public override bool CanMove(Vector3 initPos, Vector3 target) {
        bool canMove = false;
        int currX = (int)gameObject.transform.position.x;
        int currY = (int)gameObject.transform.position.y;
        // Debug.Log()
        if ((Math.Abs(currX - target.x) == 2 && Math.Abs(currY - target.y) == 1) ||
            (Math.Abs(currX - target.x) == 1 && Math.Abs(currY - target.y) == 2))
        {
            canMove = true;
        }
        else {
            hasMoved = false;
        }
        return canMove;
    }

    public override void Attack(GameObject target)
    {
        int currX = (int)gameObject.transform.position.x;
        int currY = (int)gameObject.transform.position.y;
        // Debug.Log()
        if ((Math.Abs(currX - target.transform.position.x) == 2 && Math.Abs(currY - target.transform.position.y) == 1) ||
            (Math.Abs(currX - target.transform.position.x) == 1 && Math.Abs(currY - target.transform.position.y) == 2))
        {
            MoveTo(target.transform.position);
            GameObject[] g = GameObject.FindGameObjectsWithTag("MoveOption");
            for (int i = 0; i < g.Length; ++i)
            {
                Destroy(g[i]);
            }
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MovePiece>().ShowMoves();
            hasMoved = true;
            target.GetComponent<Piece>().hp -= attack;
            if (target.transform.parent.tag != gameObject.transform.parent.tag)
            {
                hp--;
                RefreshDisplay();
            }
            if (target.GetComponent<Piece>().hp <= 0)
            {
                target.GetComponent<Piece>().Kill();
            }
            else
            {
                target.GetComponent<Piece>().RefreshDisplay();
            }
        }
        else if (target != gameObject) {
                knightAtk = false;
                atk = false;
                MovePiece.DeselectPiece();
        }
    }

    public override void WriteDescription()
    {
        description.text = "The knight has 2 attack \n" +
                           "and 3 hit points. It \n" +
                           "moves in an L shaped \n" + 
                           "pattern. If it lands on \n" +
                           "an occupied squared it \n" +
                           "deals 2 damage to the \n" +
                           "piece, and, if the piece \n" +
                           "is an enemy, the knight \n" +
                           "takes 1 damage. The knight \n" +
                           "must continue to take \n" +
                           "moves until it lands on an \n" +
                           "empty square or dies.";
        //                 "12345678912345678912345";
        description.transform.position = gameObject.transform.position - new Vector3(0, 2.8f, 2);
    }
}
