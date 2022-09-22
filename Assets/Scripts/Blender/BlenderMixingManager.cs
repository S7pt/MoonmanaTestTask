using MoonmanaTestTask.Ingredients;
using MoonmanaTestTask.UI;
using MoonmanaTestTask.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MoonmanaTestTask.Blender
{
	public class BlenderMixingManager : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField] private BlenderAnimationManager _animationManager;
		[SerializeField] private ColorManager _colorManager;
		[SerializeField] private UIManager _UIManager;

		private List<Ingredient> _ingredients;
		private Color _previousColor;

		public event Action StartedMixing;
		public event Action CompletedMixing;

		public void Setup()
		{
			_animationManager.Setup();
			_previousColor = Color.black;
		}

		private void Start()
		{
			_ingredients = new List<Ingredient>();
		}

		public void OnIngredientAdded(Ingredient ingredient)
		{
			_ingredients.Add(ingredient);
			ingredient.IngredientDestroyed += this.OnIngredientDestroyed;
		}

		private void OnIngredientDestroyed(Ingredient ingredient)
		{
			ingredient.IngredientDestroyed -= OnIngredientDestroyed;
			_ingredients.Remove(ingredient);
		}

		private void MixIngredients()
		{
			StartCoroutine(StartMixing());
		}

		private IEnumerator StartMixing()
		{
			StartedMixing?.Invoke();
			Color mixtureColor = _colorManager.GetMixedColor();
			if (mixtureColor == _previousColor)
			{
				CompletedMixing?.Invoke();
				yield break;
			}
			_previousColor = mixtureColor;
			yield return _animationManager.StartMixingAnimation(mixtureColor);
			DestroyAllIngredients();
			CompletedMixing?.Invoke();
		}

		private void DestroyAllIngredients()
		{
			foreach (Ingredient ingredient in _ingredients)
			{
				Destroy(ingredient?.gameObject);
			}
			_ingredients = new List<Ingredient>();
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			MixIngredients();
		}
	}
}