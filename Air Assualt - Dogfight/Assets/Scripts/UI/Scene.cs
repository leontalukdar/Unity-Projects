using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using AirAssault;

public class Scene : MonoBehaviour
{
	public RectTransform lblRect;
	public RectTransform lblInitial;
	public RectTransform lblFinal;
	public float lblDelta = .2f;

	public RectTransform[] buttons;
	public float btnInitial = 150f;
	public float btnFinal = 250f;
	public float btnDelta = .2f;


	public bool lblAnimationCompleted = false;
	public bool btnAnimationCompleted = false;


	// Use this for initialization
	void Start ()
	{
		lblRect.anchoredPosition = lblInitial.anchoredPosition;

		for (int i = 0; i < buttons.Length; i++)
		{
			buttons [i].anchoredPosition = new Vector2 (btnInitial, buttons [i].anchoredPosition.y);
		}
	}

	void Update ()
	{
		if (!lblAnimationCompleted)
		{
			AnimationLabel ();
		}

		if (!btnAnimationCompleted)
		{
			AnimationButton ();
		}
	}

	void AnimationLabel ()
	{
		if (Mathf.Abs (Vector2.Distance (lblRect.anchoredPosition, lblFinal.anchoredPosition)) > lblDelta)
		{
			lblRect.anchoredPosition = Vector2.Lerp (lblRect.anchoredPosition, lblFinal.anchoredPosition, lblDelta);
		}
		else
		{
			lblAnimationCompleted = true;
		}
	}

	void AnimationButton ()
	{
		bool modified = false;
		float tDelta = btnDelta;

		for (int i = 0; i < buttons.Length; i++)
		{
			if (Mathf.Abs (buttons [i].anchoredPosition.x - btnFinal) > btnDelta)
			{
				modified = true;
				Vector2 temp = new Vector2 (btnFinal, buttons [i].anchoredPosition.y);
				buttons [i].anchoredPosition = Vector2.Lerp (buttons [i].anchoredPosition, temp, tDelta);
				tDelta /= 2f;
			}
		}

		if (!modified)
		{
			btnAnimationCompleted = true;
		}
	}

	public void OnClickPlay ()
	{
		SceneManager.LoadScene (1);
	}

	public void OnClickOptions ()
	{
		
	}

	public void OnClickCredit ()
	{
		
	}

	public void OnClickExit ()
	{
		Application.Quit ();
	}
}
