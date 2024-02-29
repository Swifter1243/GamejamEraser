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
		//Scene scene = SceneManager.GetSceneByName(sceneName);
		//List<EventTrigger> triggers = new List<EventTrigger>();
		//
		//foreach(GameObject obj in scene.GetRootGameObjects())
		//{
		//	//This is stupid but whatever
		//	List<EventTrigger> tempTriggers = new List<EventTrigger>();
		//	obj.GetComponentsInChildren(tempTriggers);
		//	triggers.AddRange(tempTriggers);
		//}
		//
		//foreach(EventTrigger trigger in triggers)
		//{
		//
		//	EventTrigger.Entry evtTrig;
		//	evtTrig = new();
		//	evtTrig.eventID = EventTriggerType.PointerEnter;
        //    OnPointerEnter()
		//
		//
        //    evtTrig = new();
		//	evtTrig.eventID = EventTriggerType.PointerExit;
		//	evtTrig = new();
		//	evtTrig.eventID = EventTriggerType.PointerUp;
		//	evtTrig = new();
		//	evtTrig.eventID = EventTriggerType.PointerDown;
		//
		//
        //    trigger.triggers.Add()
		//
		//
		//
		//	trigger.callba.add	+= OnPointerEnter();
		//	trigger.OnPointerExit	+= OnPointerExit();
		//	trigger.OnPointerDown	+= OnPointerDown();
		//	trigger.OnPointerUp		+=
		//}
	}



    private void OnPointerEnter(PointerEventData evt)
    {

    }
    private void OnPointerExit(PointerEventData evt)
    {

    }
    private void OnPointerDown(PointerEventData evt)
    {

    }
    private void OnPointerUp(PointerEventData evt)
    {

    }




}
