using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Upgrades;

public class UpgradeNotifier : MonoBehaviour
{
	public GameObject availableUp;
	public GameObject availableDown;
	public Camera cam;
	public RectTransform mask;
	public GridLayoutGroup upgradeContent;

	private void Start()
	{
		StartCoroutine(UpdateLoop());
	}

	IEnumerator UpdateLoop()
	{
		while (true)
		{
			UpdateVisuals();
			yield return new WaitForSeconds(0.4f);
		}
	}

	bool TestPoint(Vector3 point)
	{
		point = cam.WorldToScreenPoint(point);
		return RectTransformUtility.RectangleContainsScreenPoint(mask, point, cam);
	}

	public void UpdateVisuals()
	{
		Vector3[] maskCorners = new Vector3[4];
		mask.GetWorldCorners(maskCorners);

		bool offScreenUp = false;
		bool offScreenDown = false;

		foreach (var upgrade in upgradeContent.GetComponentsInChildren<UpgradeUI>())
        {
			if (!upgrade.GetAvailable()) continue;

			Vector3[] corners = new Vector3[4];
			upgrade.rt.GetWorldCorners(corners);

			bool outside = true;

            foreach (var corner in corners)
            {
                if (TestPoint(corner))
				{
					outside = false;
					break;
				}
            }

			if (outside)
			{
				bool up = corners[0].y > maskCorners[0].y;

				if (up) offScreenUp = true;
				else offScreenDown = true;
			}
		}

		availableUp.SetActive(offScreenUp);
		availableDown.SetActive(offScreenDown);
    }
}
