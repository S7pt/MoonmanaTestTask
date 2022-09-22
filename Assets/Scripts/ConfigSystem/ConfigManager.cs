using MoonmanaTestTask.Levels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MoonmanaTestTask.Config
{
	public class ConfigManager : MonoBehaviour
	{
		private const string PATH = "Configs/";
		private const string LEVELS_PATH = "Levels";
		private JsonSerializerSettings _serializerOptions;
		private Dictionary<Type, List<INamedConfig>> _configs = new Dictionary<Type, List<INamedConfig>>();

		private void Awake()
		{
			_serializerOptions = new JsonSerializerSettings
			{
				Converters = {
					new ColorConverter(),
				}
			};
			DeserializeAllConfigs();
		}

		private void LoadConfig<T>(bool isFolder, string path) where T : INamedConfig, new()
		{

			if (isFolder)
			{
				TextAsset[] dataArray = Resources.LoadAll<TextAsset>(PATH + path);
				foreach (TextAsset asset in dataArray)
				{
					try
					{
						T config = JsonConvert.DeserializeObject<T>(asset.text, _serializerOptions);
						config.Name = asset.name;
						if (!_configs.ContainsKey(config.GetType()))
						{
							_configs.Add(config.GetType(), new List<INamedConfig>());
						}
						_configs[config.GetType()].Add(config);
					}
					catch (Exception e)
					{
						Debug.Log(e.Message);
					}
				}
			}
			else
			{
				TextAsset data;
				data = Resources.Load<TextAsset>(PATH + path);
				try
				{
					T config = JsonConvert.DeserializeObject<T>(data.text, _serializerOptions);
					config.Name = data.name;
					if (!_configs.ContainsKey(config.GetType()))
					{
						_configs.Add(config.GetType(), new List<INamedConfig>());
					}
					_configs[config.GetType()].Add(config);
				}
				catch (Exception e)
				{
					Debug.Log(e.Message);
				}
			}
		}

		private void DeserializeAllConfigs()
		{
			LoadConfig<Level>(true, LEVELS_PATH);
		}

		public List<T> GetAllConfigs<T>() where T : INamedConfig, new()
		{
			return _configs[typeof(T)].Cast<T>().ToList();
		}
	}
}