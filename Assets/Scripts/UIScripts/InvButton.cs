using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvButton: MonoBehaviour
{
    public int invArrayID;
    InvManager invManager;

    public void WasClicked()
    {
		invManager = GameObject.FindWithTag("UIManager").GetComponent<InvManager>();
		invManager.SlotClick(invArrayID);

        Debug.Log($"Click {invArrayID}");
    }
}
