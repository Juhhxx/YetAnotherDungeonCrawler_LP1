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
    - Contém todos os métodos a ser implementados pela classe GameView:
      - **string StartMenu()** .
      - **void ExplainNewGame()** .
      - **void ColoredText(string str, ConsoleColor color)** .
      - **void RoomDescription(Room room)** .
      - **string AwaitDecision()** .
      - **string AwaitBattleInput()** .
      - **string AwaitRoomInput()** .
      - **void AttackResult(Character characterActive, Character characterPassive, int hitPower )** .
      - **void BattleWin()** .
      - **void CantMove()** .
      - **void CanMove()** .
      - **void HealResult(Item potion)** .
      - **void PlayerStatus(Player character)** .
      - **string AskPickUpItem(Item item)** .
      - **void PickUpItem(Item item)** .
      - **void EquipItem(Item item)** .
      - **void ItemInformation(Item item)** .
      - **string ItemToUse()** .
      - **void WarningNoEnemiesToFight()** .
      - **void WarningItemNotInInventory()** .
      - **void WarningFullInventory()** .
      - **void WarningNeedName()** .
      - **void WarningWrongCommand()** .
      - **void WarningNoItemToPickUp()** .
      - **void WarningWrongItem()** .
      - **void ByeBye()** .
      - **void GameOver()** .
      - **void GameWin()** .
  
2. **Classe GameView**
   - Contém todos os métodos a ser implementados pela classe GameView:
      - **string StartMenu()** -
      - **void ExplainNewGame()** -
      - **void ColoredText(string str, ConsoleColor color)** -
      - **void RoomDescription(Room room)** -
      - **string AwaitDecision()** -
      - **string AwaitBattleInput()** -
      - **string AwaitRoomInput()** -
      - **void AttackResult(Character characterActive, Character characterPassive, int hitPower )** -
      - **void BattleWin()** -
      - **void CantMove()** -
      - **void CanMove()** -
      - **void HealResult(Item potion)** -
      - **void PlayerStatus(Player character)** -
      - **string AskPickUpItem(Item item)** -
      - **void PickUpItem(Item item)** -
      - **void EquipItem(Item item)** -
      - **void ItemInformation(Item item)** -
      - **string ItemToUse()** -
      - **void WarningNoEnemiesToFight()** -
      - **void WarningItemNotInInventory()** -
      - **void WarningFullInventory()** -
      - **void WarningNeedName()** -
      - **void WarningWrongCommand()** -
      - **void WarningNoItemToPickUp()** -
      - **void WarningWrongItem()** -
      - **void ByeBye()** -
      - **void GameOver()** -
      - **void GameWin()** -

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
