namespace GameBee.Calculator
{
    public static class ExpressionSyntax
    {
        #region PRIVATE_VARS

        private const string Operators = "+-*/Xx";

        #endregion

        #region PUBLIC_FUNCTIONS

        public static bool IsOperator(char value)
        {
            return Operators.IndexOf(value) >= 0;
        }

        public static char NormalizeOperator(char value)
        {
            if (value == 'X' || value == 'x')
                return '*';

            return value;
        }

        #endregion
    }
}
