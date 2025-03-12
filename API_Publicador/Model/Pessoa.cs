﻿using System.Text.Json.Serialization;
using System.Text.Json;
using System.Runtime.Serialization;
using static API_Publicador.Services.ApiService;
using API_Publicador.Model.Dto;

namespace API_Publicador.Domain
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
        public Endereco Endereco { get; set; }

    }

}
