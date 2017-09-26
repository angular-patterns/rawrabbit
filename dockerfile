FROM microsoft/dotnet:latest

COPY . /app

WORKDIR /app/send

RUN  dotnet restore && dotnet publish -c Release -o out

WORKDIR /app/send/out

CMD ["dotnet", "send.dll"]