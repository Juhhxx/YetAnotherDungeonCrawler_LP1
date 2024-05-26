# Yet Another Dungeon Crawler

## Autoria

Trabalho realizado por:

- Afonso Cunha - a22302960
  - Responsável por:

- Júlia Costa - a22304403
  - Responsável por:
  
- Mariana Martins - a22302203
  - Responsável por:


[URL para o repositório Git](https://github.com/Juhhxx/YetAnotherDungeonCrawler_LP1)

## Arquitetura da solução

De acordo com o que é pedido no enunciado deste projeto, a arqitetura deste mesmo seguiu os princípios por detrás do padrão de design de software MVC, estando então subdividida conceptual e logicamente em 3 partes: Model, View e Controller.

Neste modelo de design, cada uma destas 3 partes é responsável por um tipo de funcionalidade específica,comunicando umas com as outras de rígida forma e nunca se responsabilizando por algo que caia fora dentro dos seus limites conceptuais.

Com isto, Model é responsável pela lógica, manipulando dados de forma a efetivamente 

### Model - Descrição

Começando pelo Main() dá-se uma instanciação de um objeto GameManager, chamando de seguida o método GameStart()

1. **Enum BuffTypes**
    - Enum containing the 3 different types of stat buffs that can exist: HP, Attack and Defense buffs.

2. **Class Character**
    - This is an abstract class that serves as the base for any character in the game, with the purpose of being extended in order to create more role-specific character like a player or enemy. As such

      - **public int Attack(Character target)** - Method for combat between characters. It calculates the hit power of an attack by by first removing the targets defense out of the value and clamping it to 0,  and then applying the remaining value to the target's HP and clamping it to 0, returning the hit power of the attack.
      - **public bool IsDead()** - Method that sees if the character is dead by checking if **HP <= 0**, returning the boolean resulting from it.

3. **Class Enemy : Character**
    - This class has only the constructor for a specific enemy, inheriting the **Attack()** and **IsDead()** methods from its parent class - **Character**, but has no enemy specific methods.
4. **Class Player : Character**
    - Like the Enemy class, this one has a specific constructor for the player character, inheriting as well the **Attack()** and **IsDead()** methods, while having some more player-specific ones:

        - **public void SetInitialRoom(Room initialRoom)** - Method to set the room that the player is initially in;
        - **public bool Move(string direction)** - Method that moves the player between rooms, by checking if the given direction, in the current room's accessible rooms library, is not blocked off. Moves the player and returns true if the player can walk in that direction, otherwise returns false and the player doesn't move;
        - **public bool PickUpItem(Item newItem)** - Receives an Item and Adds it to the Inventory if it is not full. Returns true if the inventory is not full, returns false if it is full;
        - **public void Heal(Item potion)** - Adds the buff a potion gives to the health of the Player, and then removes the potion from the Player's Inventory;
        - **public bool Equip(Item newItem)** - Adds the buff from a weapon or shield adding it to the attack power or defense stat of the Player, then setting it to weapon or shield property, and then removes the item from the Player's current room;
        - **public Item ConfirmItem(bool isPotion)** - Method to confirm if the item in the Player's current room is or not a potion. If the method confirms the item is a potion, it returns the item, else it returns null;
        - **public Enemy ConfirmEnemy()** - Method that returns the Player's current room's enemy. It will return null if there is no enemy, and return the enemy if it is not null;
        - **public bool CheckForItem()** - Method that returns if the Player's current room has an item. It will return false if there are no items, and return true if there is an item;
        - **public bool CheckForEnemy()** - Method that returns if the Player's current room has an enemy. It will return false if there is no enemy, and return true if there are enemies in that room;
        - **public Item SearchInInventory(string name)** - Method that looks through the Inventory for the first item with a given name. Returns the item if it is not null (if it is in the inventory), and return null if there is no item;
        - **public bool FoundFinalRoom()** - Method that checks if the Player has found a final room, returning **IsFinal()** using the current room as its parameter;
5. **Class GameInitializer**
    - This class is responsible for initializing the objects necessary to the game, as well as having some methods to verify some values related and necessary to such initializations.

        - **public void InitializeGame()** - Call all of the initializer methods to start the game;
        - **private void InitializeEnemies()** - Initialize all Enemy instances given in the Enemies.txt file;
        - **private void InitializeItems()** - Initialize all Item instances given in the Items.txt file;
        - **private void InitializeRooms()** - Initialize all the Room instances give in the Rooms.txt file;
        - **private string NullOrValue(string param,string variable)** - Find if a specified variable should be null or have a value. If param is "-" the variable is null, if else the variable is equals to param. Returns the value to be set;
        - **private Enemy NullOrValue(string param,Enemy variable)** - Find if a specified variable should be null or have a value. If param is "-" the variable is null, if else the variable is equals to param. Returns the value to be set;
        - **private Item NullOrValue(string param,Item variable)** - Find if a specified variable should be null or have a value. If param is "-" the variable is null, if else the variable is equals to param. Returns the value to be set;
        - **private void SetUpRoomDirections()** - Set up the Rooms instances directions;
6. **Class Item**
    -This class does not contain anything besides the constructor for an Item object and some properties corresponding to the  traits an item can have (**Name**, **Type** and **Buff Value**);

7. **Class Room**

### View - Descrição

1. **Interface IView**
    - Contains all the methods that are to be implemented by the GameView class (that in this project assumes the full responsability of the View module of an MVC pattern)

      - **string StartMenu()**
      - **void ExplainNewGame()**
      - **void ColoredText(string str, ConsoleColor color)**
      - **void RoomDescription(Room room)**
      - **string AwaitDecision()**
      - **string AwaitBattleInput()**
      - **string AwaitRoomInput()**
      - **void AttackResult(Character characterActive, Character characterPassive, int hitPower )**
      - **void BattleWin()**
      - **void CantMove()**
      - **void CanMove()**
      - **void HealResult(Item potion)**
      - **void PlayerStatus(Player character)**
      - **string AskPickUpItem(Item item)**
      - **void PickUpItem(Item item)**
      - **void EquipItem(Item item)**
      - **void ItemInformation(Item item)**
      - **string ItemToUse()**
      - **void WarningNoEnemiesToFight()**
      - **void WarningItemNotInInventory()**
      - **void WarningFullInventory()**
      - **void WarningNeedName()**
      - **void WarningWrongCommand()**
      - **void WarningNoItemToPickUp()**
      - **void WarningWrongItem()**
      - **void ByeBye()**
      - **void GameOver()**
      - **void GameWin()**
  
2. **Classe GameView**
   In this project this class assumes the full responsability of the conceptual View module in an MVC design pattern. All its methods have eithe rthe obejctive of serving as a part of the UI or either print a message to communicate information to the player and/or even ask for input of the user. From giving flavour text to returning a player's input in order to be used elsewhere in the code, the GameView class only handles events withing these delimitations.

   - Contém todos os métodos a ser implementados pela classe GameView:
      - **string StartMenu()** - A method to print out the Start Menu dialogue that comes up every single startup and the options to Start te game or Quit;
      - **void ExplainNewGame()** - A group of prints that aim to give a little bit of a flavorful introduction to the game setting and at the same time, inform the player about the objectives in the game and how to interact with the game text-based action system, while informing them how to navigate the dungeon;
      - **void ColoredText(string str, ConsoleColor color)** - Method to make it easier to print colored text;
      - **void RoomDescription(Room room)** - Prints out the description of a specific room;
      - **string AwaitDecision()** - This method is aiming to be a general non-specific input request returning the player's answer as a **string s**;
      - **string AwaitBattleInput()** - This method is for requesting the player's input in a battle situation, returning the player's answer as a **string s**;
      - **string AwaitRoomInput()** - This method is for requesting the player's input when th player is exploring a room, returning the player's answer as a **string s**;
      - **void AttackResult(Character characterActive, Character characterPassive, int hitPower )** - Print the result of an Attack executed by one Character on another;
      - **void BattleWin()** - Print out a victory message for when a battle is won byt the player;
      - **void CantMove()** - Print affirming the player can't move that way when exploring rooms;
      - **void CanMove()** - Print that accompanies a communicates to the player that he has moved to the next room;
      - **void HealResult(Item potion)** - Print to communicate that the player has healed themselves for a certain value;
      - **void PlayerStatus(Player character)** - Prints out a little menu to show the Player's HP, Attack Power, Defense as well as the buffs *(in dark green)* applied to their base stats *(in dark blue)*
      - **string AskPickUpItem(Item item)** - Print out the request for confirmation on if the player wants to pick up a certain item, returning the player's answer as a **string s**;
      - **void PickUpItem(Item item)** - Print a sentence onto the console to communicate that the player picked up an item;
      - **void EquipItem(Item item)** - Print a sentence onto the console to communicate that the player equipped an item;
      - **void ItemInformation(Item item)** - Print the details of each item: Name, type (sword, shield or potion) and its buff value;
      - **string ItemToUse()** - Print a request for input on which item the player wants to use, returning the player's answer as a **string s**;
      - **void WarningNoEnemiesToFight()** - Print out a warning when there there are no enemies to fight;
      - **void WarningItemNotInInventory()** - Print out a warning that a certain item the player is looking for is not in their possession;
      - **void WarningFullInventory()** - Print out a warning about the player's inventory being full;
      - **void WarningNeedName()** - Print out a warning that a **name** is required and that progress is not possible without it;
      - **void WarningWrongCommand()** - Print out a warning about the player having a written a wrong/misspelled command
      - **void WarningNoItemToPickUp()** - Print out a warning about not existing any items to pickup;
      - **void WarningWrongItem()** - Print out a warning about when an item wrong/not used for a certain purpose;
      - **void ByeBye()** - Print out a goodbye message for the player when he quits the game;
      - **void GameOver()** - Print out a message in a game over situation;
      - **void GameWin()** - Print out a message for the occasion where the player finds the exit and wins the game;

### Controller - Descrição

1. **Class Controller**

### Gráfico UML

```mermaid
    classDiagram
        class GameView
        class IView
        <<interface>> IView
        class Controller
        class GameInitializer
        class Character
        class Player
        class Enemy
        class Item
        class BuffType
        <<enumeration>> BuffType
        class Room
        
        Controller --> GameView
        Controller --> Player
        Controller --> GameInitializer

        Room --> Item
        Room --> Enemy

        GameInitializer o-- Item
        GameInitializer o-- Enemy
        GameInitializer o-- Room
        GameInitializer --> Player

        Player --> Room
        Player o-- Item

        Item --> BuffType
        
        Player --|> Character
        Enemy --|> Character

        GameView ..|> IView
```

## Referências
