using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GridButton : MonoBehaviour
{
	Color onColor = Color.green;
	Color offColor = Color.gray;
	public GridSpawner spawner;

	public bool flipped;

	private void Start()
	{
		UpdateFlip(Random.Range(0, 2) == 0);
	}

	private void OnMouseEnter()
	{
		if (Input.GetMouseButton(0))
		{
			DoAction();
		}
	}

	private void OnMouseDown()
	{
		DoAction();
	}

	void UpdateFlip(bool flipped)
	{
		this.flipped = flipped;
		GetComponent<SpriteRenderer>().color = flipped ? offColor : onColor;
	}

	public void DoAction()
	{
		UpdateFlip(true);
		spawner.CheckDone();
	}
}
