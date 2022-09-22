using System;
using UnityEngine;

namespace MoonmanaTestTask.Ingredients
{
	public class Ingredient : MonoBehaviour
	{
		private const float DESTRUCTION_TIME = 1f;

		[SerializeField] private Color _ingredientColor;

		public event Action<Ingredient> IngredientDestroyed;

		public Color IngredientColor => _ingredientColor;

		private void OnTriggerExit(Collider other)
		{
			if (other.CompareTag("Blender"))
			{
				Destroy(gameObject);
			}
		}

		private void OnDestroy()
		{
			IngredientDestroyed(this);
		}
	}
}