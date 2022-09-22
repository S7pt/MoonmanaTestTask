using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MoonmanaTestTask.Ingredients
{
	public class IngredientSpawner : MonoBehaviour, IPointerClickHandler
	{
		private float INGREDIENT_SPAWN_DELAY = 0.5f;
		[SerializeField] private Ingredient _ingredient;

		private Transform _spawnPoint;
		private float _spawnCooldown;

		public event Action<Ingredient> IngredientSpawned;

		public void OnPointerClick(PointerEventData eventData)
		{
			if (_spawnCooldown > 0)
			{
				return;
			}
			Spawn();
			_spawnCooldown = INGREDIENT_SPAWN_DELAY;
		}

		private void Update()
		{
			if (_spawnCooldown > 0)
			{
				_spawnCooldown -= Time.deltaTime;
			}
		}

		public void Setup(Transform spawnPoint)
		{
			_spawnPoint = spawnPoint;
		}

		public void Spawn()
		{
			Ingredient spawnedIngredient = Instantiate(_ingredient, _spawnPoint.position, _spawnPoint.rotation);
			IngredientSpawned?.Invoke(spawnedIngredient);
		}
	}
}