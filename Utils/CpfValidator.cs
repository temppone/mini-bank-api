namespace ApiTest.Utils
{
    public class CpfValidator
    {
        public static bool IsValidCpf(string cpf)
        {
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
                return false;

            if (cpf.All(c => c == cpf[0]))
                return false;

            int[] firstCheckDigitWeights = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int firstCheckDigit = CalculateCheckDigit(cpf.Substring(0, 9), firstCheckDigitWeights);

            int[] secondCheckDigitWeights = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int secondCheckDigit = CalculateCheckDigit(cpf.Substring(0, 10), secondCheckDigitWeights);

            return cpf[9] == firstCheckDigit.ToString()[0] && cpf[10] == secondCheckDigit.ToString()[0];
        }

        private static int CalculateCheckDigit(string cpf, int[] weights)
        {
            int sum = 0;

            for (int i = 0; i < cpf.Length; i++)
            {
                sum += (cpf[i] - '0') * weights[i];
            }

            int remainder = sum % 11;
            return remainder < 2 ? 0 : 11 - remainder;
        }
    }
}