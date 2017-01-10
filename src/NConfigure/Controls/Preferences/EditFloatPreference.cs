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
    public class EditFloatPreference : EditTextPreference
    {
       

        public EditFloatPreference(Context context):base(context)
        {
            
        }

        public EditFloatPreference(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            _FillAttributes(context, attrs);

        }

        private void _FillAttributes(Context context, IAttributeSet attrs)
        {
            var attributes = context.ObtainStyledAttributes(attrs, Resource.Styleable.EditFloatPreference);
            var max = attributes.GetFloat(Resource.Styleable.EditFloatPreference_max, -1);
            var min = attributes.GetFloat(Resource.Styleable.EditFloatPreference_min, -1);
            var multiplier = attributes.GetFloat(Resource.Styleable.EditFloatPreference_multiplier, -1);

            MaxValue = max == -1 ? (float?)null : max;
            MinValue = min == -1 ? (float?)null : min;
            Multiplier = multiplier == -1 ? (float?)null : multiplier;
        }

        public EditFloatPreference(Context context, IAttributeSet attrs, int defStyle): base(context, attrs, defStyle)
        {
            _FillAttributes(context, attrs);
        }

        public float? MaxValue { get; set; }

        public float? MinValue { get; set; }

        public float? Multiplier { get; set; }

        public override string Text
        {
            get
            {
                var val = SharedPreferences.GetFloat(Key, 0);
                if (Multiplier.HasValue)
                    val = val * Multiplier.Value;
                return val.ToString();
            }

            set
            {
                float floatVal;
                if(float.TryParse(value, out floatVal))
                {
                    var val = floatVal;
                    if (Multiplier.HasValue)
                        val = floatVal / Multiplier.Value;

                    if (MaxValue != null && val > MaxValue)
                        val = MaxValue.Value;
                    if (MinValue != null && val < MinValue)
                        val = MinValue.Value;

                    SharedPreferences.Edit().PutFloat(Key, val).Commit();
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