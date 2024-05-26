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
2. **Class Character**
   - Responsável por 
3. **Class Enemy : Character**
4. **Class Player : Character**
5. **Class GameInitializer**
6. **Class GameView**
7. **Class Item**
8. **Class Room**

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

### Fluxograma

```mermaid
graph TB
```

## Referências
