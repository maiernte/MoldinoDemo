# Worker Service
Der Worker Service ist ein ASP.NET Core Web API Service, der erfordert .net8.0 SDK oder höher. In IDE die Configuration "Worker: http" wählen und Debuggen starten.


# Test Service
Der Server dient sich per Port 5000. Weil nur einer Controller implementiert wurde, die Basis-URL ist:
```
http://localhost:5000/TextValidation/
```
## Wörter/Buchstaben zählen
In Postman erstellt eine POST-Anfrage mit der URL und sendet sie:
```
http://localhost:5000/textvalidation/count
```
Wählt den Body-Typ als "raw" und den Text als JSON-Objekt:
```
{
    "Text": "Dies ist ein Testtext.",
    "Words": "ist",
}
```

Falls mehrere Wörter gezählt werden sollen, packt sie in "Words" und getrennt mit Semikolon, z.B. "ist;ein".

## Überprüfen Base64 Codierung
In Postman erstellt eine POST-Anfrage mit der URL und sendet sie:
``` 
http://localhost:5000/textvalidation/isbase64
```

Wählt den Body-Typ als "raw" und den Text als JSON-Objekt:
```
{
    "Text": "VGhpcyBpcyBhIHN0cmluZyB0ZXh0Lg=="
}
```

## Überprüfen Email-Adresse
In Postman erstellt eine POST-Anfrage mit der URL und sendet sie:
```
http://localhost:5000/textvalidation/isemail
```

Wählt den Body-Typ als "raw" und den Text als JSON-Objekt:
```
{
    "Text": "muster@moldino.com"
}
```


# Test der Funktionen

Unter die NUnit-Test Projekt "TextValidatorTest" findet man 8 Test-Methode, die jeweils die angeforderte Aufgaben entspricht. Man kann weitere Besipiele in die Test-Methode hinzufügen, um die Funktionen zu testen.
