using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Fanatic_GUI.Model;
using Fanatic_GUI.Persistency;
using Microsoft.VisualStudio.TestPlatform.TestExecutor;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace UnitTestsFanaticGUI
{
    [TestClass]
    public class PersistenceTests
    {
        public StatisticsDTO StatisticsDto { get; set; }

        [TestInitialize]
        public void BeforeEachTest()
        {
            StatisticsDto = new StatisticsDTO();
            StatisticsDto.ID = 3; // GuestID i databasen
        }

        [TestMethod]
        public void TestPostReviewsSingle()
        {
            StatisticsDto.BooleanDic = new Dictionary<int, bool>()
                {
                    {1, true },
                    {2, false },
                    {3, false },
                    {4, false },
                    {5, false },
                    {6, false },
                };
            var t1 = PersistencyClass.PostUserReviews(StatisticsDto).Result;
            if (t1.IsSuccessStatusCode)
            {
                Assert.AreEqual(HttpStatusCode.Created, t1.StatusCode);
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestPostReviewsNonExistingId()
        {
            StatisticsDto.BooleanDic = new Dictionary<int, bool>()
                {
                    {1, true },
                    {2, true },
                    {3, false },
                    {4, false },
                    {5, false },
                    {6, false },
                };
            StatisticsDto.ID = 333; // ID that doesnt exist
            var t1 = PersistencyClass.PostUserReviews(StatisticsDto).Result;
            Assert.AreEqual(HttpStatusCode.NotFound, t1.StatusCode);
        }
    }
}
