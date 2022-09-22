using MoonmanaTestTask.Config;
using MoonmanaTestTask.Ingredients;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace MoonmanaTestTask.Levels
{
	public class Level : INamedConfig
	{
		[JsonProperty]
		private Color _targetColor;

		[JsonProperty]
		private float _margin;

		[JsonProperty]
		private List<string> _ingredientsNames;

		private List<IngredientSpawner> _ingredients = new List<IngredientSpawner>();
		private string name;

		public string Name { get => name; set => name = value; }
		public Color TargetColor { get => _targetColor; set => _targetColor = value; }
		public float Margin { get => _margin; set => _margin = value; }
		public List<IngredientSpawner> Ingredients => _ingredients;

		public void Setup(IngredientsDatabase database)
		{
			foreach (string name in _ingredientsNames)
			{
				Ingredients.Add(database.GetIngredientByName(name));
			}
		}
	}
}