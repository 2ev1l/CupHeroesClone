using Game.Serialization.World;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using Universal.Events;

namespace Tests.EditMode
{
    public class WalletTests
    {
        #region fields & properties

        #endregion fields & properties

        #region methods
        [Test]
        public void WalletIncreaseTest()
        {
            Wallet wallet = new(1000);
            wallet.IncreaseValue(100);
            Assert.AreEqual(wallet.Value, 1100);
        }
        public void WalletDecreaseNegativeTest()
        {
            Wallet wallet = new(1000);
            Assert.IsFalse(wallet.TryDecreaseValue(10000));
            Assert.AreEqual(wallet.Value, 1000);

            Assert.IsTrue(wallet.TryDecreaseValue(1000));
            Assert.IsFalse(wallet.TryDecreaseValue(1));
            Assert.AreEqual(wallet.Value, 0);
        }
        [Test]
        public void WalletDecreasePositiveTest()
        {
            Wallet wallet = new(1000);
            Assert.IsTrue(wallet.TryDecreaseValue(1000));
            Assert.AreEqual(wallet.Value, 0);
        }
        #endregion methods
    }
}