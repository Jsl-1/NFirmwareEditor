using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;
using System.Reflection;

namespace NCore.Compat
{
    public interface IWrappableObject<TSource>
    {
        Boolean IsWrapped { get; }

        void SetPropertyValue(object value, [CallerMemberName] string propertyName = null);

        T GetPropertyValue<T>([CallerMemberName] string propertyName = null);

        IWrappableObject<TSource> WrapTo<TTarget>(TTarget destination);

        IWrappableObject<TSource> WrapProperty<TTarget>(Expression<Func<TSource,object>> sourceProperty, Expression<Func<TTarget, object>> targetProperty);

    }



    public class WrappableObjectHelper<TSource> : IWrappableObject<TSource>
        where TSource : IWrappableObject<TSource>
    {
        private IWrappableObject<TSource> m_Source;
        private object m_Target;

        private Dictionary<String, Tuple<Func<object>, Action<object>>> m_WrappingMethods = new Dictionary<string, Tuple<Func<object>, Action<object>>>();
        private Dictionary<String, object> m_valueStore = new Dictionary<string, object>();

        public WrappableObjectHelper(IWrappableObject<TSource> source)
        {
            m_Source = source;
        }

        public bool IsWrapped
        {
            get
            {
                return m_Target != null;
            }
        }

        public T GetPropertyValue<T>([CallerMemberName] string propertyName = null)
        {
            Tuple<Func<object>, Action<object>> wrappingMethods = null;
            if (m_WrappingMethods.TryGetValue(propertyName, out wrappingMethods))
            {
                var value = wrappingMethods.Item1();
                if(value != null)
                     return (T)wrappingMethods.Item1();
            }
            else
            {
                object value = null;
                if (m_valueStore.TryGetValue(propertyName, out value))
                {
                    if (value != null)
                        return (T)value;
                }
            }
            return default(T);
        }

        public void SetPropertyValue(object value, [CallerMemberName] string propertyName = null)
        {
            Tuple<Func<object>, Action<object>> wrappingMethods = null;
            if (m_WrappingMethods.TryGetValue(propertyName, out wrappingMethods))
            {
                wrappingMethods.Item2(value);
            }
            else
            {
                m_valueStore[propertyName] = value;
            }
        }

        public IWrappableObject<TSource> WrapProperty<TTarget>(Expression<Func<TSource, object>> sourceProperty, Expression<Func<TTarget, object>> targetProperty)
        {
            

            var sourcePropertyName = GetPropertyName<TSource>(sourceProperty);
            var targetPropertyName = GetPropertyName<TTarget>(targetProperty);

            if (m_WrappingMethods.ContainsKey(sourcePropertyName))
                throw new ArgumentException("Property alreadey wrapped");

            var getPropertyFunc = new Func<object>(() => m_Target.GetType().GetProperty(targetPropertyName).GetValue(m_Target));
            var setPropertyFunc = new Action<object>((v) => m_Target.GetType().GetProperty(targetPropertyName).SetValue(m_Target, v));
            var wrappingMethods = new Tuple<Func<object>, Action<object>>(getPropertyFunc, setPropertyFunc);

            m_WrappingMethods[sourcePropertyName] = wrappingMethods;

            return m_Source;
        }

        //public static void SetPropertyValue<T>(T target, Expression<Func<T, object>> memberLamda, object value)
        //{
        //    var memberSelectorExpression = memberLamda.Body as MemberExpression;
        //    if (memberSelectorExpression != null)
        //    {
        //        var property = memberSelectorExpression.Member as PropertyInfo;
        //        if (property != null)
        //        {
        //            property.SetValue(target, value, null);
        //        }
        //    }
        //}


        public IWrappableObject<TSource> WrapTo<TTarget>(TTarget destination)
        {
            m_Target = destination;
            return m_Source;
        }


        public static string GetPropertyName<T>(Expression<Func<T,object>> expression)
        {
            MemberExpression body = expression.Body as MemberExpression;
            if (body == null)
            {
                throw new ArgumentException("MemberExpression is expected in expression.Body", "expression");
            }
            MemberInfo member = body.Member;
            if (member.MemberType != MemberTypes.Field || member.Name == null || !member.Name.StartsWith("$VB$Local_"))
            {
                return member.Name;
            }
            return member.Name.Substring("$VB$Local_".Length);

        }

    } 

}