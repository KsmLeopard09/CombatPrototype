using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MovePocket : MonoBehaviour
{
	Texture2D itemImg;
	InvManager manager;

	[SerializeField] Vector2 ourCoords;
	Image imageComponent;

	void Start()
    {
		EventScript.MoveSlotReveal.AddListener(Reveal);
		EventScript.MoveSlotHide.AddListener(Hide);

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

	void Reveal(Vector2 newCoords, Sprite newSprite)
	{
		if (ourCoords == newCoords)
		{
			imageComponent.enabled = true;
			imageComponent.sprite = newSprite;
		}
	}

	void Reveal()
	{
		imageComponent.enabled = true;
	}
}
