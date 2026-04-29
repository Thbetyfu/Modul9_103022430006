using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Runtime.CompilerServices;
using System.Transactions;

namespace Modul9_103022430006
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankTransferConfig bankTransferConfig = new BankTransferConfig();

            if (bankTransferConfig.config.lang == "eng")
            {
                Console.WriteLine($"Please insert the amount of money to transfer:");

            }

            else if (bankTransferConfig.config.lang == "id")
            {
                Console.WriteLine($"Masukkan jumlah uang yang akan di transfer:");
            }

            double uang = Convert.ToInt32(Console.ReadLine());
            double biaya = 0;
            if (uang <= bankTransferConfig.config.transfer.threshold)
            {
                biaya = bankTransferConfig.config.transfer.low_fee;
            }
            else
            {
                biaya = bankTransferConfig.config.transfer.high_fee;
            }

            if (bankTransferConfig.config.lang == "eng")
            {
                Console.WriteLine($"Transfer fee: {biaya}");
                Console.WriteLine($"Total amount: {uang + biaya}");
                Console.WriteLine($"Select transfer method:");
            }
            else if (bankTransferConfig.config.lang == "id")
            {
                Console.WriteLine($"Biaya transfer: {biaya}");
                Console.WriteLine($"Total biaya: {uang + biaya}");
                Console.WriteLine($"Pilih metode transfer:");
            }

            Console.WriteLine($"1. {bankTransferConfig.config.methods[0]}");
            Console.WriteLine($"2. {bankTransferConfig.config.methods[1]}");
            Console.WriteLine($"3. {bankTransferConfig.config.methods[2]}");
            Console.WriteLine($"4. {bankTransferConfig.config.methods[3]}");

            int pilih = Convert.ToInt32(Console.ReadLine());

            if (bankTransferConfig.config.lang == "eng")
            {
                Console.WriteLine($"Please type {bankTransferConfig.config.confirmation.en} to Confirmacion transaction :");
            }
            else if (bankTransferConfig.config.lang == "id")
            {
                Console.WriteLine($"Silahkan ketik {bankTransferConfig.config.confirmation.id} untuk mengkonfirmasi transaksi:");
            }

            else
            {
                if (bankTransferConfig.config.lang == "en")
                {
                    Console.WriteLine("Transaction Canceld");
                }

                else if (bankTransferConfig.config.lang == "id")
                {
                    Console.WriteLine("Transaksi dibatalkan");
                }
        }
    }

    public class BankTransferConfig
    {
            public Config config;
            string configFilePath = "bank_transfer_config.json";

            public BankTransferConfig()
            {
                try { ReadConfigFile(); }
                catch (FileNotFoundException)
                {
                    setdefault();
                    WriteConfigFile();
                }
            }
                private void setdefault()
            {
                config = new Config(
                    lang: "en",
                    transfer: new TransferConfig(25000000, 6500, 15000),
                    methods: new List<string> { "RTO(real - time)", "SKN", "RTGS", "BI FAST" },
                    confirmation: new ConfirmationConfig("yes", "ya")
                );
            }

            private void ReadConfigFile()
            {
                string jsonString = File.ReadAllText(configFilePath);
                config = JsonSerializer.Deserialize<Config>(jsonString);
            }

            private void WriteConfigFile()
            {
                string jsonString = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(configFilePath, jsonString);
            }

            public void setDefaulth()
            {
            config = new Config();
            config.lang = "en";
            config.transfer = new TransferConfig(25000000, 6500, 15000);
            config.methods = new List<string> { "RTO(real - time)", "SKN", "RTGS", "BI FAST" };
            config.confirmation = new ConfirmationConfig("yes", "ya");
            }

    }
}
        

    public class TransferConfig
    {
        public int threshold { get; set; }
        public int low_fee { get; set; }
        public int high_fee { get; set; }

        public TransferConfig()
        {

        }

        public TransferConfig(int threshold, int low_fee, int high_fee)
        {
            this.threshold = threshold;
            this.low_fee = low_fee;
            this.high_fee = high_fee;
        }
    }
    public class ConfirmationConfig
    {
        public string en { get; set; }
        public string id { get; set; }

        public ConfirmationConfig()
        {

        }

        public ConfirmationConfig(string en, string id)
        {
            this.en = en;
            this.id = id;
        }

    }

    public class Config
    {
        public string lang { get; set; }
        public TransferConfig transfer { get; set; }
        public List<string> methods { get; set; }
        public ConfirmationConfig confirmation { get; set; }
        public Config()
        {
        }
        public Config(string lang, TransferConfig transfer, List<string> methods, ConfirmationConfig confirmation)
        {
            this.lang = lang;
            this.transfer = transfer;
            this.methods = methods;
            this.confirmation = confirmation;
        }
    }

}





