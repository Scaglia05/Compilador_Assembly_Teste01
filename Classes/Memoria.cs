using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_Assembly_Teste01.Classes {
    public class Memoria {
        private byte[] memoria = new byte[1024 * 64]; // Ex: 64 KB

        public int LerPalavra(int endereco) {
            return BitConverter.ToInt32(memoria, endereco);
        }

        public void EscreverPalavra(int endereco, int valor) {
            byte[] bytes = BitConverter.GetBytes(valor);
            Array.Copy(bytes, 0, memoria, endereco, 4);
        }

        public short LerMeiaPalavra(int endereco) {
            return BitConverter.ToInt16(memoria, endereco);
        }

        public void EscreverMeiaPalavra(int endereco, int valor) {
            byte[] bytes = BitConverter.GetBytes((short)valor);
            Array.Copy(bytes, 0, memoria, endereco, 2);
        }

        public byte LerByte(int endereco) {
            return memoria[endereco];
        }

        public void EscreverByte(int endereco, int valor) {
            memoria[endereco] = (byte)valor;
        }
    }
}
