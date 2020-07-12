FROM mcr.microsoft.com/dotnet/core/sdk:3.1

WORKDIR /usr/app

ENV ASPNETCORE_Kestrel__Certificates__Default__Password="sampl3"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path="/usr/app/profile/.aspnet/https/aspnetapp.pfx"

ENV PATH="${PATH}:/root/.dotnet/tools"

COPY . ./

RUN dotnet tool install --global dotnet-ef
RUN dotnet restore

EXPOSE 5001

CMD dotnet watch --project SampleFeaturesApi run --no-launch-profile