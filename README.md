# Facial-Recognition-IA-API
Api para reconhecimento facial em imagens utilizando o serviço de reconhecimento facial da Azure.

## Backend

### 1 - Especificações técnicas

- O backend é uma API desenvolvida na linguagem c#(dotnet core 5.0 ou mais recente) em que basicamente utiliza o serviço de reconhecimento facial da azure para análise das imagens.

### 2 - Configuração

- Para execução, somente é ter o dotnet 5.0 ou mais recente instalado e rodar a aplicação seja visual studio ou em um terminal com o comando ```dotnet run```.

### 3 - Funcionalidades

- Existem quatro endpoints na api sendo eles:

1) VerificaRosto:
Verifica se a imagem possui rosto e retorna a quantidade de rostos na imagem.

2) RetornaSentimentos:
Verifica o sentimento dos rostos existentes na imagem.

3) RetornarGeneros: Verifica o gênero dos rostos existentes na imagem .

4) RetornarMediaIdade: Verifica a média das idades dos rostos existentes na imagem.

 

### 4 - Testando

- Para testar a api localmente, somente é necessário inicialmente executar a aplicação(explicado no passo 2 de configuração) e então fazer a requisição para http://localhost:5000/FaceRecognition seguido de um dos endpoints especificados na seção 3 de funcionalidades, passando no cabeçalho da requisição a url para a imagem.

### 5 - Documentação

- Segue link para documentação da api no postman, contendo requisições de exemplo para teste e esclarecimento de dúvidas: https://documenter.getpostman.com/view/12304172/TVmJhJv3
