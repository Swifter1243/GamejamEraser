using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridButton : MonoBehaviour
{
	Color onColor = Color.white;
	Color offColor = Color.red;

	bool flipped = true;

	private void OnMouseEnter()
	{
		if (Input.GetMouseButton(0))
		{
			Toggle();
		}
	}

	private void OnMouseDown()
	{
		Toggle();
	}

	public void Toggle()
	{
		flipped = !flipped;
		GetComponent<SpriteRenderer>().color = flipped ? onColor : offColor;
	}
}
