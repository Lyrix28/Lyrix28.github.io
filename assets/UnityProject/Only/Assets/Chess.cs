using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chess : MonoBehaviour {

	int turn;
	int [,] record = new int[3,3];

	void Clear() {
		turn = 1;
		for (int i = 0; i < 3; i++) {  
			for (int j = 0; j < 3; j++) {  
				record[i,j] = 0;  
			}  
		}
	}

	int Check() {
		
		if (turn < 3)
			return 0;
		
		int state;
		if (record[0,0] != 0) {
			state = record [1,1];
			if ((record [0,0] == state && record [2,2] == state) || (record [0,2] == state && record [2,0] == state))
				return state;
			if ((record [0,1] == state && record [2,1] == state) || (record [1,0] == state && record [1,2] == state))
				return state;
		}

		state = record [0,0];
		if ((record [0,1] == state && record [0,2] == state) || (record [1,0] == state && record [2,0] == state))
			return state;
		
		state = record [2,2];
		if ((record [2,0] == state && record [2,1] == state) || (record [1,2] == state && record [0,2] == state))
			return state;
		
		return 0;
	}

	// Use this for initialization
	void Start () {
		Clear ();
	}

	void OnGUI () {
		if(GUI.Button(new Rect(400,50,150,50),"RESTART"))
			Clear();

		int state = Check ();

		if (turn > 9 && state == 0)
			GUI.Label (new Rect (400, 105, 50, 50), "Tied!");
		else if (state == 1) {
			GUI.Label (new Rect (400, 105, 50, 50), "O Win!");
			state = 1;
		} else if (state == 2) {
			GUI.Label (new Rect (400, 105, 50, 50), "X Win!");
			state = 2;
		}
		
		for(int i = 0 ; i < 3 ; i++)
			for (int j = 0; j < 3; j++) {
				
				if (record[i,j] == 1)
					GUI.Button (new Rect (400 + 50 * i, 130 + 50 * j, 50, 50), "O");
				else if (record[i,j] == 2)
					GUI.Button (new Rect (400 + 50 * i, 130 + 50 * j, 50, 50), "X");
				else if(GUI.Button(new Rect(400+50*i,130+50*j,50,50),""))
					if (state == 0) {
						if (turn % 2 == 0)
							record [i, j] = 1;
						else
							record [i, j] = 2;
						turn ++;
					}
			}
	}

}