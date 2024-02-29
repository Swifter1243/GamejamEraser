using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioStatics : MonoBehaviour
{
	static public AudioStatics instance;
	static public double dspPre; 
	static public double dspBufferLength; 
	static public double dspEpsilon; 

	[SerializeField]
	private List<AudioClip> uiMouseUp;
	[SerializeField]
	private List<AudioClip> uiMouseDown;
	[SerializeField]
	private List<AudioClip> uiMouseHover;
	[SerializeField]
	private List<AudioClip> uiMouseClick;

	[SerializeField]
	public AmbientStream ambience;

	private void Awake()
	{
		instance = this;

		AudioSettings.GetDSPBufferSize(out int bufferLength, out int numBuffers);
		dspPre = 0.1f;
		dspBufferLength = (float)bufferLength / AudioSettings.outputSampleRate * 2;
		dspEpsilon = 1.0d / AudioSettings.outputSampleRate;
	}
	private void Start()
	{
		ambience.AmbientStart();
	}



	public void AddCallbacks(AsyncOperation asyncScene, string sceneName)
	{
		Scene scene = SceneManager.GetSceneByName(sceneName);
		List<PointerEvent> triggers = new List<PointerEvent>();
		
		foreach(GameObject obj in scene.GetRootGameObjects())
		{
			//This is stupid but whatever
			List<PointerEvent> tempTriggers = new List<PointerEvent>();
			obj.GetComponentsInChildren(tempTriggers);
			triggers.AddRange(tempTriggers);
		}
		
		foreach(PointerEvent trigger in triggers)
		{
			trigger.PointerEnterEvent	+= OnPointerEnter;
			trigger.PointerExitEvent	+= OnPointerExit;
			trigger.PointerUpEvent		+= OnPointerUp;
			trigger.PointerDownEvent	+= OnPointerDown;
		}
	}



    private void OnPointerEnter(object sender, EventArgs evt)
    {
		AudioSource.PlayClipAtPoint(
			uiMouseHover[UnityEngine.Random.Range(0, uiMouseHover.Count)],
            Vector3.zero
            );
    }
    private void OnPointerExit(object sender, EventArgs evt)
    {
        AudioSource.PlayClipAtPoint(
            uiMouseHover[UnityEngine.Random.Range(0, uiMouseHover.Count)],
            Vector3.zero
            );
    }
    private void OnPointerDown(object sender, EventArgs evt)
    {
        AudioSource.PlayClipAtPoint(
            uiMouseDown[UnityEngine.Random.Range(0, uiMouseDown.Count)],
            Vector3.zero
            );
    }
    private void OnPointerUp(object sender, EventArgs evt)
    {
        AudioSource.PlayClipAtPoint(
            uiMouseUp[UnityEngine.Random.Range(0, uiMouseUp.Count)],
            Vector3.zero
            );
    }




}
