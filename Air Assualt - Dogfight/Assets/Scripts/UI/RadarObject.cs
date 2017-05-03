using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AirAssault;

namespace AirAssault
{

	public class RadarObject : MonoBehaviour
	{
		public Image image;
		public Sprite spriteRadarDetection;
		public Sprite spriteLock;
		public Sprite spriteCurrent;
		public Target target;
		public Text lblPlayerName;
		public Text lblPlayerDistance;
		public float blinkInterval = .5f;
		public bool blink = false;
		public bool focused = false;
		public bool locked = false;

		public Color normalColor;
		public Color lockedColor;
		public Color focusedColor;
		public Color currentColor;

		public 

    // Use this for initialization
    void Awake ()
		{
		}

		void Start ()
		{
			image.sprite = spriteRadarDetection;
			spriteCurrent = image.sprite;

			currentColor = normalColor;
			image.color = currentColor;

			if (target)
			{
				lblPlayerName.text = target.targetObject.name;
				lblPlayerDistance.text = "" + target.distance;
			}
		}

		void Update ()
		{
			ColorManager ();

			image.sprite = spriteCurrent;
			image.color = currentColor;

			if (target)
			{
				float kmeter = target.distance / 1000;

				if (kmeter < 1)
				{
					lblPlayerDistance.text = "" + (int)target.distance + "m";
				}
				else
				{
					lblPlayerDistance.text = "" + (int)kmeter + "Km";
				}
			}
		}

		IEnumerator Blink ()
		{
			while (blink)
			{
				if (spriteCurrent == spriteRadarDetection)
				{
					spriteCurrent = spriteLock;
					currentColor = lockedColor;
				}
				else
				{
					spriteCurrent = spriteRadarDetection;
					currentColor = normalColor;
				}
		
				yield return new WaitForSeconds (blinkInterval);
			}
		}

		public void BlinkOn ()
		{
			blink = true;
			StartCoroutine (Blink ());
		}

		public void BlinkOff ()
		{
			blink = false;
		}

		public void Lock ()
		{
			blink = false;
			locked = true;
			spriteCurrent = spriteLock;
		}

		public void Reset ()
		{
			blink = false;
			locked = false;
			focused = false;
			spriteCurrent = spriteRadarDetection;
		}

		public void Focus ()
		{
			focused = true;
		}

		void ColorManager ()
		{
			if (!blink)
			{
				if (locked)
				{
					currentColor = lockedColor;
				}
				else if (focused)
				{
					currentColor = focusedColor;
				}
				else
				{
					currentColor = normalColor;
				}
			}
		}
	}
}