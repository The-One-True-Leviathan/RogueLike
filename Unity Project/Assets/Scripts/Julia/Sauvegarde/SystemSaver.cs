using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SystemSaver
{
   public static void SaveMoney(Compteur compteur)
    {
        BinaryFormatter moneyFormatter = new BinaryFormatter();
        string moneyPath = Application.persistentDataPath + "/money.saving";
        FileStream moneyStream = new FileStream(moneyPath, FileMode.Create);

        DataSaver moneyData = new DataSaver(compteur);

        moneyFormatter.Serialize(moneyStream, moneyData);
        moneyStream.Close();
    }

    public static DataSaver LoadMoney()
    {
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
            Debug.LogError("Save file not found in" + moneyPath);
            return null;
        }

    }
}
