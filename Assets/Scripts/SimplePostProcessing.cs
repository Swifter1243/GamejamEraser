using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class SimplePostProcessing : MonoBehaviour
{
	public Material _postProcessingMaterial;

	private void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		if (_postProcessingMaterial != null)
		{
			Graphics.Blit(src, dst, _postProcessingMaterial);
		}
		else
		{
			Graphics.Blit(src, dst);
		}
	}
}