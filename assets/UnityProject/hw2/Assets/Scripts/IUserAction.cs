using System;
using UnityEngine;

public interface IUserAction
{
	void GameOver();
	void Clicked(GameObject obj);
	int Check ();
	void Restart ();
}