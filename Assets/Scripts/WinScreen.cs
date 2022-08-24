using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] Game game;
public void ContinueGame()
    {
        game.ReloadLevel();
    }

}
