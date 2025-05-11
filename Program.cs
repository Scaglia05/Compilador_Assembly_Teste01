using InstrucoesApp = Compilador_Assembly_Teste01.Classes.Instrucoes;
using Compilador_Assembly_Teste01.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Compilador_Assembly_Teste01.Classes.TabelaInstrucoes;

Console.WriteLine("Digite o caminho do arquivo:");
string filePath = Console.ReadLine();

if (!File.Exists(filePath)) {
    Console.WriteLine("Erro: O arquivo especificado não foi encontrado.");
    return;
}

Console.WriteLine("Digite o clock em MHz (ex: 1000000Hz para 1 MHz):");
if (!decimal.TryParse(Console.ReadLine(), out decimal clockMHz) || clockMHz <= 0) {
    Console.WriteLine("Erro: O valor do clock deve ser um número positivo.");
    return;
}

Console.WriteLine("Digite a quantidade de ciclos para as instruções tipo R:");
if (!int.TryParse(Console.ReadLine(), out int cyclesR) || cyclesR <= 0) {
    Console.WriteLine("Erro: O valor de ciclos para instruções tipo R deve ser um número positivo.");
    return;
}

Console.WriteLine("Digite a quantidade de ciclos para as instruções tipo I:");
if (!int.TryParse(Console.ReadLine(), out int cyclesI) || cyclesI <= 0) {
    Console.WriteLine("Erro: O valor de ciclos para instruções tipo I deve ser um número positivo.");
    return;
}

Console.WriteLine("Digite a quantidade de ciclos para as instruções tipo J:");
if (!int.TryParse(Console.ReadLine(), out int cyclesJ) || cyclesJ <= 0) {
    Console.WriteLine("Erro: O valor de ciclos para instruções tipo J deve ser um número positivo.");
    return;
}

decimal tempoClockUnicoSegundos = 1m / (clockMHz * 1_000_000m);

Console.WriteLine($"Arquivo: {filePath}");
Console.WriteLine($"Clock: {clockMHz} MHz");
Console.WriteLine($"Tempo de um ciclo de clock: {tempoClockUnicoSegundos} segundos");
Console.WriteLine($"Ciclos tipo R: {cyclesR}");
Console.WriteLine($"Ciclos tipo I: {cyclesI}");
Console.WriteLine($"Ciclos tipo J: {cyclesJ}");

Memoria memoria = new Memoria();
var labels = new Dictionary<string, int>();
var registradores = Registradores.CriarRegistradores();

var ciclosInstrucoes = InstrucoesApp.ParseWordsToArray(filePath, cyclesI, cyclesJ, cyclesR, tempoClockUnicoSegundos);

Console.WriteLine("Instruções com ciclos:");
foreach (var item in ciclosInstrucoes) {
    Console.WriteLine($"{item.Key} => {item.Value} ciclos");
}

var linhasPrograma = File.ReadAllLines(filePath);

for (int i = 0; i < linhasPrograma.Length; i++) {
    var linha = linhasPrograma[i].Trim();
    if (string.IsNullOrWhiteSpace(linha) || linha.StartsWith("#")) continue;
    if (linha.EndsWith(":")) {
        string nomeLabel = linha.Substring(0, linha.Length - 1).Trim();
        labels[nomeLabel] = i;
    }
}

InstrucoesApp instrucoes = new InstrucoesApp();
int pc = 0;


while (pc < linhasPrograma.Length) {
    var linha = linhasPrograma[pc].Trim();

    if (string.IsNullOrWhiteSpace(linha) || linha.StartsWith("#") || linha.EndsWith(":")) {
        pc++;
        continue;
    }

    var (instrucao, operandos) = InstrucoesApp.ParseInstrucao(linha);

    if (!string.IsNullOrEmpty(instrucao) && operandos != null) {
        Totalizador.TotalInstrucoes++;

        if (ciclosInstrucoes.TryGetValue(instrucao, out int ciclosInstrucao)) {
            Totalizador.TotalCiclos += ciclosInstrucao;
            //Totalizador.TempoTotalSegundos += ciclosInstrucao * tempoClockUnicoSegundos;
        }

        instrucoes.Executar(instrucao, operandos, registradores, memoria, labels, pc, ciclosInstrucoes, tempoClockUnicoSegundos);

        if (instrucao.StartsWith("j")) {
            string label = operandos.FirstOrDefault();
            if (labels.ContainsKey(label)) {
                pc = labels[label];
            } else {
                Console.WriteLine($"Erro: Label {label} não encontrada.");
                break;
            }
        } else if (instrucao.StartsWith("b")) {
            pc = registradores["PC"];
        } else {
            pc++;
        }

    } else {
        Console.WriteLine($"Erro ao processar a linha: {linha}. A instrução não foi válida.");
        break;
    }
}

Console.WriteLine($"\nTotais:");
Console.WriteLine($"Total de Instruções Executadas: {Totalizador.TotalInstrucoes}");
Console.WriteLine($"Total de Ciclos: {Totalizador.TotalCiclos}");
Console.WriteLine($"Tempo Total Estimado de Execução: {Totalizador.TempoTotalSegundos} segundos");

