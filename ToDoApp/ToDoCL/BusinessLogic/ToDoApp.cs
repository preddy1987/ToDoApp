using ToDoApp.Exceptions;
using System;
using ToDoApp.Models;

namespace ToDoApp
{
    class ToDoApp 
    {
        /// <summary>
        /// Manages the user authentication and authorization
        /// </summary>
        private RoleManager _roleMgr = null;

        /// <summary>
        /// The data access layer for the ToDoApp
        /// </summary>
        private IToDoApp _database = null;

        #region Constructor
        public ToDoApp(IToDoApp database)
        {
            _database = database;
            _roleMgr = new RoleManager(null);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns true if the vending machine has an authenticated user
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return _roleMgr.User != null;
            }
        }

        /// <summary>
        /// Adds a new user to the vending machine system
        /// </summary>
        /// <param name="userModel">Model that contains all the user information</param>
        public void RegisterUser(UserItem userModel)
        {
            UserItem userItem = null;
            try
            {
                userItem = _database.GetUserItem(userModel.Username);
            }
            catch (Exception)
            {
            }

            if (userItem != null)
            {
                throw new UserExistsException("The username is already taken.");
            }

            if (userModel.Password != userModel.ConfirmPassword)
            {
                throw new PasswordMatchException("The password and confirm password do not match.");
            }

            PasswordManager passHelper = new PasswordManager(userModel.Password);
            UserItem newUser = new UserItem()
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                Username = userModel.Username,
                Salt = passHelper.Salt,
                Hash = passHelper.Hash,
                RoleId = (int)RoleManager.eRole.Customer
            };

