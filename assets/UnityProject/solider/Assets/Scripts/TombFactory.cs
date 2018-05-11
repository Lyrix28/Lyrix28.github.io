using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class TombFactory {

	private static TombFactory instance { get; set; }
	public static TombFactory Instance { get { return instance ?? (instance = new TombFactory()); } }

	public GameObject tomb;

	private List<GameObject> used = new List<GameObject>();
	private List<GameObject> free = new List<GameObject>();

	public TombFactory() {
		tomb = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/stone_tomb"), Vector3.zero, Quaternion.identity);
		tomb.SetActive (false);
	}

	public GameObject GetTomb()
	{
		GameObject newTomb = null;
		if (free.Count > 0)
		{
			newTomb = free [0];
			free.Remove(free[0]);
		}  
		else  
		{
			newTomb = GameObject.Instantiate<GameObject> (tomb);
		}



		used.Add(newTomb);  
		newTomb.SetActive(true);  
		return newTomb;  
	}  

	public void FreeMonster(GameObject monster)  
	{  
		foreach (var gb in used)  
		{  
			if (monster.GetInstanceID() == gb.GetInstanceID())  
			{  
				gb.SetActive (false);
				free.Add (gb);
				used.Remove (gb);
				break;
			}  
		}
	}  

}  
