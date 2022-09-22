using MoonmanaTestTask.Blender;
using MoonmanaTestTask.Config;
using MoonmanaTestTask.Ingredients;
using MoonmanaTestTask.UI;
using MoonmanaTestTask.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace MoonmanaTestTask.Levels
{
	public class LevelManager : MonoBehaviour
	{
		[SerializeField] private ConfigManager _configManager;
		[SerializeField] private IngredientsDatabase _database;
		[SerializeField] private ColorManager _colorManager;
		[SerializeField] private UIManager _UIManager;
		[SerializeField] private BlenderMixingManager _blender;
		[SerializeField] private LevelGoalVisuals _goalVisuals;
		[SerializeField] private LevelFlowHandler _handler;
		[SerializeField] private List<Transform> _spawnerSpawnPoints;
		[SerializeField] private Transform _ingredientSpawnPoint;

		private List<Level> _levels = new List<Level>();
		private List<IngredientSpawner> _activeSpawners;
		private int _currentLevelIndex = 0;

		private void Start()
		{
			_levels = _configManager.GetAllConfigs<Level>();
			_levels.ForEach((x) => x.Setup(_database));
			StartLevel(_currentLevelIndex);
		}

		private void StartLevel(int index)
		{
			Level currentLevel = _levels[index];
			SetupSpawners(currentLevel);
			_blender.Setup();
			_goalVisuals.Setup(currentLevel.TargetColor);
			_colorManager.Setup(currentLevel.TargetColor);
			_UIManager.Setup();
			_UIManager.NextLevelButton.onClick.AddListener(StartNextLevel);
			_handler.Setup(currentLevel.Margin);
		}

		private void StartNextLevel()
		{
			EndLevel(_currentLevelIndex);
			_currentLevelIndex = (_currentLevelIndex + 1) % _levels.Count;
			StartLevel(_currentLevelIndex);
		}

		private void EndLevel(int index)
		{
			Level levelToEnd = _levels[index];
			RemoveSpawners();
			_UIManager.NextLevelButton.onClick.RemoveListener(StartNextLevel);
		}

		private void RemoveSpawners()
		{
			foreach (IngredientSpawner spawner in _activeSpawners)
			{
				spawner.IngredientSpawned -= _colorManager.OnIngredientAdded;
				spawner.IngredientSpawned -= _blender.OnIngredientAdded;
				Destroy(spawner.gameObject);
			}
		}

		private void SetupSpawners(Level currentLevel)
		{
			_activeSpawners = new List<IngredientSpawner>();
			for (int i = 0; i < currentLevel.Ingredients.Count; i++)
			{
				IngredientSpawner currentSpawner = Instantiate(currentLevel.Ingredients[i],
					_spawnerSpawnPoints[i].position, _spawnerSpawnPoints[i].rotation);
				currentSpawner.IngredientSpawned += _colorManager.OnIngredientAdded;
				currentSpawner.IngredientSpawned += _blender.OnIngredientAdded;
				currentSpawner.Setup(_ingredientSpawnPoint);
				_activeSpawners.Add(currentSpawner);
			}
		}
	}
}