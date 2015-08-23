using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClickCounter : MonoBehaviour {

	private Text _label;
	private int _count;

	// Use this for initialization
	void Start () {
		_count = 0;
		_label = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void IncrementCount()
	{
		_count += 1;
		_label.text = _count.ToString();
	}
}
