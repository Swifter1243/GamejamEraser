using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;






public class AudioStreamer : MonoBehaviour
{
	[SerializeField]
	List<AudioSource> sources;
	AudioSource lastSource;
	int sourceIndex = 0; 

	[SerializeField]
	List<AudioClip> clips;
	AudioClip lastClip;

	double dspEpsilon;
	double dspBufferLength;

	public bool Play
	{
		set
		{
			if (value)
			{
				if (!isPlaying) { StartCoroutine(PlayNext()); }
			}
            isPlaying = value;
        }
	}


	private double TimeUntilStop
	{
		get
		{
            return sources.Max<AudioSource, double>(
				(src) => { return src != null && src.isPlaying ? ((double)src.clip.length-src.time) : 0; }
            );
        }
	}



	bool isStopping = true;
	bool isPlaying = false;

	void Start()
	{
		AudioSettings.GetDSPBufferSize(out int bufferLength, out int numBuffers);
		dspBufferLength = (float)bufferLength / AudioSettings.outputSampleRate * 2;

        dspEpsilon = 1.0d/AudioSettings.outputSampleRate;
		Play= true;
	}





	IEnumerator PlayNext()
	{
		/*
		double nextClipIn = DSP_PRE;
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
				nextClipIn = (lastSource.isPlaying) ? lastClip.length - lastSource.time : DSP_PRE;
				Debug.Log($"Playing {nextClip.name} in {nextClipIn} while {(nextSource.isPlaying ? "already playing" : "not already playing")}");
				nextSource.PlayScheduled(nextClipIn + dspEpsilon);
			}

			lastSource = nextSource;
			lastClip = nextClip;

			yield return new WaitForSecondsRealtime((float)nextClipIn + nextClip.length - DSP_PRE);
		} while (!isStopping);
		 */

		//Get largest of two clip times
		double nextClipIn = TimeUntilStop;

		//Wait until right before over
        yield return new WaitForSecondsRealtime((float)(nextClipIn - dspBufferLength));
		while (isPlaying)
		{
            //Get next source
            sourceIndex = (sourceIndex + 1) % sources.Count();
            AudioSource nextSource = sources[sourceIndex];

            //Get next clip
            AudioClip nextClip = clips[Random.Range(0, clips.Count)];
            while (lastClip == nextClip) nextClip = clips[Random.Range(0, clips.Count)];
            nextSource.clip = nextClip;

			//Schedule clip
			nextClipIn = TimeUntilStop;
            nextSource.PlayScheduled(AudioSettings.dspTime + nextClipIn + dspEpsilon);
			nextClipIn += nextClip.length;

			//Store as last clip
			lastClip = nextClip;
			lastSource = nextSource;

            //Wait until right before next clip
            yield return new  WaitForSecondsRealtime((float)(nextClipIn - dspBufferLength));
        }


		isPlaying = false;
		yield return null;
	}
	
}