            _database.AddUserItem(newUser);
            LoginUser(newUser.Username, userModel.Password);
        }

        /// <summary>
        /// Logs a user into the vending machine system and throws exceptions on any failures
        /// </summary>
        /// <param name="username">The username of the user to authenicate</param>
        /// <param name="password">The password of the user to authenicate</param>
        public void LoginUser(string username, string password)
        {
            UserItem user = null;

            try
            {
                user = _database.GetUserItem(username);
            }
            catch (Exception)
            {
                throw new Exception("Either the username or the password is invalid.");
            }

            PasswordManager passHelper = new PasswordManager(password, user.Salt);
            if (!passHelper.Verify(user.Hash))
            {
                throw new Exception("Either the username or the password is invalid.");
            }

            _roleMgr = new RoleManager(user);
        }

        /// <summary>
        /// Logs the current user out of the vending machine system
        /// </summary>
        public void LogoutUser()
        {
            _roleMgr = new RoleManager(null);
        }

        /// <summary>
        /// List of all the registered vending machine users
        /// </summary>
        //public IList<UserItem> Users
        //{
        //    get
        //    {
        //        return _database.GetUserItems();
        //    }
        //}

        ///// <summary>
        ///// Array matrix of all the vending items in the vending machine slots
        ///// </summary>
        //public VendingItem[,] VendingItems
        //{
        //    get
        //    {
        //        var items = _db.GetVendingItems();
        //        var result = new VendingItem[InventoryManager.ColCount(items), InventoryManager.RowCount(items)];

        //        foreach (var item in items)
        //        {
        //            result[item.Inventory.Column - 1, item.Inventory.Row - 1] = item;
        //        }

        //        return result;
        //    }
        //}

        ///// <summary>
        ///// List of all the vending machine products
        ///// </summary>
        //public IList<ProductItem> Products
        //{
        //    get
        //    {
        //        return _db.GetProductItems();
        //    }
        //}

        ///// <summary>
        ///// List of all the vending machine operation types
        ///// </summary>
        //public IList<OperationTypeItem> OperationTypes
        //{
        //    get
        //    {
        //        return _log.GetOperationTypeItems();
        //    }
        //}

        /// <summary>
        /// RoleManager used to validate user permissions and have access to the current user information
        /// </summary>
        public RoleManager Role
        {
            get
            {
                return _roleMgr;
            }
        }

        ///// <summary>
        ///// Adds money to the vending machine and updates the running total
        ///// </summary>
        ///// <param name="amt"></param>
        //public void FeedMoney(double amt)
        //{
        //    _transMgr.AddFeedMoneyOperation(amt, Role.User.Id);
        //}

        ///// <summary>
        ///// Reduces the qty of the vending item by 1 if there is at least 1 item left in the slot
        ///// </summary>
        ///// <param name="row">The 1 based row for the item</param>
        ///// <param name="col">The 1 based col for the item</param>
        //public VendingItem PurchaseItem(int row, int col)
        //{
        //    var item = _db.GetVendingItem(row, col);

        //    if (item.Product.Price > RunningTotal)
        //    {
        //        throw new InsufficientFundsException("The vending machine does not have enough funds for this purchase.");
        //    }

        //    if (item.Inventory.Qty == 0)
        //    {
        //        throw new SoldOutException("Product is sold out.");
        //    }
        //    else
        //    {
        //        item.Inventory.Qty--;
        //        _db.UpdateInventoryItem(item.Inventory);
        //    }

        //    _transMgr.AddPurchaseTransaction(item.Product.Id, CurrentUser.Id);

        //    return item;
        //}

        ///// <summary>
        ///// Converts the running total into change and resets the running total to zero
        ///// </summary>
        ///// <returns>The change denominations to give to the user</returns>
        //public Change ReturnChange()
        //{
        //    var change = _transMgr.AddGiveChangeOperation(Role.User.Id);
        //    return change;
        //}

        /// <summary>
        /// The current logged in user of the vending machine
        /// </summary>
        public UserItem CurrentUser
        {
            get
            {
                return _roleMgr.User;
            }
        }

        ///// <summary>
        ///// Gets a transaction report for the given year and user passed in
        ///// </summary>
        ///// <param name="year">If null it will return data for all years, otherwise only for the given year</param>
        ///// <param name="userId">If null it will return data for all years, otherwise only for the given user</param>
        ///// <returns>Report that contains transaction data</returns>
        //public Report GetReport(int? year, int? userId)
        //{
        //    Report report = null;

        //    if (year == null)
        //    {
        //        year = DateTime.Now.Year;
        //    }

        //    var products = _db.GetProductItems();
        //    List<TransactionItem> transactions = null;
        //    if (userId == null)
        //    {
        //        transactions = _db.GetTransactionItemsForYear((int)year);
        //    }
        //    else
        //    {
        //        transactions = _db.GetTransactionItemsForYear((int)year, (int)userId);
        //    }
        //    report = new Report(transactions, products);

        //    return report;
        //}

        ///// <summary>
        ///// Gets a list of all the loggable vending operations for the given date range
        ///// </summary>
        ///// <param name="startDate">If null it will return all operations, otherwise only for the given start date</param>
        ///// <param name="endDate">If null it will return all operations, otherwise only for the given end date</param>
        ///// <returns>List of vending operations</returns>
        //public IList<VendingOperation> GetLog(DateTime? startDate, DateTime? endDate)
        //{
        //    IList<VendingOperation> result = null;

        //    if (startDate != null && endDate != null)
        //    {
        //        result = _log.GetLogData((DateTime)startDate, (DateTime)endDate);
        //    }
        //    else
        //    {
        //        result = _log.GetLogData();
        //    }

        //    return result;
        //}

        ///// <summary>
        ///// Restocks all the vending machine slots
        ///// </summary>
        ///// <param name="qty">The amount to restock the machine with</param>
        //public void Restock(int qty = 5)
        //{
        //    var items = _db.GetInventoryItems();
        //    foreach (var item in items)
        //    {
        //        item.Qty = qty;
        //        _db.UpdateInventoryItem(item);
        //    }
        //}
        #endregion
    }
}
