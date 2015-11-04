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
            HasParameterAttribute = memberInfo.GetCustomAttribute<ParameterAttribute>() != null;
        }

        public bool CanRead { get; private set; }

        public bool CanWrite { get; private set; }

        public bool Base64Encoded { get; private set; }

        public bool SerializeNull { get; private set; }

        public bool IsStatic { get; private set; }

        public bool IsPublic { get; private set; }

        public bool HasParameterAttribute { get; private set; }

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

        public string JsonFieldName
        {
            get { return MemberInfo.GetJsonFieldName(); }
        }

        public ReflectionUtils.GetDelegate GetDelegate
        {
            get
            {
                ReflectionUtils.GetDelegate getDelegate = null;
                if (_propertyInfo != null)
                {
                    getDelegate = ReflectionUtils.GetGetMethod(_propertyInfo);
                }
                if (_fieldInfo != null)
                {
                    getDelegate = ReflectionUtils.GetGetMethod(_fieldInfo);
                }


                if (getDelegate == null)
                {
                    throw new InvalidOperationException("Property and Field cannot both be null");
                }

                if (Base64Encoded)
                {
                    return delegate (object source)
                    {
                        var value = getDelegate(source);
                        var stringValue = value as string;
                        return stringValue == null ? value : stringValue.ToBase64String();
                    };
                }

                return getDelegate;
            }
        }
        public ReflectionUtils.SetDelegate SetDelegate
        {
            get
            {
                ReflectionUtils.SetDelegate setDelegate = null;
                if (_propertyInfo != null)
                {
                    setDelegate = ReflectionUtils.GetSetMethod(_propertyInfo);
                }
                if (_fieldInfo != null)
                {
                    setDelegate = ReflectionUtils.GetSetMethod(_fieldInfo);
                }
                if (setDelegate == null)
                {
                    throw new InvalidOperationException("Property and Field cannot both be null");
                }
                if (Base64Encoded)
                {
                    return delegate (object source, object value)
                    {
                        var stringValue = value as string;
                        if (stringValue == null)
                        {
                            setDelegate(source, value);
                        }
                        setDelegate(source, stringValue.FromBase64String());
                    };
                }
                return setDelegate;
            }
        }

        public Type Type
        {
            get
            {
                if (_propertyInfo != null)
                {
                    return _propertyInfo.PropertyType;
                }
                if (_fieldInfo != null)
                {
                    return _fieldInfo.FieldType;
                }
                throw new InvalidOperationException("Property and Field cannot both be null");
            }
        }

        public bool CanDeserialize
        {
            get
            {
                return (IsPublic || HasParameterAttribute)
                    && !IsStatic
                    && CanWrite
                    && (_fieldInfo == null || !_fieldInfo.IsInitOnly);
            }
        }

        public bool CanSerialize
        {
            get { return IsPublic && CanRead && !IsStatic; }
        }
    }
}
