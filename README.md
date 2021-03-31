# Securely store passwords in DB using hash (SHA256) + salt / pepper.

### Never store passwords in a clear format ! 👎❌

### Never send passwords in a clear format ! 👎❌

### Do not store passwords with a simple hash : if two users have the same password, they will end up with the same hash 🚱

### Reverse Hash / Rainbow tables can be used to find passwords : https://crackstation.net/

# Good habit for passwords : Hash + Salt + Pepper 👍✔️

### Unsalted hashed passwords :
[Rainbow Tables SHA256] ![image](https://user-images.githubusercontent.com/64968597/113207408-7c06b300-9268-11eb-8c28-f8f904fd9b9d.JPG)

### Salted hashed passwords :
[Rainbow Tables SHA256 FAIL] ![image](https://user-images.githubusercontent.com/64968597/113208787-1ddacf80-926a-11eb-9957-7087c8d0773a.JPG)

### Database shape : 
[SQL Database] ![image](https://user-images.githubusercontent.com/64968597/113208893-44006f80-926a-11eb-9c5d-15fb6c230ee0.JPG)

