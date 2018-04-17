using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public class DiskFactory {

	private static DiskFactory _instance { get; set; }
	public static DiskFactory instance { get { if (_instance == null)  _instance = new DiskFactory(); return _instance; } }
	
	public GameObject disk;

	private List<GameObject> used = new List<GameObject>();
	private List<GameObject> free = new List<GameObject>();

	public GameObject getDisk(int round)
	{
		GameObject newDisk = null;
		if (free.Count > 0)
		{
			newDisk = free [0];
			free.Remove(free[0]);
		}  
		else  
		{
			newDisk = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/disk"), Vector3.zero, Quaternion.identity);
		}

		newDisk.transform.position = new Vector3(Random.Range(-10f, 10f),5,0);

		switch (round)  
		{
			case 1:  
			case 2:
				newDisk.transform.localScale = new Vector3 (3,0.5f,3);
				newDisk.GetComponent<Renderer>().material.color = Color.white;  
				break;  
			case 3:
			case 4:
				newDisk.transform.localScale = new Vector3 (2,0.4f,2);
				newDisk.GetComponent<Renderer>().material.color = Color.blue;  
				break;  
			case 5:
			case 6:
				newDisk.transform.localScale = new Vector3 (1,0.3f,1);
				newDisk.GetComponent<Renderer>().material.color = Color.black;  
				break;  
		}  

		used.Add(newDisk);  
		newDisk.SetActive(true);  
		return newDisk;  
	}  

	public void freeDisk(GameObject disk)  
	{  
		foreach (var gb in used)  
		{  
			if (disk.GetInstanceID() == gb.GetInstanceID())  
			{  
				gb.SetActive (false);
				free.Add (gb);
				used.Remove (gb);
				break;
			}  
		}
	}  

}  