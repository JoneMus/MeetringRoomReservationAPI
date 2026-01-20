FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["MeetingRoomReservationAPI/MeetingRoomReservationAPI.csproj", "MeetingRoomReservationAPI/"]
RUN dotnet restore "MeetingRoomReservationAPI/MeetingRoomReservationAPI.csproj"

COPY . .
WORKDIR "/src/MeetingRoomReservationAPI"
RUN dotnet build "MeetingRoomReservationAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MeetingRoomReservationAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeetingRoomReservationAPI.dll"]