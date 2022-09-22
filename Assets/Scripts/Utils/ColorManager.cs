using MoonmanaTestTask.Ingredients;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoonmanaTestTask.Utils
{
	public class ColorManager : MonoBehaviour
	{
		private const float COLORS_DIVIDER = 0.03f;
		private readonly Color INITIAL_COLOR = Color.black;

		private List<Color> _currentIngredientColors;
		private Color _targetColor;
		private Color _currentColor;

		public event Action IngredientsMixed;

		public void Setup(Color targetColor)
		{
			_targetColor = targetColor;
			_currentIngredientColors = new List<Color>();
			_currentColor = INITIAL_COLOR;
		}

		public void OnIngredientAdded(Ingredient ingredient)
		{
			_currentIngredientColors.Add(ingredient.IngredientColor);
		}

		public void MixIngredients()
		{
			if (_currentIngredientColors.Count == 0)
			{
				return;
			}
			Vector3 channelsSum = new Vector3();
			float ingredientsCount = _currentIngredientColors.Count;
			foreach (Color color in _currentIngredientColors)
			{
				Color ingredientColor = color;
				channelsSum += new Vector3(ingredientColor.r, ingredientColor.g, ingredientColor.b);
			}
			channelsSum /= ingredientsCount;
			_currentColor = new Color(channelsSum.x, channelsSum.y, channelsSum.z);
			IngredientsMixed?.Invoke();
		}

		public float GetSimilarityPercent()
		{
			float redChannelSimilarity = Mathf.Min(_targetColor.r, _currentColor.r) / Mathf.Max(_targetColor.r, _currentColor.r);
			float greenChannelSimilarity = Mathf.Min(_targetColor.g, _currentColor.g) / Mathf.Max(_targetColor.g, _currentColor.g);
			float blueChannelSimilarity = Mathf.Min(_targetColor.b, _currentColor.b) / Mathf.Max(_targetColor.b, _currentColor.b);
			float similarity = (redChannelSimilarity + blueChannelSimilarity + greenChannelSimilarity) / COLORS_DIVIDER;
			return similarity;
		}

		public Color GetMixedColor()
		{
			MixIngredients();
			return _currentColor;
		}
	}
}