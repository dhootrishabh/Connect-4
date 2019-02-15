using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour {

    public Button button;
    private Color playerCoinColor;
    private int buttonIndex;
    private GameController gameController;

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }

    public void SetButtonIndex(int index)
    {
        buttonIndex = index;
    }

    public void btnClick()
    {
        //print(buttonIndex);
        //playerCoinColor = gameController.coinColor;
        //button.image.color = playerCoinColor;
        gameController.FillFromBase(buttonIndex);
        gameController.ChangeChances();
    }

    
}
