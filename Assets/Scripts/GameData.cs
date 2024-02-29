using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Upgrades;

public static class GameData
{
	private static ProgressFormat Progress => _progress ? _progress : _progress = Object.FindObjectOfType<ProgressFormat>();
	private static ProgressFormat _progress;

	private static UpgradeData[] AllUpgrades => _allUpgrades ??= Object
		.FindObjectsOfType<UpgradeUI>()
		.Select(ui => ui.Upgrade)
		.ToArray();
	private static UpgradeData[] _allUpgrades;

	public static string MaxFormatted = Helpers.FormatBytes(int.MaxValue);

	#if UNITY_EDITOR
	[MenuItem("Assets/Reset Game Lol")]
	#endif
	public static void ResetData()
	{
		PlayerPrefs.DeleteAll();
	}

	private static int TotalSpent => AllUpgrades.Sum(upgrade => upgrade.Spent);

	public static int BytesRemaining
	{
		get => PlayerPrefs.GetInt("bytes_remaining", int.MaxValue);
		private set => PlayerPrefs.SetInt("bytes_remaining", value);
	}

	public static int Currency => int.MaxValue - BytesRemaining - TotalSpent;

	public static int FreedBytes => int.MaxValue - BytesRemaining;

	public static void EraseBytes(int amount)
	{
		BytesRemaining -= amount;

		if (BytesRemaining <= 0)
		{
			Scene toUnload = SceneManager.GetSceneByName("Grid2");
            GameObject[] toDestroy = toUnload.GetRootGameObjects();

			AsyncOperation asyncOp = SceneManager.UnloadSceneAsync(toUnload);
			asyncOp.completed += (AsyncOperation asyncOp) => { foreach (GameObject obj in toDestroy) GameObject.Destroy(obj); };

			SceneManager.LoadScene("Credits", LoadSceneMode.Additive);
			AudioStatics.instance.ambience.AmbientEnd();
			
			return;

		}
	}
}