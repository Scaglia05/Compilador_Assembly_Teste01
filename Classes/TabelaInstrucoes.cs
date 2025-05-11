using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_Assembly_Teste01.Classes {
    public class TabelaInstrucoes {
        public Dictionary<string, int> registradores = new();

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

        public static class Registradores {
            public static Dictionary<string, int> CriarRegistradores() {
                return new Dictionary<string, int>
                {
                    { "$zero", 0 },
                    { "$v0", 0 }, { "$v1", 0 },
                    { "$a0", 0 }, { "$a1", 0 }, { "$a2", 0 }, { "$a3", 0 },
                    { "$t0", 0 }, { "$t1", 0 }, { "$t2", 0 }, { "$t3", 0 },
                    { "$t4", 0 }, { "$t5", 0 }, { "$t6", 0 }, { "$t7", 0 },
                    { "$s0", 0 }, { "$s1", 0 }, { "$s2", 0 }, { "$s3", 0 },
                    { "$s4", 0 }, { "$s5", 0 }, { "$s6", 0 }, { "$s7", 0 },
                    { "$t8", 0 }, { "$t9", 0 },
                    { "$gp", 0 }, { "$sp", 0 }, { "$fp", 0 }, { "$ra", 0 },
                    { "PC", 0 }
                };
            }
        }

    }

}
