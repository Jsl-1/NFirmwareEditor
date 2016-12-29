using System;

namespace System.Windows.Forms
{
    /// <summary>Specifies the <see cref="T:System.Windows.Forms.ComboBox" /> style.</summary>
    /// <filterpriority>2</filterpriority>
    public enum ComboBoxStyle
    {
        /// <summary>Specifies that the list is always visible and that the text portion is editable. This means that the user can enter a new value and is not limited to selecting an existing value in the list.</summary>
        Simple,
        /// <summary>Specifies that the list is displayed by clicking the down arrow and that the text portion is editable. This means that the user can enter a new value and is not limited to selecting an existing value in the list. When using this setting, the <see cref="F:System.Windows.Forms.AutoCompleteMode.Append" /> value of <see cref="P:System.Windows.Forms.ComboBox.AutoCompleteMode" /> works the same as the <see cref="F:System.Windows.Forms.AutoCompleteMode.SuggestAppend" /> value. This is the default style.</summary>
        DropDown,
        /// <summary>Specifies that the list is displayed by clicking the down arrow and that the text portion is not editable. This means that the user cannot enter a new value. Only values already in the list can be selected. The list displays only if <see cref="P:System.Windows.Forms.ComboBox.AutoCompleteMode" /> is <see cref="F:System.Windows.Forms.AutoCompleteMode.Suggest" /> or <see cref="F:System.Windows.Forms.AutoCompleteMode.SuggestAppend" />.</summary>
        DropDownList
    }
}