using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Queen : Piece {

    // Use this for initialization
    public override void Start()
    {
        attack = 5;
        hp = 5;
        base.Start();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public override bool CanMove(Vector3 initPos, Vector3 target) {
        bool canMove = false;
        int currX = (int)initPos.x;
        int currY = (int)initPos.y;
        if (target.x == currX || target.y == currY)
        {
            canMove = SMove(target);
        }
        else if ((currX + currY == target.x + target.y) || (currX - currY == target.x - target.y)) {
            canMove = DMove(target);
        }
        return canMove;
    }

    public bool SMove(Vector3 target) {
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

    public bool DMove(Vector3 target) {
        bool canMove = false;
        int currX = (int)gameObject.transform.position.x;
        int currY = (int)gameObject.transform.position.y;
        int startPosY = Math.Min(currY, (int)target.y) + 1;
        int endPosY = Math.Max(currY, (int)target.y);
        int startPosX = Math.Min(currX, (int)target.x) + 1;
        int endPosX = Math.Max(currX, (int)target.x);
        bool blocked = false;
        if (currX - currY == target.x - target.y) {
            int j = startPosX;
            for (int i = startPosY; i < endPosY; ++i) {
                Debug.Log(i + " " + j + Board.full[i, j]);
                if (Board.full[i, j])
                {
                    blocked = true;
                    break;
                }
                ++j;
            }
            if (!blocked) {
                canMove = true;
            }
        }
        else if (currX + currY == target.x + target.y)
        {
            int j = endPosX - 1;
            for(int i = startPosY; i < endPosY; ++i) {
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
        if (gameObject.tag == "Queen" && CanMove(initPos, target.transform.position) && Distance(gameObject.transform.position, target.transform.position) == 1)
        {
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
        Debug.Log("1");
        description.text = "The queen has 5 attack \n" +
                           "and 5 hit points. It \n" +
                           "has a movement speed of \n" +
                           "7 and can move in all \n" +
                           "eight directions.";
        //                 "12345678912345678912345";
        description.transform.position = gameObject.transform.position - new Vector3(0, 2f, 2);
    }
}
