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
            _expressionLabel.text = text;
            SetExpressionText(true);
        }

        public void SetResult(string text)
        {
            _resultLabel.text = text;
            SetExpressionText(false);
        }

        #endregion

        #region PRIVATE_FUNCTIONS

        private void HandleButtonPressed(ButtonInput button)
        {
            if (CalculatorManager.Instance != null)
                CalculatorManager.Instance.HandleInput(button);
        }

        private void SetExpressionText(bool isActive)
        {
            _expressionLabel.gameObject.SetActive(isActive);
            _resultLabel.gameObject.SetActive(!isActive);
        }

        #endregion

        #region CO-ROUTINES

        #endregion

        #region UI_CALLBACKS

        #endregion
    }
}
