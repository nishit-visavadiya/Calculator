using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameBee.Calculator
{
    [RequireComponent(typeof(Button))]
    public class ButtonInput : MonoBehaviour
    {
        #region PUBLIC_VARS

        public event Action<ButtonInput> OnPressed;
        public ButtonType Type => _type;
        public string Value => _value;

        #endregion

        #region PRIVATE_VARS

        [SerializeField] private ButtonType _type;
        [SerializeField] private Text _inputText;
        [SerializeField] private string _value;

        #endregion

        #region UNITY_CALLBACKS

        #endregion

        #region PUBLIC_FUNCTIONS

        #endregion

        #region PRIVATE_FUNCTIONS

        #endregion

        #region CO-ROUTINES

        #endregion

        #region UI_CALLBACKS

        public void OnButtonClicked()
        {
            OnPressed?.Invoke(this);
        }

        #endregion

        #region EDITOR_CALLBACKS

        [ContextMenu("SetDigit")]
        public void SetDigit()
        {
            _inputText = GetComponentInChildren<Text>();
            if (_inputText != null)
                _value = _inputText.text;
        }

        #endregion
    }
}
