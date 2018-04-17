using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSDirector : System.Object {

	private static SSDirector instance;

	public static SSDirector Instance{ get { return instance ?? (instance = new SSDirector()); } }

	public ISceneController currentSceneController{ get; set; }
}
