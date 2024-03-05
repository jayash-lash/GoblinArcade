using Player;
using UnityEngine;
using Zenject;

namespace UI
{
    public class AttackButtonMediator : MonoBehaviour
    {
        private PlayerObserver _player;
        [Inject] private void Construct(PlayerObserver player) => _player = player; 
        
        [SerializeField] private AttackButtonView _attackButtonView;
        
        private bool _isEnemyInRange;
    
        private void OnEnable()
        {
            _attackButtonView.OnSimpleButtonClick += OnSimpleButtonClickHandler;
            _attackButtonView.OnStrongAttackButtonClick += OnStrongAttackButtonClickHandler;
            _player.PlayerAttack.OnRangeAttackChanged += StrongButtonInteraction;
        }
    
        private void OnDisable()
        {
            _attackButtonView.OnSimpleButtonClick -= OnSimpleButtonClickHandler;
            _attackButtonView.OnStrongAttackButtonClick -= OnStrongAttackButtonClickHandler;
            _player.PlayerAttack.OnRangeAttackChanged -= StrongButtonInteraction;
        }

        private void OnSimpleButtonClickHandler() => _player.PlayerAttack.AttackOnClick();

        private void OnStrongAttackButtonClickHandler()
        {
            var canAttack = _player.PlayerAttack.IsAttacking;
            var cooldown = _player.PlayerAttack.StrongAttackDelay;
        
            _player.PlayerAttack.StrongAttackOnClick();
            _attackButtonView.OnCoolDown(canAttack, cooldown);
        }

        private void StrongButtonInteraction(bool value)
        {
            _isEnemyInRange = value;
            _attackButtonView.UpdateButtonState(_isEnemyInRange);
        }

 
    }
}