FROM mcr.microsoft.com/dotnet/core/sdk:3.1

WORKDIR /usr/app

ENV ASPNETCORE_Kestrel__Certificates__Default__Password="sampl3"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path="/usr/app/profile/.aspnet/https/aspnetapp.pfx"

ENV PATH="${PATH}:/root/.dotnet/tools"

RUN dotnet tool install --global dotnet-ef

COPY . ./

RUN dotnet restore SampleFeaturesApi/SampleFeaturesApi.csproj

EXPOSE 5001

CMD dotnet watch --project SampleFeaturesApi/SampleFeaturesApi.csproj run --no-launch-profile