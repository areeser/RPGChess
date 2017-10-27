using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bishop : Piece {

    // Use this for initialization
    public override void Start()
    {
        attack = 3;
        hp = 3;
        InitDisplays();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public override void Move(Vector3 target)
    {
        base.Move(target);
    }

    public override bool CanMove(Vector3 initPos, Vector3 target)
    {
        bool canMove = false;
        int currX = (int)initPos.x;
        int currY = (int)initPos.y;
        if ((currX + currY == target.x + target.y) || (currX - currY == target.x - target.y))
        {
            canMove = DMove(target);
        }
        return canMove;
    }

    public bool DMove(Vector3 target)
    {
        bool canMove = false;
        int currX = (int)gameObject.transform.position.x;
        int currY = (int)gameObject.transform.position.y;
        int startPosY = Math.Min(currY, (int)target.y) + 1;
        int endPosY = Math.Max(currY, (int)target.y);
        int startPosX = Math.Min(currX, (int)target.x) + 1;
        int endPosX = Math.Max(currX, (int)target.x);
        bool blocked = false;
        if (currX - currY == target.x - target.y)
        {
            int j = startPosX;
            for (int i = startPosY; i < endPosY; ++i)
            {
                Debug.Log(i + " " + j + Board.full[i, j]);
                if (Board.full[i, j])
                {
                    blocked = true;
                    break;
                }
                ++j;
            }
            if (!blocked)
            {
                canMove = true;
            }
        }
        else if (currX + currY == target.x + target.y)
        {
            int j = endPosX - 1;
            for (int i = startPosY; i < endPosY; ++i)
            {
                Debug.Log(i + " " + j);
                if (Board.full[i, j])
                {
                    blocked = true;
                    break;
                }
                --j;
            }
            if (!blocked)
            {
                canMove = true;
            }
        }
        return canMove;
    }

    public override void Attack(GameObject target)
    {
        int currX = (int)gameObject.transform.position.x;
        int currY = (int)gameObject.transform.position.y;
        int startPosY = Math.Min(currY, (int)target.transform.position.y) + 1;
        int endPosY = Math.Max(currY, (int)target.transform.position.y);
        int startPosX = Math.Min(currX, (int)target.transform.position.x) + 1;
        int endPosX = Math.Max(currX, (int)target.transform.position.x);
        bool blocked = false;
        if (currX - currY == target.transform.position.x - target.transform.position.y)
        {
            int j = startPosX;
            for (int i = startPosY; i < endPosY; ++i)
            {
                
                Debug.Log(i + " " + j + Board.full[i, j]);
                if (Board.full[i, j])
                {
                    blocked = true;
                }
                ++j;
            }
            if (!blocked)
            {
                Debug.Log(gameObject.GetComponent<Piece>().hp);
                target.GetComponent<Piece>().hp -= attack;
                Debug.Log(gameObject.GetComponent<Piece>().hp);
                base.Attack(target);
            }
            else
            {
                Debug.Log("Else");
                MovePiece.DeselectPiece();
            }
        }
        else if (currX + currY == target.transform.position.x + target.transform.position.y)
        {
            int j = endPosX - 1;
            for (int i = startPosY; i < endPosY; ++i)
            {
                Debug.Log(i + " " + j);
                if (Board.full[i, j])
                {
                    blocked = true;
                }
                --j;
            }
            if (!blocked)
            {
                Debug.Log("If");
                Debug.Log(gameObject.GetComponent<Piece>().hp);
                target.GetComponent<Piece>().hp -= attack;
                Debug.Log(gameObject.GetComponent<Piece>().hp);
                base.Attack(target);
            }
            else
            {
                MovePiece.DeselectPiece();
                Debug.Log("Else");
            }
        }
        else
        {
            MovePiece.DeselectPiece();
        }
    }

    public override void WriteDescription()
    {
        description.text = "The bishop has 3 attack \n" +
                           "and 3 hit points. It has \n" + 
                           "a movement speed of 7 and\n" + 
                           "can move diagonally. If it\n" + 
                           "has not moved this turn it\n" +       
                           "can make a ranged attack \n" +
                           "against an enemy in a \n" +
                           "space it could move into.";
        //                 "12345678912345678912345";
        description.transform.position = gameObject.transform.position - new Vector3(0, 2.5f, 2);
    }
}
