using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvPocket : MonoBehaviour
{
	Texture2D itemImg;
	InvManager manager;

	[SerializeField] Vector2 ourCoords;
	Image imageComponent;

	void Start()
	{
		EventScript.InvPocketReveal.AddListener(Reveal);
		EventScript.InvPocketHide.AddListener(Hide);

		imageComponent = GetComponent<Image>();
		Reveal();
	}

	void Update()
	{

	}

	void Hide(Vector2 newCoords)
	{
		if (ourCoords == newCoords)
		{
			imageComponent.enabled = false;

		}
	}
	void Hide()
	{
		imageComponent.enabled = false;
	}

	void Reveal(Vector2 newCoords)
	{
		if (ourCoords == newCoords)
		{
			imageComponent.enabled = true;
		}
	}

	void Reveal()
	{
		imageComponent.enabled = true;
	}
}
