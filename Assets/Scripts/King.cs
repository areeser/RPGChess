using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class King : Piece {

    // Use this for initialization

    int speed = 1;

    public override void Start()
    {
        hp = 4;
        attack = 6;
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Kill() {
        if (gameObject.transform.parent.tag == "Black")
        {
            SceneManager.LoadScene("WhiteWins");
        }
        else {
            SceneManager.LoadScene("BlackWins");
        }
        base.Kill();
    }

    public override void Move(Vector3 target)
    {
        base.Move(target);
    }

    public override bool CanMove(Vector3 initPos, Vector3 target)
    {
        bool canMove = false;
        if (Distance(initPos, target) <= speed)
        {
            canMove = true;
        }
        return canMove;
    }

    public override void Attack(GameObject target)
    {
        Debug.Log(Distance(initPos, target.transform.position));
        if (CanMove(initPos, gameObject.transform.position) && Distance(initPos, target.transform.position) <= speed)
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
        description.text = "The king has 6 attack \n" +
                           "and 4 hit points. It \n" +
                           "has a movement speed of \n" +
                           "1 and can move in all \n" +
                           "eight directions. If it \n" +
                           "is destroyed, you will \n" +
                           "lose the game.";
        //                 "12345678912345678912345";
        description.transform.position = gameObject.transform.position - new Vector3(0, 2f, 2);
    }
}
