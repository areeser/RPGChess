using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Rook : Piece {

	// Use this for initialization
	public override void Start () {
        attack = 2;
        hp = 6;
        base.Start();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Move(Vector3 target) {
        if (CanMove(gameObject.transform.position, target)) {
            MoveTo(target);
        }
    }

    public override bool CanMove(Vector3 initPos, Vector3 target)
    {
        bool canMove = false;
        int currX = (int)initPos.x;
        int currY = (int)initPos.y;
        if (target.x == currX || target.y == currY)
        {
            canMove = SMove(target);
        }
        return canMove;
    }

    public bool SMove(Vector3 target)
    {
        bool canMove = false;
        bool blocked = false;
        int currX = (int)gameObject.transform.position.x;
        int currY = (int)gameObject.transform.position.y;
        if (target.x == currX)
        {
            int startPos = Math.Min(currY, (int)target.y);
            int endPos = Math.Max(currY, (int)target.y);
            for (int i = startPos + 1; i < endPos; ++i)
            {
                if (Board.full[i, currX])
                {
                    blocked = true;
                }
            }
            if (!blocked)
            {
                canMove = true;
            }
        }
        else if (target.y == currY)
        {
            int startPos = Math.Min(currX, (int)target.x);
            int endPos = Math.Max(currX, (int)target.x);
            for (int i = startPos + 1; i < endPos; ++i)
            {
                if (Board.full[currY, i])
                {
                    blocked = true;
                }
            }
            if (!blocked)
            {
                canMove = true;
            }
        }
        return canMove;
    }

    /*public override bool CanMove(Vector3 initPos, Vector3 target)
    {
        bool canMove = false;
        int currX = (int) initPos.x;
        int currY = (int) initPos.y;
        bool blocked = false;
        if (target.x == currX) {
            int startPos = Math.Min(currY, (int)target.y);
            int endPos = Math.Max(currY, (int)target.y);
            for (int i = startPos + 1; i < endPos; ++i) {
                if (Board.full[i, currX]) {
                    blocked = true;
                }
            }
            if (!blocked) {
                canMove = true;
            }
        }
        else if (target.y == currY) {
            int startPos = Math.Min(currX, (int)target.x);
            int endPos = Math.Max(currX, (int)target.x);
            for (int i = startPos + 1; i < endPos; ++i)
            {
                if (Board.full[currY, i])
                {
                    blocked = true;
                }
            }
            if (!blocked)
            {
                canMove = true;
            }
        }
        return canMove;
    }*/

    public override void Attack(GameObject target)
    {
        Debug.Log(CanMove(initPos, target.transform.position) + " " + Distance(gameObject.transform.position, target.transform.position));
        if (CanMove(initPos, target.transform.position) && Distance(gameObject.transform.position, target.transform.position) == 1)
        {
            Debug.Log("2");
            target.GetComponent<Piece>().hp -= attack;
            base.Attack(target);
        }
        else
        {
            MovePiece.DeselectPiece();
        }
    }

    public override void WriteDescription()
    {
        description.text = "The rook has 2 attack \n" +
                           "and 6 hit points. It \n" +
                           "has a movement speed of \n" +
                           "7 and can move horizontally\n" + 
                           "and vertically.";
        //                 "12345678912345678912345";
        description.transform.position = gameObject.transform.position - new Vector3(0, 2f, 2);
    }
}
