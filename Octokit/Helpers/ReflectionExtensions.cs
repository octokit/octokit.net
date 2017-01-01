﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Octokit.Internal;
using Octokit.Reflection;

namespace Octokit
{
    internal static class ReflectionExtensions
    {
        public static string GetJsonFieldName(this MemberInfo memberInfo)
        {
            var memberName = memberInfo.Name;
            var paramAttr = memberInfo.GetCustomAttribute<ParameterAttribute>();

            if (paramAttr != null && !string.IsNullOrEmpty(paramAttr.Key))
            {
                memberName = paramAttr.Key;
            }

            return memberName.ToRubyCase();
        }

        public static IEnumerable<PropertyOrField> GetPropertiesAndFields(this Type type)
        {
            return ReflectionUtils.GetProperties(type).Select(property => new PropertyOrField(property))
                .Union(ReflectionUtils.GetFields(type).Select(field => new PropertyOrField(field)))
                .Where(p => (p.IsPublic || p.HasParameterAttribute) && !p.IsStatic);
        }

        public static bool IsDateTimeOffset(this Type type)
        {
            return type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset?);
        }

        public static bool IsNullable(this Type type)
        {
            return type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

#if !NO_SERIALIZABLE
        public static Type GetTypeInfo(this Type type)
        {
            return type;
        }
#endif

#if HAS_TYPEINFO
        public static IEnumerable<MemberInfo> GetMember(this Type type, string name)
        {
            return type.GetTypeInfo().DeclaredMembers.Where(m => m.Name == name);
        }

        public static bool IsAssignableFrom(this Type type, Type otherType)
        {
            return type.GetTypeInfo().IsAssignableFrom(otherType.GetTypeInfo());
        }
#endif
        public static IEnumerable<PropertyInfo> GetAllProperties(this Type type)
        {
#if HAS_TYPEINFO
            var typeInfo = type.GetTypeInfo();
            var properties = typeInfo.DeclaredProperties;

            var baseType = typeInfo.BaseType;

            return baseType == null ? properties : properties.Concat(baseType.GetAllProperties());
#else
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
#endif
        }

        public static bool IsEnumeration(this Type type)
        {
#if HAS_TYPEINFO
            return type.GetTypeInfo().IsEnum;
#else
            return type.IsEnum;
#endif
        }
    }
}
