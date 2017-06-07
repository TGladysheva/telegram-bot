# CookBot
## Описание
  Бот - кулинарная книга для Telegram
## Авторы
   Ишин Данил, Гладышева Татьяна, Немцев Евгений
## План презентации
### Суть проекта
Данный бот позволяет по некоторым критериям получить рецепт. В данный момент поддерживаются следующие команды:
* /recipeName - найти рецепт по названию.
* /recipeIngr - получить список рецептов, в которых содержится указанный ингридиент.
* /recipeList - отобразить список всех рецептов.
* /help - информация о поддерживаемых командах.

#### Пример использования команды:
![Example](Example.PNG)

Легко добавлять новые типы команд, релаизовав интерфейс и использовать различаные базы данных, благодаря интерфейсу [IDatabase](CookBot/source/Infrastructure/Databases/IDatabase.cs). Также существует более глобальная точка расширения [IBot](/CookBot/source/App/IBot.cs), о которой будет расказано ниже.

Расширяемость команд продемонстрирована выше. Для баз данных - [ArrayDatabase](/CookBot/source/Infrastructure/Databases/ArrayDatabase.cs) (используется массив, который изначально засериализован). Для демонстрации IBot как точки расширения помимо [CookBot](/CookBot/source/App/CookBot.cs), был написан [HelloBot](/CookBot/source/App/HelloBot.cs) (который здоровается с вами в ответ на любое сообщение).

Вклад в проект:
* Ишин Данил - работа с предметным и инфраструктурным слоем
* Гладышева Татьяна - работа с прикладным слоем, TelegramAPI, контейнер
* Немцев Евгений - общая структура приложения, тестирование, TelegramAPI

## Точки расширения
1. Команды.

    Для того, чтобы создать новую команду достаточно реализовать интерфейс [IBotCommand](CookBot/source/App/Commands/IBotCommand.cs). У команды должно быть имя(свойство *Name*), описание(свойство *Description*), а также метод *Execute*, в который передается ссылка на базу данных и аргументы команды. Примеры реализации можно посмотреть в [этой папке](/CookBot/source/App/Commands) .
    
2. База данных.

    Для того, чтобы добавить новую базу данных, нужно реализовать дженерик интерфейс [IDatabase](/CookBot/source/Infrastructure/Databases/IDatabase.cs). Этот интерфейс декларирует два метода *GetAllSuitable*, *GetAnySuitable*, в которые передается лямбда-выражение, которому должен удовлетворять искомый объект.

3. Интерфейс [IBot](/CookBot/source/App/IBot.cs)

  Для реализации этого интерфейса необходимо реализовать метод *HandleCommand*, который возвращает ответ на полученное сообщение. Благодаря этому интерфейсу при работе с TelgramAPI(или другим API), мы можем не привязываться к конкретному боту. В нашем проекте есть две реализации этого интерфейса [CookBot](/CookBot/source/App/CookBot.cs) и [HelloBot](/CookBot/source/App/HelloBot.cs). Так же есть класс [TelegramHandler](/CookBot/source/App/TelegramHandler.cs), который работает с TelegramAPI и ему в конструктор передается реализация интерфейса [IBot](/CookBot/source/App/IBot.cs). Тем самым, создав нового бота, мы можем просто передать его в конструктор [TelegramHandler](/CookBot/source/App/TelegramHandler.cs) и не думать снова о том, как работать с TelegramAPI.
