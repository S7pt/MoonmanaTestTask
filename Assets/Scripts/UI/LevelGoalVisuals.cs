using UnityEngine;
using UnityEngine.UI;

namespace MoonmanaTestTask.UI
{
	public class LevelGoalVisuals : MonoBehaviour
	{
		[SerializeField] private RawImage _cupFillingImage;

		public void Setup(Color targetColor)
		{
			_cupFillingImage.color = targetColor;
		}
	}
}