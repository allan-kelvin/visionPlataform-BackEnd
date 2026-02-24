# 🚀 Vision Platform

> SaaS de Gestão e Controle de Qualidade de Software  
> Desenvolvido com .NET 8 + Clean Architecture + Angular 20

---

## 📌 Sobre o Projeto

O **Vision Platform** é um sistema SaaS voltado para gestão de qualidade de software (QA), controle de versões, tarefas, evidências e permissões granulares por usuário.

O objetivo é fornecer uma plataforma moderna, segura e escalável para:

- 📦 Controle de versões de sistemas
- 🧩 Gestão de tarefas por versão
- 🔀 Controle de merge, script e tag
- 🔐 Sistema real de permissões
- 📊 Dashboard inteligente
- 🏢 Organização por áreas e clientes
- 👤 Controle completo de usuários

---

# 🏗️ Arquitetura

O backend foi desenvolvido utilizando **Clean Architecture**, separando responsabilidades em camadas bem definidas.



---

## 🔹 Camadas

### 🧠 Domain
- Entidades
- Interfaces de repositório
- Regras de negócio puras  
Não depende de nenhuma outra camada.

---

### ⚙️ Application
- Services
- DTOs
- Interfaces de serviços
- Lógica de aplicação  
Depende apenas do Domain.

---

### 🗄️ Infrastructure
- DbContext
- Repositórios
- JWT
- Seeder automático
- Configurações EF Core  
Depende do Domain.

---

### 🌐 API
- Controllers
- Configuração JWT
- CORS
- Swagger
- Policies de autorização  
Depende de Application + Infrastructure.

---

# 🔐 Segurança

- Autenticação via JWT
- Autorização via Policy + PermissionRequirement
- Sistema real de permissões:
  - RolePermissions
  - UserPermissions
- BCrypt para hash de senha
- Controle de acesso por endpoint

---

# 🧩 Funcionalidades Implementadas

## ✅ Autenticação
- Login via email + senha
- Geração de token JWT
- Claims: Id, Email, Role

---

## ✅ Sistema de Permissões Real
- Permissões por cargo (RolePermission)
- Permissões customizadas por usuário (UserPermission)
- Handler customizado (`PermissionHandler`)
- Atributo `[HasPermission("Permission.Name")]`

---

## ✅ CRUD Completo

### 👤 Users
- Criar
- Listar
- Atualizar
- Excluir
- Controle por permissão

### 📦 Versions
- CRUD completo
- Bloqueio de alteração após liberação
- Bloqueio de liberação se houver tarefas sem merge

### 🧩 VersionTasks
- CRUD
- Assign QA
- Marcar Merge
- Controle de Script e Tag
- Relacionamento com:
  - Version
  - Area
  - Cliente
  - QA User

### 🏢 Areas
- CRUD completo

### 🧑‍💼 Clientes
- CRUD completo

---

## 📊 Dashboard Inteligente

- Total de versões
- Total de tarefas
- Versões liberadas
- Tarefas pendentes de merge
- Query otimizada (projection direta)

---

# 🗄️ Banco de Dados

- MySQL
- Entity Framework Core 8
- Migrations
- Seeder automático

---

## 🔥 Seeder Automático

Ao iniciar a aplicação:

- Executa migrations
- Cria Roles
- Cria Permissions
- Vincula permissões ao Administrador
- Cria usuário admin padrão

Login padrão:

```json
{
  "email": "admin@vision.com",
  "password": "123456"
}
````

 🧰 # Tecnologias Utilizadas

## Backend
.NET 8
ASP.NET Core
Entity Framework Core
MySQL
JWT
BCrypt
Swagger

## Frontend

Angular 20
Standalone API
Vite
Interceptors
Guards
Bootstrap


🚀 # Como Rodar o Projeto
🗄️ Backend
1️⃣ Criar banco

```sql
CREATE DATABASE visiondb;
````
## Rodar projeto
```bash
dotnet ef database update --project VisionPlatform.Infrastructure --startup-project VisionPlatform.API
```
## Swagger
```bash
dotnet run --project VisionPlatform.API
````
https://localhost:7293/swagger
