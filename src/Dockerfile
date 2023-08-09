#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Set up baseStage to prepare clean enviroment before copying
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS basestage
# Set working dir as /app
WORKDIR /app
# Expose ports
#Http
#EXPOSE 1112
EXPOSE 80
#Grpc
#EXPOSE 1113

# Setup build stage 
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS buildstage
# Set working dir as /src
WORKDIR /src
# Copy the source code into the /src/code folder
COPY ["Application", "code/Application"]
COPY ["Domain", "code/Domain"]
COPY ["Infrastructure", "code/Infrastructure"]
COPY ["WebApi", "code/WebApi"]
RUN pwd 
RUN ls /src/code
RUN ls /src/code/WebApi/
RUN dotnet restore /src/code/Domain/Domain.csproj
RUN dotnet restore /src/code/Application/Application.csproj
RUN dotnet restore /src/code/Infrastructure/Infrastructure.csproj
RUN dotnet restore /src/code/WebApi/WebApi.csproj
RUN ls .
#COPY . .
#RUN ls .
#WORKDIR "/src/code"
#RUN dotnet build "Identity.WebApi/Identity.WebApi.csproj" -c Release -o /src/build

# Build published files of the prepared code
FROM buildstage AS publishstage
WORKDIR "/src/code"
RUN pwd
RUN ls
RUN dotnet publish "WebApi/WebApi.csproj" -c Release -o /src/build
# Copy published code
FROM basestage AS finalstage
WORKDIR /app
RUN ls .
# Copy the published code into /app folder
COPY --from=publishstage /src/build .
RUN ls /app
#RUN apt-get update
#RUN apt-get install telnet -y
#RUN apt-get install net-tools -y
#RUN apt-get install iputils-ping -y
#RUN apt-get install htop -y
#RUN apt-get install vim -y
#RUN apt-get install lsof -y
ENTRYPOINT ["dotnet", "WebApi.dll"]
