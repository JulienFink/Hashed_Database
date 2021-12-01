# Securely store passwords in a database using SHA256 hashes

### Never store passwords in a clear format ! 👎❌

### Do not store passwords with a simple hash : if two users have the same password, they will end up with the same hash 🚱

### Reverse Hash / Rainbow tables can be used to find passwords : https://crackstation.net/

# Good habits : pwd_stored = hash(pwd + random_salt)  👍✔️

### Unsalted hashed passwords :
[Rainbow Tables SHA256] ![image](https://user-images.githubusercontent.com/64968597/144275573-f772f84c-0805-41be-9458-9968713e0528.png)

### Salted hashed passwords :
[Rainbow Tables SHA256 FAIL] ![image](https://user-images.githubusercontent.com/64968597/144275315-3936af5a-b0c0-4ab1-ad66-371f0f0f5cf3.png)

### Database shape : 
[SQL Database] ![image](https://user-images.githubusercontent.com/64968597/144274970-7fe2c306-4481-480a-a4d8-59a03e7f3bf8.png)

