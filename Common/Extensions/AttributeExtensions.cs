using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;

public static class AttributeExtensions
{
    public enum MemberTypes
    {
        Property,
        Field
    }

    private static BindingFlags FieldsBindingFlags = BindingFlags.Public | BindingFlags.NonPublic
         | BindingFlags.Instance;

    public static TAttribute GetAttribute<TAttribute>(this Type type, string memberName, MemberTypes memberType, bool inherit = false) where TAttribute : Attribute
    {
        Assert.IsNotNull(type);

        IEnumerable<TAttribute> attributes = null;

        switch (memberType)
        {
            case MemberTypes.Property:
                attributes = GetAttributesFromProperty<TAttribute>(type, memberName, inherit);
                break;

            case MemberTypes.Field:
                attributes = GetAttributesFromField<TAttribute>(type, memberName, inherit);
                break;

            default:
                Debug.LogError($"Member Type {memberType} not supported, returning null instead. Obj type: [{type.FullName}], Member name: [{memberName}]");
                return null;
        }

        var attribute = attributes.FirstOrDefault();

        var attributesCount = attributes.Count();

        if (attributesCount > 1)
            Debug.LogError($"Found multiple attributes of type [{typeof(TAttribute)}]. Returning first");
        else if (attributesCount == 0)
            Debug.LogError($"Found zero attributes of type [{typeof(TAttribute)}]. Returning null instead");

        return attribute;
    }

    public static TAttribute GetAttribute<TAttribute>(this object obj, string memberName, MemberTypes memberType, bool inherit = false) where TAttribute : Attribute
    {
        Assert.IsNotNull(obj);
        var type = obj.GetType();

        return GetAttribute<TAttribute>(type, memberName, memberType, inherit);
    }

    private static IEnumerable<TAttribute> GetAttributesFromField<TAttribute>(this Type type, string memberName, bool inherit = false) where TAttribute : Attribute
    {
        Assert.IsNotNull(type);

        var field = type.GetField(memberName, FieldsBindingFlags);
        Assert.IsNotNull(field, nameof(field));

        var attributes = field.GetCustomAttributes(inherit: inherit)
                              .Where(t => t is TAttribute)
                              .Cast<TAttribute>();

        return attributes;
    }

    private static IEnumerable<TAttribute> GetAttributesFromProperty<TAttribute>(this Type type, string memberName, bool inherit = false) where TAttribute : Attribute
    {
        Assert.IsNotNull(type);
        var property = type.GetProperty(memberName);
        Assert.IsNotNull(property, nameof(property));

        var attributes = property.GetCustomAttributes(inherit: inherit)
                                 .Where(t => t is TAttribute)
                                 .Cast<TAttribute>();

        return attributes;
    }
}