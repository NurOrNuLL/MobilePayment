# MobilePayment


## Используемый фреймворк

```csproj
<TargetFramework>net5.0</TargetFramework>
```

## Локализация через атрибут

```http
Accept-Language: ru-kz
```

## Пример healt-check

```http
GET https://localhost:5001/health
```

## Пример post запросов

```http

POST https://localhost:5001/payment
Content-Type: application/json
Accept-Language: ru-kz

{
  "phoneNumber": "7079239374",
  "amount": {{$randomInt}}
}



POST https://localhost:5001/payment
Content-Type: application/json
Accept-Language: kk-kz

{
  "phoneNumber": "7079239374",
  "amount": {{$randomInt}}
}


```


