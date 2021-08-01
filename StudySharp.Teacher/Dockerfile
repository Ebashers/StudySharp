FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["StudySharp.Teacher/StudySharp.Teacher.csproj", "StudySharp.Teacher/"]
RUN dotnet restore "StudySharp.Teacher/StudySharp.Teacher.csproj"
COPY . .
WORKDIR "/src/StudySharp.Teacher"
RUN dotnet build "StudySharp.Teacher.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StudySharp.Teacher.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet StudySharp.Teacher.dll
