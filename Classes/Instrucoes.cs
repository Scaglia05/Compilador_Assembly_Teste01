using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_Assembly_Teste01.Classes {
    public class Instrucoes {

        public static Dictionary<string, int> ParseWordsToArray(string filePath, int TipoI, int TipoJ, int TipoR) {
            var resultado = new Dictionary<string, int>(); // Agora o dicionário mapeia o conteúdo da linha + a instrução para o número de ciclos
            var linhas = File.ReadAllLines(filePath);

            foreach (var linha in linhas) {
                string linhaLimpa = linha.Trim();

                // Ignora linhas vazias ou comentários
                if (string.IsNullOrEmpty(linhaLimpa) || linhaLimpa.StartsWith("#"))
                    continue;

                // Quebra a linha em palavras
                var tokens = linhaLimpa.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length == 0)
                    continue;

                string instrucao = tokens[0].ToLower(); // Instrução em minúsculo para garantir compatibilidade com o dicionário

                if (TabelaInstrucoes.Instrucoes.TryGetValue(instrucao, out string tipo)) {
                    int ciclos = tipo switch {
                        "R" => TipoR,
                        "I" => TipoI,
                        "J" => TipoJ,
                        _ => 0
                    };

                    // Mapeia a linha lida + a instrução diretamente para o número de ciclos
                    resultado[$"{linhaLimpa} ==> ({instrucao})"] = ciclos;
                } else {
                    // Se a instrução não estiver na tabela, pode ser registrada como "Desconhecida" com ciclos 1
                    resultado[$"{linhaLimpa} ==> {linhaLimpa}"] = 1;
                }
            }

            return resultado;
        }
    }
}
