using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class FirstController : MonoBehaviour, ISceneController, IUserAction {

	public SSActionManager actionManager { get; set; }

	public GameObject ship;
	public int good_on_left, bad_on_left, side;
	public GameObject left, right;
	public Stack<Vector3> left_shore = new Stack<Vector3>();
	public Stack<Vector3> right_shore = new Stack<Vector3>();

	void Awake() {
		SSDirector director = SSDirector.Instance;
		director.currentSceneController = this;
		director.currentSceneController.LoadResources ();
	}

	public void LoadResources() {
		Instantiate(Resources.Load("Prefabs/Shore"), new Vector3(5,0,0), Quaternion.identity);
		Instantiate(Resources.Load("Prefabs/Shore"), new Vector3(-5,0,0), Quaternion.identity);
		ship = Instantiate(Resources.Load("Prefabs/Ship"), new Vector3(1,0,0), Quaternion.identity) as GameObject;
		for (int i = 0; i < 3; ++i)
		{
			left_shore.Push (new Vector3(-3-i,1,0));
			left_shore.Push (new Vector3(-6-i,1,0));
			Instantiate(Resources.Load("Prefabs/Good"), new Vector3(3+i,1,0), Quaternion.identity);
			Instantiate(Resources.Load("Prefabs/Bad"), new Vector3(6+i,1,0), Quaternion.identity);
		}
		good_on_left = 0;
		left = null;
		bad_on_left = 0;
		right = null;
		side = 1;
	}

	public void Restart() {
		EditorSceneManager.LoadScene ("Main");
	}

	/*public void Clicked(GameObject obj) {
		if (obj.name.Equals ("Ship(Clone)")) {
			ShipMove ();
		} else if (obj.name.Equals ("Good(Clone)") || obj.name.Equals ("Bad(Clone)")) {
			if (obj.transform.position.x > 1.5 || obj.transform.position.x < -1.5)
				GetOnShip (obj);
			else
				GetOffShip (obj);
		} else
			return;
	}*/

	public int Check() {
		if (good_on_left == 3 && bad_on_left == 3) {
			return 1;
		}
		int _good_on_left = good_on_left, _bad_on_left = bad_on_left;
		if (side == -1) {
			if (left != null) {
				if (left.name.Equals ("Good(Clone)"))
					_good_on_left++;
				else
					_bad_on_left++;
			}
			if (right != null) {
				if (right.name.Equals ("Good(Clone)"))
					_good_on_left++;
				else
					_bad_on_left++;
			}
		}
		if (_good_on_left < _bad_on_left && _good_on_left != 0 && _bad_on_left != 0)
			return -1;
		int good_on_right = 3 - _good_on_left, bad_on_right = 3 - _bad_on_left;
		if (good_on_right < bad_on_right && good_on_right != 0 && bad_on_right != 0)
			return -1;
		return 0;
	}

	void GetOnShip(GameObject obj) {
		if (left != null && right != null)
			return;
		if (side == -1) {
			if (obj.transform.position.x > 0)
				return;
			left_shore.Push (obj.transform.position);
			if (obj.name.Equals ("Good(Clone)")) {
				good_on_left--;
			} else {
				bad_on_left--;
			}
		} else {
			if (obj.transform.position.x < 0)
				return;
			right_shore.Push (obj.transform.position);
		}
		Vector3 p = Vector3.zero;
		if (left == null) {
			p = Vector3.left;
			left = obj;
		} else if (right == null) {
			p = Vector3.right;
			right = obj;
		}
		obj.transform.position = ship.transform.position + Vector3.up + p*0.5f;

	}

	void GetOffShip(GameObject obj) {
		if (side == 1)
			obj.transform.position = right_shore.Pop();
		else {
			obj.transform.position = left_shore.Pop();
			if (obj.name.Equals ("Good(Clone)"))
				good_on_left++;
			else
				bad_on_left++;
		}
		if (obj == left)
			left = null;
		else
			right = null;
	}

	void ShipMove() {
		if (left == null && right == null)
			return;
		Vector3 p;
		if (side == 1) {
			p = Vector3.left * 2;
			side = -1;
		} else {
			p = Vector3.right * 2;
			side = 1;
		}
		ship.transform.position += p;
		if (left != null)
			left.transform.position += p;
		if (right != null)
			right.transform.position += p;
		
	}

	public void GameOver() {
		
	}
}
