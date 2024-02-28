using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class AudioStreamer : MonoBehaviour
{
	[SerializeField]
	AudioSource[] sources;
	AudioSource lastSource;
	int sourceIndex = 0; 

	[SerializeField]
	List<AudioClip> clips;
	AudioClip lastClip;

	double dspEpsilon;
	const float DSP_PRE = 0.1f;

	public bool Play
	{
		set
		{
            isStopping = !value;
            if (value)
			{
				if (!isPlaying) { StartCoroutine(PlayNext()); }
			}
		}
	}

	bool isStopping = true;
	bool isPlaying = false;

	void Start()
	{
		sources = GetComponentsInChildren<AudioSource>();

		dspEpsilon = 1.0d/AudioSettings.outputSampleRate;

		Play= true;
		
	}


	IEnumerator PlayNext()
	{
		double nextClipIn = 0;
		do {
			//Get an audio clip
            AudioClip nextClip = clips[Random.Range(0, clips.Count)];
            while (lastClip == nextClip) nextClip = clips[Random.Range(0, clips.Count)];

            //Get the next source
            sourceIndex = (sourceIndex + 1) % sources.Length;
            AudioSource nextSource = sources[sourceIndex];
            nextSource.clip = nextClip;

			if (lastSource == null) nextSource.Play();
			else
			{
                nextClipIn = (lastSource != null) ? lastSource.time - lastClip.length : 0;
                nextSource.PlayScheduled(nextClipIn + dspEpsilon);
            }

            lastSource = nextSource;
            lastClip = nextClip;

            yield return new WaitForSecondsRealtime((float)nextClipIn + nextSource.time - DSP_PRE);
        } while (!isStopping);

		yield return new WaitForSecondsRealtime(DSP_PRE);

		isPlaying = false;
		yield return null;
	}
	
}
