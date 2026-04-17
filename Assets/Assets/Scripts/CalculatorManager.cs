using System;
using System.Globalization;
using UnityEngine;

namespace GameBee.Calculator
{
    public class CalculatorManager : MonoBehaviour
    {
        #region PUBLIC_VARS

        public static CalculatorManager Instance { get; private set; }

        #endregion

        #region PRIVATE_VARS

        [SerializeField] private CalculatorView _view;

        private const string FreshState = "0";

        [SerializeField] private string _expression = FreshState;
        
        [SerializeField] private string _lastResult;

        #endregion

        #region UNITY_CALLBACKS

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            Clear();
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }

        #endregion

        #region PUBLIC_FUNCTIONS

        public void HandleInput(ButtonInput button)
        {
            if (button == null)
                return;

            switch (button.Type)
            {
                case ButtonType.Digit:
                    AppendDigit(button.Value);
                    break;
                case ButtonType.Decimal:
                    AppendDecimal();
                    break;
                case ButtonType.Operator:
                    AppendOperator(button.Value);
                    break;
                case ButtonType.Delete:
                    Delete();
                    break;
                case ButtonType.Clear:
                    Clear();
                    break;
                case ButtonType.Equals:
                    Evaluate();
                    break;
            }
        }

        #endregion

        #region PRIVATE_FUNCTIONS

        private void AppendDigit(string digit)
        {
            if (string.IsNullOrEmpty(digit))
                return;

            _lastResult = null;

            int segmentStart = FindCurrentNumberStart();
            string currentSegment = _expression.Substring(segmentStart);

            if (currentSegment == FreshState)
            {
                if (digit[0] == '0')
                    return;
                _expression = _expression.Substring(0, segmentStart) + digit;
            }
            else
            {
                _expression += digit;
            }
            SetExpression();
        }

        private void AppendDecimal()
        {
            _lastResult = null;

            int segmentStart = FindCurrentNumberStart();

            if (segmentStart == _expression.Length)
                return;

            for (int i = segmentStart; i < _expression.Length; i++)
            {
                if (_expression[i] == '.')
                    return;
            }
            _expression += '.';
            SetExpression();
        }

        private void AppendOperator(string op)
        {
            if (string.IsNullOrEmpty(op))
                return;

            if (_lastResult != null)
            {
                _expression = _lastResult;
                _lastResult = null;
            }

            if (_expression.Length == 0)
                return;

            char last = _expression[_expression.Length - 1];
            if (ExpressionSyntax.IsOperator(last))
            {
                _expression = _expression.Substring(0, _expression.Length - 1) + op;
            }
            else
            {
                _expression += op;
            }
            SetExpression();
        }

        private void Delete()
        {
            if (_lastResult != null)
            {
                _lastResult = null;
                _expression = FreshState;
                SetExpression();
                return;
            }

            if (_expression.Length == 0 || _expression == FreshState)
                return;

            _expression = _expression.Substring(0, _expression.Length - 1);

            if (_expression.Length == 0)
                _expression = FreshState;

            SetExpression();
        }

        private void Clear()
        {
            _expression = FreshState;
            _lastResult = null;
            if (_view != null)
            {
                _view.SetResult(string.Empty);
                _view.SetExpression(_expression);
            }
        }

        
        private void Evaluate()
        {
            if (_lastResult != null)
                return;

            string expr = _expression;

            while (expr.Length > 0 && ExpressionSyntax.IsOperator(expr[expr.Length - 1]))
                expr = expr.Substring(0, expr.Length - 1);

            if (expr.Length == 0)
                return;

            try
            {
                double result = Calculator.Evaluate(expr);
                if (double.IsNaN(result) || double.IsInfinity(result))
                {
                    ShowResult("Error");
                }
                else
                {
                    string formatted = result.ToString("G15", CultureInfo.InvariantCulture);
                    ShowResult(formatted);
                    _lastResult = formatted;
                }
            }
            catch (Exception)
            {
                ShowResult("Error");
            }

            _expression = FreshState;
        }

        private void SetExpression()
        {
            _view.SetExpression(_expression);
        }

        private void ShowResult(string text)
        {
            _view.SetResult(text);
        }

        private int FindCurrentNumberStart()
        {
            for (int i = _expression.Length - 1; i >= 0; i--)
            {
                if (ExpressionSyntax.IsOperator(_expression[i]))
                    return i + 1;
            }
            return 0;
        }

        #endregion

        #region CO-ROUTINES

        #endregion

        #region EVENT_HANDLERS

        #endregion

        #region UI_CALLBACKS

        #endregion
    }
}
