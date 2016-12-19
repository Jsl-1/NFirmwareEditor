using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NDataflash.Definition
{
    public interface IPropertyDefinitionContainer
    {
        String Name { get; }

        List<PropertyDefinition> PropertyDefinitions { get; set; }

        PropertyDefinition GetChildPropertyDefinition(string datamember);
    }

    public static class IPropertyDefinitionContainerHelper
    {
        public static PropertyDefinition GetChildPropertyDefinition(IPropertyDefinitionContainer container, string dataMember)
        {
           
                var dataMemberArray = dataMember.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                var propertyId = dataMemberArray[0];
                var propertyDefinition = container.PropertyDefinitions.Where(p => p.Id == propertyId).SingleOrDefault();
                if (propertyDefinition == null)
                    return null;
                if (dataMemberArray.Length > 1)
                {
                    if (propertyDefinition is IPropertyDefinitionContainer)
                    {
                        var childDatamembers = dataMemberArray.Skip(1).ToArray();
                        return ((IPropertyDefinitionContainer)propertyDefinition).GetChildPropertyDefinition(String.Join(".", childDatamembers));
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return propertyDefinition;
                }
        }
    }
}