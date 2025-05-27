# CKLLib & CKLDrawing

### Библиотеки для взаимодействия с алгеброй динамических отношений и визуализации ее объектов

**Version: 1.1**

### Полезные ссылки

- **[Библиотеки версии 1.0](https://github.com/mishabogdanov5/CKL_Lib)**
- **[Инструментарий, использующий библиотеку](https://github.com/Yvunglord/CKL_Studio)**

## Короткое пояснение к каждой из библиотек

  - ### CKLLib

    Библиотека, в которой описана алгбера динамических отношений, а так же операции данной алгебры.

  - ### CKLDrawing

    Библиотека, отображающая динамическое отношение в виде диаграммы Ганта. CKLView - компонент WPF, являющйся диаграммой Ганта.             

## Как загрузить **.dll** файлы библиотек локально

- **Установить dotnet (Windows)**

    ```powershell
    winget install Microsoft.DotNet.SDK.8   
    ```

 - **Клонировать репозиторий**
  
    ```git
    git clone -b dev git@github.com:mishabogdanov5/CKLLib-1.1.git
    ```
- **Сборка библиотек** 

    Собирать библиотеки нужно из папки, в которую был склонирован репозиторий
    ```powershell
    dotnet build -c Release
    ```

## После этого файлы будут находится: 
- *<имя папки>\CKLLib\bin\Release\net8.0\CKLLib.dll*
- *<имя папки>\CKLDrawing\bin\Release\net8.0-windows\CKLDrawing.dll*

## Установка зависимостей в проект:


Для начала перейдите в папку проекта, после чего добавьте ссылку на .dll файл библиотеки с помощью dotnet:

```powershell
cd C:\path\to\proj
dotnet add reference <имя папки>\CKLLib\bin\Release\net8.0\CKLLib.dll
```

Для CKLDRawing аналогично

***Важно понимать, что CKLLib можно использовать для любого .NET проекта, а CKLDrawing только для проектов, базирующихся на WPF***