using UnityEngine;
using UnityEngine.UI;

namespace GameBee.Calculator
{
    public class CalculatorView : MonoBehaviour
    {
        #region PUBLIC_VARS

        #endregion

        #region PRIVATE_VARS

        [SerializeField] private Text _expressionLabel;
        [SerializeField] private Text _resultLabel;
        [SerializeField] private ButtonInput[] _buttons;

        #endregion

        #region UNITY_CALLBACKS

        private void OnEnable()
        {
            if (_buttons == null)
                return;
            for (int i = 0; i < _buttons.Length; i++)
            {
                if (_buttons[i] != null)
                    _buttons[i].OnPressed += HandleButtonPressed;
            }
        }

        private void OnDisable()
        {
            if (_buttons == null) return;
            for (int i = 0; i < _buttons.Length; i++)
            {
                if (_buttons[i] != null)
                    _buttons[i].OnPressed -= HandleButtonPressed;
            }
        }

        #endregion

        #region PUBLIC_FUNCTIONS

        public void SetExpression(string text)
        {
            if (_expressionLabel != null)
            {
                _expressionLabel.text = text;
                _expressionLabel.gameObject.SetActive(true);
            }
            if (_resultLabel != null)
                _resultLabel.gameObject.SetActive(false);
        }

        public void SetResult(string text)
        {
            if (_resultLabel != null)
            {
                _resultLabel.text = text;
                _resultLabel.gameObject.SetActive(true);
            }
            if (_expressionLabel != null)
                _expressionLabel.gameObject.SetActive(false);
        }

        #endregion

        #region PRIVATE_FUNCTIONS

        #endregion

        #region CO-ROUTINES

        #endregion

        #region EVENT_HANDLERS

        private void HandleButtonPressed(ButtonInput button)
        {
            if (CalculatorManager.Instance != null)
                CalculatorManager.Instance.HandleInput(button);
        }

        #endregion

        #region UI_CALLBACKS

        #endregion

        #region EDITOR_CALLBACKS

        #endregion
    }
}
