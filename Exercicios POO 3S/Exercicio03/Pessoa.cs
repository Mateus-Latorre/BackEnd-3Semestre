using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercicio03
{
    public class Pessoa
    {
        public string Nome = "";
        public int Idade = 0;

        public void ExibirDados()
        {
            Console.WriteLine($"Nome: {Nome}");
            Console.WriteLine($"Idade: {Idade}");
        }
        public void AlterarIdade(int novaIdade)
        {
            if (novaIdade < 0)
            {
                Console.WriteLine("Idade não pode ser negativa. Idade permanece inalterada.");
            }
            else
            {
                Idade = novaIdade;
            }
        }
    }

}