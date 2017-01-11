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
using Android.Preferences;
using Android.Util;
using Java.Lang;
using Android.Content.Res;

namespace NToolbox.Controls.Preferences
{
    public class EditIntPreference : EditTextPreference
    {
        public EditIntPreference(Context context):base(context)
        {
            
        }

        public EditIntPreference(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            _FillAttributes(context, attrs);
        }

        public EditIntPreference(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            _FillAttributes(context, attrs);
        }

        private void _FillAttributes(Context context, IAttributeSet attrs)
        {
            //for (int i = 0; i < attrs.AttributeCount; i++)
            //{
            //    var attr = attrs.GetAttributeName(i);
            //    if(attr == "max")
            //    {
            //        var val = attrs.GetAttributeIntValue(i, -1);
            //        if (val != -1)
            //            MaxValue = val;
            //    }
            //    else if (attr == "min")
            //    {
            //        var val = attrs.GetAttributeIntValue(i, -1);
            //        if (val != -1)
            //            MinValue = val;
            //    }
            //    else if (attr == "valuedivider")
            //    {
            //        var val = attrs.GetAttributeIntValue(i, -1);
            //        if (val != -1)
            //            Divider = val;
            //    }
            //}
            var attributes = context.ObtainStyledAttributes(attrs, Resource.Styleable.EditIntPreference, 0, 0);
            try
            {

                var max = attributes.GetInt(Resource.Styleable.EditIntPreference_max, -1);
                var min = attributes.GetInt(Resource.Styleable.EditIntPreference_min, -1);
                var divider = attributes.GetInt(Resource.Styleable.EditIntPreference_valuedivider, -1);

                MaxValue = max == -1 ? (int?)null : max;
                MinValue = min == -1 ? (int?)null : min;
                Divider = divider == -1 ? (float?)null : Convert.ToSingle(divider);
            }
            finally
            {
                attributes.Recycle();
            }
        }



        public int? MaxValue { get; set; }

        public int? MinValue { get; set; }

        public float? Divider { get; set; }

        public override string Text
        {
            get
            {
                var val = Convert.ToSingle(SharedPreferences.GetInt(Key, 0));
                if (Divider.HasValue)
                    val = val / Divider.Value;
                return val.ToString();
            }

            set
            {
                float val;
                if(float.TryParse(value, out val))
                {
                    if (Divider.HasValue)
                        val = val * Divider.Value;

                    if (MaxValue != null && val > MaxValue)
                        val = MaxValue.Value;
                    if (MinValue != null && val < MinValue)
                        val = MinValue.Value;

                    base.SharedPreferences.Edit().PutInt(base.Key, Convert.ToInt32(val)).Commit();
                }
            }
        }

        protected override void OnSetInitialValue(bool restorePersistedValue, Java.Lang.Object defaultValue)
        {
            if (restorePersistedValue)
                EditText.SetText(Text, TextView.BufferType.Editable);
            else
                base.OnSetInitialValue(restorePersistedValue, defaultValue);
        }

      
    }
}