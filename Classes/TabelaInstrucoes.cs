using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_Assembly_Teste01.Classes {
    public static class TabelaInstrucoes {
        public static Dictionary<string, string> Instrucoes = new()
        {
            { "add",  "R" }, { "sub",  "R"}, { "and",  "R"}, { "or",   "R"},
            { "nor",  "R"}, { "sll",  "R"}, { "srl",  "R"}, { "jr",   "R"},
            { "slt",  "R"}, { "sltu", "R"},

            { "addi", "I"}, { "andi", "I"}, { "ori",  "I"},
            { "lw",   "I"}, { "sw",   "I"}, { "lh",   "I"},
            { "sh",   "I"}, { "lb",   "I"}, { "sb",   "I"},
            { "beq",  "I"}, { "bne",  "I"}, { "slti", "I"}, { "sltiu", "I"},

            { "j",    "J" }, { "jal",  "J" }

        };
    }

}
