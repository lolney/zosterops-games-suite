﻿using UnityEngine;
using System.Collections;

public class DestroyCage : MonoBehaviour {
	
	void Start() {}
	void Update() {}
	
	void OnTriggerEnter2D(Collider2D other){
	
		GameObject scripts = GameObject.Find("Scripts");
		MazeGeneration mg = scripts.GetComponent<MazeGeneration>();
		Node end;
				
		string name = other.gameObject.name;
		if(name.Equals("Main Character") || name.StartsWith("Note")) {
			Debug.Log("Main Char Entered");
			end = mg.map[MazeGeneration.gridSize - 2, MazeGeneration.gridSize - 2];
			if(destroy(end)){
				UpdateText.playerCages++;
				UpdateText.cages--;
			}
		}
		else if(name.Equals("Cat")) {
			Debug.Log("Cat Entered");
			end = mg.map[0, MazeGeneration.gridSize - 2];
			if(destroy(end)){
				UpdateText.catCages++;
				UpdateText.cages--;
			}
		}
			
	}
	
	
	private bool destroy(Node end) {
		
		Transform WeeWee = transform.FindChild("Wee Wee NPC(Clone)");
		if(WeeWee != null){
			
			WeeWeeAI ai = WeeWee.gameObject.GetComponent<WeeWeeAI>();
			
			Node start = PathFinding.findClosestNode(WeeWee.transform.position);
			start.cage = false;
			Debug.Log("Postion: " + WeeWee.transform.position);
			Debug.Log("Closest Node: " + start.center);
			
			ai.beginPathNav(start, end);
			
			for(int c=0; c<transform.childCount -1; c++) {
				Transform child = transform.GetChild(c);
				Rigidbody2D b = child.GetComponent<Rigidbody2D>();
				b.WakeUp();
				b.velocity = Random.insideUnitCircle * 50;
				b.angularVelocity = Random.Range(100, 600);
				
				child.gameObject.AddComponent<DestroyAfterSeconds>();
			}
			
			transform.DetachChildren();
			Destroy(gameObject); 
			return true;
		}
		else return false;
	}
}
	