using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class InvManager : MonoBehaviour
{
	[SerializeField] InvItem[,] savedInventoryArray;
    PlayerMenuState currentPlayerMenuState;

    //InvPocket[,] invPockets;    //instead of having this script send a bunch of Events out into the ether, They will report to
	//MovePocket[,] movePockets;  //this manager at the start of the scene load, in order to front load the work and lag

	Vector2 coord1;
    InvItem invItem1;
    InvPocket invPocket1;
    Vector2 coord2;
    InvItem invItem2;
    InvPocket invPocket2;

    Vector2 moveCoord1;
    Vector2 moveCoord2;


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
        Open();

		//EventScript.PauseGame.AddListener(Pause);
		//EventScript.ResumeGame.AddListener(Resume);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        currentPlayerMenuState = PlayerMenuState.NotCarrying;

        for (int y = 0; y < savedInventoryArray.GetLength(0); y++)
        {
            for (int x = 0; x < savedInventoryArray.GetLength(1); x++)
            {
				EventScript.InvPocketImagePlace.Invoke(x, y, savedInventoryArray[x, y].sprite);
			}   //  We are gonna have to do some debugging on some Items that are not saved to local storage, methinks
		}
    }

    public void SlotClick(Vector2 Coord)
    {
        if (currentPlayerMenuState == PlayerMenuState.NotCarrying)
        {
			//save coord in coord1
            coord1 = Coord;
			//reference Item Array, save item1 as ItemArray[coord1]
            invItem1 = savedInventoryArray[(int)coord1.x, (int)coord1.y];
            //Send Event invSlot HIDE Event<coord1>   just to give the illusion to the player of picking up the item
            EventScript.InvPocketHide.Invoke(coord1);
			//Send Event moveSlot REVEAL Event<item1.image, coord1> make the movePocket at this coord Visable (enable the image)
			//                                                      and hand it the image to present (sprite2D?)
			EventScript.MoveSlotReveal.Invoke(coord1, invItem1.sprite);
			//moveCoord1 = coord1
            moveCoord1 = coord1;

		}
        else if (currentPlayerMenuState == PlayerMenuState.Carrying)
		{
			//save coord in coord2
            coord2 = Coord;
			//Send Event moveSlot HIDE Event<moveCoord1>
            EventScript.MoveSlotHide.Invoke(moveCoord1);
            //save item2 as ItemArray[coord2]
            invItem2 = savedInventoryArray[(int)coord2.x, (int)coord2.y];
            //Update ItemArray (ItemArray[coord1] = item2; ItemArray[coord2] = item1)
            savedInventoryArray[(int)coord1.x, (int)coord1.y] = invItem1;
            savedInventoryArray[(int)coord2.x, (int)coord2.y] = invItem2;

			//Send Event InvPocketImagePlace<coord1.x, coord1.y, saveInventoryArray[coord1.x,coord1.y].image>
			//Send Event InvPocketImagePlace<coord2.x, coord2.y, saveInventoryArray[coord2.x,coord2.y].image>
			EventScript.InvPocketImagePlace.Invoke((int)coord1.x, (int)coord1.y, savedInventoryArray[(int)coord1.x, (int)coord1.y].sprite);
			EventScript.InvPocketImagePlace.Invoke((int)coord2.x, (int)coord2.y, savedInventoryArray[(int)coord2.x, (int)coord2.y].sprite);

			//Send Event invSlot REVEAL Event<coord1>
			EventScript.InvPocketReveal.Invoke(coord1);
		}
	}

    public void ItemDragController()
    {
        //retrieve new moveCoord2 (somehow lmao)(maybe via the input itself? maybe we can do button.OnSelect in some event way)

        //Send Event moveSlot HIDE Event<moveCoord1>
        EventScript.MoveSlotHide.Invoke(moveCoord1);
		//Send Event moveSlot REVEAL Event <moveCoord2, item1.image>
		EventScript.MoveSlotReveal.Invoke(moveCoord2, invItem1.sprite);
        //moveCoord1 = moveCoord2
        moveCoord1 = moveCoord2;
	}

    enum PlayerMenuState
    {
        None,
        NotCarrying,
        Carrying
    }
}
