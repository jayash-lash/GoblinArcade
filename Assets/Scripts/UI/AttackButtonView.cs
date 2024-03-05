using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AttackButtonView : MonoBehaviour
    {
        public event Action OnSimpleButtonClick;
        public event Action OnStrongAttackButtonClick;
    
        [SerializeField] private Button _strongAttackButton;
        [SerializeField] private Button _simpleButton;
        [SerializeField] private Image _coolDownFon;

        private void OnEnable()
        {
            _strongAttackButton.onClick.AddListener(() => OnStrongAttackButtonClick?.Invoke());
            _simpleButton.onClick.AddListener(() => OnSimpleButtonClick?.Invoke());
        }
    
        public void DisableAllButtons()
        {
            _simpleButton.gameObject.SetActive(false); 
            _strongAttackButton.gameObject.SetActive(false);
        }

        public void UpdateButtonState(bool value)
        {
            var canInteract = value;
            _strongAttackButton.interactable = canInteract;
        }

        public void OnCoolDown(bool value, float cooldown)
        {
            if (!value)
            {
                _strongAttackButton.interactable = false;
                var cooldownTime = cooldown;
                StartCoroutine(UpdateCooldownView(cooldownTime));
            }
            else _strongAttackButton.interactable = true;
        }

        private IEnumerator UpdateCooldownView(float cooldown)
        {
            _strongAttackButton.interactable = false;
            _coolDownFon.fillAmount = 0f;
            
            while (_coolDownFon.fillAmount < 1f)
            {
                _coolDownFon.fillAmount += Time.deltaTime / cooldown;
                yield return null;
            }
        }

        private void OnDisable()
        {
            _strongAttackButton.onClick.RemoveAllListeners();
            _simpleButton.onClick.RemoveAllListeners();
        }
    }
}