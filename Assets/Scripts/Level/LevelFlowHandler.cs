using MoonmanaTestTask.Blender;
using MoonmanaTestTask.Utils;
using System;
using UnityEngine;

namespace MoonmanaTestTask.Levels
{
	public class LevelFlowHandler : MonoBehaviour
	{
		private const int FULL_SIMILARITY = 100;

		[SerializeField] private BlenderMixingManager _blender;
		[SerializeField] private ColorManager _colorManager;

		private float _similarityMargin;
		public event Action LevelCompleted;

		public void Setup(float margin)
		{
			_similarityMargin = margin;
		}

		private void Awake()
		{
			_blender.CompletedMixing += OnCompletedMixing;
		}

		private void OnCompletedMixing()
		{
			float currentSimilarity = _colorManager.GetSimilarityPercent();
			if (FULL_SIMILARITY - currentSimilarity <= _similarityMargin)
			{
				LevelCompleted?.Invoke();
			}
		}

		private void OnDestroy()
		{
			_colorManager.IngredientsMixed -= OnCompletedMixing;
		}
	}
}