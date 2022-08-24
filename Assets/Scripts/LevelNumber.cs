using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class LevelNumber : MonoBehaviour
    {
        [SerializeField] private Text Text;
        [SerializeField] private Text Text2;
        [SerializeField] private Game Game;

        private void Start()
        {
            Text.text = (Game.LevelIndex + 1).ToString();
            Text2.text = (Game.LevelIndex + 2).ToString();
        }

    }
}
