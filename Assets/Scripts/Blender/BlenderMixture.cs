using UnityEngine;

namespace MoonmanaTestTask.Blender
{
	public class BlenderMixture : MonoBehaviour
	{
		private const float COLOR_TRANSITION_STEP = 0.003f;
		private readonly Color INITIAL_COLOR = Color.white;

		[SerializeField] private MeshRenderer _mixtureRenderer;

		private Color _currentMixtureVisualsColor;
		private Color _currentMixtureColor;

		public void Setup()
		{
			_currentMixtureColor = INITIAL_COLOR;
			_currentMixtureColor = INITIAL_COLOR;
		}

		private void Awake()
		{
			_currentMixtureVisualsColor = _mixtureRenderer.material.color;
		}

		private void Update()
		{
			CheckMixtureVisuals();
		}

		private void CheckMixtureVisuals()
		{
			if (_currentMixtureVisualsColor != _currentMixtureColor)
			{
				Vector3 currentColorChannels = new Vector3(_currentMixtureColor.r, _currentMixtureColor.g, _currentMixtureColor.b);
				Vector3 visualsColorChannels = new Vector3(_currentMixtureVisualsColor.r, _currentMixtureVisualsColor.g, _currentMixtureVisualsColor.b);
				Vector3 newColor = Vector3.MoveTowards(visualsColorChannels, currentColorChannels, COLOR_TRANSITION_STEP);
				_currentMixtureVisualsColor = new Color(newColor.x, newColor.y, newColor.z);
				_mixtureRenderer.material.color = _currentMixtureVisualsColor;
			}
		}

		public void Fill(Color mixtureColor)
		{
			_currentMixtureColor = mixtureColor;
		}
	}
}