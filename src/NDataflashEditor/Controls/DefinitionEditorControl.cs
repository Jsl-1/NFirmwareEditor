using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NDataflashEditor.Storages;
using NDataflashEditor.Managers;
using NDataflashEditor.Models;
using NDataflash;
using NDataflash.Definition;
using NDataflashEditor.Classes;

namespace NDataflashEditor.Windows.DataflashDefinitionEditor
{
    partial class DataflashDefinitionEditorControl : UserControl
    {
        public DataflashDefinition Definition { get; set; }

        public DataflashManager DataflashManager { get; set; }

        public DataflashDefinitionEditorControl()
        {
            InitializeComponent();
        }

        public void UpdateData()
        {
            UpdateBinaryGridView();
            UpdateDefinitionTreeList();
        }

        private void UpdateDefinitionTreeList()
        {
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();
            if(Definition != null)
            {
                var root = new TreeNode(Definition.ProductId);
                root.Tag = Definition;
                treeView1.Nodes.Add(root);
                foreach(var property in Definition.PropertyDefinitions)
                {
                    root.Nodes.Add(_GetNodes(property));
                }
                    
            }
            treeView1.EndUpdate();
            treeView1.ExpandAll();
        }

        private TreeNode _GetNodes(PropertyDefinition property)
        {
            var node = new TreeNode(property.Id);
            node.Tag = property;

            if (property is IPropertyDefinitionContainer)
            {
                var propertyContainer = (IPropertyDefinitionContainer)property;
                foreach (var p in propertyContainer.PropertyDefinitions)
                {
                    node.Nodes.Add(_GetNodes(p));
                }
            }
           return node;
        }

        private void UpdateBinaryGridView()
        {
            var oldBinaryViewDataSource = dataGridViewBinary.DataSource as List<BinaryViewRowObject>;
            var binaryViewDataSource = new List<BinaryViewRowObject>();
            for (var i = 0; i < DataflashManager.Dataflash.Data.Length; i++)
            {
                var binaryViewRow = new BinaryViewRowObject() { Offset = i, Value = DataflashManager.Dataflash.Data[i] };
                if (oldBinaryViewDataSource != null && i < oldBinaryViewDataSource.Count)
                    binaryViewRow.Changed = DataflashManager.Dataflash.Data[i] != oldBinaryViewDataSource[i].Value;
                binaryViewDataSource.Add(binaryViewRow);
            }
            dataGridViewBinary.DataSource = binaryViewDataSource;
            dataGridViewChanges.DataSource = binaryViewDataSource.Where(x => x.Changed).ToList();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(e.Node == null)
            {
                propertyGrid1.SelectedObject = null;               
            }
            else
            {
                   
             
                    if (e.Node.Tag is PropertyDefinition)
                    {
                        var propertyDefinition = (PropertyDefinition)e.Node.Tag;

                        propertyGrid1.SelectedObject = new DataPropertyViewObject(propertyDefinition, DataflashManager);

                        _SynchronizeBinaryViewPosition(propertyDefinition);
                    }
                    else
                    {
                        dataGridViewBinary.ClearSelection();
                    }
                
            }
        }

        private void _SynchronizeBinaryViewPosition(PropertyDefinition propertyDefinition)
        {
            var range = DataflashManager.PropertyOffsets[propertyDefinition];
            dataGridViewBinary.ClearSelection();
            for (var i = range; i < range + propertyDefinition.GetPropertyByteLength(); i++)
            {
                for (var j = 0; j < dataGridViewBinary.Columns.Count; j++)
                {
                    dataGridViewBinary[j, Convert.ToInt32(i)].Selected = true;
                }

            }
            dataGridViewBinary.FirstDisplayedScrollingRowIndex = Convert.ToInt32(range);
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            UpdateBinaryGridView();
            if(e.ChangedItem.PropertyDescriptor is DataflashPropertyDescriptor){
                var descriptor = (DataflashPropertyDescriptor)e.ChangedItem.PropertyDescriptor;
                _SynchronizeBinaryViewPosition(descriptor.DataPropertyDefinition);
            }
        }
    }

    class DataPropertyViewObject : IDisposable, ICustomTypeDescriptor
    {
        private PropertyDefinition m_dataPropertyDefinition;
        private DataflashManager m_DataFlashManager;

        public DataPropertyViewObject(PropertyDefinition definition, DataflashManager manager)
        {
            m_dataPropertyDefinition = definition;
            m_DataFlashManager = manager;
        }

        public PropertyDefinition PropertyDefinition
        {
            get { return m_dataPropertyDefinition; }
        }

        public Object Value
        {
            get { return m_DataFlashManager.GetPropertyValue(PropertyDefinition); }
        }

        public void Dispose()
        {
            m_dataPropertyDefinition = null;
            m_DataFlashManager = null;
        }

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(PropertyDefinition);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(PropertyDefinition);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(PropertyDefinition);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(PropertyDefinition);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(PropertyDefinition);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(PropertyDefinition);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(PropertyDefinition, editorBaseType);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(PropertyDefinition);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(PropertyDefinition, attributes);
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return GetProperties(new Attribute[0]);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            List<PropertyDescriptor> pds = new List<PropertyDescriptor>();
            foreach (var property in TypeDescriptor.GetProperties(PropertyDefinition, attributes))
            {
                pds.Add((PropertyDescriptor)property);
            }
            
            var realOffsetProperty = new CustomValuePropertyDescriptor("RealOffset", m_DataFlashManager.PropertyOffsets[PropertyDefinition]);
            pds.Add(realOffsetProperty);

            var realSizeProperty = new CustomValuePropertyDescriptor("RealSize", PropertyDefinition.GetPropertyByteLength());
            pds.Add(realSizeProperty);

            var valueProperty = new DataflashPropertyDescriptor(m_DataFlashManager, PropertyDefinition, attributes, "Value");            
            pds.Add(valueProperty);

            return new PropertyDescriptorCollection(pds.ToArray());
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
           return PropertyDefinition;
        }
    }



    class BinaryViewRowObject
    {
        public Int32 Offset { get; set; }

        public byte Value { get; set; }

        public bool Changed { get; set; }
    }

}
