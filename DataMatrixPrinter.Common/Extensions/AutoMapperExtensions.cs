using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace DataMatrixPrinter.Common.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IEnumerable<Attribute> GetMappedAttributes(
         this IConfigurationProvider mapper,
         Type viewModelType,
         string viewModelPropertyName,
         IList<Attribute> existingAttributes)
        {
            if (viewModelType != null)
            {
                foreach (var typeMap in mapper.GetAllTypeMaps().Where(i => i.DestinationType == viewModelType))
                {
                    var propertyIsComponentType =
                        typeMap.SourceType.GetProperties()
                            .FirstOrDefault(
                                d =>
                                    d.Name.EndsWith("Component") &&
                                    d.PropertyType.GetProperties().Any(s => s.Name == viewModelPropertyName));

                    var domainClassProperty = typeMap.SourceType.GetProperty(viewModelPropertyName);

                    var mappedProperies = typeMap.GetPropertyMaps().ToList();
                    var isConvertedBooltoStringProperty = mappedProperies
                        .FirstOrDefault(
                            d =>
                                domainClassProperty != null &&
                                d.DestinationProperty.Name == viewModelPropertyName &&
                                d.DestinationPropertyType == typeof(string) &&
                                domainClassProperty.PropertyType == typeof(bool));

                    if (propertyIsComponentType != null)
                    {
                        var componentProperty = propertyIsComponentType.PropertyType.GetProperty(viewModelPropertyName);
                        foreach (Attribute propertyMap in componentProperty.GetCustomAttributes(true))
                        {
                            if (existingAttributes.All(i => i.GetType() != propertyMap.GetType()))
                            {
                                yield return propertyMap;
                            }
                        }
                    }
                    if (domainClassProperty != null && isConvertedBooltoStringProperty != null)
                    {
                        var customAttributesOfTheDomainClass = domainClassProperty.GetCustomAttributes(true);
                        foreach (Attribute propertyMap in customAttributesOfTheDomainClass)
                        {
                            if (existingAttributes.All(i => i.GetType() != propertyMap.GetType()))
                            {
                                yield return propertyMap;
                            }
                        }
                    }
                    else
                    {
                        var maps = mappedProperies
                        .Where(propertyMap => !propertyMap.IsIgnored() && propertyMap.SourceMember != null)
                        .Where(propertyMap => propertyMap.DestinationProperty.Name == viewModelPropertyName);

                        foreach (var propertyMap in maps)
                        {
                            foreach (Attribute attribute in propertyMap.SourceMember.GetCustomAttributes(true))
                            {
                                if (existingAttributes.All(i => i.GetType() != attribute.GetType()))
                                {
                                    yield return attribute;
                                }
                            }
                        }
                    }
                }
            }

            if (existingAttributes == null)
            {
                yield break;
            }

            foreach (var attribute in existingAttributes)
            {
                yield return attribute;
            }
        }

        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var unmappedPropertyNames = expression.TypeMap.GetUnmappedPropertyNames();
            foreach (var property in unmappedPropertyNames)
            {
                expression.ForMember(property, a => a.Ignore());
            }
            return expression;
        }
    }

}
