using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class King : Pawn {

    // Use this for initialization
    public override void Start()
    {
        hp = 4;
        attack = 6;
        base.Start();
        speed = 1;
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

    public override void Attack(GameObject target)
    {
        base.Attack(target);
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
