﻿using System.Text.Json.Serialization;
using System.Text.Json;
using System.Runtime.Serialization;
using static API_Publicador.Services.ApiService;

namespace API_Publicador.Domain
{
    public class Endereco
    {
        [JsonPropertyName("cep")]
        public string Cep { get; set; }

        [JsonPropertyName("logradouro")]
        public string Logradouro { get; set; }

        [JsonPropertyName("bairro")]
        public string Bairro { get; set; }

        [JsonPropertyName("localidade")]
        public string Localidade { get; set; }

        [JsonPropertyName("uf")]
        public string Uf { get; set; }

    }


}
