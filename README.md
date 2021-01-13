# microservices

CQRS DDD EventSource(EventStore) and Materialized view with mongo

Command with Net.Core
Query with Python&GO


run start.bat


POST - http://localhost:6000/create
[CODE]
{
    "id": "2b4d9300-bb65-48b2-9ade-1c2d16f06201",
    "owner": "sinan",
    "Products": [
        {
            "productId": "e7508e76-40cb-420e-a3ea-de65aeaf20e2",
            "name": "t-shirt",
            "quantity": 2,
            "unitPrice": 154
        },
        {
            "productId": "f02a45e2-fdc2-43ae-b00a-aca713cf7087",
            "name": "kazak",
            "quantity": 12,
            "unitPrice": 5
        }
    ]
}
[CODE]
