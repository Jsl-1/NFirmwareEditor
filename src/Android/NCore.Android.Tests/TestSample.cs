using System;
using NUnit.Framework;
using NCore.Compat;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace NCore.Android.Tests
{
    [TestFixture]
    public class TestsSample
    {

        [Test]
        public void TestPropertyReadAndWrite()
        {
            var target = new TestObjectTarget();
            var source = new TEstObject1();
            source.WrapTo(target)
                .WrapProperty<TestObjectTarget>(x => x.TestSource1, x => x.TestTarget1)
                .WrapProperty<TestObjectTarget>(x => x.TestSource2, x => x.TestTarget2);
            
            target.TestTarget1 = "test1";
            source.TestSource2 = "test2";

            Assert.AreEqual(source.TestSource1, target.TestTarget1);
            Assert.AreEqual(source.TestSource2, target.TestTarget2);

        }
    }

    public class TEstObject1 : IWrappableObject<TEstObject1>
    {
        public string TestSource1
        {
            get { return GetPropertyValue<String>();  }
            set { SetPropertyValue(value); }
        }

        public string TestSource2
        {
            get { return GetPropertyValue<String>(); }
            set { SetPropertyValue(value); }
        }


        public TEstObject1()
        {
            Wrapper = new WrappableObjectHelper<TEstObject1>(this);
        }

        private WrappableObjectHelper<TEstObject1> Wrapper;

        public bool IsWrapped
        {
            get
            {
                return Wrapper.IsWrapped;
            }
        }

        public void SetPropertyValue(object value, [CallerMemberName] string propertyName = null)
        {
            Wrapper.SetPropertyValue(value, propertyName);
        }

        public IWrappableObject<TEstObject1> WrapTo<TTarget>(TTarget destination)
        {
            return Wrapper.WrapTo(destination);
        }

        public IWrappableObject<TEstObject1> WrapProperty<TTarget>(Expression<Func<TEstObject1, object>> sourceProperty, Expression<Func<TTarget, object>> targetProperty)
        {
            return Wrapper.WrapProperty(sourceProperty, targetProperty);
        }

        public T GetPropertyValue<T>([CallerMemberName] string propertyName = null)
        {
            return Wrapper.GetPropertyValue<T>(propertyName);
        }
    }

    public class TestObjectTarget
    {
        public string TestTarget1 { get; set; }

        public string TestTarget2 { get; set; }
    }
}