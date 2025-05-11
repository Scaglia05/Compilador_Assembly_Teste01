using InstrucoesApp = Compilador_Assembly_Teste01.Classes.Instrucoes;
using Compilador_Assembly_Teste01.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Compilador_Assembly_Teste01.Classes.TabelaInstrucoes;

Console.WriteLine("Digite o caminho do arquivo:");
string filePath = Console.ReadLine();

// Verificar se o arquivo existe
if (!File.Exists(filePath)) {
    Console.WriteLine("Erro: O arquivo especificado não foi encontrado.");
    return;
}

// Passo 2: Solicitar o clock em Hz
Console.WriteLine("Digite o clock em Hz (ex: 1000000 para 1 MHz):");
if (!int.TryParse(Console.ReadLine(), out int clockHz) || clockHz <= 0) {
    Console.WriteLine("Erro: O valor do clock deve ser um número positivo.");
    return;
}

// Passo 3: Solicitar a quantidade de ciclos para as instruções
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

// Exibir os valores inseridos
Console.WriteLine($"Arquivo: {filePath}");
Console.WriteLine($"Clock: {clockHz} Hz");
Console.WriteLine($"Ciclos tipo R: {cyclesR}");
Console.WriteLine($"Ciclos tipo I: {cyclesI}");
Console.WriteLine($"Ciclos tipo J: {cyclesJ}");

// Inicializar memória, registradores e labels
Memoria memoria = new Memoria();
var labels = new Dictionary<string, int>();
var registradores = Registradores.CriarRegistradores();

// Passo 4: Utilizar o ParseWordsToArray para obter o dicionário de ciclos e instruções
var ciclosInstrucoes = InstrucoesApp.ParseWordsToArray(filePath, cyclesI, cyclesJ, cyclesR);

// Exibir o dicionário de instruções com ciclos
Console.WriteLine("Instruções com ciclos:");
foreach (var item in ciclosInstrucoes) {
    Console.WriteLine($"{item.Key} => {item.Value} ciclos");
}

// Ler o programa
var linhasPrograma = File.ReadAllLines(filePath);

// Primeira leitura: identificar e mapear labels
for (int i = 0; i < linhasPrograma.Length; i++) {
    var linha = linhasPrograma[i].Trim();

    // Ignora comentários e linhas vazias
    if (string.IsNullOrWhiteSpace(linha) || linha.StartsWith("#")) continue;

    // Verifica se é um label (termina com :)
    if (linha.EndsWith(":")) {
        string nomeLabel = linha.Substring(0, linha.Length - 1).Trim();
        labels[nomeLabel] = i; // mapeia label para linha do código
    }
}

// Instanciar a classe InstrucoesApp (que é um alias para Instrucoes)
InstrucoesApp instrucoes = new InstrucoesApp();

// Configurar o Program Counter (PC)
int pc = 0;

// Segunda leitura: executar as instruções
while (pc < linhasPrograma.Length) {
    var linha = linhasPrograma[pc].Trim();

    if (string.IsNullOrWhiteSpace(linha) || linha.StartsWith("#") || linha.EndsWith(":")) {
        pc++;  // Ignorar linhas vazias, comentários ou labels
        continue;
    }

    // Usando o ParseInstrucao para separar a instrução e os operandos
    var (instrucao, operandos) = InstrucoesApp.ParseInstrucao(linha);

    // Verifica se a instrução e operandos foram corretamente extraídos
    if (!string.IsNullOrEmpty(instrucao) && operandos != null) {
        // Executar a instrução
        instrucoes.Executar(instrucao, operandos, registradores, memoria, labels);

        if (instrucao.StartsWith("j")) {
            string label = operandos.FirstOrDefault();  // A label para o salto
            if (labels.ContainsKey(label)) {
                pc = labels[label];  // Atualiza o PC para o endereço da label de destino
            } else {
                Console.WriteLine($"Erro: Label {label} não encontrada.");
                break;
            }
        } else if (instrucao.StartsWith("b")) {
            // O registrador "PC" já foi atualizado dentro da instrução "beq" ou "bne"
            pc = registradores["PC"];
        } else {
            pc++;  // Instrução comum
        }
    } else {
        Console.WriteLine($"Erro ao processar a linha: {linha}. A instrução não foi válida.");
        break;
    }
}
