using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvPocket : MonoBehaviour
{
	Sprite itemSprite;
	InvManager manager;

	public int ourID;
	Image imageComponent;

	void Start()
	{
		EventScript.InvPocketReveal.AddListener(Reveal);
		EventScript.InvPocketHide.AddListener(Hide);
		EventScript.InvPocketImagePlace.AddListener(ImageSet);

		imageComponent = GetComponent<Image>();
		Reveal();
	}

	void Update()
	{

	}

	void ImageSet(int incomingID, Sprite incomingSprite)
	{
		Debug.Log($"incomingID{incomingID}, ourID{ourID}");
		if (incomingID == ourID)
		{
			itemSprite = incomingSprite;
			imageComponent.sprite = itemSprite;
		}
	}

	void Hide(int newID)
	{
		if (ourID == newID)
		{
			imageComponent.enabled = false;

		}
	}
	void Hide()
	{
		imageComponent.enabled = false;
	}

	void Reveal(int newCoords)
	{
		if (ourID == newCoords)
		{
			imageComponent.enabled = true;
		}
	}

	void Reveal()
	{
		imageComponent.enabled = true;
	}
}
