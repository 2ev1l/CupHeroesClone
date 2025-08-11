using System.Collections;
using System.Collections.Generic;
using Game.DataBase;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Universal.Events;

namespace Tests.EditMode
{
    public class StatTests
    {
        #region fields & properties

        #endregion fields & properties

        #region methods
        [Test]
        public void StatIncreaseTest()
        {
            Health s = new(100);
            s.Value += 10;
            Assert.AreEqual(s.Value, 110);
            s.Value += 1;
            Assert.AreEqual(s.Value, 111);
        }
        [Test]
        public void StatDecreasePositiveTest()
        {
            Health s = new(100);
            s.Value -= 10;
            Assert.AreEqual(s.Value, 90);
            s.Value -= 90;
            Assert.AreEqual(s.Value, 0);
        }
        [Test]
        public void StatDecreaseNegativeTest()
        {
            Health s = new(100);
            Assert.Throws<System.ArgumentOutOfRangeException>(delegate { s.Value -= 101; });
            Assert.AreEqual(s.Value, 100);
        }
        public void StatSoftDecreaseValueTest()
        {
            Health s = new(100);
            s.SetValueOrZero(s.Value - 101);
            Assert.AreEqual(s.Value, 0);
        }

        #endregion methods
    }
}