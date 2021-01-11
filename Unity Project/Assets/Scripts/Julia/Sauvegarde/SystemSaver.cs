using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SystemSaver
{
   public static void SaveMoney(Compteur compteur)
    {
        Debug.LogWarning("Attempted Saving Money");
        BinaryFormatter moneyFormatter = new BinaryFormatter();
        string moneyPath = Application.persistentDataPath + "/money.saving";
        FileStream moneyStream = new FileStream(moneyPath, FileMode.Create);

        DataSaver moneyData = new DataSaver(compteur);

        moneyFormatter.Serialize(moneyStream, moneyData);
        moneyStream.Close();
    }

    public static DataSaver LoadMoney()
    {
        Debug.LogWarning("Attempted Loading Money");
        string moneyPath = Application.persistentDataPath + "/money.saving";
        if (File.Exists(moneyPath))
        {
            BinaryFormatter moneyFormatter = new BinaryFormatter();
            FileStream moneyStream = new FileStream(moneyPath, FileMode.Open);

            DataSaver moneyData = moneyFormatter.Deserialize(moneyStream) as DataSaver;
            moneyStream.Close();

            return moneyData;
        }else
        {
            //Debug.LogError("Save file not found in" + moneyPath);
            return null;
        }

    }

    public static void SaveShop(ShoppingManager shopping)
    {
        Debug.LogWarning("Attempted Saving Shop");
        BinaryFormatter shopFormatter = new BinaryFormatter();
        string shopPath = Application.persistentDataPath + "/shop.saving";
        FileStream shopStream = new FileStream(shopPath, FileMode.Create);

        DataSaver shopData = new DataSaver(shopping);

        shopFormatter.Serialize(shopStream, shopData);
        shopStream.Close();
    }


    public static DataSaver LoadShop()
    {
        Debug.LogWarning("Attempted Loading Shop");
        string shopPath = Application.persistentDataPath + "/shop.saving";
        if (File.Exists(shopPath))
        {
            BinaryFormatter shopFormatter = new BinaryFormatter();
            FileStream shopStream = new FileStream(shopPath, FileMode.Open);

            DataSaver shopData = shopFormatter.Deserialize(shopStream) as DataSaver;
            shopStream.Close();

            return shopData;
        }
        else
        {
            //Debug.LogError("Save file not found in" + shopPath);
            return null;
        }

    }

    /*public static void SaveGameLaunch(SaveandLoad game)
    {
        BinaryFormatter gameFormatter = new BinaryFormatter();
        string gamePath = Application.persistentDataPath + "/game.saving";
        FileStream gameStream = new FileStream(gamePath, FileMode.Create);

        DataSaver gameData = new DataSaver(game);

        gameFormatter.Serialize(gameStream, gameData);
        gameStream.Close();
    }

    public static DataSaver LoadGame()
    {
        string gamePath = Application.persistentDataPath + "/game.saving";
        if (File.Exists(gamePath))
        {
            BinaryFormatter gameFormatter = new BinaryFormatter();
            FileStream gameStream = new FileStream(gamePath, FileMode.Open);

            DataSaver gameData = gameFormatter.Deserialize(gameStream) as DataSaver;
            gameStream.Close();

            return gameData;
        }
        else
        {
            //Debug.LogError("Save file not found in" + shopPath);
            return null;
        }

    }*/
}
