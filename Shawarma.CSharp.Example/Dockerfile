FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Shawarma.CSharp.Example/Shawarma.CSharp.Example.csproj", "Shawarma.CSharp.Example/"]
RUN dotnet restore "Shawarma.CSharp.Example/Shawarma.CSharp.Example.csproj"
COPY . .
WORKDIR "/src/Shawarma.CSharp.Example"
RUN dotnet build "Shawarma.CSharp.Example.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shawarma.CSharp.Example.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shawarma.CSharp.Example.dll"]