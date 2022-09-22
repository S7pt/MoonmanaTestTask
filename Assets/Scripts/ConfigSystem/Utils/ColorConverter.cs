using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace MoonmanaTestTask.Config
{
	public class ColorConverter : JsonConverter<Color>
	{
		public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return new Color();
			}

			if (reader.TokenType == JsonToken.String)
			{
				JToken token = JToken.Load(reader);

				if (ColorUtility.TryParseHtmlString(token.ToString(), out Color color))
				{
					return color;
				}
				else
				{
					try
					{
						string[] splitedvalue = token.ToString().Split(',');
						Color result = new Color(float.Parse(splitedvalue[0]), float.Parse(splitedvalue[1]), float.Parse(splitedvalue[2]));
						return result;
					}
					catch (Exception e)
					{
						Debug.LogError("Color: " + token.ToString() + " can't be parsed: " + e);
					}
				}
			}
			return new Color();
		}

		public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}