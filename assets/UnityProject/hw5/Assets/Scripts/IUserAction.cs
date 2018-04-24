using System;
using UnityEngine;

public interface IUserAction
{
	void GameOver();
	void Restart ();
	void Stop ();
	void Continue ();
}