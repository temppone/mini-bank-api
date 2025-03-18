namespace ApiTest.Utils
{
    public class CnpjValidator
    {
        public static bool IsValidCnpj(string cnpj)
        {
            cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

            if (cnpj.Length != 14)
                return false;

            if (cnpj.All(c => c == cnpj[0]))
                return false;

            int[] firstCheckDigitWeights = new int[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int firstCheckDigit = CalculateCheckDigit(cnpj.Substring(0, 12), firstCheckDigitWeights);

            int[] secondCheckDigitWeights = new int[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int secondCheckDigit = CalculateCheckDigit(cnpj.Substring(0, 13), secondCheckDigitWeights);

            return cnpj[12] == firstCheckDigit.ToString()[0] && cnpj[13] == secondCheckDigit.ToString()[0];
        }

        private static int CalculateCheckDigit(string cnpj, int[] weights)
        {
            int sum = 0;

            for (int i = 0; i < cnpj.Length; i++)
            {
                sum += (cnpj[i] - '0') * weights[i];
            }

            int remainder = sum % 11;
            return remainder < 2 ? 0 : 11 - remainder;
        }
    }
}
