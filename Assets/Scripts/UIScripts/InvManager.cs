using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class InvManager : MonoBehaviour
{
	public InvItem[] savedInventoryArray;

	PlayerMenuState currentPlayerMenuState;

	//InvPocket[,] invPockets;    //instead of having this script send a bunch of Events out into the ether, They will report to
	//MovePocket[,] movePockets;  //this manager at the start of the scene load, in order to front load the work and lag

	int slotID1;
	InvItem invItem1;
	InvPocket invPocket1;
	int slotID2;
	InvItem invItem2;
	InvPocket invPocket2;

	int moveID1;
	int moveID2;


	//private void OnEnable()
	//{
	//	PlayerInputAsset.Enable();
	//}

	//private void OnDisable()
	//{
	//	PlayerInputAsset.Disable();
	//}

	void Start()
	{
		//Load Inventory array from local storage
		InventoryArrayLoadTEST();

		Open();
		//EventScript.PauseGame.AddListener(Pause);
		//EventScript.ResumeGame.AddListener(Resume);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void InventoryArrayLoadTEST()
	{   //Eventually we want to load these items from local storage, but for our purposes right now, that will not be needed
		//savedInventoryArray = new InvItem[10];
		//savedInventoryArray[0] = savedInvItem1;
		//savedInventoryArray[1] = savedInvItem2;
		//savedInventoryArray[2] = savedInvItem3;
		//savedInventoryArray[3] = savedInvItem4;
		//savedInventoryArray[4] = savedInvItem5;
		//savedInventoryArray[5] = savedInvItem6;
		//savedInventoryArray[6] = savedInvItem7;
		//savedInventoryArray[7] = savedInvItem8;
		//savedInventoryArray[8] = savedInvItem9;
		//savedInventoryArray[9] = savedInvItem10;
	}


	public void Open()
	{
		currentPlayerMenuState = PlayerMenuState.NotCarrying;

		for (int y = 0; y < savedInventoryArray.GetLength(0); y++)
		{
			Debug.Log($"InvokeImagePlace({y})");
			EventScript.InvPocketImagePlace.Invoke(y, savedInventoryArray[y].sprite);
			//Debug.Log($"Byeah X:{x},Y:{y} SpriteName:{savedInventoryArray[x, y].sprite.name}");
		}
	}

	public void SlotClick(int incomingID)
	{
		if (currentPlayerMenuState == PlayerMenuState.NotCarrying)
		{
			//save coord in coord1
			slotID1 = incomingID;
			//reference Item Array, save item1 as ItemArray[coord1]
			invItem1 = savedInventoryArray[slotID1];
			//Send Event invSlot HIDE Event<coord1>   just to give the illusion to the player of picking up the item
			EventScript.InvPocketHide.Invoke(slotID1);
			//Send Event moveSlot REVEAL Event<item1.image, coord1> make the movePocket at this coord Visable (enable the image)
			//                                                      and hand it the image to present (sprite2D?)
			EventScript.MoveSlotReveal.Invoke(slotID1, invItem1.sprite);
			//moveCoord1 = coord1
			moveID1 = slotID1;

			//Debug.Log($"SlotClick: slotID1:{slotID1} invItem1:{invItem1.sprite.name} InvokePocketHide({slotID1}) InvokeMoveSlotReveal({slotID1}{invItem1.sprite.name})");

			currentPlayerMenuState = PlayerMenuState.Carrying;
		}
		else if (currentPlayerMenuState == PlayerMenuState.Carrying)
		{
			//save coord in coord2
			slotID2 = incomingID;
			//Send Event moveSlot HIDE Event<moveCoord1>
			EventScript.MoveSlotHide.Invoke(moveID1);
			//save item2 as ItemArray[coord2]
			invItem2 = savedInventoryArray[slotID2];
			//Update ItemArray (ItemArray[coord1] = item2; ItemArray[coord2] = item1)
			savedInventoryArray[slotID1] = invItem2;
			savedInventoryArray[slotID2] = invItem1;

			//Send Event InvPocketImagePlace<coord1.x, coord1.y, saveInventoryArray[coord1.x,coord1.y].image>
			//Send Event InvPocketImagePlace<coord2.x, coord2.y, saveInventoryArray[coord2.x,coord2.y].image>
			EventScript.InvPocketImagePlace.Invoke(slotID1, savedInventoryArray[slotID1].sprite);
			EventScript.InvPocketImagePlace.Invoke(slotID2, savedInventoryArray[slotID2].sprite);

			//Send Event invSlot REVEAL Event<coord1>
			EventScript.InvPocketReveal.Invoke(slotID1);

			currentPlayerMenuState= PlayerMenuState.NotCarrying;
		}
	}

	public void ItemDragController()
	{
		//retrieve new moveCoord2 (somehow lmao)(maybe via the input itself? maybe we can do button.OnSelect in some event way)

		//Send Event moveSlot HIDE Event<moveCoord1>
		EventScript.MoveSlotHide.Invoke(moveID1);
		//Send Event moveSlot REVEAL Event <moveCoord2, item1.image>
		EventScript.MoveSlotReveal.Invoke(moveID2, invItem1.sprite);
		//moveCoord1 = moveCoord2
		moveID1 = moveID2;
	}

	enum PlayerMenuState
	{
		None,
		NotCarrying,
		Carrying
	}
}
