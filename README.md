# CKL Lib

### Библиотека для взаимодействия с алгеброй динамических отношений
**Version: 1.1**


**[Версия 1.0](https://github.com/mishabogdanov5/CKL_Lib)**

## Как загрузить **.dll** файлы библиотек локально

- **Установить dotnet (Windows)**
    ```cmd
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
- *<имя папки>/CKLLib/bin/Release/net8.0/CKLLib.dll*
- *<имя папки>/CKLDrawing/bin/Release/net8.0-windows/CKLDrawing.dll*
