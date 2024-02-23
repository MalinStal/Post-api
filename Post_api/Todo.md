
#Post API 

just nu så sparas inte all data som vi vill ha från en user och en post. Post och User conectar inte till varan och tror det är för att vi måste
 ha en lista i users med postdto som skapas ner i databasen. 
 Sen vet ja inte hur vi ska få alla post som läggs till att hamna i ätt users lista av posts. men antar att man måste hantera userService i 
 postService och filtrera på users och sedan lägga till i userPostlistan när man skapar en ny post.

 lite luddig....

 Todo! 
 ✔️ skapa ett github repo till detta projekt !!
 
 - hantera post listan i user med dto ev visevärsa och samma med comments 
 - när man skapar en ny post så ska detta kopplas ihop med en user och läggas in i den användarens lista av posts. 

 när relationen mellan user och post funkar...

 -skapa en service för comments 
 -skapa en colection för comments 
 -koppla ihop commetns med användare och post. 

 tror inte vi behöver göra så mycket mer när dessa relationer är kopplade. 



