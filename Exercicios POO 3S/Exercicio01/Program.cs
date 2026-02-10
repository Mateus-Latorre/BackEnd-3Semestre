using System;
Pessoa p1 = new Pessoa();
Console.WriteLine("Digite o nome da pessoa:");
p1.Nome = Console.ReadLine();
Console.WriteLine("Digite a idade da pessoa:");
p1.Idade = int.Parse(Console.ReadLine());
Console.WriteLine($"O nome da pessoa é {p1.Nome} e a idade é {p1.Idade} anos.");