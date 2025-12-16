# VeiculosAPI - Teste Prático .NET

API REST desenvolvida como parte do teste técnico para a vaga de Desenvolvedor .NET. O projeto gerencia o cadastro de veículos aplicando conceitos de **Clean Architecture**, **CQRS** e boas práticas de desenvolvimento moderno.

## 🚀 Tecnologias Utilizadas

O projeto segue estritamente as tecnologias obrigatórias solicitadas:

* **.NET 8** (LTS)
* **ASP.NET Core Web API**
* **Entity Framework Core** (Provider **InMemory**)
* **MediatR** (Padrão CQRS)
* **FluentValidation**
* **Swagger / OpenAPI**

## 🏗 Arquitetura e Decisões Técnicas

A solução foi organizada em camadas (`WebApi`, `Application`, `Domain`, `Infra`) visando baixo acoplamento e alta coesão.

### 1. CQRS e MediatR (Substituição do Service)
Para atender rigorosamente ao requisito de utilização do **MediatR** para todas as operações, a camada de serviço (`VeiculoService`) foi decomposta em **Handlers** (Comandos e Consultas).
* Isso adere ao princípio de Responsabilidade Única (SRP), onde cada classe é responsável por apenas uma operação de negócio (Criar, Atualizar, Listar, etc.).

### 2. Migrations com InMemory
Embora o provider `InMemory` não suporte migrações nativamente, foi utilizada uma abordagem *Code First* gerando as Migrations via provider temporário. Isso garante a existência da pasta `Migrations` no projeto `Infra` (conforme solicitado), validando o domínio sobre a criação e versionamento do esquema de banco de dados.

### 3. Diferenciais Implementados
Além dos requisitos funcionais, foram adicionados:
* **Soft Delete:** A exclusão via API é lógica (`Deletado = true`), preservando o histórico do registro.
* **Auditoria:** As entidades registram automaticamente a `DataCriacao` e `DataAlteracao`.
* **DTOs:** O retorno da API utiliza Data Transfer Objects para formatar dados (ex: converter Enum de Marca para texto) e proteger a estrutura interna da entidade.
* **Pipeline Behavior:** A validação do *FluentValidation* ocorre automaticamente no pipeline do MediatR antes de atingir os Handlers, garantindo retorno `400 Bad Request` padronizado.

## ⚙️ Como Executar o Projeto

### Pré-requisitos
* **SDK .NET 8** instalado (devido ao arquivo `global.json` presente na raiz que garante a consistência da versão).

### Passo a Passo
#### 1.  Clone o repositório ou extraia os arquivos.
#### 2.  Abra o terminal na pasta raiz da solução.
#### 3.  Restaure as dependências e compile o projeto:
    ```bash
    dotnet restore
    dotnet build
    ```
#### 4.  Execute a aplicação:
    ```bash
    dotnet run --project VeiculosAPI.WebApi/VeiculosAPI.WebApi.csproj --launch-profile "Development"
    ```
#### 5.  Acesse a documentação interativa (Swagger) em:
    * `https://localhost:5001/swagger` (ou a porta indicada no seu terminal).

---

## 📌 Exemplos de Uso (JSON)

Abaixo estão exemplos de cargas úteis para teste via Postman ou Swagger.

### 1. Cadastrar Veículo (POST)
**Endpoint:** `/api/Veiculos`
```json
{
  "descricao": "Honda Civic Touring",
  "marca": 6,
  "modelo": "Touring 1.5 Turbo",
  "valor": 120000.00,
  "opcionais": "Teto solar, Bancos em couro, Sensor de chuva"
}
```
Observação: O campo marca aceita o ID do Enum (ex: 6 para Honda).

### 2. Listar Veículos (GET)
Endpoint: /api/Veiculos

Retorna uma lista formatada (DTO):
```json
[
  {
    "id": 1,
    "descricao": "Honda Civic Touring",
    "marca": "Honda",
    "modelo": "Touring 1.5 Turbo",
    "valor": 120000.00,
    "opcionais": "Teto solar, Bancos em couro, Sensor de chuva",
    "dataCriacao": "2025-12-16T20:00:00.0000000-03:00"
  }
]
```

### 3. Atualizar Veículo (PUT)
Endpoint: /api/Veiculos/{id}
```json
{
  "id": 1,
  "descricao": "Honda Civic Touring - VENDIDO",
  "marca": 6,
  "modelo": "Touring 1.5 Turbo",
  "valor": 115000.00,
  "opcionais": "Teto solar, Bancos em couro"
}
```

### 4. Remover Veículo (DELETE)
Endpoint: /api/Veiculos/{id}

Realiza a exclusão lógica (Soft Delete). O veículo deixará de aparecer na listagem padrão.