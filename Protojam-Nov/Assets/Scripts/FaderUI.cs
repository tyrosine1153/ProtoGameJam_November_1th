using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FaderUI : MonoBehaviour
{

	#region FIELDS
	public Image fadeOutUIImage;
	//public AudioSource audiosource;
	public float fadeSpeed = 0.8f;

	public enum Fade
	{
		In, //Alpha = 1
		Out // Alpha = 0
	}
	#endregion

	#region MONOBHEAVIOR
	void OnEnable()
	{
		StartCoroutine(Fading(Fade.Out));
	}
	#endregion

	#region FADE
	private IEnumerator Fading(Fade fade)
	{
		float alpha = (fade == Fade.Out) ? 1 : 0;
		float fadeEndValue = (fade == Fade.Out) ? 0 : 1;
		if (fade == Fade.Out)
		{
			while (alpha >= fadeEndValue)
			{
				SetColorImage(ref alpha, fade);
				yield return null;
			}
			fadeOutUIImage.enabled = false;
		}
		else
		{
			fadeOutUIImage.enabled = true;
			while (alpha <= fadeEndValue)
			{
				SetColorImage(ref alpha, fade);
				yield return null;
			}
		}
	}
	#endregion

	#region HELPERS
	public void startFade(Fade direction, string sceneToLoad)
    {
		StartCoroutine(FadeAndLoadScene(direction, sceneToLoad));
	}
	
	public IEnumerator FadeAndLoadScene(Fade fade, string sceneToLoad)
	{
		yield return Fading(fade);
		SceneManager.LoadScene(sceneToLoad);
	}
	public IEnumerator FadeAndLoadScene(Fade fade)
    {
		yield return Fading(fade);
	}

	private void SetColorImage(ref float alpha, Fade fadeDirection)
	{
		fadeOutUIImage.color = new Color(fadeOutUIImage.color.r, fadeOutUIImage.color.g, fadeOutUIImage.color.b, alpha);
		alpha += Time.deltaTime * (1.0f / fadeSpeed) * ((fadeDirection == Fade.Out) ? -1 : 1);
	}
	#endregion
}
