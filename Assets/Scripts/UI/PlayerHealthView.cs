using Player;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class PlayerHealthView : MonoBehaviour
    {
        private PlayerObserver _player;
        [Inject] private void Construct(PlayerObserver player) => _player = player;
        
        [SerializeField] private TextMeshProUGUI _healthText;

        private void Update() => UpdatePlayerHealth();
        private void UpdatePlayerHealth() => _healthText.text = "Health: " + _player.Health.CurrentHealth;
    }
}