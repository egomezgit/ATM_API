# ATM API

## Requisitos

- Docker
- Docker Compose

## Ejecución

1. Clonar el repositorio.
2. Ejecutar el siguiente comando:

```bash
docker-compose up --build
```

## Documentación de la API

La documentación de la API está disponible a través de Swagger. Para acceder a la documentación, sigue estos pasos:

1. Inicia la aplicación utilizando Docker Compose como se indicó anteriormente.
2. Abre tu navegador web y navega a `https://localhost:5001/swagger`.

## Ejemplos de Consumo de Endpoints

### Endpoint: Login

**URL**: `POST /api/auth/login`

**Request (Postman)**:
1. Selecciona el método `POST`.
2. Ingresa la URL `https://localhost:5001/api/auth/login`.
3. En la pestaña `Body`, selecciona `raw` y `JSON`.
4. Ingresa el siguiente JSON:

```json
{
  "cardNumber": "1234567890",
  "pin": "1234"
}
```
**Request (curl)**:

```curl
curl -X 'POST' \
  'https://localhost:5001/api/auth/login' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "cardNumber": "1234567890",
  "pin": "1234"
}'
```

**Response**:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJDYXJkTnVtYmVyIjoiMTIzNDU2Nzg5MCIsImV4cCI6MTc0MDUyMzkzMSwiaXNzIjoiQVRQTVNlcnZpY2VzV2ViQ29yZUlkZW50aXR5IiwiYXVkIjoiQVRQTVNlcnZpY2VzV2ViQ29yZUlkZW50aXR5VXNlciJ9.pAFrqJDQKPGFWwsl6jh70dGDzC-tJ3r_tRbsAtyzlCE"
}
```

### Endpoint: Retiro

**URL**: `POST /api/transaction/withdraw`

**Request (Postman)**:
1. Selecciona el método `POST`.
2. Ingresa la URL `https://localhost:5001/api/transaction/withdraw`.
3. En la pestaña `Body`, selecciona `raw` y `JSON`.
4. Ingresa el siguiente JSON:
```json
{
  "cardNumber": "9876543210",
  "amount": 10
}
```
**Request (curl)**:

```curl
curl -X 'POST' \
  'https://localhost:5001/api/Transaction/withdraw' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJDYXJkTnVtYmVyIjoiMTIzNDU2Nzg5MCIsImV4cCI6MTc0MDUyMzkzMSwiaXNzIjoiQVRQTVNlcnZpY2VzV2ViQ29yZUlkZW50aXR5IiwiYXVkIjoiQVRQTVNlcnZpY2VzV2ViQ29yZUlkZW50aXR5VXNlciJ9.pAFrqJDQKPGFWwsl6jh70dGDzC-tJ3r_tRbsAtyzlCE' \
  -H 'Content-Type: application/json' \
  -d '{
  "cardNumber": "9876543210",
  "amount": 10
}'
```

**Response**:
```json

{
  "success": true,
  "newBalance": 490,
  "message": "Transaction successful."
}
```
### Endpoint: Operacion

**URL**: `POST /api/Transaction/history`

**Request (Postman)**:
1. Selecciona el método `POST`.
2. Ingresa la URL `https://localhost:5001/api/Transaction/history`.
3. En la pestaña `Body`, selecciona `raw` y `JSON`.
4. Ingresa el siguiente JSON:
```json
{
  "cardNumber": "9876543210",
  "page": 3
}
```
**Request (curl)**:
```curl
curl -X 'POST' \
  'https://localhost:5001/api/Transaction/history' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJDYXJkTnVtYmVyIjoiMTIzNDU2Nzg5MCIsImV4cCI6MTc0MDUyMzkzMSwiaXNzIjoiQVRQTVNlcnZpY2VzV2ViQ29yZUlkZW50aXR5IiwiYXVkIjoiQVRQTVNlcnZpY2VzV2ViQ29yZUlkZW50aXR5VXNlciJ9.pAFrqJDQKPGFWwsl6jh70dGDzC-tJ3r_tRbsAtyzlCE' \
  -H 'Content-Type: application/json' \
  -d '{
  "cardNumber": "9876543210",
  "page": 3
}'
```

**Response**:
```json
{
  "totalRecords": 21,
  "currentPage": 3,
  "totalPages": 3,
  "transactions": [
    {
      "cardNumber": null,
      "amount": -100,
      "transactionDate": "2025-02-05T21:51:08.010235Z",
      "transactionType": null
    }
  ]
}}
```

### Endpoint: Saldo

**URL**: `POST /api/balance/{cardNumber}`

**Request (Postman)**:
1. Selecciona el método `POST`.
2. Ingresa la URL `https://localhost:5001/api/balance`.
3. En la pestaña `Body`, selecciona `raw` y `JSON`.
4. Ingresa el siguiente JSON:
```json
{
  "cardNumber": "9876543210"
}
```
**Request (curl)**:
```curl
curl -X 'POST' \
  'https://localhost:5001/api/Transaction/balance' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJDYXJkTnVtYmVyIjoiMTIzNDU2Nzg5MCIsImV4cCI6MTc0MDUyMzkzMSwiaXNzIjoiQVRQTVNlcnZpY2VzV2ViQ29yZUlkZW50aXR5IiwiYXVkIjoiQVRQTVNlcnZpY2VzV2ViQ29yZUlkZW50aXR5VXNlciJ9.pAFrqJDQKPGFWwsl6jh70dGDzC-tJ3r_tRbsAtyzlCE' \
  -H 'Content-Type: application/json' \
  -d '{
  "cardNumber": "9876543210"
}'
```

**Response**:
```json
{
  "userName": "Jane Smith",
  "accountNumber": "ACC789012",
  "balance": 490,
  "lastTransactionDate": "2025-02-25T21:55:27.161922Z"
}
```


### Configuración de JWT

La autenticación JWT está configurada en el archivo `Program.cs`. Asegúrate de actualizar los valores de `Issuer`, `Audience` y `Key` en el archivo `appsettings.json`.


### Migraciones de Base de Datos

Las migraciones de la base de datos se ejecutan automáticamente al iniciar la aplicación si hay cambios pendientes. Asegúrate de revisar el archivo `Program.cs` para más detalles.

### Diagrama de Entidad-Relacion 


### Diagrama de Entidad-Relación

![Diagrama de Entidad-Relación](./DER.png)









