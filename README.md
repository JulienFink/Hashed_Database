# Securely store passwords in DB using hash (SHA256) + salt / pepper.

### Never store passwords in a clear format ! 👎❌

### Never send passwords in a clear format ! 👎❌

### Do not store passwords with a simple Hash : if two users have the same password, they will end up with the same hash 🚱

### Reverse Hash / Rainbow tables can be used to find passwords.

# __Good habit for passwords : Hash + Salt + Pepper__ 👍✔️

### Unsalted hashed passwords :
[Rainbow Tables SHA256] ![image](https://user-images.githubusercontent.com/64968597/113207408-7c06b300-9268-11eb-8c28-f8f904fd9b9d.JPG)

### Salted hashed passwords :
[Rainbow Tables SHA256 FAIL] ![image](https://user-images.githubusercontent.com/64968597/113207650-c425d580-9268-11eb-8af1-036759a369c0.JPG)

