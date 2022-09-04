using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace ViewModel
{
    public static  class ValidatorCustom<T> where T : class
    {
        public static ResponseGenerico Validator(T request)
        {
            ResponseGenerico response = new()
            {
                Success = true,
                Message = "Sin Error"
            };
            if (request.GetType() == typeof(T))
            {
                var item = request.GetType().GetProperties();
                response = ValidateProperties(item, request);
            }

            return response;
        }

        public static ResponseGenerico ValidatorList(List<T> request)
        {
            ResponseGenerico response = new()
            {
                Success = true,
                Message = "Sin Error"
            };
            foreach (var item in request)
            {
                response = ValidateProperties(item.GetType().GetProperties(), item);
            }
            return response;
        }
        private static ResponseGenerico ValidateProperties(PropertyInfo[] propertyInfo, object request)
        {
            foreach (PropertyInfo item in propertyInfo)
            {

                var propertyValue = item.GetValue(request);
                var propertyName = item.PropertyType.Name.ToUpper();
                var property = item.PropertyType;
                if (propertyName.Equals("STRING"))
                {
                    var attributes = item.GetCustomAttributes(true);
                    foreach (object attrs in attributes)
                    {
                        ValidatorAttribute validator = attrs as ValidatorAttribute;
                        if (validator!.Required)
                        {
                            string valor = (string)propertyValue!;
                            if (string.IsNullOrWhiteSpace(valor))
                            {
                                return new ResponseGenerico { Success = false, Message =  $"Campo {item.Name} es requerido " };
                            }
                            else
                            {
                                if (validator.Length > 0)
                                {
                                    if (valor.Length < validator.Length)
                                        return new ResponseGenerico { Success = false, Message = $"Campo no cumple con la longitud requeridad {item.Name}"};
                                }

                            }

                        }
                    }
                }
                else if (propertyName.Equals("Boolean")) { }
                else if (propertyName.Equals("INT32")) { }
                else if (propertyName.Equals("Single")) { }
                else if (propertyName.Equals("Double")) { }
                else if (propertyName.Equals("Decimal")) { }
                else if (propertyName.Equals("Byte")) { }
                //Asumimo que es una entidad
                else
                {
                    string s = property.FullName!;
                    //var type = typeof(class).MakeGenericType(item.GetType());
                    Type myClassType = Type.GetType(s);
                    Type t = item.GetType().GetGenericArguments()[0];
                    Type typeOfMyType = typeof(T);
                    Type typeOfMyTypeWithGenericArgument = typeOfMyType.MakeGenericType(t);
                    var instanceOfGenericMyType =
                                    (T)Activator.CreateInstance(typeOfMyTypeWithGenericArgument);

                    ValidateProperties(item.GetType().GetProperties(), item);
                }
            }


            return new ResponseGenerico { Success = true, Message = "Modelo valido" };
        }
    }
}
