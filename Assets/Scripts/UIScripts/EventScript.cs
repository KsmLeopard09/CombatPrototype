using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

static public class EventScript
{
	static public UnityEvent PauseGame = new UnityEvent();

	static public UnityEvent <int, int, Sprite> InvPocketImagePlace = new UnityEvent<int, int, Sprite>();

	static public UnityEvent <Vector2> InvPocketHide = new UnityEvent<Vector2>();
	static public UnityEvent <Vector2> InvPocketReveal = new UnityEvent<Vector2>();

	static public UnityEvent <Vector2> MoveSlotHide = new UnityEvent<Vector2>();
	static public UnityEvent <Vector2, Sprite> MoveSlotReveal = new UnityEvent<Vector2, Sprite>();
}
