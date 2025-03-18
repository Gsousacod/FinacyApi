# FinacyApi 📊💰

FinacyApi é uma API financeira desenvolvida em **ASP.NET Core** e **Entity Framework Core**, permitindo o gerenciamento de usuários, receitas, despesas e metas financeiras.  
Ela oferece uma arquitetura bem estruturada, utilizando **ViewModels** para melhor separação de responsabilidades e segurança.  

---

## 📌 **Índice**
- [Tecnologias Utilizadas](#-tecnologias-utilizadas)
- [Configuração do Projeto](#-configuração-do-projeto)
- [Executando a API](#-executando-a-api)
- [Estrutura do Projeto](#-estrutura-do-projeto)
- [Modelos da API](#-modelos-da-api)
- [Endpoints](#-endpoints)
- [Melhorias Futuras](#-melhorias-futuras)
- [Autor](#-autor)

---

## 🚀 **Tecnologias Utilizadas**
- **ASP.NET Core 7.0**
- **Entity Framework Core**
- **PostgreSQL (Banco de Dados)**
- **Swagger (Documentação da API)**
- **AutoMapper (Mapeamento de DTOs/ViewModels)**

---

## 🛠 **Configuração do Projeto**

## **1️⃣ Instalar o SDK do .NET Core**
Antes de iniciar, instale o .NET SDK compatível.  
Baixe aqui: [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)

## **2️⃣ Clonar o Repositório**
```sh
git clone https://github.com/seu-usuario/FinacyApi.git
cd FinacyApi
```

## **3️⃣ Configurar o Banco de Dados**
Edite o arquivo appsettings.json com as credenciais do seu banco de dados:

```sh
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=FinacyDb;Username=postgres;Password=suasenha"
}
```
4️⃣ Aplicar Migrations
Execute os comandos abaixo para criar o banco:

```sh

dotnet ef migrations add InitialCreate
dotnet ef database update
```
▶️ Executando a API
Para iniciar o servidor, use:

```sh

dotnet run
```
A API estará disponível em:
🔗 http://localhost:5000

Para acessar a documentação via Swagger, abra:
🔗 http://localhost:5000/swagger

## 📂 **Estrutura do Projeto**

```plaintext
📁 FinacyApi
│── 📁 Controllers      # Controladores da API
│── 📁 Models             # Modelos de Dados
│── 📁 ViewModels         # DTOs para resposta segura
│── 📁 Data               # Contexto do banco de dados
│── 📁 Migrations         # Arquivos de migração do EF Core
│── `appsettings.json`       # Configurações da API
│── `Program.cs`             # Ponto de entrada da aplicação
```
---
## 📡 **Endpoints**

### 🧑‍💼 **Usuários**
| Método | Rota             | Descrição                    |
|--------|------------------|------------------------------|
| GET    | `/api/users`      | Lista todos os usuários      |
| GET    | `/api/users/{id}` | Retorna um usuário específico|
| POST   | `/api/users`      | Cria um novo usuário         |
| PUT    | `/api/users/{id}` | Atualiza um usuário          |
| DELETE | `/api/users/{id}` | Remove um usuário            |

### 💰 **Receitas**
| Método | Rota             | Descrição                    |
|--------|------------------|------------------------------|
| GET    | `/api/revenues`   | Lista todas as receitas      |
| POST   | `/api/revenues`   | Adiciona uma nova receita    |
| GET    | `/api/revenues/{id}` | Retorna uma receita específico|
| PUT    | `/api/revenues/{id}` | Atualiza uma receita          |
| DELETE | `/api/revenues/{id}` | Remove uma receita            |

### 💸 **Despesas**
| Método | Rota             | Descrição                    |
|--------|------------------|------------------------------|
| GET    | `/api/expenses`   | Lista todas as despesas      |
| POST   | `/api/expenses`   | Adiciona uma nova despesa    |
| GET    | `/api/expense/{id}` | Retorna uma despesa específico|
| PUT    | `/api/expense/{id}` | Atualiza uma despesa          |
| DELETE | `/api/expense/{id}` | Remove uma despesa            |

### 🎯 **Metas Financeiras**
| Método | Rota             | Descrição                    |
|--------|------------------|------------------------------|
| GET    | `/api/metas`      | Lista todas as metas financeiras |
| POST   | `/api/metas`      | Adiciona uma nova meta financeira |
| GET    | `/api/metas/{id}` | Retorna uma meta financeira específico|
| PUT    | `/api/metas/{id}` | Atualiza uma meta financeira          |
| DELETE | `/api/metas/{id}` | Remove uma meta financeira            |


---
## 🚀 **Melhorias Futuras**
- ✅ Autorização com base no "role" de cada usuário.
- ✅ Implementação de logs
- 📌 Notificações sobre vencimento de contas
- ✅ Relatórios financeiros em PDF
---

## 👨‍💻 **Autor**
- Desenvolvido por [Gabriel] 🚀
- Entre em contato: 📩 gs5103809@gmail.com
- GitHub: github.com/Gsousacod

📌 Gostou do projeto? Deixe uma estrela ⭐ no GitHub!
---

## ✅ **O que esse README cobre?**
✔ Explicação detalhada do projeto  
✔ Instruções claras para instalação  
✔ Estrutura organizada  
✔ Endpoints da API bem documentados  
✔ Melhorias futuras  

