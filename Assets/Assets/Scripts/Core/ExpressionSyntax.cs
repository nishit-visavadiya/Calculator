namespace GameBee.Calculator
{
    public static class ExpressionSyntax
    {
        private const string Operators = "+-*/Xx";

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
    }
}
