using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MovePocket : MonoBehaviour
{
	Texture2D itemImg;
	InvManager manager;

	[SerializeField] int ourID;
	Image imageComponent;

	void Start()
    {
		EventScript.MoveSlotReveal.AddListener(Reveal);
		EventScript.MoveSlotHide.AddListener(Hide);

		imageComponent = GetComponent<Image>();
		Hide();
	}

    void Update()
    {
        
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

	void Reveal(int newID, Sprite newSprite)
	{
		if (ourID == newID)
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
