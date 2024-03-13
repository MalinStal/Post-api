
#Post API 

 lite luddig....

Done!
 ✔️ skapa ett github repo till detta projekt !! 
 ✔️ skapa en service för comments 
 ✔️ skapa en collection för comments 
 ✔️ koppla ihop comments med användare och post. 
 ✔️ skapa att man måste logga in för att kunna skapa post och comments.
Todo!

-se till att bara den som är kopplat till comments kan ta bort och ändra kommentaren. samt dubbelkolla att de funkar på post genom att skapa en ny user som försöker ta bort någon annans post. 
 
- En post ska nu också kunna innehålla flera bild filer. 
- skapa olika nivåer av användare, en user och en admin roll som kan hantera allas post och kommentarer. 
- få med namn på user i kommentarerna 

### bild hantering 
om vi tänker på vad som är rimmligt för en blogg är ju att kvalitet på bilder är viktigt vilket gör att bild storleken inte får vara för liten. Därför tror jag att det kan vara bra att ha typ 350kb som max file och att den filen om det behövs får delas upp i två byte[] om det inte funkar för databasen att hantera mindre filer. 

okej det som är gjort nu är filService som validerar filens innehåll med specifika krav och sedan skapar en fil som sparas i context. Frågan är ska filen sparas som comments i en lista i post? elelr hur gör vi med det? 

kolla över hur filen sparas plus att man ska ju kunna spara flera filer så detta behöver också ses över. juste vi får nog skapa en lista dirket i post servisen och sen skicka in den sparade lista till databasen nr vi skapar en ny post??? tror jag elelr så får man pusha varje img i en loop till den listan som är i post databsen efter att man skapat en post ?kanske enklare ?? fundera på lösningar och kolla vidare på detta :) 