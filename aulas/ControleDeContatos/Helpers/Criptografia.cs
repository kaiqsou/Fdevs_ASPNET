using System.Security.Cryptography;
using System.Text;

namespace ControleDeContatos.Helpers
{
    public static class Criptografia
    {
        public static string GerarHash(this string valor) // 'this' é o método de extensão para poder usar .GerarHash()
        {
            var hash = SHA1.Create();
            var encoding = new ASCIIEncoding();
            var array = encoding.GetBytes(valor);

            array = hash.ComputeHash(array);

            var strHexa = new StringBuilder();

            foreach (var item in array)
            {
                strHexa.Append(item.ToString("x2"));
            }

            return strHexa.ToString();
        }
    }
}
