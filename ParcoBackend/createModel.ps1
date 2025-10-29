# createModel.ps1
# Uso:
# ./createModel.ps1 "localhost" "minhaBase" "meuUsuario" "minhaSenha"

param (
    [string]$dbhost,
    [string]$database,
    [string]$user,
    [string]$password
)

# Monta a string de conexão do PostgreSQL
$strconn = "Host=$dbhost;Database=$database;Username=$user;Password=$password"

# Adiciona os pacotes necessários
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 7.0.14
dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.14

# Instala a ferramenta dotnet-ef (caso ainda não tenha)
dotnet tool install --global dotnet-ef

# Gera o modelo (DbContext + entidades)
dotnet ef dbcontext scaffold $strconn Npgsql.EntityFrameworkCore.PostgreSQL --force -o Model
