using System;
using Player;
using UnityEngine;

namespace Obstacles
{
    public class Enemy : Square
    {
        private void Start()
        {
            OnDie += () =>
            {
                GameManager.Instance.GameOver();
                PlayerMovement.Instance.Graphic.SetActive(false);
            };
        }
    }
}
