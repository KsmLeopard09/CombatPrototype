using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvButton: MonoBehaviour
{
    [SerializeField] Vector2 invArrayCoord;
    InvManager invManager;

    public void WasClicked()
    {
		invManager = GameObject.FindWithTag("UIManager").GetComponent<InvManager>();
		invManager.SlotClick(invArrayCoord);

        Debug.Log($"Click {invArrayCoord}");
    }
}
