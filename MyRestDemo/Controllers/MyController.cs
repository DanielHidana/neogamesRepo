using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyRestDemo.Models;

namespace MyRestDemo.Controllers
{
    public class MyController : ApiController
    {
        /// <summary>
        /// Purchase Items Array
        /// </summary>
        private Purchase[] PurchaseItems = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public MyController()
        {
            //Init Purchase Array Info
            PurchaseItems = BuildPurchaseArray();
        }

        /// <summary>
        /// Build Purchase Array
        /// </summary>
        /// <returns></returns>
        public Purchase[] BuildPurchaseArray ()
        {
            Purchase[] tempPurchaseArr = new Purchase[20];
            for (int i=0;i<20;i++)
            {
                tempPurchaseArr[i] = (new Purchase { Amount = 1 * i / 2 + 8, PurchaseDate = DateTimeOffset.Now, ItemNumber = i });
            }
            return tempPurchaseArr;
        }


        /// <summary>
        /// Get from Purchase list a specefic items from the require date time and required size (bulkSize) 
        /// </summary>
        /// <param name="bulkSize">required bulk size</param>
        /// <param name="dateTime">required date time to search from </param>
        /// <returns></returns>
        public Purchase[] Get(int bulkSize, DateTimeOffset dateTime)
        {
            Purchase[] listToReturn = new Purchase[0];
            int idx = 0;
            foreach (var curr in PurchaseItems)
            {
                if (DateTimeOffset.Compare(dateTime, curr.PurchaseDate) < 0 || DateTimeOffset.Compare(dateTime, curr.PurchaseDate) == 0)
                {
                    bulkSize = GetCorrectBulkSize(bulkSize, idx);
                    listToReturn = new Purchase[bulkSize];
                    Array.Copy(PurchaseItems, idx, listToReturn, 0, bulkSize);
                    break;
                }
                idx++;
            }

            return listToReturn;
        }

        /// <summary>
        /// Alternative Solution - from Purchase list a specefic items by item number (ID) and required size (bulkSize)
        /// </summary>
        /// <param name="bulkSize">required bulk size</param>
        /// <param name="itemNumber">required item number</param>
        /// <returns></returns>
        public Purchase[] Get(int bulkSize, int itemNumber)
        {
            bulkSize = GetCorrectBulkSize(bulkSize, itemNumber);
            Purchase[] listToReturn = new Purchase[bulkSize];
            int idx = 0;
            foreach (var curr in PurchaseItems)
            {
                if (itemNumber == curr.ItemNumber || itemNumber < curr.ItemNumber)
                {
                    Array.Copy(PurchaseItems, idx, listToReturn, 0, bulkSize);
                    break;
                }
                idx++;
            }

            return listToReturn;
        }

        /// <summary>
        /// Get Correct Bulk Size by checking the selected item with the Array length and the bulk size
        /// </summary>
        /// <param name="bulkSize">required bulk size</param>
        /// <param name="itemIdx">selected item number</param>
        /// <returns></returns>
        private int GetCorrectBulkSize(int bulkSize, int itemIdx)
        {
            int ans = PurchaseItems.Length - itemIdx;

            if (ans < bulkSize)
            {
                bulkSize = ans;
            }
            return bulkSize;
        }
    }
}
