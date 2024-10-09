using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

static public class EventScript
{
	static public UnityEvent PauseGame = new UnityEvent();

	static public UnityEvent <int, Sprite> InvPocketImagePlace = new UnityEvent<int, Sprite>();

	static public UnityEvent <int> InvPocketHide = new UnityEvent<int>();
	static public UnityEvent <int> InvPocketReveal = new UnityEvent<int>();

	static public UnityEvent <int> MoveSlotHide = new UnityEvent<int>();
	static public UnityEvent <int, Sprite> MoveSlotReveal = new UnityEvent<int, Sprite>();
}
