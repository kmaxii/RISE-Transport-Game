 using System;
 using System.Collections.Generic;
 using Mapbox.Unity.MeshGeneration.Interfaces;
 using UnityEngine;
 using UnityEngine.Serialization;
 using UnityEngine.UI;

 namespace Mapbox.Examples.Scripts
{
	public class PoiLabelTextSetter : MonoBehaviour, IFeaturePropertySettable
	{
		[FormerlySerializedAs("_text")] [SerializeField]
		Text text;
		[FormerlySerializedAs("_background")] [SerializeField]
		Image background;


		[SerializeField] private Renderer inWorldImage;
		[SerializeField] private Renderer onTopImage;
		private static readonly int MainTex = Shader.PropertyToID("_MainTex");

		public void Set(String text)
		{
			this.text.text = text;
			RefreshBackground();
		}

		public void SetImage(Sprite sprite)
		{
			inWorldImage.GetComponent<SpriteRenderer>().sprite = sprite;
			onTopImage.material.SetTexture(MainTex, sprite.texture);
		}

		public void SetBackgroundColor(Color color)
		{
			background.color = color;
		}
		
		public void Set(Dictionary<string, object> props)
		{
			text.text = "";

			if (props.ContainsKey("name"))
			{
				text.text = props["name"].ToString();
			}
			else if (props.ContainsKey("house_num"))
			{
				text.text = props["house_num"].ToString();
			}
			else if (props.ContainsKey("type"))
			{
				text.text = props["type"].ToString();
			}
			RefreshBackground();
		}

		public void RefreshBackground()
		{
			RectTransform backgroundRect = background.GetComponent<RectTransform>();
			LayoutRebuilder.ForceRebuildLayoutImmediate(backgroundRect);
		}
	}
}