using Player;

namespace Obstacles
{
    public class Bonus : Square
    {
        private void Start()
        {
            OnDie += () =>
            {
                GameManager.Instance.IncreaseScore();
            };
        }
    }
}
