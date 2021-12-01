# Securely store passwords in DB using hash (SHA256) + salt/pepper.

### Never store passwords in a clear format ! 👎❌

### Never send passwords in a clear format ! 👎❌

### Do not store passwords with a simple hash : if two users have the same password, they will end up with the same hash 🚱

### Reverse Hash / Rainbow tables can be used to find passwords : https://crackstation.net/

# Good habit for passwords : Hash + Salt 👍✔️

### Unsalted hashed passwords :
[Rainbow Tables SHA256] ![image]()

### Salted hashed passwords :
[Rainbow Tables SHA256 FAIL] ![image]()

### Database shape : 
[SQL Database] ![image](https://user-images.githubusercontent.com/64968597/144274970-7fe2c306-4481-480a-a4d8-59a03e7f3bf8.png)

