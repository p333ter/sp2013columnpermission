using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace SPGuysCustomFieldPermissionLibrary
{
    public delegate void GenericSetter(object target, object value);
    public delegate object GenericGetter(object target);

    class ILUtils
    {
        ///
        /// Creates a dynamic setter for the property
        ///
        public static GenericSetter CreateSetMethod(Type targetType, String propName)
        {

            GenericSetter result = null;

            PropertyInfo propertyInfo = targetType.GetProperty(propName,
                BindingFlags.NonPublic | BindingFlags.Instance);

            if (propertyInfo != null)
            {
                MethodInfo setMethod = propertyInfo.GetSetMethod(true);
                if (setMethod != null)
                {

                    Type[] arguments = new Type[2];
                    arguments[0] = arguments[1] = typeof(object);

                    DynamicMethod setter = new DynamicMethod(
                      String.Concat("_Set", propertyInfo.Name, "_"),
                      typeof(void), arguments, propertyInfo.DeclaringType);
                    ILGenerator generator = setter.GetILGenerator();
                    generator.Emit(OpCodes.Ldarg_0);
                    generator.Emit(OpCodes.Castclass, propertyInfo.DeclaringType);
                    generator.Emit(OpCodes.Ldarg_1);

                    if (propertyInfo.PropertyType.IsClass)
                        generator.Emit(OpCodes.Castclass, propertyInfo.PropertyType);
                    else
                        generator.Emit(OpCodes.Unbox_Any, propertyInfo.PropertyType);

                    generator.EmitCall(OpCodes.Callvirt, setMethod, null);
                    generator.Emit(OpCodes.Ret);

                    result = (GenericSetter)setter.CreateDelegate(typeof(GenericSetter));
                }
            }
            return result;
        }

        ///
        /// Creates a dynamic getter for the property
        ///
        public static GenericGetter CreateGetMethod(Type targetType, String propName)
        {

            GenericGetter result = null;

            PropertyInfo propertyInfo = targetType.GetProperty(propName,
                BindingFlags.NonPublic | BindingFlags.Instance);

            if (propertyInfo != null)
            {
                MethodInfo getMethod = propertyInfo.GetGetMethod(true);
                if (getMethod != null)
                {

                    Type[] arguments = new Type[1];
                    arguments[0] = typeof(object);

                    DynamicMethod getter = new DynamicMethod(
                      String.Concat("_Get", propertyInfo.Name, "_"),
                      typeof(object), arguments, propertyInfo.DeclaringType);
                    ILGenerator generator = getter.GetILGenerator();
                    generator.DeclareLocal(typeof(object));
                    generator.Emit(OpCodes.Ldarg_0);
                    generator.Emit(OpCodes.Castclass, propertyInfo.DeclaringType);
                    generator.EmitCall(OpCodes.Callvirt, getMethod, null);

                    if (!propertyInfo.PropertyType.IsClass)
                        generator.Emit(OpCodes.Box, propertyInfo.PropertyType);

                    generator.Emit(OpCodes.Ret);

                    result = (GenericGetter)getter.CreateDelegate(typeof(GenericGetter));
                }
            }

            return result;

        }

    }
}

