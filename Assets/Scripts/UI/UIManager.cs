using MoonmanaTestTask.Blender;
using MoonmanaTestTask.Levels;
using MoonmanaTestTask.Utils;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MoonmanaTestTask.UI
{
	public class UIManager : MonoBehaviour
	{
		private const int INITIAL_SIMILARITY_PERCENT = 0;
		private const float LEVEL_COMPLETED_UI_DELAY = 2f;

		[SerializeField] private TMP_Text _similarityText;
		[SerializeField] private GameObject _levelCompletedPanel;
		[SerializeField] private GameObject _raycastBlocker;
		[SerializeField] private Button _nextLevelButton;
		[SerializeField] private BlenderMixingManager _blender;
		[SerializeField] private LevelFlowHandler _flowHandler;
		[SerializeField] private ColorManager _colorManager;

		public Button NextLevelButton => _nextLevelButton;

		private void Awake()
		{
			_blender.StartedMixing += OnStartedMixing;
			_blender.CompletedMixing += OnCompletedMixing;
			_flowHandler.LevelCompleted += OnLevelCompleted;
		}

		public void Setup()
		{
			_levelCompletedPanel.SetActive(false);
			SetSimilarityPercent(INITIAL_SIMILARITY_PERCENT);
			SetTapInput(true);
		}

		public void SetTapInput(bool isActive)
		{
			_raycastBlocker.SetActive(!isActive);
		}
		private void OnStartedMixing()
		{
			SetTapInput(false);
		}

		private void OnCompletedMixing()
		{
			SetTapInput(true);
			SetSimilarityPercent(_colorManager.GetSimilarityPercent());
		}

		private void SetSimilarityPercent(float similarity)
		{
			_similarityText.text = similarity.ToString("0.00") + "%";
		}

		private void OnLevelCompleted()
		{
			StartCoroutine(nameof(ShowLevelCompletedUI));
		}

		private IEnumerator ShowLevelCompletedUI()
		{
			SetTapInput(false);
			yield return new WaitForSeconds(LEVEL_COMPLETED_UI_DELAY);
			_levelCompletedPanel.SetActive(true);
			SetTapInput(true);
		}

		private void OnDestroy()
		{
			_blender.StartedMixing -= OnStartedMixing;
			_flowHandler.LevelCompleted -= OnLevelCompleted;
			_blender.CompletedMixing -= OnCompletedMixing;
		}
	}
}