using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Button[] buttonList;
    public Color coinColor;
    private Color blankColor;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public GameObject startButton;
    public GameObject playerChoicePanel;
    
    private int currentButton;
    
    private void Awake()
    {
        blankColor = coinColor;
        SetGameControllerReferenceOnButtons();
        SetButtonIndexReference();
        gameOverPanel.SetActive(false);
        SetConnectBoardActive(false);
        startButton.SetActive(true);
        playerChoicePanel.SetActive(false);
    }
    
    public void ChoosePlayer(string color)
    {
        if(color == "Red")
        {
            coinColor = Color.red;
        }
        else if(color == "Yellow")
        {
            coinColor = Color.yellow;
        }
        playerChoicePanel.SetActive(false);
        SetConnectBoardActive(true);
    }

    public void StartGame()
    {
        startButton.SetActive(false);
        playerChoicePanel.SetActive(true);
        
    }

    public void Restart()
    {
        BoardReset();
        gameOverPanel.SetActive(false);
        SetConnectBoardActive(false);
        startButton.SetActive(true);

    }

    public void GameOver(Color winningColor)
    {
        if (winningColor == Color.red)
        {
            gameOverText.text = "Red Wins";
        }
        else if (winningColor == Color.yellow)
        {
            gameOverText.text = "Yellow Wins";
        }
        else
        {
            gameOverText.text = "It's a draw";
        }
        gameOverPanel.SetActive(true);
    }

    public void ChangeChances()
    {
        
        if(coinColor == Color.red)
        {

            coinColor = Color.yellow;
        }
        else
        {
            coinColor = Color.red;
        }
    }

    public void SetGameControllerReferenceOnButtons()
    {
        for(int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponent<Coins>().SetGameControllerReference(this);
        }
    }

    public void SetButtonIndexReference()
    {
        for(int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponent<Coins>().SetButtonIndex(i);
        }
    }

    public void FillFromBase(int selectedButton)
    {
        if(CheckFilled() == 1)
        {
            GameOver(Color.white);
        }
        else
        {
            currentButton = selectedButton;

            int lowestbutton = currentButton + 35;
            for (int i = 0; i < 6; i++)
            {
                if (buttonList[lowestbutton].image.color == Color.red || buttonList[lowestbutton].image.color == Color.yellow)
                {
                    lowestbutton -= 7;
                }
                else
                {
                    buttonList[lowestbutton].image.color = coinColor;
                    break;
                }
            }
            Check4InRow(lowestbutton, coinColor);
        }
        
    }

    public void Check4InRow(int filledButton, Color filledColor)
    {
        //Connected 4 logic here
        //Check vertical
        int downFlag = Check4Down(filledButton, filledColor);
        if(downFlag == 4)
        {
            print("Vertical");
            GameOver(filledColor);
            SetConnectBoardActive(false);
        }

        //Check horizontal
        int rightFlag = Check4Right(filledButton, filledColor);
        int leftFlag = Check4Left(filledButton, filledColor);

        int totalMatches = rightFlag + leftFlag - 1;

        if(totalMatches >= 4)
        {
            print("Horizontal");
            GameOver(filledColor);
            SetConnectBoardActive(false);
        }

        //Check Diagonal
        int leftdiagFlag = Check4DiagRight(filledButton, filledColor);
        int rightdiagFlag = Check4DiagRight(filledButton, filledColor);
        int leftdiagupFlag = Check4DiagUpLeft(filledButton, filledColor);
        int rightdiagupFlag = Check4DiagUpRight(filledButton, filledColor);

        int totalMatchLeft = rightdiagupFlag + leftdiagFlag - 1;
        int totalMatchright = leftdiagupFlag + rightdiagFlag - 1;

        if(totalMatchLeft >= 4)
        {
            print("Left Diagonal");
            GameOver(filledColor);
            SetConnectBoardActive(false);
        }
        if (totalMatchright >= 4)
        {
            print("Right Diagonal");
            GameOver(filledColor);
            SetConnectBoardActive(false);
        }
    }

    public void SetConnectBoardActive(bool toggle)
    {
        for(int i = 0; i < 7; i++)
        {
            buttonList[i].interactable = toggle;
        }
    }
    //Cheching Logics
    public int Check4Down(int filledButton, Color filledColor)
    {
        int checkButton = filledButton + 7;
        int flag = 1;
        for (int i = 0; i < 3; i++)
        {
            if (checkButton < 42 && checkButton >= 0)
            {
                if (buttonList[checkButton].image.color == filledColor)
                {
                    flag += 1;
                    checkButton += 7;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
        return flag;
    }

    public int Check4Right(int filledButton, Color filledColor)
    {
        int checkButton = filledButton + 1;
        int flag = 1;
        for (int i = 0; i < 3; i++)
        {
            if (checkButton < 42 && checkButton >= 0)
            {
                if (buttonList[checkButton].image.color == filledColor)
                {
                    flag += 1;
                    checkButton += 1;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
        return flag;
    }
    public int Check4Left(int filledButton, Color filledColor)
    {
        int checkButton = filledButton - 1;
        int flag = 1;
        for (int i = 0; i < 3; i++)
        {
            if (checkButton < 42 && checkButton >= 0)
            {
                if (buttonList[checkButton].image.color == filledColor)
                {
                    flag += 1;
                    checkButton -= 1;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
        return flag;
    }
    public int Check4DiagLeft(int filledButton, Color filledColor)
    {
        int checkButton = filledButton + 6;
        int flag = 1;
        for (int i = 0; i < 3; i++)
        {
            if (checkButton < 42 && checkButton >= 0)
            {
                if (buttonList[checkButton].image.color == filledColor)
                {
                    flag += 1;
                    checkButton += 6;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
        return flag;
    }
    public int Check4DiagRight(int filledButton, Color filledColor)
    {
        int checkButton = filledButton + 8;
        int flag = 1;
        for (int i = 0; i < 3; i++)
        {
            if (checkButton < 42 && checkButton >= 0)
            {
                if (buttonList[checkButton].image.color == filledColor)
                {
                    flag += 1;
                    checkButton += 8;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
        return flag;
    }

    public int Check4DiagUpLeft(int filledButton, Color filledColor)
    {
        int checkButton = filledButton - 8;
        int flag = 1;
        for (int i = 0; i < 3; i++)
        {
            if (checkButton < 42 && checkButton >= 0)
            {
                if (buttonList[checkButton].image.color == filledColor)
                {
                    flag += 1;
                    checkButton -= 8;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
        return flag;
    }
    public int Check4DiagUpRight(int filledButton, Color filledColor)
    {
        int checkButton = filledButton - 6;
        int flag = 1;
        for (int i = 0; i < 3; i++)
        {
            if (checkButton < 42 && checkButton >= 0)
            {
                if (buttonList[checkButton].image.color == filledColor)
                {
                    flag += 1;
                    checkButton -= 6;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
        return flag;
    }

    public void BoardReset()
    {

        for(int i = 0; i < buttonList.Length; i++)
        {

            buttonList[i].image.color = blankColor;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public int CheckFilled()
    {
        int filled = 0;
        for(int i = 0; i < buttonList.Length; i++)
        {
            if (buttonList[i].image.color == Color.red || buttonList[i].image.color == Color.yellow)
            {
                filled++;
            }
            else
            {
                break;
            }
        }
        if (filled < 42)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

}