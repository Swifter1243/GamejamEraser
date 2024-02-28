using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStatics : MonoBehaviour
{
	static private AudioStatics instance;
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
	private AmbientStream ambience;

	private void Start()
	{
		instance = this;

        AudioSettings.GetDSPBufferSize(out int bufferLength, out int numBuffers);
        dspBufferLength = (float)bufferLength / AudioSettings.outputSampleRate * 2;
        dspEpsilon = 1.0d / AudioSettings.outputSampleRate;
		
		//ambience.AmbientStart();
    }




}
