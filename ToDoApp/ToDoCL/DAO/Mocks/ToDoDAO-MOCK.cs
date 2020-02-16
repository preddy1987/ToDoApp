using ToDoApp.Interfaces;
using ToDoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoEFDB.Models;

namespace ToDoApp.Mock
{
    public class ToDoDAO_MOCK : IToDoDAO
    {
        private static Dictionary<int, UserItem> _userItems = new Dictionary<int, UserItem>();

        //private static Dictionary<int, BankingTransaction> _bankingTransactions = new Dictionary<int, BankingTransaction>();
        private static Dictionary<int, RoleItem> _roleItems = new Dictionary<int, RoleItem>();

        private static int _userId = 1;
        private static int _categoryId = 1;
        private static int _productId = 1;
        private static int _inventoryId = 1;
        private static int _bankingTransactionId = 1;
        private static int _transactionItemId = 1;
        private static int _roleId = 1;

        #region BankingTransaction

        public int AddTransactionSet(BankingTransaction vendTrans, List<TransactionItem> transItems)
        {
            int vendTransId = AddVendingTransaction(vendTrans.Clone());
            foreach (var item in transItems)
            {
                TransactionItem newItem = item.Clone();
                newItem.VendingTransactionId = vendTransId;
            }
            return vendTransId;
        }

        public int AddVendingTransaction(BankingTransaction item)
        {
            item.Id = _bankingTransactionId++;
            _bankingTransactions.Add(item.Id, item.Clone());
            return item.Id;
        }

        public BankingTransaction GetVendingTransaction(int id)
        {
            BankingTransaction item = null;
            if (_bankingTransactions.ContainsKey(id))
            {
                item = _bankingTransactions[id];
            }
            else
            {
                throw new Exception("Item does not exist.");
            }
            return item.Clone();
        }

        public List<BankingTransaction> GetVendingTransactions()
        {
            List<BankingTransaction> items = new List<BankingTransaction>();
            foreach (var item in _bankingTransactions)
            {
                items.Add(item.Value.Clone());
            }
            return items;
        }

        #endregion

        #region UserItem

        public int AddUserItem(UserItem item)
        {
            item.Id = _userId++;
            _userItems.Add(item.Id, item.Clone());
            return item.Id;
        }

        public bool UpdateUserItem(UserItem item)
        {
            if (_userItems.ContainsKey(item.Id))
            {
                _userItems[item.Id] = item.Clone();
            }
            else
            {
                throw new Exception("Item does not exist.");
            }
            return true;
        }

        public void DeleteUserItem(int userId)
        {
            if (_userItems.ContainsKey(userId))
            {
                _userItems.Remove(userId);
            }
            else
            {
                throw new Exception("Item does not exist.");
            }
        }

        public UserItem GetUserItem(int userId)
        {
            UserItem item = null;

            if (_userItems.ContainsKey(userId))
            {
                item = _userItems[userId];
            }
            else
            {
                throw new Exception("Item does not exist.");
            }

            return item.Clone();
        }

        public List<UserItem> GetUserItems()
        {
            List<UserItem> items = new List<UserItem>();
            foreach (var item in _userItems)
            {
                items.Add(item.Value.Clone());
            }
            return items;
        }

        public UserItem GetUserItem(string username)
        {
            UserItem item = null;

            foreach (var user in _userItems)
            {
                if (user.Value.Username == username)
                {
                    item = user.Value;
                    break;
                }
            }

            if (item == null)
            {
                throw new Exception("Item does not exist.");
            }

            return item.Clone();
        }

        #endregion

        #region RoleItem

        public int AddRoleItem(RoleItem item)
        {
            _roleItems.Add(item.Id, item.Clone());
            return item.Id;
        }

        public List<RoleItem> GetRoleItems()
        {
            List<RoleItem> items = new List<RoleItem>();
            foreach (var item in _roleItems)
            {
                items.Add(item.Value.Clone());
            }
            return items;
        }

        public RoleItem GetRoleItem(int id)
        {
            RoleItem item = null;

            if (_roleItems.ContainsKey(id))
            {
                item = _roleItems[id];
            }
            else
            {
                throw new Exception("Item does not exist.");
            }

            return item.Clone();
        }

        public bool UpdateRoleItem(RoleItem item)
        {
            if (_roleItems.ContainsKey(item.Id))
            {
                _roleItems[item.Id] = item.Clone();
            }
            else
            {
                throw new Exception("Item does not exist.");
            }
            return true;
        }

        public void DeleteRoleItem(int id)
        {
            if (_roleItems.ContainsKey(id))
            {
                _roleItems.Remove(id);
            }
            else
            {
                throw new Exception("Item does not exist.");
            }
        }

        #endregion





        public decimal GetBalance(BankAccount account)
        {

            return account.Balance;
        }

        public int GetNumberOfDeposits(BankAccount account)
        {
            return account.NumberOfDeposits;
        }

        public int GetNumberOfWithdrawals(BankAccount account)
        {
            return account.NumberOfWithdrawals;
        }





        public void SetBalance(BankAccount account, decimal balance)
        {
            account.SetBalance(balance);
        }


    }
}
