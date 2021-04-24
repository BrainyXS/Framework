using System;
using System.Collections.Generic;
using Framework.Contract.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.UI.Test
{
    [TestClass]
    public class NavigationContextTest
    {
        [TestMethod]
        public void ParameterTestOne()
        {
            var context = new NavigationContext();
            context.Params.Add(new KeyValuePair<Type, object>(typeof(bool), false));
            context.Params.Add(new KeyValuePair<Type, object>(typeof(string), "Test"));
            
            Assert.AreEqual(context.OfType<bool>(), false);
            Assert.AreEqual(context.OfType<string>(), "Test");
            Assert.AreEqual(context.AllOfType<bool>().Length, 1);
            Assert.AreEqual(context.AllOfType<string>().Length, 1);
        }
        [TestMethod]
        public void ParameterTestMultiple()
        {
            var context = new NavigationContext();
            context.Params.Add(new KeyValuePair<Type, object>(typeof(bool), false));
            context.Params.Add(new KeyValuePair<Type, object>(typeof(bool), true));
            context.Params.Add(new KeyValuePair<Type, object>(typeof(bool), true));
            
            Assert.AreEqual(context.OfType<bool>(), false);
            Assert.AreEqual(context.AllOfType<bool>().Length, 3);
        }
    }
}