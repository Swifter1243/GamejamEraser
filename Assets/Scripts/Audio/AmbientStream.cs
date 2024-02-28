using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AmbientStream : MonoBehaviour
{
	enum AmbientState
	{
		Off,
		Starting,
		Running,
		Ending
	}
	private AmbientState state = AmbientState.Off;

	[SerializeField]
	List<AudioSource> sources;
	int sourceIndex;
	[SerializeField]
	List<AudioClip> clips;
	[SerializeField]
	AudioClip startClip;
	[SerializeField]
	AudioClip endClip;

	private AudioSource lastSource;
	private AudioClip lastClip;

	public bool isPlayingDebug = false;
	private bool isPlaying = false;

	[SerializeField]
	[Range(0,1)]
	private float intensitySpread;
	[Range(0,1)]
	public float debugIntensity;
	private const float INTENSITY_SCALE = 3.0f / 20;
	private const float INTENSITY_LOGSCALE = 1.0f / 7;


    public void AmbientStart()
	{
		if (state == AmbientState.Off)
		{
			state = AmbientState.Starting;
			StartCoroutine(AmbientRun());
		}
	}
	public void AmbientEnd() => state = AmbientState.Ending;

    private void Update()
    {
        if (isPlaying != isPlayingDebug)
		{
			isPlaying = isPlayingDebug;
			if (isPlaying) AmbientStart();
			else AmbientEnd();
		}
    }

	



    private IEnumerator AmbientRun()
	{
		double nextClipIn = 0;

		while (state != AmbientState.Off)
		{
            //Get next source
            sourceIndex = (sourceIndex + 1) % sources.Count();
			AudioSource nextSource = sources[sourceIndex];

            //Get next clip
            AudioClip nextClip = null;

            switch (state)
			{
				case AmbientState.Starting:
					{
						nextClip = startClip;
						state = AmbientState.Running;
						break;
					}
				case AmbientState.Running:
					{
						nextClip = GetNextClip();
                        break;
					}
				case AmbientState.Ending:
					{
						nextClip = endClip;
						break;
					}
			}
			nextSource.clip = nextClip;

			//Schedule clip
			if (lastSource != null && lastSource.isPlaying)
			{
				nextClipIn = (double)lastClip.length - lastSource.time;
				nextSource.PlayScheduled(AudioSettings.dspTime + nextClipIn + AudioStatics.dspEpsilon);
			}
			else
			{
				nextClipIn = 0;
				nextSource.PlayScheduled(AudioSettings.dspTime); //Play immediately
			}

            nextClipIn += nextClip.length;

            //Store as last clip
            lastClip = nextClip;
            lastSource = nextSource;

			if(state== AmbientState.Ending) { state = AmbientState.Off; }

            //Wait until right before next clip
            yield return new WaitForSeconds((float)(nextClipIn - AudioStatics.dspPre));
		}

		yield return null;
	}



	AudioClip GetNextClip()
	{
		AudioClip nextClip;

		float intensity = Mathf.Clamp(Mathf.Log10(StatsFormat.deltaBytes) * INTENSITY_LOGSCALE, 0, 1);
		Debug.Log($"Playing next clip with intensity {intensity}");

		//Random distribution along a target intensity where intensity is proportional to the index
		do
		{
			float range = 1 - intensitySpread;
			float spread = Random.Range(0, intensitySpread);
			nextClip = clips[Mathf.FloorToInt((spread + intensity * range) * clips.Count())];
		} while (lastClip == nextClip);

		return nextClip;
    }
}
