using Common;
using UnityEngine;

namespace Player
{
    public class PlayerObserver : MonoBehaviour
    {
        //should be bind in GameObjectInstaller
        public Health Health => _playerHealth;
        public PlayerAttack PlayerAttack => _playerAttack;

        [SerializeField] private Health _playerHealth;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerAttack _playerAttack;
        [SerializeField] private CharacterAnimation _characterAnimation;


        private void OnEnable()
        {
            _playerHealth.OnHealthZero += _characterAnimation.PlayDieAnim;
            _playerHealth.OnHealthZero += _playerMovement.StopMovement;
            
            _playerAttack.OnLightAttack += _characterAnimation.PlayLightAttackAnim;
            _playerAttack.OnStrongAttack += _characterAnimation.PlayStrongAttackAnim;
            
            _playerMovement.OnMove += _characterAnimation.PlayAnimateMovement;
        }
        
        private void OnDisable()
        {
            _playerHealth.OnHealthZero -= _characterAnimation.PlayDieAnim;
            _playerHealth.OnHealthZero -= _playerMovement.StopMovement;

            _playerAttack.OnLightAttack -= _characterAnimation.PlayLightAttackAnim;
            _playerAttack.OnStrongAttack -= _characterAnimation.PlayStrongAttackAnim;
            
            _playerMovement.OnMove -= _characterAnimation.PlayAnimateMovement;
        }
    }
}