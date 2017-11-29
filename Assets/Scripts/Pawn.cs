using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pawn : Queen {

    public bool hasMoved = false;
    

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        attack = 1;
        hp = 1;
        speed = 2;
        InitDisplays();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public override void Move(Vector3 target) {
        base.Move(target);
        if (!hasMoved && MovePiece.selectedPiece != null) {
            speed = 1;
            hasMoved = true;
        }
    }

    public override bool CanMove(Vector3 initPos, Vector3 target)
    {
        return base.CanMove(initPos, target);
    }

    public override void Attack(GameObject target)
    {
        Debug.Log(Distance(initPos, target.transform.position));
        base.Attack(target);
        if (target.GetComponent<Piece>().hp <= 0)
        {
            attack++;
            speed++;
            RefreshDisplay();
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
