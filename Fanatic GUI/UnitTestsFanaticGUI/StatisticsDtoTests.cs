using System;
using System.Collections.Generic;
using Fanatic_GUI.Model;
using Fanatic_GUI.Persistency;
using Fanatic_GUI.ViewModel;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace UnitTestsFanaticGUI
{
    [TestClass]
    public class StatisticsDtoTests
    {

        public StatisticsDTO StatisticsDto { get; set; }

        [TestInitialize]
        public void BeforeEachTest()
        {
            StatisticsDto = new StatisticsDTO();
        }

        [TestMethod]
        public void UserIdZero()
        {
            StatisticsDto.ID = 0;
            Assert.AreEqual(0, StatisticsDto.ID);
        }

        [TestMethod]
        public void UserIdAboveZero()
        {
            StatisticsDto.ID = 1;
            Assert.AreEqual(1, StatisticsDto.ID);
        }

        [TestMethod]
        public void UserIdMaxPositiveInt()
        {
            StatisticsDto.ID = Int32.MaxValue;
            Assert.AreEqual(Int32.MaxValue, StatisticsDto.ID);
        }

        [TestMethod]
        public void UserIdBorderTest()
        {
            unchecked
            {
                Assert.ThrowsException<ArgumentException>(() =>
                {
                    StatisticsDto.ID = Int32.MaxValue + 1;
                    Assert.AreEqual(Int32.MinValue, StatisticsDto.ID);
                });
            }

        }

        [TestMethod]
        public void UserIdBelowZero()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                StatisticsDto.ID = -1;
            });
        }

        [TestMethod]
        public void UserIdMaxNegativeInt()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                StatisticsDto.ID = Int32.MinValue;
            });
        }

        [TestMethod]
        public void BooleanDicAllFalse()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                StatisticsDto.BooleanDic = new Dictionary<int, bool>()
                {
                    {1, false },
                    {2, false },
                    {3, false },
                    {4, false },
                    {5, false },
                    {6, false },
                };
            });
        }

        [TestMethod]
        public void BooleanDicNull()
        {
            Assert.ThrowsException<NullReferenceException>(() =>
            {
                StatisticsDto.BooleanDic = null;
            });
        }
    }
}
