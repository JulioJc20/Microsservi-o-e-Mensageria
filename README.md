📌 Projeto: API com Mensageria e Consumo de CEP

🛠️ Tecnologias Utilizadas

.NET 6 - Framework principal da aplicação

Dapper - ORM para facilitar a comunicação com o banco de dados

SQL Server - Banco de dados relacional

RabbitMQ - Mensageria para comunicação entre as APIs

HttpClient - Para consumo de APIs externas

ViaCEP - API externa para consulta de CEP

📖 Descrição
Este projeto consiste em um sistema de mensageria que integra duas APIs:

API Publicadora:

Envia mensagens para a fila do RabbitMQ.
Consome a API ViaCEP para obter informações de endereço.
Chama a API Consumidora via HttpClient para buscar dados armazenados no banco.

API Consumidora:

Lê as mensagens da fila do RabbitMQ.
Processa e insere os dados no banco de dados (SQL Server).
Executa operações de inserção, atualização e exclusão de registros.

📂 Estrutura do Banco de Dados

O banco de dados contém duas tabelas principais:

Pessoas: Armazena os dados pessoais (Nome, CPF, RG, Data de Nascimento, etc.).
Endereço: Armazena os dados de moradia (CEP, Rua, Bairro, Cidade, Estado).

As tabelas estão relacionadas através do campo IdCep.

🚀 Funcionalidades

Cadastro de Pessoas com endereço integrado via CEP.
Atualização e Exclusão de dados através da API Consumidora.
Consulta de registros salvos no banco.
Filtragem por Nome para retornar dados específicos.

🔧 Como Executar

Configurar o RabbitMQ (localhost por padrão).
Rodar o SQL Server e executar os scripts de criação do banco.
Executar a API Consumidora antes da Publicadora.
