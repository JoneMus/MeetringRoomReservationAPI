# Meeting Room Reservation API
Tämä on C#-kielellä ja .NET-ympäristössä toteutettu REST-rajapinta kokoushuoneiden varaamiseen.

## Ominaisuudet
- Varausten hallinta: Luo, peruuta ja listaa huonekohtaisia varauksia.
- Validointi:
  - Estää päällekkäiset varaukset samaan huoneeseen.
  - Estää menneisyyteen sijoittuvat varaukset.
  - Varmistaa, että varattava huone on olemassa järjestelmässä.
- Yksikkötestit
## Teknologiat
- Runtime: .NET 8.0
- Framework: ASP.NET Core Web API (Controller-malli)
- Tietokanta: Entity Framework Core In-Memory
- Dokumentaatio: Swagger
- Testaus: xUnit
## Käyttöönotto
- .NET SDK asennettuna
- Valinnainen: Postman tai vastaava työkalu testaukseen (Swagger on myös käytettävissä).

## Sovelluksen ajaminen
1. Kloonaa tai lataa projekti koneellesi.
2. Avaa terminaali projektin juurikansiossa.
3. Palauta riippuvuudet ja käynnistä sovellus:
```
dotnet restore
dotnet run --project MeetingRoomReservationAPI
```
4. Avaa selain ja siirry osoitteeseen (portti voi vaihdella): http://localhost:5058/swagger (tai terminaalin ilmoittama osoite).

## Sovelluksen ajaminen Dockerissa
1. Kloonaa tai lataa projekti koneellesi.
2. Avaa terminaali projektin juurikansiossa.
3. Rakenna kontti:
``docker build -t meeting-room-api .``
4. Käynnistä kontti:
``docker run -d -p 8080:8080 --name reservation-api -e ASPNETCORE_ENVIRONMENT=Development meeting-room-api``
5. Rajapinta saatavissa nyt osoitteesta: http://localhost:8080/swagger

## Testien ajaminen
``dotnet test``

## API-rajapinnan käyttö
Esimerkki varauksen luomisesta
POST ``/api/bookings``
```
{
  "meetingRoomId": 1,
  "startTime": "2026-06-01T10:00:00",
  "endTime": "2026-06-01T11:00:00",
  "reservedBy": "Kehittäjä Ehdokas"
}
```
Testaamista varten tietokantaan on lisätty seuraavat huoneet:
- ID 1: Neukkari Havu
- ID 2: Studio Kivi
- ID 3: Auditorio Meri
