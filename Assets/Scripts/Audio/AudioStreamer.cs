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


	private float TimeUntilStop
	{
		get
		{
            return sources.Max<AudioSource, float>(
				(src) => { return src != null && src.isPlaying ? (src.clip.length-src.time) : 0; }
            );
        }
	}



	bool isStopping = true;
	bool isPlaying = false;

	void Start()
	{
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
		//Wait until right before over

		float nextClipIn = TimeUntilStop;

        yield return new WaitForSecondsRealtime(nextClipIn - DSP_PRE);

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
            nextSource.PlayScheduled(nextClipIn + dspEpsilon);

            //Wait until right before next clip
            yield return new WaitForSecondsRealtime(nextClipIn - DSP_PRE);

        }


        yield return new WaitForSecondsRealtime(DSP_PRE);

		isPlaying = false;
		yield return null;
	}
	
}
