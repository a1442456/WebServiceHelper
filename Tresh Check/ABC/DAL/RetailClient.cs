using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace WebTrashCheck
{
   public class RetailClient
    {
        private string _clientName;
        private string _serverLink;
        private bool _isFusionTransfered = false;
        private int _cashCount;
        private Dictionary<string, string> _PCtoUsers;

        public RetailClient(RetailClient rc, string dbName, string portNumber)
        {
            _PCtoUsers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            FillPCtoUserDictionary();
        }

        public Dictionary<string, string> PCtoUsers
        {
            get { return _PCtoUsers; }
        }

        private void FillPCtoUserDictionary()
        {
            _PCtoUsers.Add("it3m", "Красников Антон");
            _PCtoUsers.Add("Anthony_Admin", "Красников Антон");
            _PCtoUsers.Add("DESKTOP-JIEVTLN", "Владислав Пышинский");
            _PCtoUsers.Add("it13", "Владислав Пышинский");
            _PCtoUsers.Add("srv129", "Владислав Пышинский");
            _PCtoUsers.Add("it24", "Андрей Жилинский");
            _PCtoUsers.Add("it12", "Пранович Дмитрий");
            _PCtoUsers.Add("-Z-z-Z-z-Z-", "Потепалов Дмитрий");
            _PCtoUsers.Add("it30", "Зубковский Дмитрий");
            _PCtoUsers.Add("Dmitry", "Зубковский Дмитрий");
            _PCtoUsers.Add("bw0911w", "Васько Артём");
        }

        public int CashCount 
        {
            get {return _cashCount; }
            set {_cashCount = value;}
        }

        public string ServerLink
        {
            get { return _serverLink; }
            set { _serverLink = value; }
        }

        public bool IsFusionTransfered
        {
            get { return _isFusionTransfered; }
            set { _isFusionTransfered = value; }
        }

        public string MagNumber
        {
            get { return _clientName; }
            set { _clientName = value; }
        }

        public RetailClient()
        {
            _PCtoUsers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            FillPCtoUserDictionary();
        }

        public RetailClient(string clientName)
        {
            if (IsClientNameInList(clientName))
            {
                this._clientName = clientName;
            }
        }

        public static List<RetailClient> GetAllCurrentClients()
        {
            List<RetailClient> clientList = new List<RetailClient>();

            RetailClient client = new RetailClient();

            #region clientList filling
            client._clientName = "18";
            client._serverLink = @"http://172.27.15.18/ukm";
            client.IsFusionTransfered = true;
            client._cashCount = 2;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "43";
            client._serverLink = @"http://172.16.2.46/ukm";
            client._cashCount = 2;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "44";
            client._serverLink = @"http://172.16.2.46/ukm";
            client._cashCount = 2;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "50";
            client._serverLink = @"http://172.16.2.48/ukm";
            client._cashCount = 3;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "96";
            client._serverLink = @"http://172.16.2.48/ukm";
            client.IsFusionTransfered = true;
            client._cashCount = 2;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "102";
            client.IsFusionTransfered = true;
            client._serverLink = @"http://172.16.2.42/ukm";
            client._cashCount = 5;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "104";
            client._serverLink = @"http://172.16.2.45/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 4;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "105";
            client._serverLink = @"http://172.16.2.43/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 4;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "106";
            client._serverLink = @"http://172.16.2.45/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 3;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "107";
            client._serverLink = @"http://172.16.2.44/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 4;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "108";
            client._serverLink = @"http://172.16.2.43/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 5;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "109";
            client._serverLink = @"http://172.16.2.43/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 4;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "110";
            client._serverLink = @"http://172.16.2.43/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 3;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "111";
            client._serverLink = @"http://172.16.2.45/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 4;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "113";
            client._serverLink = @"http://172.16.2.44/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 3;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "115";
            client._serverLink = @"http://172.16.2.41/ukm";
            client.IsFusionTransfered = true;
            client._cashCount = 8;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "117";
            client._serverLink = @"http://172.16.2.42/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 4;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "118";
            client._serverLink = @"http://172.16.2.41/ukm";
            client.IsFusionTransfered = true;
            client._cashCount = 7;
            clientList.Add(client);
            client = new RetailClient();

            client._serverLink = @"http://172.16.2.41/ukm";
            client._clientName = "119";
            client._isFusionTransfered = true;
            client._cashCount = 4;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "120";
            client._serverLink = @"http://172.16.2.45/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 7;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "121";
            client._serverLink = @"http://172.16.2.43/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 3;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "123";
            client._serverLink = @"http://172.16.2.43/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 7;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "124";
            client._serverLink = @"http://172.16.2.43/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 4;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "126";
            client._serverLink = @"http://172.16.2.42/ukm";
            client._cashCount = 3;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "127";
            client._serverLink = @"http://172.16.2.46/ukm";
            client._cashCount = 7;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "129";
            client._serverLink = @"http://172.16.2.47/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 7;
            clientList.Add(client);
            client = new RetailClient();

            client._isFusionTransfered = true;
            client._clientName = "130";
            client._serverLink = @"http://172.16.2.44/ukm";
            client._cashCount = 6;
            clientList.Add(client);
            client = new RetailClient();

            client._isFusionTransfered = true;
            client._clientName = "131";
            client._serverLink = @"http://172.16.2.46/ukm";
            client._cashCount = 4;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "132";
            client._serverLink = @"http://172.16.2.46/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 2;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "133";
            client._serverLink = @"http://172.16.2.43/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 5;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "134";
            client._serverLink = @"http://172.16.2.47/ukm";
            client._cashCount = 3;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "138";
            client._serverLink = @"http://172.16.2.42/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 4;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "140";
            client._serverLink = @"http://172.16.2.42/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 5;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "142";
            client._serverLink = @"http://172.16.2.46/ukm";
            client._cashCount = 7;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "146";
            client._serverLink = @"http://172.16.2.45/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 4;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "147";
            client._serverLink = @"http://172.16.2.42/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 5;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "148";
            client._serverLink = @"http://172.16.2.44/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 4;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "151";
            client._serverLink = @"http://172.16.2.45/ukm";
            client._cashCount = 6;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "152";
            client._serverLink = @"http://172.16.2.45/ukm";
            client._cashCount = 6;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "154";
            client._serverLink = @"http://172.16.2.44/ukm";
            client._cashCount = 6;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "156";
            client._serverLink = @"http://172.16.2.41/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 5;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "157";
            client._serverLink = @"http://172.16.2.42/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 8;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "159";
            client._serverLink = @"http://172.16.2.41/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 6;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "160";
            client._serverLink = @"http://172.16.2.46/ukm";
            client._cashCount = 3;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "161";
            client._serverLink = @"http://172.16.2.48/ukm";
            client._cashCount = 4;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "162";
            client._serverLink = @"http://172.16.2.48/ukm";
            client._cashCount = 5;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "164";
            client._serverLink = @"http://172.16.2.46/ukm";
            client._cashCount = 4;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "165";
            client._serverLink = @"http://172.16.2.47/ukm";
            client._cashCount = 3;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "166";
            client._serverLink = @"http://172.16.2.43/ukm";
            client._cashCount = 3;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "167";
            client._serverLink = @"http://172.16.2.46/ukm";
            client._cashCount = 3;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "168";
            client._serverLink = @"http://172.16.2.48/ukm";
            client._cashCount = 4;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "170";
            client._serverLink = @"http://172.16.2.47/ukm";
            client._cashCount = 4;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "171";
            client._serverLink = @"http://172.16.2.45/ukm";
            client._cashCount = 6;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "172";
            client._serverLink = @"http://172.16.2.41/ukm";
            client.IsFusionTransfered = true;
            client._cashCount = 4;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "173";
            client._serverLink = @"http://172.16.2.48/ukm";
            client._cashCount = 3;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "174";
            client._serverLink = @"http://172.16.2.46/ukm";
            client._cashCount = 7;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "175";
            client._serverLink = @"http://172.16.2.41/ukm";
            client._cashCount = 8;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "176";
            client._serverLink = @"http://172.16.2.43/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 4;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "177";
            client._serverLink = @"http://172.16.2.47/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 4;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "178";
            client._serverLink = @"http://172.16.2.41/ukm";
            client._cashCount = 7;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "179";
            client._serverLink = @"http://172.16.2.45/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 4;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "180";
            client._serverLink = @"http://172.16.2.47/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 5;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "181";
            client._serverLink = @"http://172.16.2.48/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 6;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "182";
            client._serverLink = @"http://172.16.2.47/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 3;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "183";
            client._serverLink = @"http://172.16.2.47/ukm";
            client._cashCount = 3;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "184";
            client._serverLink = @"http://172.16.2.47/ukm";
            client._cashCount = 5;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "185";
            client._serverLink = @"http://172.16.2.42/ukm";
            client._cashCount = 4;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "186";
            client._serverLink = @"http://172.16.2.42/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 3;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "187";
            client._serverLink = @"http://172.16.2.42/ukm";
            client._cashCount = 4;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "188";
            client._serverLink = @"http://172.16.2.48/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 7;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "189";
            client._serverLink = @"http://172.16.2.48/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 4;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "190";
            client._serverLink = @"http://172.16.2.47/ukm";
            client._cashCount = 3;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "191";
            client._serverLink = @"http://172.16.2.48/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 3;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "192";
            client._serverLink = @"http://172.16.2.41/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 6;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "193";
            client._serverLink = @"http://172.16.2.44/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 4;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "194";
            client._serverLink = @"http://172.16.2.48/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 5;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "195";
            client._serverLink = @"http://172.16.2.41/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 3;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "196";
            client._serverLink = @"http://172.16.2.44/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 3;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "197";
            client._serverLink = @"http://172.16.2.43/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 3;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "198";
            client._serverLink = @"http://172.16.2.48/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 6;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "199";
            client._serverLink = @"http://172.16.2.44/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 3;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "200";
            client._serverLink = @"http://172.16.2.46/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 6;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "201";
            client._serverLink = @"http://172.16.2.42/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 3;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "241";
            client._serverLink = @"http://172.16.2.44/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 6;
            clientList.Add(client);
            client = new RetailClient();

            client._clientName = "242";
            client._serverLink = @"http://172.16.2.44/ukm";
            client._isFusionTransfered = true;
            client._cashCount = 4;
            clientList.Add(client);
            client = new RetailClient();

            #endregion

            return clientList;
        }

        public bool IsClientNameInList(string clientName)
        {
            bool isExist = false;

            foreach (RetailClient client in GetAllCurrentClients())
            {
                if (client._clientName != clientName)
                {
                    continue;
                }

                else
                {
                    isExist = true;
                }
            }

            return isExist;
        }       

        public void SetOnDefault()
        {
            this._clientName = string.Empty;
           
        }

       /// <summary>
       /// Set client1 vlues from client2
       /// </summary>
       /// <param name="client1">ref Param Client which should be equal client2</param>
       /// <param name="client2"></param>
        public static void EqualTwoClients(ref RetailClient client1, RetailClient client2)
        {
            client1.MagNumber = client2.MagNumber;
            client1._isFusionTransfered = client2._isFusionTransfered;
            client1._serverLink = client2._serverLink;
            client1._cashCount = client2._cashCount;
            client1._PCtoUsers = client2._PCtoUsers;
        }

        public static bool CompareTwoClients(RetailClient client1, RetailClient client2)
        {
            if (client1._clientName == client2._clientName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        internal static RetailClient GetRetailClientpByClientName(string clientName)
        {
            foreach (RetailClient client in GetAllCurrentClients())
            {
                if (client._clientName != clientName)
                {
                    continue;
                }

                else
                {
                    return client;
                }
            }
            throw new Exception("Такого ТО нет!\n");
        }
    }
}
