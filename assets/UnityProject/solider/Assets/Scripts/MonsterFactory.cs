using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class MonsterFactory {

	private static MonsterFactory instance { get; set; }
	public static MonsterFactory Instance { get { return instance ?? (instance = new MonsterFactory()); } }
	
	public GameObject monster;

	private List<GameObject> used = new List<GameObject>();
	private List<GameObject> free = new List<GameObject>();

	public MonsterFactory() {
		monster = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Skeleton@Skin"), Vector3.zero, Quaternion.identity);
		monster.SetActive (false);
	}

	public GameObject GetMonster()
	{
		GameObject newMonster = null;
		if (free.Count > 0)
		{
			newMonster = free [0];
			free.Remove(free[0]);
		}  
		else  
		{
			newMonster = GameObject.Instantiate<GameObject> (monster);
		}



		used.Add(newMonster);  
		newMonster.SetActive(true);  
		return newMonster;  
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