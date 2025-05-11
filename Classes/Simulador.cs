using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_Assembly_Teste01.Classes {
    public class Simulador {
        public string FilePath { get; set; }
        public decimal ClockMHz { get; set; }
        public int CyclesR { get; set; }
        public int CyclesI { get; set; }
        public int CyclesJ { get; set; }

        // Tempo de um único ciclo de clock em segundos
        public decimal TempoClockUnicoSegundos => 1m / (ClockMHz * 1_000_000m);

        // Tempo total acumulado em segundos (você pode somar com base nos ciclos executados)
        public decimal ClockTotalSegundos { get; set; }

        public Dictionary<string, int> Labels { get; set; } = new();
        public Dictionary<string, int> Registradores { get; set; } = TabelaInstrucoes.Registradores.CriarRegistradores();
        public Memoria Memoria { get; set; } = new();
    }

}
