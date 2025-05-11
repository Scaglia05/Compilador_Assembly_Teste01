
using Compilador_Assembly_Teste01.Classes;

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

var intrucoes = Instrucoes.ParseWordsToArray(filePath, cyclesI,cyclesJ,cyclesR);
var intrucoes02 = Instrucoes.ParseWordsToArray(filePath, cyclesI, cyclesJ, cyclesR);
