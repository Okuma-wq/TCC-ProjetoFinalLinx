# ProjetoLinx

## Como Rodar Migrations
Para executar as mitrations é necessário instalar a ferramenta do dotnet para EntityFramework, para instalala utilize o seguinte comando: 
```
dotnet tool install --global dotnet-ef
```

Obs: Certifique-se de que a string de conexão localizada na `appsettings.Development.json` está correta

Certifique-se de que o terminal está na pasta do projeto com o comando:
```
cd ProjetoLinx.webApi
```

Para executar as migrations é necessário estar no diretório do projeto e executar o seguinte comando:
```
dotnet ef Database update
```