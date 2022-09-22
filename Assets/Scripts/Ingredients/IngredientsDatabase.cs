using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoonmanaTestTask.Ingredients
{
	[CreateAssetMenu(fileName = "IngredientsData", menuName = "ScriptableObjects/Ingredients Data", order = 1)]
	public class IngredientsDatabase : ScriptableObject
	{
		[SerializeField] private List<IngredientData> _data;

		public IngredientSpawner GetIngredientByName(string name)
		{
			return _data.Find((x) => x.Name == name).IngredientSpawner;
		}

		[Serializable]
		private struct IngredientData
		{
			public string Name;
			public IngredientSpawner IngredientSpawner;
		}
	}
}