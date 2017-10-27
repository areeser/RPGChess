using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pawn : Piece {

    public bool hasMoved = false;
    public int speed = 2;
    

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        speed = 2;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public override void Move(Vector3 target) {
        base.Move(target);
        if (MovePiece.selectedPiece != null) {
            speed = 1;
        }
    }

    public override bool CanMove(Vector3 initPos, Vector3 target)
    {
        bool canMove = false;
        if (Distance(initPos, target) <= speed) {
            canMove = true;
        }
        return canMove;
    }

    public override void Attack(GameObject target)
    {
        Debug.Log(Distance(initPos, target.transform.position));
        if (CanMove(initPos, gameObject.transform.position) && Distance(initPos, target.transform.position) <= speed) {
            target.GetComponent<Piece>().hp -= attack;
            if (gameObject.tag == "Pawn")
            {
                if (target.GetComponent<Piece>().hp <= 0) {
                    attack++;
                    RefreshDisplay();
                }
            }
            base.Attack(target);
        }
        else {
            MovePiece.DeselectPiece();
        }
    }

    public override void WriteDescription()
    {
        description.text = "The pawn has 1 attack \n" +
                           "and 1 hit point. When \n" +
                           "it destroys an enemy \n" +
                           "it gains 1 attack. It \n" +
                           "has a movement speed of \n" +
                           "1 and can move in all \n" + 
                           "eight directions. If it \n" +
                           "has not moved before, it \n" +
                           "has a movement speed of 2";
        //                 "12345678912345678912345";
        description.transform.position = gameObject.transform.position - new Vector3(0, 2.5f, 2);
    }
}
