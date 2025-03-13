﻿namespace API_Publicador.Domain
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }


        public Pessoa()
        {
                
        }

        public Pessoa(string nome, string cpf, string rg, string sexo, DateTime dataNascimento, string email, string telefone)
        {
            Nome = nome;
            Cpf = cpf;
            Rg = rg;
            Sexo = sexo;
            DataNascimento = dataNascimento;
            Email = email;
            Telefone = telefone;
           
        }
    }
}
