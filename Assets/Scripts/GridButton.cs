using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GridButton : MonoBehaviour
{
	Color onColor = Color.green;
	Color offColor = Color.gray;
	public GridSpawner spawner;

	public bool isOff;

	private void Start()
	{
		UpdateFlip(Random.Range(0, 4) == 0);
	}

	private void OnMouseEnter()
	{
		if (Input.GetMouseButton(0))
		{
			TurnOff();
		}
	}

	private void OnMouseDown()
	{
		TurnOff();
	}

	void UpdateFlip(bool isOff)
	{
		this.isOff = isOff;
		GetComponent<SpriteRenderer>().color = isOff ? offColor : onColor;
	}

	public void TurnOff()
	{
		UpdateFlip(true);
		spawner.CheckDone();
	}
}
