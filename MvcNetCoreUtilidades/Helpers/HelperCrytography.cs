using System.Security.Cryptography;
using System.Text;

namespace MvcNetCoreUtilidades.Helpers
{
    public class HelperCrytography
    {
        //Creamos un string para el salt.
        public static string Salt { get; set; }

        //Metodo para generar un salt
        private static string GenerateSalt()
        {
            Random random = new Random();
            string salt = "";
            for (int x = 1; x<=30; x++)
            {
                //Generamos un aleatorio ASCII
                int num = random.Next(1, 255);
                char letra = Convert.ToChar(num);
                salt += letra;
            }
            return salt;
        }

        //Creamos un metodo eficiente para el cifrado
        public static string CifrarContenido(string contenido, bool comparar)
        {
            if (comparar == false)
            {
                //No queremos comparar, solo cifrado, creamos un nuevo salt
                Salt = GenerateSalt();
            }
            //Realizamos el cifrado
            string contenidoSalt = contenido + Salt;
            //Utilizamos el objeto grande para cifrar
            SHA512 managed = SHA512.Create();
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] salida;
            salida = encoding.GetBytes(contenidoSalt);
            //Realizar n interacciones sobre el propio cifrado
            for (int x = 0; x<43; x++)
            {
                //Cifrado sobre cifrado
                salida = managed.ComputeHash(salida);
            }
            //Debemos liberar la memoria
            managed.Clear();
            string resultado = encoding.GetString(salida);
            return resultado;
        }

        //Creamos los metodos de tipo static, simplemente devolvemos un texto cifrado simple
        public static string EncriptarTextoBasico(string contenido)
        {
            //El cifrado se realiza a nivel de bytes, debemos convertir el texto de netrada a bytes.
            byte[] entrada;
            //Cuando cifremos los bytes, nos dara una salida de bytes[]
            byte[] salida;
            //Necesitamos una clase para convertir de byte a string, y viceversa.
            UnicodeEncoding encoding = new UnicodeEncoding();
            //Necesitamos un objeto para cifrar el contenido.
            SHA1 managed = SHA1.Create();
            //Convertirmo el texto a bytes[]
            entrada = encoding.GetBytes(contenido);
            //Los objetos de cifrado tienen un metodo llamado ComputerHash()
            //que recibe un array de bytes, realizan acciones internas y devuelve el array de bytes cifrado
            salida = managed.ComputeHash(entrada);
            //Convertimos los bytes a texto
            string resultado = encoding.GetString(salida);
            return resultado;
        }
    }
}
