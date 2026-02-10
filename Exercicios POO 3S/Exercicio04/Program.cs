using Exercicio04;
Pessoa pessoa1 = new Pessoa("João", 25);
pessoa1.ExibirDados();
pessoa1.AlterarIdade(30);
pessoa1.ExibirDados();
Console.WriteLine($"");
Console.WriteLine($"");
Pessoa pessoa2 = new Pessoa("Maria", -5); // Idade negativa, deve ser tratada
pessoa2.ExibirDados();
pessoa2.AlterarIdade(-10); // Tentativa de alterar para idade negativa, deve ser tratada
pessoa2.ExibirDados();