using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Piece : MonoBehaviour {

    public float displayPosX = 10;
    public float displayPosY = 5;
    public GameObject display;

    public Vector3 initPos;
    public int hp = 1;
    public int attack = 1;
    public bool selected = false;
    public bool atk;
    public static bool attacking = false; 
    public Text hpDisplay;
    public Text atkDisplay;
    public Text description;

    // Use this for initialization
    public virtual void Start () {
        InitDisplays();
        initPos = gameObject.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        //atk = attacking;
	}

    public virtual void Kill() {
        Board.full[(int)gameObject.transform.position.y, (int)gameObject.transform.position.x] = false;
        Destroy(gameObject);
    }

    public int Distance(Vector3 initPos, Vector3 endPos) {
        int xDist = Math.Abs((int) Math.Floor(initPos.x - endPos.x));
        int yDist = Math.Abs((int) Math.Floor(initPos.y - endPos.y));
        return Math.Max(xDist, yDist);
    }

    public void MoveTo(Vector3 position)
    {
        Board.full[(int)gameObject.transform.position.y, (int)gameObject.transform.position.x] = false;
        gameObject.transform.position = position;
        Board.full[(int)gameObject.transform.position.y, (int)gameObject.transform.position.x] = true;
        attacking = true;
    }

    public virtual void Move(Vector3 target) {
        if (CanMove(gameObject.transform.position, target)) {
            MoveTo(target);
        }
        else
        {
            GameObject[] g = GameObject.FindGameObjectsWithTag("MoveOption");
            for (int i = 0; i < g.Length; ++i)
            {
                Destroy(g[i]);
            }
            MovePiece.selectedPiece.GetComponent<Piece>().DestroyDescription();
            MovePiece.selectedPiece = null;
        }
    }

    public virtual bool CanMove(Vector3 initPos, Vector3 target) {
        return false;
    }

    public void InitDisplays() {
        hpDisplay.text = hp.ToString();
        hpDisplay.transform.position = gameObject.transform.position - new Vector3(-0.25f, 0.3f, 2);
        atkDisplay.text = attack.ToString();
        atkDisplay.transform.position = gameObject.transform.position - new Vector3(0.25f, 0.3f, 2);
    }

    public void MoveDisplays() {
        hpDisplay.transform.position = gameObject.transform.position - new Vector3(-0.25f, 0.3f, 2);
        atkDisplay.transform.position = gameObject.transform.position - new Vector3(0.25f, 0.3f, 2);
    }

    public void RefreshDisplay() {
        hpDisplay.text = hp.ToString();
        atkDisplay.text = attack.ToString();
    }

    public virtual void Attack(GameObject target) {
        if (target.GetComponent<Piece>().hp <= 0) {
            target.GetComponent<Piece>().Kill();
        }
        target.GetComponent<Piece>().RefreshDisplay();
        MovePiece.EndTurn();
    }

    public virtual void DestroyDescription()
    {
        Destroy(GameObject.FindGameObjectWithTag("Display"));
        GameObject.FindGameObjectWithTag("DescriptionBG").GetComponent<SpriteRenderer>().enabled = false;
    }

    public virtual void CreateDescription()
    {
        display = Instantiate(gameObject, new Vector3(displayPosX, displayPosY, 0), Quaternion.identity);
        display.GetComponent<Piece>().WriteDescription();
        display.tag = "Display";
        GameObject.FindGameObjectWithTag("DescriptionBG").GetComponent<SpriteRenderer>().enabled = true;
    }

    public virtual void WriteDescription() {
       
    }
}

