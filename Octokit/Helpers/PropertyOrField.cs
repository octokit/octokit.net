using System;
using System.Reflection;
using Octokit.Helpers;
using Octokit.Internal;
using Octokit.Reflection;

namespace Octokit
{
    internal class PropertyOrField
    {
        readonly PropertyInfo _propertyInfo;
        readonly FieldInfo _fieldInfo;

        public PropertyOrField(PropertyInfo propertyInfo) : this((MemberInfo)propertyInfo)
        {
            _propertyInfo = propertyInfo;

            CanRead = propertyInfo.CanRead;
            CanWrite = propertyInfo.CanWrite;
            IsStatic = ReflectionUtils.GetGetterMethodInfo(propertyInfo).IsStatic;
            IsPublic = ReflectionUtils.GetGetterMethodInfo(propertyInfo).IsPublic;
        }

        public PropertyOrField(FieldInfo fieldInfo) : this((MemberInfo)fieldInfo)
        {
            _fieldInfo = fieldInfo;

            CanRead = true;
            CanWrite = true;
            IsStatic = fieldInfo.IsStatic;
            IsPublic = fieldInfo.IsPublic;
        }

        protected PropertyOrField(MemberInfo memberInfo)
        {
            MemberInfo = memberInfo;
            Base64Encoded = memberInfo.GetCustomAttribute<SerializeAsBase64Attribute>() != null;
            SerializeNull = memberInfo.GetCustomAttribute<SerializeNullAttribute>() != null;
        }

        public bool CanRead { get; private set; }

        public bool CanWrite { get; private set; }

        public bool Base64Encoded { get; private set; }

        public bool SerializeNull { get; private set; }

        public bool IsStatic { get; private set; }

        public bool IsPublic { get; private set; }

        public MemberInfo MemberInfo { get; private set; }

        public object GetValue(object instance)
        {
            if (_propertyInfo != null)
                return _propertyInfo.GetValue(instance);
            if (_fieldInfo != null)
                return _fieldInfo.GetValue(instance);
            throw new InvalidOperationException("Property and Field cannot both be null");
        }

        public void SetValue(object instance, object value)
        {
            if (_propertyInfo != null)
            {
                _propertyInfo.SetValue(instance, value);
                return;
            }
            if (_fieldInfo != null)
            {
                _fieldInfo.SetValue(instance, value);
                return;
            }
            throw new InvalidOperationException("Property and Field cannot both be null");
        }
    }
}
